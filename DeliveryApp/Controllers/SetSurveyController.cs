using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeliveryApp.Models;

namespace DeliveryApp.Controllers
{
    public class SetSurveyController : Controller
    {
        // GET: SetVisitInstaller
        private AppDTEntities db = new AppDTEntities();
        public JsonResult Index()
        {
            int valor = Convert.ToInt32(Request.QueryString["inst_id"]);

            var visitas = db.vis_survey;

            return Json(visitas, JsonRequestBehavior.AllowGet);
        }
    }
}