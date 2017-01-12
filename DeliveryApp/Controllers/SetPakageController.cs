using DeliveryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeliveryApp.Controllers
{
    public class SetPakageController : Controller
    {
        // GET: SetPakage
        private AppDTEntities db = new AppDTEntities();
        public JsonResult Index()
        {
            int valor = Convert.ToInt32(Request.QueryString["Paq"]);
            var paquete = db.vis_PAQUETE_DETALLE.Where(a => a.paqu_id == valor).ToList();
            return Json(paquete, JsonRequestBehavior.AllowGet);
        }
    }
}