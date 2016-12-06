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
    public class ProductController : Controller
    {
        private AppDTEntities db = new AppDTEntities();

        public ActionResult Product()
        {
            return View();
        }

        public ActionResult PRODUCTO_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<PRODUCTO> producto = db.PRODUCTO;
            DataSourceResult result = producto.ToDataSourceResult(request, pRODUCTO => new {
                prod_id = pRODUCTO.prod_id,
                prod_codigo = pRODUCTO.prod_codigo,
                prod_name = pRODUCTO.prod_name,
                prod_unidad = pRODUCTO.prod_unidad
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PRODUCTO_Create([DataSourceRequest]DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<PRODUCTO> producto)
        {
            var entities = new List<PRODUCTO>();
            if (producto != null && ModelState.IsValid)
            {
                foreach(var pRODUCTO in producto)
                {
                    var entity = new PRODUCTO
                    {
                            prod_codigo = pRODUCTO.prod_codigo,
                            prod_name = pRODUCTO.prod_name,
                            prod_unidad = pRODUCTO.prod_unidad
                    };

                    db.PRODUCTO.Add(entity);
                    entities.Add(entity);
                }
                db.SaveChanges();
            }

            return Json(entities.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PRODUCTO_Update([DataSourceRequest]DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<PRODUCTO> producto)
        {
            var entities = new List<PRODUCTO>();
            if (producto != null && ModelState.IsValid)
            {
                foreach(var pRODUCTO in producto)
                {
                    var entity = new PRODUCTO
                    {
                        prod_id = pRODUCTO.prod_id,
                        prod_codigo = pRODUCTO.prod_codigo,
                        prod_name = pRODUCTO.prod_name,
                        prod_unidad = pRODUCTO.prod_unidad
                    };

                    entities.Add(entity);
                    db.PRODUCTO.Attach(entity);
                    db.Entry(entity).State = EntityState.Modified;                        
                }
                db.SaveChanges();
            }

            return Json(entities.ToDataSourceResult(request, ModelState));
        } 

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PRODUCTO_Destroy([DataSourceRequest]DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<PRODUCTO> producto)
        {
            var entities = new List<PRODUCTO>();
            if (ModelState.IsValid)
            {
                foreach(var pRODUCTO in producto)
                {
                    var entity = new PRODUCTO
                    {
                        prod_id = pRODUCTO.prod_id,
                        prod_codigo = pRODUCTO.prod_codigo,
                        prod_name = pRODUCTO.prod_name,
                        prod_unidad = pRODUCTO.prod_unidad
                    };

                    entities.Add(entity);
                    db.PRODUCTO.Attach(entity);
                    db.PRODUCTO.Remove(entity);
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
    }
}
