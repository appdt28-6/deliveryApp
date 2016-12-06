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
    public class SurveyDataController : Controller
    {
        private AppDTEntities db = new AppDTEntities();

        public ActionResult SurveyData()
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

        public ActionResult SURVEY_CUESTION_OP_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<SURVEY_CUESTION_OP> survey_cuestion_op = db.SURVEY_CUESTION_OP;
            DataSourceResult result = survey_cuestion_op.ToDataSourceResult(request, sURVEY_CUESTION_OP => new {
                op_id = sURVEY_CUESTION_OP.op_id,
                surv_id = sURVEY_CUESTION_OP.surv_id,
                surv_c1 = sURVEY_CUESTION_OP.surv_c1,
                surv_r1 = sURVEY_CUESTION_OP.surv_r1,
                surv_c2 = sURVEY_CUESTION_OP.surv_c2,
                surv_r2 = sURVEY_CUESTION_OP.surv_r2,
                surv_txt = sURVEY_CUESTION_OP.surv_txt
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
