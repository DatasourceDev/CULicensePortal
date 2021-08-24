using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CULCS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using CULCS.DAL;
using CULCS.Services;
using System.IO;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Net.Mail;
using System.Text;
using CULCS.DTO;
using Microsoft.Identity.Web;
using CULCS.AD;

namespace CULCS.Controllers
{
    public class ControllerBase : Controller
    {
        public readonly ILogger<ControllerBase> _logger;
        public CuContext _context;
        public ILoginServices _loginServices;
        public IGraphProvider _graphProvider;

        public ControllerBase()
        {
        }

        public ControllerBase(CuContext context, ILogger<ControllerBase> logger, ILoginServices loginServices, IGraphProvider graphProvider)
        {
            this._context = context;
            this._logger = logger;
            this._loginServices = loginServices;
            this._graphProvider = graphProvider;
        }
        protected SelectList ListDomain()
        {
            var list = new List<string>();
            list.Add("Chula.ac.th");
            list.Add("Student.chula.ac.th");
            list.Add("Alumni.chula.ac.th");
            return new SelectList(list);
        }
        protected SelectList ListProgram(string domain)
        {
            var list = new List<ProgramLicense>();
            if(domain == "Chula.ac.th")
                list.AddRange(this._context.ProgramLicenses.Where(w => w.ChulaDomain == true).OrderBy(o => o.ID));
            else if (domain == "Student.chula.ac.th")
                list.AddRange(this._context.ProgramLicenses.Where(w => w.StudentChulaDomain == true).OrderBy(o => o.ID));
            else if (domain == "Alumni.chula.ac.th")
                list.AddRange(this._context.ProgramLicenses.Where(w => w.AlumniChulaDomain == true).OrderBy(o => o.ID));

            return new SelectList(list, "ID", "ProgramName");
        }
        protected SelectList ListProvince()
        {
            var list = new List<Province>();
            list.AddRange(this._context.Provinces.OrderBy(o => o.ProvinceCode));
            return new SelectList(list, "ProvinceID", "ProvinceName");
        }
        protected SelectList ListAumphur(int? pID)
        {
            var list = new List<Aumphur>();
            list.AddRange(this._context.Aumphurs.Where(w => w.ProvinceID == pID).OrderBy(o => o.AumphurCode));
            return new SelectList(list, "AumphurID", "AumphurName");
        }
        protected SelectList ListTumbon(int? aID)
        {
            var list = new List<Tumbon>();
            list.AddRange(this._context.Tumbons.Where(w => w.AumphurID == aID).OrderBy(o => o.TumbonName));
            return new SelectList(list, "TumbonID", "TumbonName");
        }


        [HttpGet]
        public ActionResult LoadAumphur(int? pID, string lang)
        {
            //if (lang == "EN")
            //    return Json(ListAumphurEn(pID));
            //else
            return Json(ListAumphur(pID));

        }
        [HttpGet]
        public ActionResult LoadTumbon(int? aID, string lang)
        {
            //if (lang == "EN")
            //    return Json(ListTumbonEn(aID));
            //else
            return Json(ListTumbon(aID));

        }
        [HttpGet]
        public ActionResult LoadPostal(int? tID)
        {
            var tumbon = this._context.Tumbons.Where(w => w.TumbonID == tID).FirstOrDefault();
            if (tumbon != null)
                return Json(tumbon.PostalCode);
            else
                return Json("");
        }

       

        //public async Task<IActionResult> MailRegister(string email)
        //{
        //    User model = null;
        //    var htmlToConvert = await RenderViewAsync("MailRegister", model, true);
        //    var msg = sendNotificationEmail(email, "ลงทะเบียน ผู้ใช้ชั่วคราวสำเร็จ", htmlToConvert.ToString());

        //    return Json(new { Msg = msg });
        //}
        //public async Task<IActionResult> MailOTP(string email, OTPDTO model)
        //{
        //    var htmlToConvert = await RenderViewAsync("MailOTP", model, true);
        //    var msg = sendNotificationEmail(email, "รหัสลงทะเบียน", htmlToConvert.ToString());

        //    return Json(new { Msg = msg });
        //}
        public async Task<string> RenderViewAsync<TModel>(string viewName, TModel model, bool partial = false)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = this.ControllerContext.ActionDescriptor.ActionName;
            }

            this.ViewData.Model = model;

            using (var writer = new StringWriter())
            {
                IViewEngine viewEngine = this.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
                ViewEngineResult viewResult = viewEngine.FindView(this.ControllerContext, viewName, !partial);

                if (viewResult.Success == false)
                {
                    return $"A view with the name {viewName} could not be found";
                }

                ViewContext viewContext = new ViewContext(
                    this.ControllerContext,
                    viewResult.View,
                    this.ViewData,
                    this.TempData,
                    writer,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);

                return writer.GetStringBuilder().ToString();
            }

            
        }

        //public string sendNotificationEmail(string to, string header, string message)
        //{
        //    var setup = this._context.Setups.FirstOrDefault();
        //    var msg = new System.Text.StringBuilder();
        //    try
        //    {
        //        //msg.Append("TO_EMAIL: " + to + "   ");
        //        //msg.Append(" SMTP_SERVER: " + smtp.SMTP_SERVER + "   ");
        //        //msg.Append(" SMTP_PORT: " + smtp.SMTP_PORT + "   ");
        //        //msg.Append(" SMTP_FROM: " + smtp.SMTP_FROM + "   ");
        //        //msg.Append(" SMTP_USERNAME: " + smtp.SMTP_USERNAME + "   ");
        //        //msg.Append(" SMTP_PASSWORD: " + smtp.SMTP_PASSWORD + "   ");
        //        //msg.Append(" STMP_SSL: " + smtp.STMP_SSL + "   ");

        //        var SMTP_SERVER = setup.SMTP_Server;
        //        var SMTP_PORT = setup.SMTP_Port;
        //        var SMTP_USERNAME = setup.SMTP_Username;
        //        var SMTP_PASSWORD = setup.SMTP_Password;
        //        var SMTP_FROM = setup.SMTP_From;
        //        bool STMP_SSL = setup.SMTP_SSL;

        //        SmtpClient smtpClient = new SmtpClient(SMTP_SERVER, SMTP_PORT);
        //        System.Net.NetworkCredential cred = new System.Net.NetworkCredential(SMTP_USERNAME, SMTP_PASSWORD);

        //        smtpClient.UseDefaultCredentials = false;
        //        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        //        smtpClient.EnableSsl = STMP_SSL;

        //        var mail = new MailMessage(SMTP_FROM, to, header, message);
        //        mail.BodyEncoding = Encoding.UTF8;
        //        mail.IsBodyHtml = true;

        //        smtpClient.Credentials = cred;
        //        smtpClient.Send(mail);

        //        return msg.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        msg.AppendLine(" EXCEPTION: " + ex.Message);
        //    }
        //    return msg.ToString();
        //}
    }
}
