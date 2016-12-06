using DeliveryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeliveryApp.Controllers
{
    public class SetLoginInstallerController : Controller
    {
        // GET: SetLoginInstaller
        private AppDTEntities db = new AppDTEntities();
        public JsonResult Index()
        {
            string user = Convert.ToString(Request.QueryString["user"]);
            string pass = Convert.ToString(Request.QueryString["pass"]);

            var login = db.INSTALLER.Where(inst => inst.inst_name==user && inst.inst_phone==pass).ToList();

           return Json(login, JsonRequestBehavior.AllowGet);
           
           
        }
    }
}