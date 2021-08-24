﻿using CULCS.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CULCS.Services
{
    public interface ILoginServices
    {
        bool isAuthen();

        string AuthenName();
        string UserRole();
        bool isInAdminRoles(string[] roles);
        bool isInRoles(string[] roles);
        void Logout();
        void Login(string username, string usertype, bool isPersistent);

    }
    public class LoginServices : ILoginServices
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private HttpContext httpContext;

        public bool isAuthen()
        {
            return this.httpContext.User.Identity.IsAuthenticated;
        }
        public string AuthenName()
        {
            return this.httpContext.User.Identity.Name;
        }

        public string UserRole()
        {
            var rolename = this.httpContext.User.FindFirstValue(ClaimTypes.Role);
            return rolename;
        }

        public bool isInAdminRoles(string[] roles)
        {
            //if (UserRole() == RoleName.Admin)
            //    return true;

            //bool isRole = false;
            //for (var i = 0; i < roles.Length; i++)
            //{
            //    isRole = this.httpContext.User.IsInRole(roles[i]);
            //    if (isRole) { break; }
            //}
            //return isRole;

            return true;
        }
        public bool isInRoles(string[] roles)
        {
            bool isRole = false;
            for (var i = 0; i < roles.Length; i++)
            {
                isRole = this.httpContext.User.IsInRole(roles[i]);
                if (isRole) { break; }
            }
            return isRole;
        }

        public LoginServices(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.httpContext = this.httpContextAccessor.HttpContext;
        }
        public LoginServices(HttpContext httpContext)
        {
            this.httpContext = httpContext;
        }       
     
        public async void Login(string username, string usertype, bool isPersistent)
        {
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, username));
            identity.AddClaim(new Claim(ClaimTypes.Role, usertype));
            identity.AddClaim(new Claim(ClaimTypes.Name, username));
            identity.AddClaim(new Claim("Language", LanguageCode.Thai.ToString()));


            // Authenticate using the identity
            var principal = new ClaimsPrincipal(identity);
            await this.httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = isPersistent });
        }

        public async void Logout()
        {
            await this.httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
