using DeliveryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeliveryApp.Controllers
{
    public class GetSurveyAnswareController : Controller
    {
        // GET: GetSurveyAnsware
        private AppDTEntities db = new AppDTEntities();
        public ActionResult Index()
        {
            int surv = Convert.ToInt32(Request.QueryString["surv"]);
            int cues1 = Convert.ToInt32(Request.QueryString["c1"]);
            int res1= Convert.ToInt32(Request.QueryString["r1"]);
            int cues2 = Convert.ToInt32(Request.QueryString["c2"]);
            int res2 = Convert.ToInt32(Request.QueryString["r2"]);
            string obs = Convert.ToString(Request.QueryString["obs"]);
            int cust = Convert.ToInt32(Request.QueryString["custid"]); ;
            int visit = Convert.ToInt32(Request.QueryString["visitid"]);

            string date = DateTime.Now.ToString("MM/dd/yyyy HH:00");
            char[] delimiterChars = { ' ' };
            string[] words = date.Split(delimiterChars);
            var hora = words[1];

            var succes = "";

            using (AppDTEntities objDataContext = new AppDTEntities())
            {
                try
                {
                    SURVEY_CUESTION_OP objEmp = new SURVEY_CUESTION_OP();
                    // fields to be insert
                    objEmp.surv_id = surv;
                    objEmp.surv_c1 = cues1;
                    objEmp.surv_r1= res1;
                    objEmp.surv_c2 = cues2;
                    objEmp.surv_r2 = res2;
                    objEmp.surv_txt = obs;
                    objEmp.cust_id = cust;
                    objEmp.visi_id = visit;
                    db.SURVEY_CUESTION_OP.Add(objEmp);
                    db.SaveChanges();

                    VISITA_REGISTRO result = (from p in db.VISITA_REGISTRO
                                              where p.visi_id == visit
                                              select p).FirstOrDefault();

                    //result.reg_end = singaporetime.Hour.ToString() + ":" + singaporetime.Minute.ToString("00.##");
                    result.reg_end = hora;
                    result.reg_status = 2;

                    db.SaveChanges();

                    VISITA_ASSIGN result2 = (from p in db.VISITA_ASSIGN
                                             where p.visi_id == visit
                                              select p).FirstOrDefault();

                    //result.reg_end = singaporetime.Hour.ToString() + ":" + singaporetime.Minute.ToString("00.##");
                    result2.visi_status = 1;
                    

                    db.SaveChanges();

                    succes = "Ok";
                }
                catch (Exception e)
                {
                    succes = "NoOk";
                }
            }

            return Json(new { succes }, JsonRequestBehavior.AllowGet);
        }
    }
}