using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeliveryApp.Models
{
    public class TimeZoneMX
    {

        public string GetDateMX()
        {
            TimeZone time2 = TimeZone.CurrentTimeZone;
            DateTime test = time2.ToUniversalTime(DateTime.Now);
            var singapore = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
            var singaporetime = TimeZoneInfo.ConvertTimeFromUtc(test, singapore);
            var date = singaporetime.Date.ToString();
            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
            string[] words = date.Split(delimiterChars);
            var dateIni = words[0];
            return dateIni;
        }

        public string GetHoraMX()
        {
            TimeZone time2 = TimeZone.CurrentTimeZone;
            DateTime test = time2.ToUniversalTime(DateTime.Now);
            var singapore = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
            var singaporetime = TimeZoneInfo.ConvertTimeFromUtc(test, singapore);
            var hora = singaporetime.Hour.ToString() + ":" + singaporetime.Minute.ToString("00.##");
            return hora;
        }

        public DateTime GetFechaMX()
        {
            TimeZone time2 = TimeZone.CurrentTimeZone;
            DateTime test = time2.ToUniversalTime(DateTime.Now);
            var singapore = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
            var singaporetime = TimeZoneInfo.ConvertTimeFromUtc(test, singapore);
            return singaporetime;
        }
    }
}