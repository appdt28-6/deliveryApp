using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeliveryApp.Models;

namespace DeliveryApp.Controllers
{
    public class GetNovisitReasonController : Controller
    {
        // GET: SetVisitInstaller
        public ActionResult Index()
        {
            string inst_id = Convert.ToString(Request.QueryString["instid"]);
            int visitid = Convert.ToInt32(Request.QueryString["visitid"]);
            int custid = Convert.ToInt32(Request.QueryString["custid"]);
            string lat = Convert.ToString(Request.QueryString["lat"]);
            string lon = Convert.ToString(Request.QueryString["lon"]);
            string reason= Convert.ToString(Request.QueryString["reason"]);
            //TimeZoneMX horamx = new TimeZoneMX();
            //var hora = horamx.GetHoraMX();
            //var fecha = horamx.GetDateMX();
            //var fechac= horamx.GetFechaMX();
            string date = DateTime.Now.ToString("MM/dd/yyyy HH:00");
            char[] delimiterChars = { ' ' };
            string[] words = date.Split(delimiterChars);
            var fecha = words[0];
            var hora = words[1];

            var succes = "";

            using (AppDTEntities objDataContext = new AppDTEntities())
            {
                try { 
                    NOTVISIT nOTVISIT = new NOTVISIT();
                    // fields to be insert
                    nOTVISIT.inst_id = inst_id;
                    nOTVISIT.vist_id = visitid;
                    nOTVISIT.novi_date = DateTime.Parse(fecha); ;
                    nOTVISIT.novi_reazon =reason;
                    objDataContext.NOTVISIT.Add(nOTVISIT);
                    ///////////////////
                    VISITA_REGISTRO vISITA = new VISITA_REGISTRO();
                    // fields to be insert
                    vISITA.inst_id = int.Parse(inst_id);
                    vISITA.reg_lat = lat;
                    vISITA.reg_lon = lon;
                    vISITA.cust_id = custid;
                    vISITA.reg_date = fecha;
                    vISITA.reg_ini = hora;
                    vISITA.reg_end = hora;
                    vISITA.visi_id = visitid;
                    vISITA.reg_status = 3;
                    objDataContext.VISITA_REGISTRO.Add(vISITA);

                    objDataContext.SaveChanges();

                    VISITA_ASSIGN result2 = (from p in objDataContext.VISITA_ASSIGN
                                             where p.visi_id == visitid
                                             select p).FirstOrDefault();

                    //result.reg_end = singaporetime.Hour.ToString() + ":" + singaporetime.Minute.ToString("00.##");
                    result2.visi_status = 3;


                    objDataContext.SaveChanges();

                    succes = "Ok";
                }catch(Exception e)
                {
                    succes = "NoOk";
                }
            }
            
            return Json(new { succes }, JsonRequestBehavior.AllowGet);
        }
    }
}