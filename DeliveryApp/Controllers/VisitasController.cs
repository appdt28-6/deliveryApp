﻿using System;
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

namespace DeliveryApp.Controllers
{
    public class VisitasController : Controller
    {
        private AppDTEntities db = new AppDTEntities();

        public ActionResult Visitas()
        {
            if (Session["user_id"] != null)
            {
                //TimeZoneMX datemx = new TimeZoneMX();
                //var dateIni = datemx.GetDateMX();
                string date = DateTime.Now.Date.ToString("MM/dd/yyyy");
                ViewData["DynamicMapa"]=db.VISITA_REGISTRO.Where(x =>x.reg_date== date).ToList();

                return View();
            }
            else
            {
                return Redirect("~/Home/Login");
            }
        }

        public ActionResult VISITA_REGISTRO_Read([DataSourceRequest]DataSourceRequest request)
        {
            //TimeZoneMX datemx = new TimeZoneMX();
            //var dateIni = datemx.GetDateMX();


            string date = DateTime.Now.Date.ToString("MM/dd/yyyy");
            // IQueryable<vis_VISITA_REGISTRO> visita_registro = db.vis_VISITA_REGISTRO.Where(x => String.Compare(x.reg_date, dateIni) > 0 && String.Compare(x.reg_date, dateEnd) < 0&&x.reg_status<=2);
            IQueryable<vis_VISITA_REGISTRO> visita_registro = db.vis_VISITA_REGISTRO.Where(x => x.reg_date== date && x.reg_status <= 2);
            DataSourceResult result = visita_registro.ToDataSourceResult(request, vISITA_REGISTRO => new {
                reg_id = vISITA_REGISTRO.reg_id,
                visi_id = vISITA_REGISTRO.visi_id,
                inst_name = vISITA_REGISTRO.inst_name,
                cust_name = vISITA_REGISTRO.cust_name,
                reg_date = vISITA_REGISTRO.reg_date,
               // reg_lat = vISITA_REGISTRO.reg_lat,
                //reg_lon = vISITA_REGISTRO.reg_lon,
                reg_ini = vISITA_REGISTRO.reg_ini,
                reg_end = vISITA_REGISTRO.reg_end,
                reg_status = vISITA_REGISTRO.reg_status
            });

            return Json(result);
        }


        public ActionResult VISITA_REGISTRO_Read_Survey([DataSourceRequest]DataSourceRequest request,int visita)
        {
  
            IQueryable<SURVEY_CUESTION_OP> visita_registro = db.SURVEY_CUESTION_OP.Where(s=>s.visi_id==visita);
            DataSourceResult result = visita_registro.ToDataSourceResult(request, vISITA_REGISTRO => new {
                reg_id = vISITA_REGISTRO.visi_id,
                surv_r1 = vISITA_REGISTRO.surv_r1,
                surv_r2 = vISITA_REGISTRO.surv_r2,
                surv_txt = vISITA_REGISTRO.surv_txt
            });

            return Json(result);
        }

        public ActionResult VISITA_REGISTRO_Read_NotVisist([DataSourceRequest]DataSourceRequest request)
        {
            //TimeZoneMX datemx = new TimeZoneMX();
            //var dateIni = datemx.GetDateMX();

            string date = DateTime.Now.Date.ToString("MM/dd/yyyy");

            IQueryable<vis_VISITA_NOTVISIT> visita_registro = db.vis_VISITA_NOTVISIT.Where(x => x.reg_date == date && x.reg_status == 3);
            DataSourceResult result = visita_registro.ToDataSourceResult(request, vISITA_REGISTRO => new {
                reg_id = vISITA_REGISTRO.reg_id,
                visi_id = vISITA_REGISTRO.visi_id,
                inst_id = vISITA_REGISTRO.inst_id,
                cust_id = vISITA_REGISTRO.cust_id,
                reg_date = vISITA_REGISTRO.reg_date,
                //reg_lat = vISITA_REGISTRO.reg_lat,
                //reg_lon = vISITA_REGISTRO.reg_lon,
                reg_ini = vISITA_REGISTRO.reg_ini,
                novi_reazon = vISITA_REGISTRO.novi_reazon
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
