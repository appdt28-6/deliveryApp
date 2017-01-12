using DeliveryApp.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterimobelWS.Controllers
{
    public class SrcVisitasController : Controller
    {
        // GET: SrcVisitas
        private AppDTEntities db = new AppDTEntities();
        public ActionResult SrcVisitas()
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
       

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult VISITA_ASSIGN_Read([DataSourceRequest]DataSourceRequest request, string F1, string F2)
        {
            string date1 = "";
            string date2 = "";
            if (F1 != null && F2 != null){ 
            Session["f1"] = F1;
            Session["f2"] = F2;
            }
            else
            {
                date1=(Session["f1"].ToString()==null)?"": Session["f1"].ToString();
                date2=(Session["f2"].ToString() == null)?"": Session["f2"].ToString();
            }

            IQueryable<sp_Busqueda_Visitas_Assign_Result> visita_assign = db.sp_Busqueda_Visitas_Assign(date1, date2).ToList().AsQueryable();
            DataSourceResult result = visita_assign.ToDataSourceResult(request, vISITA_ASSIGN => new {
                visi_id = vISITA_ASSIGN.visi_id,
                cust_name = vISITA_ASSIGN.cust_name,
                inst_name = vISITA_ASSIGN.inst_name,
                visi_date = vISITA_ASSIGN.visi_date,
                visi_op = vISITA_ASSIGN.visi_op,
                visi_status = vISITA_ASSIGN.visi_status
            });

            return Json(result);
        }
    }
}