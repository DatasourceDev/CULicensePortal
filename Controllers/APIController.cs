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
using CULCS.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CULCS.Controllers
{
    public class APIController : ControllerBase
    {
        public CULCS.DTO.AzureAD _azureAD;
        public APIController(CuContext context, ILogger<APIController> logger, ILoginServices loginServices, IOptions<CULCS.DTO.AzureAD> azureAD, IGraphProvider graphProvider) : base(context, logger, loginServices, graphProvider)
        {
            _azureAD = azureAD.Value;
        }
       
        public IActionResult SignOutSuccess()
        {
            return View();
        }

        [HttpGet]
        [Route("api/program")]
        public async Task<Object> program(string code)
        {
            var user = await _graphProvider.GetUserByEmail(code);
            if (user == null)
                return null;
            var email = user.UserPrincipalName;
            var domain = email.Substring(email.IndexOf("@") + 1, (email.Length - email.IndexOf("@")) - 1);

            var AzureUserId = user.Id;
            var programs = _context.ProgramLicenses.Where(w => w.Status == Status.Enable);
            if (domain.ToLower() == "student.chula.ac.th")
                programs = programs.Where(w => w.StudentChulaDomain == true);
            else if (domain.ToLower() == "chula.ac.th")
                programs = programs.Where(w => w.ChulaDomain == true);
            else if (domain.ToLower() == "alumni.chula.ac.th")
                programs = programs.Where(w => w.AlumniChulaDomain == true);

            programs = programs.OrderBy(o => o.AzureGroup.GroupName).ThenBy(o => o.ProgramName);

            return programs.Select(s => new
            {
                ProgramLicense = s.ProgramName,
            }).ToArray();
        }

        [HttpGet]
        [Route("api/borrow")]
        public async Task<Object> Borrow(string code)
        {
            var user = await _graphProvider.GetUserByEmail(code);
            if (user == null)
                return null;
            var AzureUserId = user.Id;
            var borrows = _context.Borrows.Include(i=>i.ProgramLicense).Where(w => w.AzureUserId == AzureUserId).OrderByDescending(o => o.BorrowDate);
            return borrows.Select(s => new
            {
                AzureUserId = s.AzureUserId,
                ProgramLicense = s.ProgramLicense.ProgramName,
                BorrowDate = DateUtil.ToDisplayDate(s.BorrowDate),
                ExpiryDate = DateUtil.ToDisplayDate(s.ExpiryDate),
                ReturnDate = DateUtil.ToDisplayDate(s.ReturnDate),
            }).ToArray();
        }
    }
}
