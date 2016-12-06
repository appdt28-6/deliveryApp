
﻿using DeliveryApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeliveryApp.Controllers
{
    public class GetFinVisitaController : Controller
    {
        // GET: SetVisitInstaller
        private AppDTEntities db = new AppDTEntities();
        public JsonResult Index()
        {
            int inst_id = Convert.ToInt32(Request.QueryString["instid"]);
            int visitid = Convert.ToInt32(Request.QueryString["visitid"]);
            int custid = Convert.ToInt32(Request.QueryString["custid"]);
            string lat = Convert.ToString(Request.QueryString["lat"]);
            string lon = Convert.ToString(Request.QueryString["lon"]);
            //TimeZone time2 = TimeZone.CurrentTimeZone;
            //DateTime test = time2.ToUniversalTime(DateTime.Now);
            //var singapore = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
            //var singaporetime = TimeZoneInfo.ConvertTimeFromUtc(test, singapore);
            //TimeZoneMX horamx = new TimeZoneMX();
            //var hora = horamx.GetHoraMX();

            string date = DateTime.Now.ToString("MM/dd/yyyy HH:00");
            char[] delimiterChars = { ' ' };
            string[] words = date.Split(delimiterChars);
            var fecha = words[0];
            var hora = words[1];

            var succes = "";

            using (AppDTEntities objDataContext = new AppDTEntities())
            {
                try
                {
                    VISITA_REGISTRO result = (from p in db.VISITA_REGISTRO
                                              where p.visi_id == visitid
                                              select p).FirstOrDefault();

                    //result.reg_end = singaporetime.Hour.ToString() + ":" + singaporetime.Minute.ToString("00.##");
                    result.reg_end = hora;
                    result.reg_status = 2;

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