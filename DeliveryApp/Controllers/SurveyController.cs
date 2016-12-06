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
    public class SurveyController : Controller
    {
        private AppDTEntities db = new AppDTEntities();

        public ActionResult Survey()
        {
            return View();
        }

        public ActionResult SURVEY_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<SURVEY> survey = db.SURVEY.Where(s=>s.surv_deleted=="N");
            DataSourceResult result = survey.ToDataSourceResult(request, sURVEY => new {
                surv_id = sURVEY.surv_id,
                surv_name = sURVEY.surv_name,
                surv_deleted = sURVEY.surv_deleted,
                surv_datecreate = sURVEY.surv_datecreate
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SURVEY_Create([DataSourceRequest]DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<SURVEY> survey)
        {
            var entities = new List<SURVEY>();
            if (survey != null && ModelState.IsValid)
            {
                foreach(var sURVEY in survey)
                {
                    var entity = new SURVEY
                    {
                        surv_id = sURVEY.surv_id,
                        surv_name = sURVEY.surv_name,
                            surv_deleted = "N",
                            surv_datecreate = DateTime.Now
                    };

                    db.SURVEY.Add(entity);
                    entities.Add(entity);
                }
                db.SaveChanges();
            }

            return Json(entities.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SURVEY_Update([DataSourceRequest]DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<SURVEY> survey)
        {
            var entities = new List<SURVEY>();
            if (survey != null && ModelState.IsValid)
            {
                foreach(var sURVEY in survey)
                {
                    var entity = new SURVEY
                    {
                        surv_id = sURVEY.surv_id,
                        surv_name = sURVEY.surv_name,
                        surv_deleted = "N",
                        surv_datecreate = sURVEY.surv_datecreate,
                    };

                    entities.Add(entity);
                    db.SURVEY.Attach(entity);
                    db.Entry(entity).State = EntityState.Modified;                        
                }
                db.SaveChanges();
            }

            return Json(entities.ToDataSourceResult(request, ModelState));
        } 

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SURVEY_Destroy([DataSourceRequest]DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<SURVEY> survey)
        {
            var entities = new List<SURVEY>();
            if (ModelState.IsValid)
            {
                foreach(var sURVEY in survey)
                {
                    var entity = new SURVEY
                    {
                        surv_id = sURVEY.surv_id,
                        surv_name = sURVEY.surv_name,
                        surv_deleted = "Y",
                        surv_datecreate = sURVEY.surv_datecreate,
                    };

                    entities.Add(entity);
                    db.SURVEY.Attach(entity);
                    db.SURVEY.Remove(entity);
                }
                db.SaveChanges();
            }

            return Json(entities.ToDataSourceResult(request, ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult SURVEY_CUESTION_Read([DataSourceRequest]DataSourceRequest request,int surv)
        {
            IQueryable<SURVEY_CUESTION> survey = db.SURVEY_CUESTION.Where(s =>s.surv_id==surv && s.surv_deleted == "N");
            DataSourceResult result = survey.ToDataSourceResult(request, sURVEY => new {
                surv_cuestion_id = sURVEY.surv_cuestion_id,
                surv_id = sURVEY.surv_id,
                surv_name_custion = sURVEY.surv_name_custion,
                surv_type_cuestion = sURVEY.surv_type_cuestion,
                surv_deleted = sURVEY.surv_deleted,
                
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SURVEY_CUESTION_Create([DataSourceRequest]DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<SURVEY_CUESTION> survey ,int surv)
        {
            var entities = new List<SURVEY_CUESTION>();
            if (survey != null && ModelState.IsValid)
            {
                foreach (var sURVEY in survey)
                {
                    var entity = new SURVEY_CUESTION
                    {
                        surv_id = surv,
                        surv_cuestion_id = sURVEY.surv_cuestion_id,
                        surv_name_custion = sURVEY.surv_name_custion,
                        surv_type_cuestion= sURVEY.surv_type_cuestion,
                        surv_deleted = "N",
                    };

                    db.SURVEY_CUESTION.Add(entity);
                    entities.Add(entity);
                }
                db.SaveChanges();
            }

            return Json(entities.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SURVEY_CUESTION_Update([DataSourceRequest]DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<SURVEY_CUESTION> survey,int surv)
        {
            var entities = new List<SURVEY_CUESTION>();
            if (survey != null && ModelState.IsValid)
            {
                foreach (var sURVEY in survey)
                {
                    var entity = new SURVEY_CUESTION
                    {
                        surv_id =surv,
                        surv_cuestion_id = sURVEY.surv_cuestion_id,
                        surv_name_custion = sURVEY.surv_name_custion,
                        surv_type_cuestion = sURVEY.surv_type_cuestion,
                        surv_deleted = "N",
                    };

                    entities.Add(entity);
                    db.SURVEY_CUESTION.Attach(entity);
                    db.Entry(entity).State = EntityState.Modified;
                }
                db.SaveChanges();
            }

            return Json(entities.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SURVEY_CUESTION_Destroy([DataSourceRequest]DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<SURVEY_CUESTION> survey,int surv)
        {
            var entities = new List<SURVEY_CUESTION>();
            if (ModelState.IsValid)
            {
                foreach (var sURVEY in survey)
                {
                    var entity = new SURVEY_CUESTION
                    {
                        surv_id = surv,
                        surv_cuestion_id = sURVEY.surv_cuestion_id,
                        surv_name_custion = sURVEY.surv_name_custion,
                        surv_type_cuestion = sURVEY.surv_type_cuestion,
                        surv_deleted = "Y",
                    };

                    entities.Add(entity);
                    db.SURVEY_CUESTION.Attach(entity);
                    db.Entry(entity).State = EntityState.Modified;
                    //db.SURVEY.Remove(entity);
                }
                db.SaveChanges();
            }

            return Json(entities.ToDataSourceResult(request, ModelState));
        }
    }
}
