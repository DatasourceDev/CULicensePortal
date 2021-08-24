using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CULCS.Models;
using CULCS.DAL;
using CULCS.Services;
using Microsoft.Identity.Web;
using CULCS.AD;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CULCS.Extensions;
using CULCS.DTO;
using Microsoft.AspNetCore.Authorization;

namespace CULCS.Controllers
{
    [Authorize]
    public class HomeController : ControllerBase
    {

        public HomeController(CuContext context, ILogger<HomeController> logger, ILoginServices loginServices, IGraphProvider graphProvider) : base(context, logger, loginServices, graphProvider)
        {

        }
        public async Task<IActionResult> Profile()
        {
            var user = await _graphProvider.GetUserByEmail(this.HttpContext.User.Identity.Name);
            if (user == null)
                return RedirectToAction("Logout", "Auth");
            return View(user);
        }
        [HttpGet]
        public ActionResult LoadProgram(string domain)
        {
            return Json(ListProgram(domain));
        }

        public async Task<IActionResult> Index(SearchDTO model)
        {
            var user = await _graphProvider.GetUserByEmail(this.HttpContext.User.Identity.Name);
            if (user == null)
                return RedirectToAction("Logout", "Auth");

            var lists = _context.Borrows.Include(i => i.ProgramLicense).Where(w=>1==1);
            if (!string.IsNullOrEmpty(model.text_search))
            {
                lists = lists.Where(w => w.UserPrincipalName.Contains(model.text_search));
            }
            if (!string.IsNullOrEmpty(model.dfrom))
            {
                var date = DateUtil.ToDate(model.dfrom);
                lists = lists.Where(w=>w.BorrowDate.Value.Date >= date | w.ExpiryDate.Value.Date >= date);
            }
            if (!string.IsNullOrEmpty(model.dto))
            {
                var date = DateUtil.ToDate(model.dto);
                lists = lists.Where(w => w.BorrowDate.Value.Date <= date | w.ExpiryDate.Value.Date <= date);
            }
            if (!string.IsNullOrEmpty(model.domain_search))
                lists = lists.Where(w => w.Domain == model.domain_search);

            if (model.borrow_status_search.HasValue)
                lists = lists.Where(w => w.BorrowStatus == model.borrow_status_search);

            if (model.program_search.HasValue)
                lists = lists.Where(w => w.ProgramLicenseID == model.program_search);

            if (_loginServices.UserRole() == UserType.User.toUserTypeName())
                lists = lists.Where(w => w.UserPrincipalName == this.HttpContext.User.Identity.Name);

            lists = lists.OrderByDescending(o => o.ID);
            int skipRows = (model.pageno - 1) * 25;
            var itemcnt = lists.Count();
            var pagelen = itemcnt / 25;
            if (itemcnt % 25 > 0)
                pagelen += 1;

            model.itemcnt = itemcnt;
            model.pagelen = pagelen;
            model.lists = lists.Skip(skipRows).Take(25).AsQueryable();

            ViewBag.ListDomain = this.ListDomain();

            return View(model);
        }
        public async Task<IActionResult> Borrow(int? id)
        {
            var user = await _graphProvider.GetUserByEmail(this.HttpContext.User.Identity.Name);
            if (user == null)
                return RedirectToAction("Logout", "Auth");

            Borrow model = new Borrow();
            if (id.HasValue && id > 0)
            {
                model = _context.Borrows.Include(i => i.ProgramLicense).Where(w => w.ID == id).FirstOrDefault();
                if (model == null)
                    return RedirectToAction("Index", "Home");
            }
            else
            {
                model.AzureUserId = user.Id;
                model.UserPrincipalName = user.UserPrincipalName;
                model.DisplayName = user.DisplayName;
                model.BorrowDateStr = DateUtil.ToDisplayDate(DateUtil.Now());
                var email = this.HttpContext.User.Identity.Name;
                var domain = email.Substring(email.IndexOf("@") + 1, (email.Length - email.IndexOf("@")) - 1);

                model.Domain = domain;

                var programs = _context.ProgramLicenses.Where(w => w.Status == Status.Enable);
                if (domain.ToLower() == "student.chula.ac.th")
                    programs = programs.Where(w => w.StudentChulaDomain == true);
                else if (domain.ToLower() == "chula.ac.th")
                    programs = programs.Where(w => w.ChulaDomain == true);
                else if (domain.ToLower() == "alumni.chula.ac.th")
                    programs = programs.Where(w => w.AlumniChulaDomain == true);

                programs = programs.OrderBy(o => o.AzureGroup.GroupName).ThenBy(o => o.ProgramName);

                ViewBag.ListProgram = new SelectList(programs, "ID", "ProgramName");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Borrow(Borrow model)
        {
            var program = _context.ProgramLicenses.Include(i => i.AzureGroup).Where(w => w.ID == model.ProgramLicenseID).FirstOrDefault();
            if (program == null)
                ModelState.AddModelError("ProgramLicenseID", "The Program License field is required.");

            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(model.BorrowDateStr))
                    model.BorrowDate = DateUtil.ToDate(model.BorrowDateStr);

                if (!string.IsNullOrEmpty(model.ExpiryDateStr))
                    model.ExpiryDate = DateUtil.ToDate(model.ExpiryDateStr);

                if (model.BorrowDate.Value.Date < DateUtil.Now().Date)
                {
                    //ModelState.AddModelError("BorrowDateStr", "วันที่ยืมจะต้องมากว่าหรือเท่ากับวันที่ปัจจุบัน");
                }
                else
                {
                    if (model.ExpiryDate.Value.Date < model.BorrowDate.Value.Date)
                        ModelState.AddModelError("ExpiryDateStr", "วันที่คืนจะต้องมากว่าหรือเท่ากับวันที่ยืม");


                    if (model.BorrowDate.Value.Date > DateUtil.Now().AddDays(program.MaxBorrowAdvance).Date)
                        ModelState.AddModelError("BorrowDateStr", "การยืมล่วงหน้าจะต้องไม่เกินเ " + program.MaxBorrowAdvance + " วัน");

                    if (program.PeriodType == PeriodType.Day)
                    {
                        if (model.ExpiryDate.Value.Date > model.BorrowDate.Value.AddDays(program.MaxBorrow).Date)
                            ModelState.AddModelError("ExpiryDateStr", "วันที่คืนเกินเวลาที่กำหนด (" + program.MaxBorrow + " วัน)");

                    }
                    else if (program.PeriodType == PeriodType.Month)
                    {
                        if (model.ExpiryDate.Value.Date > model.BorrowDate.Value.AddMonths(program.MaxBorrow).Date)
                            ModelState.AddModelError("ExpiryDateStr", "วันที่คืนเกินเวลาที่กำหนด (" + program.MaxBorrow + " เดือน)");
                    }
                    else if (program.PeriodType == PeriodType.Year)
                    {
                        if (model.ExpiryDate.Value.Date > model.BorrowDate.Value.AddYears(program.MaxBorrow).Date)
                            ModelState.AddModelError("ExpiryDateStr", "วันที่คืนเกินเวลาที่กำหนด (" + program.MaxBorrow + " ปี)");
                    }
                }

                var user = await _graphProvider.GetUserByEmail(this.HttpContext.User.Identity.Name);
                if (user == null)
                {
                    ModelState.AddModelError("AzureUserId", "ไม่พบข้อมูลผู้ใช้");
                }
                var group = await _graphProvider.GetGroupById(program.AzureGroup.AzureGroupId);
                if (group == null)
                {
                    ModelState.AddModelError("AzureUserId", "ไม่พบข้อมูลก Azure Group");
                }

                if (ModelState.IsValid)
                {
                    ViewBag.Message = ReturnMessage.Error;
                    ViewBag.ReturnCode = ReturnCode.Error;
                    if (model.ID == 0)
                    {
                        if (model.BorrowDate.Value.Date > DateUtil.Now())
                            model.BorrowStatus = BorrowStatus.Advance;

                        var borrows = _context.Borrows.Where(w => w.AzureUserId == user.Id &  w.ProgramLicenseID == model.ProgramLicenseID & (w.BorrowStatus == BorrowStatus.Borrowing | w.BorrowStatus == BorrowStatus.Advance) );
                        if (borrows.Count() > 0)
                        {
                            foreach (var borrow in borrows)
                            {
                                borrow.BorrowStatus = BorrowStatus.Cancelled;
                                var rresult = await _graphProvider.RemoveMember(group.Id, user.Id);
                            }
                        }

                        model.Create_On = DateUtil.Now();
                        model.Create_By = this.HttpContext.User.Identity.Name;
                        model.Update_On = DateUtil.Now();
                        model.Update_By = this.HttpContext.User.Identity.Name;

                        this._context.Borrows.Add(model);
                        this._context.SaveChanges();
                        if (model.BorrowStatus == BorrowStatus.Borrowing)
                        {
                           
                            var result = await _graphProvider.AddMember(group.Id, user.Id);
                            if (result)
                            {
                                if (!string.IsNullOrEmpty(program.AzureGroup.TempAzureGroupId))
                                {
                                    var grouptemp = await _graphProvider.GetGroupById(program.AzureGroup.TempAzureGroupId);
                                    if (grouptemp != null)
                                    {
                                        var result2 = await _graphProvider.RemoveMember(grouptemp.Id, user.Id);
                                    }
                                }
                                return RedirectToAction("Index", "Home", new { code = ReturnCode.Success, msg = ReturnMessage.Success });
                            }
                        }
                        else
                            return RedirectToAction("Index", "Home", new { code = ReturnCode.Success, msg = ReturnMessage.Success });
                    }
                }
            }
            var email = this.HttpContext.User.Identity.Name;
            var domain = email.Substring(email.IndexOf("@") + 1, (email.Length - email.IndexOf("@")) - 1);

            model.Domain = domain;

            var programs = _context.ProgramLicenses.Where(w => w.Status == Status.Enable);
            if (domain.ToLower() == "student.chula.ac.th")
                programs = programs.Where(w => w.StudentChulaDomain == true);
            else if (domain.ToLower() == "chula.ac.th")
                programs = programs.Where(w => w.ChulaDomain == true);
            else if (domain.ToLower() == "alumni.chula.ac.th")
                programs = programs.Where(w => w.AlumniChulaDomain == true);

            programs = programs.OrderBy(o => o.AzureGroup.GroupName).ThenBy(o => o.ProgramName);

            ViewBag.ListProgram = new SelectList(programs, "ID", "ProgramName");
            return View(model);
        }
        public async Task<IActionResult> Borrowing(SearchDTO model)
        {
            var user = await _graphProvider.GetUserByEmail(this.HttpContext.User.Identity.Name);
            if (user == null)
                return RedirectToAction("Logout", "Auth");

            var lists = _context.Borrows.Include(i => i.ProgramLicense).Where(w => w.BorrowStatus == BorrowStatus.Borrowing);
            if (!string.IsNullOrEmpty(model.text_search))
            {
                lists = lists.Where(w => w.UserPrincipalName.Contains(model.text_search));
            }

            if (!string.IsNullOrEmpty(model.dfrom))
            {
                var date = DateUtil.ToDate(model.dfrom);
                lists = lists.Where(w => w.BorrowDate.Value.Date >= date | w.ExpiryDate.Value.Date >= date);
            }
            if (!string.IsNullOrEmpty(model.dto))
            {
                var date = DateUtil.ToDate(model.dto);
                lists = lists.Where(w => w.BorrowDate.Value.Date <= date | w.ExpiryDate.Value.Date <= date);
            }
            if (!string.IsNullOrEmpty(model.domain_search))
                lists = lists.Where(w => w.Domain == model.domain_search);

            if (model.borrow_status_search.HasValue)
                lists = lists.Where(w => w.BorrowStatus == model.borrow_status_search);

            if (model.program_search.HasValue)
                lists = lists.Where(w => w.ProgramLicenseID == model.program_search);

            lists = lists.OrderByDescending(o => o.ID);

            int skipRows = (model.pageno - 1) * 25;
            var itemcnt = lists.Count();
            var pagelen = itemcnt / 25;
            if (itemcnt % 25 > 0)
                pagelen += 1;

            model.itemcnt = itemcnt;
            model.pagelen = pagelen;
            model.lists = lists.Skip(skipRows).Take(25).AsQueryable();

            ViewBag.ListDomain = this.ListDomain();

            return View(model);
        }
    }
}
