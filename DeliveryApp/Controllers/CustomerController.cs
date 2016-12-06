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
using System.IO;
using GoogleMaps.LocationServices;

namespace DeliveryApp.Controllers
{
    public class CustomerController : Controller
    {
        private AppDTEntities db = new AppDTEntities();

        public ActionResult Customer()
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

        public ActionResult CUSTOMER_Read([DataSourceRequest]DataSourceRequest request)
        {

            IQueryable<CUSTOMER> customer = db.CUSTOMER;
            DataSourceResult result = customer.ToDataSourceResult(request, cUSTOMER => new {
                cust_id = cUSTOMER.cust_id,
                cust_name = cUSTOMER.cust_name,
                cust_phone = cUSTOMER.cust_phone,
                cust_mail = cUSTOMER.cust_mail,
                cust_dir = cUSTOMER.cust_dir,
                cust_longitud = cUSTOMER.cust_longitud,
                cust_latitud = cUSTOMER.cust_latitud
            });

            //return Json(result);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CUSTOMER_Create([DataSourceRequest]DataSourceRequest request, CUSTOMER cUSTOMER)
        {

            if (ModelState.IsValid)
            {
                //AIzaSyCiiCLOp5oABnCYHuTAOplcwGJ7MfZFFnA

                var address = cUSTOMER.cust_dir;

                var locationService = new GoogleLocationService();
                var point = locationService.GetLatLongFromAddress(address);

                string latitude =Convert.ToString(point.Latitude);
                string longitude = Convert.ToString(point.Longitude);

                var entity = new CUSTOMER
                {
                    cust_id = cUSTOMER.cust_id,
                    cust_name = cUSTOMER.cust_name,
                    cust_phone = cUSTOMER.cust_phone,
                    cust_mail = cUSTOMER.cust_mail,
                    cust_dir = cUSTOMER.cust_dir,
                    cust_longitud = longitude,
                    cust_latitud = latitude
                };

                db.CUSTOMER.Add(entity);
                db.SaveChanges();
                cUSTOMER.cust_id = entity.cust_id;
            }

            return Json(new[] { cUSTOMER }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CUSTOMER_Update([DataSourceRequest]DataSourceRequest request, CUSTOMER cUSTOMER)
        {
            if (ModelState.IsValid)
            {
                //AIzaSyCiiCLOp5oABnCYHuTAOplcwGJ7MfZFFnA

                var address = cUSTOMER.cust_dir;

                var locationService = new GoogleLocationService();
                var point = locationService.GetLatLongFromAddress(address);

                string latitude = Convert.ToString(point.Latitude);
                string longitude = Convert.ToString(point.Longitude);

                var entity = new CUSTOMER
                {
                    cust_id = cUSTOMER.cust_id,
                    cust_name = cUSTOMER.cust_name,
                    cust_phone = cUSTOMER.cust_phone,
                    cust_mail = cUSTOMER.cust_mail,
                    cust_dir = cUSTOMER.cust_dir,
                    cust_longitud = longitude,
                    cust_latitud = latitude
                };

                db.CUSTOMER.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { cUSTOMER }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CUSTOMER_Destroy([DataSourceRequest]DataSourceRequest request, CUSTOMER cUSTOMER)
        {
            if (ModelState.IsValid)
            {
                var address = cUSTOMER.cust_dir;

                var locationService = new GoogleLocationService();
                var point = locationService.GetLatLongFromAddress(address);

                string latitude = Convert.ToString(point.Latitude);
                string longitude = Convert.ToString(point.Longitude);

                var entity = new CUSTOMER
                {
                    cust_id = cUSTOMER.cust_id,
                    cust_name = cUSTOMER.cust_name,
                    cust_phone = cUSTOMER.cust_phone,
                    cust_mail = cUSTOMER.cust_mail,
                    cust_dir = cUSTOMER.cust_dir,
                    cust_longitud = longitude,
                    cust_latitud = latitude
                };

                db.CUSTOMER.Attach(entity);
                db.CUSTOMER.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { cUSTOMER }.ToDataSourceResult(request, ModelState));
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
