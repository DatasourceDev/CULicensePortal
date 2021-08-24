using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CULCS.AD
{
    public class GraphAuthenticationProvider : IAuthenticationProvider
    {
        public const string GRAPH_URI = "https://graph.microsoft.com/";
        private string _tenantId { get; set; }
        private string _clientId { get; set; }
        private string _clientSecret { get; set; }

        public GraphAuthenticationProvider(IConfiguration configuration)
        {
            _tenantId = configuration.GetValue<string>("AzureAd:TenantId");
            _clientId = configuration.GetValue<string>("AzureAd:ClientId");
            _clientSecret = configuration.GetValue<string>("AzureAd:ClientSecret");
        }

        public async Task AuthenticateRequestAsync(HttpRequestMessage request)
        {
            AuthenticationContext authContext = new AuthenticationContext($"https://login.microsoftonline.com/{_tenantId}");

            ClientCredential creds = new ClientCredential(_clientId, _clientSecret);

            AuthenticationResult authResult = await authContext.AcquireTokenAsync(GRAPH_URI, creds);

           request.Headers.Add("Authorization", "Bearer " + authResult.AccessToken);

        }
    }
}
