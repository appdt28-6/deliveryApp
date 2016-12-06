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
    public class InstallerController : Controller
    {
        private AppDTEntities db = new AppDTEntities();

        public ActionResult Installer()
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

        public ActionResult INSTALLER_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<INSTALLER> installer = db.INSTALLER;
            DataSourceResult result = installer.ToDataSourceResult(request, iNSTALLER => new {
                inst_id = iNSTALLER.inst_id,
                inst_name = iNSTALLER.inst_name,
                inst_phone = iNSTALLER.inst_phone,
                inst_mail = iNSTALLER.inst_mail,
                inst_role = iNSTALLER.inst_role,
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult INSTALLER_Create([DataSourceRequest]DataSourceRequest request, INSTALLER iNSTALLER)
        {
            if (ModelState.IsValid)
            {
                var entity = new INSTALLER
                {
                    inst_name = iNSTALLER.inst_name,
                    inst_phone = iNSTALLER.inst_phone,
                    inst_mail = iNSTALLER.inst_mail,
                    inst_role = iNSTALLER.inst_role,
                };

                db.INSTALLER.Add(entity);
                db.SaveChanges();
                iNSTALLER.inst_id = entity.inst_id;
            }

            return Json(new[] { iNSTALLER }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult INSTALLER_Update([DataSourceRequest]DataSourceRequest request, INSTALLER iNSTALLER)
        {
            if (ModelState.IsValid)
            {
                var entity = new INSTALLER
                {
                    inst_id = iNSTALLER.inst_id,
                    inst_name = iNSTALLER.inst_name,
                    inst_phone = iNSTALLER.inst_phone,
                    inst_mail = iNSTALLER.inst_mail,
                    inst_role = iNSTALLER.inst_role,
                };

                db.INSTALLER.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { iNSTALLER }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult INSTALLER_Destroy([DataSourceRequest]DataSourceRequest request, INSTALLER iNSTALLER)
        {
            if (ModelState.IsValid)
            {
                var entity = new INSTALLER
                {
                    inst_id = iNSTALLER.inst_id,
                    inst_name = iNSTALLER.inst_name,
                    inst_phone = iNSTALLER.inst_phone,
                    inst_mail = iNSTALLER.inst_mail,
                    inst_role = iNSTALLER.inst_role,
                };

                db.INSTALLER.Attach(entity);
                db.INSTALLER.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { iNSTALLER }.ToDataSourceResult(request, ModelState));
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
