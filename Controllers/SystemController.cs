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
using CULCS.DTO;
using CULCS.AD;
using CULCS.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CULCS.Controllers
{
    public class SystemController : ControllerBase
    {
        public SystemController(CuContext context, ILogger<SystemController> logger, ILoginServices loginServices, IGraphProvider graphProvider) : base(context, logger, loginServices, graphProvider)
        {

        }
        public IActionResult Group(SearchDTO model)
        {
            var lists = this._context.AzureGroups.OrderBy(o => o.ID);
            model.lists = lists;

            ViewBag.Message = model.msg;
            ViewBag.ReturnCode = model.code;
            return View(model);
        }
        public  IActionResult GroupInfo(int? id)
        {
            var model = new AzureGroup();
            if (id.HasValue)
            {
                model = _context.AzureGroups.Where(w => w.ID == id).FirstOrDefault();
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult GroupInfo(AzureGroup model)
        {
            if (this.isExistGroupAzure(model))
            {
                ModelState.AddModelError("AzureGroupId", "Azure Group ซ้ำในระบบ");
                ModelState.AddModelError("AzureGroupName", "Azure Group ซ้ำในระบบ");
            }
            if (this.isExistGroupName(model))
            {
                ModelState.AddModelError("GroupName", "ชื่อกลุ่มที่กำหนดเองซ้ำในระบบ");
            }
            if (this.isExistGroupName(model))
            {
                ModelState.AddModelError("GroupName", "ชื่อกลุ่มที่กำหนดเองซ้ำในระบบ");
            }
            if (ModelState.IsValid)
            {
                ViewBag.Message = ReturnMessage.Error;
                ViewBag.ReturnCode = ReturnCode.Error;

                if (model.ID > 0)
                {
                    var obj = _context.AzureGroups.Where(w => w.ID == model.ID).FirstOrDefault();
                    if (obj != null)
                    {
                        obj.Update_On = DateUtil.Now();
                        obj.Update_By = this.HttpContext.User.Identity.Name;
                        obj.AzureGroupName = model.AzureGroupName;
                        obj.AzureGroupId = model.AzureGroupId;
                        obj.TempAzureGroupName = model.TempAzureGroupName;
                        obj.TempAzureGroupId = model.TempAzureGroupId;
                        obj.GroupName = model.GroupName;
                        obj.Status = model.Status;
                        this._context.SaveChanges();
                        return RedirectToAction("Group", "System", new { code = ReturnCode.Success, msg = ReturnMessage.Success });
                    }
                }
                else
                {
                    model.Create_On = DateUtil.Now();
                    model.Create_By = this.HttpContext.User.Identity.Name;
                    model.Update_On = DateUtil.Now();
                    model.Update_By = this.HttpContext.User.Identity.Name;

                    this._context.AzureGroups.Add(model);
                    this._context.SaveChanges();
                    return RedirectToAction("Group", "System", new { code = ReturnCode.Success, msg = ReturnMessage.Success });
                }
            }
            return View(model);
        }

        public IActionResult GroupDel(int? id)
        {
            var msg = ReturnMessage.Error;
            var code = ReturnCode.Error;
            if (id.HasValue)
            {
                var model = _context.AzureGroups.Where(w => w.ID == id).FirstOrDefault();
                if (model != null)
                {
                    var groups = _context.ProgramLicenses.Where(w => w.AzureGroupID == id);
                    if (groups.Count() > 0)
                    {
                        msg = ReturnMessage.DataInUse;
                        code = ReturnCode.Error;
                        return RedirectToAction("Group", "System", new { code = code, msg = msg });
                    }                   

                    _context.AzureGroups.Remove(model);
                    _context.SaveChanges();
                    msg = ReturnMessage.Success;
                    code = ReturnCode.Success;
                }
            }
            return RedirectToAction("Group", "System", new { code = code, msg = msg });
        }
        public async Task<JsonResult> GetGroup(string Id)
        {
            var group = await _graphProvider.GetGroupById(Id);
            if (group != null)
            {
                return Json(new { groupname = group.DisplayName, groupId = group.Id, result = ReturnCode.Success });
            }
            return Json(new { error = ReturnMessage.Error, result = ReturnCode.Error });
        }

        public IActionResult Program(SearchDTO model)
        {
            var lists = this._context.ProgramLicenses.Include(i=>i.AzureGroup).OrderBy(o => o.AzureGroup.GroupName).ThenBy(o=>o.ProgramName);
            model.lists = lists;

            ViewBag.Message = model.msg;
            ViewBag.ReturnCode = model.code;
            return View(model);
        }
        public IActionResult ProgramInfo(int? id)
        {
            var model = new ProgramLicense();
            model.MaxBorrow = 30;
            model.MaxBorrowAdvance = 30;
            if (id.HasValue)
            {
                model = _context.ProgramLicenses.Where(w => w.ID == id).FirstOrDefault();
            }
            ViewBag.ListGroup = new SelectList(_context.AzureGroups.Where(w=>w.Status == Status.Enable).OrderBy(o=>o.GroupName),"ID", "GroupName");
            return View(model);
        }
        [HttpPost]
        public IActionResult ProgramInfo(ProgramLicense model)
        {          
            if (this.isExistProgramName(model))
            {
                ModelState.AddModelError("ProgramName", "ชื่อซ้ำในระบบ");
            }
           
            if (ModelState.IsValid)
            {
                ViewBag.Message = ReturnMessage.Error;
                ViewBag.ReturnCode = ReturnCode.Error;

                if (model.ID > 0)
                {
                    var obj = _context.ProgramLicenses.Where(w => w.ID == model.ID).FirstOrDefault();
                    if (obj != null)
                    {
                        obj.Update_On = DateUtil.Now();
                        obj.Update_By = this.HttpContext.User.Identity.Name;
                        obj.ProgramName = model.ProgramName;
                        obj.AzureGroupID = model.AzureGroupID;
                        obj.AlumniChulaDomain = model.AlumniChulaDomain;
                        obj.ChulaDomain = model.ChulaDomain;
                        obj.StudentChulaDomain = model.StudentChulaDomain;
                        obj.MaxBorrowAdvance = model.MaxBorrowAdvance;
                        obj.MaxBorrow = model.MaxBorrow;
                        obj.PeriodType = model.PeriodType;
                        obj.Status = model.Status;
                        this._context.SaveChanges();
                        return RedirectToAction("Program", "System", new { code = ReturnCode.Success, msg = ReturnMessage.Success });
                    }
                }
                else
                {
                    model.Create_On = DateUtil.Now();
                    model.Create_By = this.HttpContext.User.Identity.Name;
                    model.Update_On = DateUtil.Now();
                    model.Update_By = this.HttpContext.User.Identity.Name;

                    this._context.ProgramLicenses.Add(model);
                    this._context.SaveChanges();
                    return RedirectToAction("Program", "System", new { code = ReturnCode.Success, msg = ReturnMessage.Success });
                }
            }
            ViewBag.ListGroup = new SelectList(_context.AzureGroups.Where(w => w.Status == Status.Enable).OrderBy(o => o.GroupName), "ID", "GroupName");
            return View(model);
        }
        public IActionResult ProgramDel(int? id)
        {
            var msg = ReturnMessage.Error;
            var code = ReturnCode.Error;
            if (id.HasValue)
            {
                var model = _context.ProgramLicenses.Where(w => w.ID == id).FirstOrDefault();
                if (model != null)
                {
                    var borrows = _context.Borrows.Where(w => w.ProgramLicenseID == id);
                    if (borrows.Count() > 0)
                    {
                        msg = ReturnMessage.DataInUse;
                        code = ReturnCode.Error;
                        return RedirectToAction("Program", "System", new { code = code, msg = msg });
                    }

                    _context.ProgramLicenses.Remove(model);
                    _context.SaveChanges();
                    msg = ReturnMessage.Success;
                    code = ReturnCode.Success;
                }
            }
            return RedirectToAction("Program", "System", new { code = code, msg = msg });
        }
        public IActionResult Setup()
        {
            var model = _context.Setups.FirstOrDefault();
            if (model == null)
                model = new Setup();
            return View(model);
        }
        [HttpPost]
        public IActionResult Setup(Setup model)
        {
            
            if (ModelState.IsValid)
            {
                ViewBag.Message = ReturnMessage.Error;
                ViewBag.ReturnCode = ReturnCode.Error;
                if (model.ID > 0)
                {
                    model.Update_On = DateUtil.Now();
                    model.Update_By = this.HttpContext.User.Identity.Name;
                    this._context.Update(model);
                    this._context.SaveChanges();
                }
                else
                {
                    model.Create_On = DateUtil.Now();
                    model.Create_By = this.HttpContext.User.Identity.Name;
                    model.Update_On = DateUtil.Now();
                    model.Update_By = this.HttpContext.User.Identity.Name;
                    this._context.Setups.Add(model);
                    this._context.SaveChanges();
                }
                ViewBag.Message = ReturnMessage.Success;
                ViewBag.ReturnCode = ReturnCode.Success;
            }
            return View(model);
        }
        public IActionResult Admin()
        {
            var model = new SearchDTO();
            return View(model);
        }

        public IActionResult AdminInfo()
        {
            return View();
        }


        public bool isExistGroupAzure(AzureGroup model)
        { 
            if (string.IsNullOrEmpty(model.AzureGroupId))
                return false;
            var query = this._context.AzureGroups.Where(c => c.AzureGroupId == model.AzureGroupId & (model.ID > 0 ? c.ID != model.ID : true) & c.Status != Status.Disable).FirstOrDefault();
            return (query != null);
        }
        public bool isExistGroupName(AzureGroup model)
        {
            if (string.IsNullOrEmpty(model.GroupName))
                return false;
            var query = this._context.AzureGroups.Where(c => c.GroupName == model.GroupName & (model.ID > 0 ? c.ID != model.ID : true) & c.Status != Status.Disable).FirstOrDefault();
            return (query != null);
        }
        public bool isExistProgramName(ProgramLicense model)
        {
            if (string.IsNullOrEmpty(model.ProgramName))
                return false;
            var query = this._context.ProgramLicenses.Where(c => c.ProgramName == model.ProgramName & (model.ID > 0 ? c.ID != model.ID : true)).FirstOrDefault();
            return (query != null);
        }
        public bool isExistGroup(ProgramLicense model)
        {
            var query = this._context.ProgramLicenses.Where(c => c.AzureGroupID == model.AzureGroupID & (model.ID > 0 ? c.ID != model.ID : true)).FirstOrDefault();
            return (query != null);
        }
    }
}
