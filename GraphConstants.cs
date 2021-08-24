using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace CULCS
{
    public class GraphConstants
    {
        // Defines the permission scopes used by the app
        public readonly static string[] Scopes =
        {
            "User.Read",
            "MailboxSettings.Read",
            "Calendars.ReadWrite"
        };
    }
    public static class GraphClaimTypes
    {
        public const string DisplayName = "graph_name";
        public const string Email = "graph_email";
        public const string Photo = "graph_photo";
        public const string TimeZone = "graph_timezone";
        public const string DateTimeFormat = "graph_datetimeformat";
    }

    // Helper methods to access Graph user data stored in
    // the claims principal
    public static class GraphClaimsPrincipalExtensions
    {
        public static string GetUserGraphDisplayName(this ClaimsPrincipal claimsPrincipal)
        {
            return "Adele Vance";
        }

        public static string GetUserGraphEmail(this ClaimsPrincipal claimsPrincipal)
        {
            return "adelev@contoso.com";
        }

        public static string GetUserGraphPhoto(this ClaimsPrincipal claimsPrincipal)
        {
            return "/img/no-profile-photo.png";
        }
    }
}
