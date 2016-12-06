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
using System.Threading;

namespace DeliveryApp.Controllers
{
    public class AssignRouteController : Controller
    {
        private AppDTEntities db = new AppDTEntities();

        public ActionResult AssignRoute()
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

        public ActionResult VISITA_ASSIGN_Read([DataSourceRequest]DataSourceRequest request)
        {
      
            string date =DateTime.Now.Date.ToString("MM/dd/yyyy");
            char[] delimiterChars = {' ',',','.',':','\t' };
            string[] words = date.Split(delimiterChars);
            var dateIni = words[0]+" 00:00 AM";
            var dateEnd = words[0] + " 23:59 PM";
                IQueryable<vis_Assigned_Visit> visita_assign = db.vis_Assigned_Visit.Where(x => String.Compare(x.visi_date,dateIni) > 0 && String.Compare(x.visi_date, dateEnd) < 0);
            DataSourceResult result = visita_assign.ToDataSourceResult(request, vISITA_ASSIGN => new {
                visi_id = vISITA_ASSIGN.visi_id,
                cust_id = vISITA_ASSIGN.cust_id,
                cust_name = vISITA_ASSIGN.cust_name,
                inst_id = vISITA_ASSIGN.inst_id,
                inst_name = vISITA_ASSIGN.inst_name,
                visi_date = vISITA_ASSIGN.visi_date,
                visi_op = vISITA_ASSIGN.visi_op,
                visi_status = vISITA_ASSIGN.visi_status
            });

            return Json(result);
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult VISITA_ASSIGN_Update([DataSourceRequest]DataSourceRequest request, vis_Assigned_Visit vISITA_ASSIGN)
        {
            if (ModelState.IsValid)
            {
                var entity = new VISITA_ASSIGN
                {
                    visi_id = vISITA_ASSIGN.visi_id,
                    inst_id = vISITA_ASSIGN.inst_id,
                    visi_date = vISITA_ASSIGN.visi_date,
                    visi_op = vISITA_ASSIGN.visi_op,
                    visi_status = 0
                };

                db.VISITA_ASSIGN.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { vISITA_ASSIGN }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult VISITA_ASSIGN_Destroy([DataSourceRequest]DataSourceRequest request, vis_Assigned_Visit vISITA_ASSIGN)
        {
            if (ModelState.IsValid)
            {
                var entity = new VISITA_ASSIGN
                {
                    visi_id = vISITA_ASSIGN.visi_id,
                    inst_id = vISITA_ASSIGN.inst_id,
                    visi_date = vISITA_ASSIGN.visi_date,
                    visi_op = vISITA_ASSIGN.visi_op,
                    visi_status = 1
                };

                db.VISITA_ASSIGN.Attach(entity);
                db.VISITA_ASSIGN.Remove(entity);
                //db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { vISITA_ASSIGN }.ToDataSourceResult(request, ModelState));
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

        public ActionResult GetCustomer()
        {

            var cust = db.CUSTOMER.ToList();

            return Json(cust, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetInstaller()
        {

            var cust = db.INSTALLER.ToList();

            return Json(cust, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SetVisita(int Inst,int Cust,int Prio,string Fecha)
        {
            try
            {
                char[] delimiterChars = {' ', '\t' };
                string[] words = Fecha.Split(delimiterChars);
                var fecha = words[0];
                var hora = words[1];

                var entity = new VISITA_ASSIGN
                {
                    inst_id = Inst,
                    cust_id = Cust,
                    visi_op = Prio,
                    visi_date = fecha,
                    visi_status = 0,
                    visi_hora=hora
                };
                db.VISITA_ASSIGN.Add(entity);
                db.SaveChanges();
                return Json(new { succes = "OK" });
            }
            catch (Exception e)
            {
                return View();
            }


        }

    }
}
