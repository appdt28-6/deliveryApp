using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using DeliveryApp.Models;
using System.Globalization;

namespace DeliveryApp.Controllers
{
    public class NotVisitController : Controller
    {
        private AppDTEntities db = new AppDTEntities();

        public ActionResult NotVisit()
        {
            if (Session["user_id"] != null)
            {
                return View();
            }
            else
            {
                return Redirect("~/Home/Login");
            }
        }

        public ActionResult NOTVISIT_Read([DataSourceRequest]DataSourceRequest request)
        {
            string date = DateTime.Now.Date.ToString();
            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
            string[] words = date.Split(delimiterChars);
            var date1 = words[0].ToString();
            DateTime dateIni = DateTime.ParseExact(date1, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            IQueryable<NOTVISIT> notvisit = db.NOTVISIT.Where(x => x.novi_date== dateIni);
            DataSourceResult result = notvisit.ToDataSourceResult(request, nOTVISIT => new {
                novi_id = nOTVISIT.novi_id,
                vist_id = nOTVISIT.vist_id,
                inst_id = nOTVISIT.inst_id,
                novi_date = nOTVISIT.novi_date,
                novi_reazon = nOTVISIT.novi_reazon
            });

            return Json(result);
        }

        [HttpPost]
        public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }
    
        [HttpPost]
        public ActionResult Pdf_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
