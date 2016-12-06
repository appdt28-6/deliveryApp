using DeliveryApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace DeliveryApp.Controllers
{
    public class SetVisitInstallerController : Controller
    {
        // GET: SetVisitInstaller
        private AppDTEntities db = new AppDTEntities();
        public JsonResult Index()
        {
           
            int valor = Convert.ToInt32(Request.QueryString["inst_id"]);
            //TimeZoneMX horamx = new TimeZoneMX();
            //var fechac = horamx.GetFechaMX();
            string date = DateTime.Now.Date.ToString("MM/dd/yyyy");
            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
            string[] words = date.Split(delimiterChars);
            var dateIni = words[0] + " 00:00 AM";
            var dateEnd = words[0] + " 23:59 PM";
    
            var visitas = db.vis_VISITA_CUSTOMER.Where(v => String.Compare(v.visi_date,dateIni) > 0 && String.Compare(v.visi_date, dateEnd) < 0&&v.inst_id==valor);
            //var visitas = db.vis_VISITA_CUSTOMER;
            return Json(visitas, JsonRequestBehavior.AllowGet);
        }

       
    }
}