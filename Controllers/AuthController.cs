using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CULCS.Models;
using CULCS.DTO;
using CULCS.DAL;
using CULCS.Services;

using Microsoft.Graph;
using Microsoft.Identity.Client;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using CULCS.AD;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using System.Security;

namespace CULCS.Controllers
{
    public class AuthController : ControllerBase
    {
        public CULCS.DTO.AzureAD _azureAD;
        public AuthController(CuContext context, ILogger<AuthController> logger, ILoginServices loginServices, IOptions<CULCS.DTO.AzureAD> azureAD, IGraphProvider graphProvider) : base(context, logger, loginServices, graphProvider)
        {
            _azureAD = azureAD.Value;
        }
       
        public IActionResult SignOutSuccess()
        {
            return View();
        }
        public  IActionResult Login(LanguageCode lang)
        {           
            var model = new LoginDTO();
            model.LanguageCode = lang;
            model.Languages = _context.Languages.Where(w => w.LanguageCode == lang & w.Page == Page.Login);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            var user = await _graphProvider.GetUserByEmail(model.UserName);
            if (user != null)
            {
                if (await _graphProvider.ValidateAuth(_azureAD, model.UserName, model.Password))
                {
                    var setup = _context.Setups.FirstOrDefault();
                    var isadmin = await _graphProvider.IsAdmin(user.Id, setup.AdminAzureGroupName);
                    if (isadmin)
                    {
                        this._loginServices.Login(model.UserName, UserType.Admin.ToString(), true);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        this._loginServices.Login(model.UserName, UserType.User.ToString(), true);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("Password", "รหัสผ่านไม่ถูกต้อง");
                }
            }
            else
            {
                ModelState.AddModelError("UserName", "ไม่พบข้อมูลอีเมลผู้ใช้");
            }
            model.Languages = _context.Languages.Where(w => w.LanguageCode == model.LanguageCode & w.Page == Page.Login);
            return View(model);
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        public IActionResult Logout()
        {
            this._loginServices.Logout();
            return RedirectToAction("Login", "Auth");
        }

    }
}
