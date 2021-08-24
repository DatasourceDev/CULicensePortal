using Microsoft.Graph;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Security;
using CULCS.DTO;

namespace CULCS.AD
{
    public interface IGraphProvider
    {
        Task<User> GetUserByEmail(string email);
        Task<string> GetIdByGroupName(string groupName);
        Task<bool> IsAdmin(string id, string admingroupname);
        Task<string> AddUsertoGroup(string id, string gid);
        Task<string> GetGroupName(string groupName);

        Task<Group> GetGroupById(string Id);

        Task<bool> ValidateAuth(AzureAD azureAD, string username, string password);

        Task<bool> AddMember(string id, string mid);
        Task<bool> RemoveMember(string id, string mid);

    }
    public class MicrosoftGraphProvider : IGraphProvider
    {
        private readonly IGraphServiceClient _graphServiceClient;

        public MicrosoftGraphProvider(IGraphServiceClient graphServiceClient)
        {
            _graphServiceClient = graphServiceClient;
        }
        public async Task<bool> ValidateAuth(AzureAD azureAD, string username, string password)
        {
            try
            {
                var clientId = azureAD.ClientId;
                var tenantId = azureAD.TenantId;
                var instance = azureAD.Instance;

                var scopes = new string[] { "User.Read" };
                IPublicClientApplication app = PublicClientApplicationBuilder.Create(clientId).WithAuthority(instance + tenantId).Build();

                var result1 = app.AcquireTokenInteractive(scopes);

                var securePassword = new SecureString();

                foreach (char c in password)
                    securePassword.AppendChar(c);

                var result = await app.AcquireTokenByUsernamePassword(scopes, username, securePassword).ExecuteAsync();

            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            try
            {
                var user = await _graphServiceClient.Users[email]
                          .Request()
                          .GetAsync();

                if (user == null || string.IsNullOrEmpty(user.Id))
                {
                    return null;
                }

                return user;
            }
            catch
            {
                return null;
            }
        }

        public async Task<string> GetIdByGroupName(string groupName)
        {
            var group = await _graphServiceClient.Groups.Request()
                            .Filter($"displayName eq '{groupName}'")
                            .Select("displayName,description,id").GetAsync();

            if (group == null || group.Count == 0 || string.IsNullOrEmpty(group[0].Id))
            {
                return string.Empty;
            }

            return group[0].Id;
        }
        public async Task<Group> GetGroupById(string Id)
        {
            var group = await _graphServiceClient.Groups[Id]
                            .Request()
                            .GetAsync();

            if (group == null || string.IsNullOrEmpty(group.Id))
            {
                return null;
            }

            return group;
        }
        public async Task<string> GetGroupName(string groupName)
        {
            var group = await _graphServiceClient.Groups.Request()
                            .Filter($"displayName eq '{groupName}'")
                            .Select("displayName,description,id").GetAsync();

            if (group == null || group.Count == 0 || string.IsNullOrEmpty(group[0].Id))
            {
                return string.Empty;
            }

            return group[0].Id;
        }
        public async Task<bool> AddMember(string id, string mid)
        {
            var group = new List<string>()
            {
                id
            };
            var user = await _graphServiceClient.Users[mid].CheckMemberGroups(group).Request().PostAsync();
            if (user.Count() == 0)
            {
                var directoryObject = new DirectoryObject
                {
                    Id = $"{mid}"
                };

                await _graphServiceClient.Groups[id].Members.References
                    .Request()
                    .AddAsync(directoryObject);
            }
            return true;
        }
        public async Task<bool> RemoveMember(string id, string mid)
        {
            var group = new List<string>()
            {
                id
            };
            var user = await _graphServiceClient.Users[mid].CheckMemberGroups(group).Request().PostAsync();
            if (user.Count() > 0)
            {
                await _graphServiceClient.Groups[id].Members[mid].Reference
                    .Request()
                    .DeleteAsync();
            }
            return true;
        }
        public async Task<bool> IsAdmin(string id, string admingroupname)
        {
            var group = await _graphServiceClient.Groups.Request()
                            .Filter($"displayName eq '"+ admingroupname + "'")
                            .Select("displayName,description,id").GetAsync();
            if (group == null || group.Count == 0 || string.IsNullOrEmpty(group[0].Id))
            {
                return false;
            }

            var groupIds = new List<String>() { group[0].Id };

            var user = await _graphServiceClient.Users[id].CheckMemberGroups(groupIds).Request().PostAsync();

            if (user == null || user.Count == 0)
            {
                return false;
            }
            return true;
        }
        public async Task<string> AddUsertoGroup(string id, string gid)
        {
            var user = await _graphServiceClient.Users[id]
                            .Request()
                            .GetAsync();

            if (user == null || string.IsNullOrEmpty(user.Id))
            {
                return string.Empty;
            }

            var members = await _graphServiceClient.Groups[gid].Members.Request().GetAsync();

            members.Add(user);
            //await _graphServiceClient.Groups[gid].Members.Request()(members);

            return "";
        }
    }
}
