using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TransportManagement.Models;

namespace TransportManagement.Utilities
{
    public class SystemUtilites
    {
        public static string SendSystemNotification(NotificationType type, string message)
        {
            var userMessage = new MessageVM();
            if (type == NotificationType.Success)
            {
                userMessage = new MessageVM() { CssClassName = "alert alert-success", Title = "Thành công", Message = message };
            }
            else
            {
                userMessage = new MessageVM() { CssClassName = "alert alert-danger", Title = "Không thành công", Message = message};
            }
            
            return JsonConvert.SerializeObject(userMessage);
        }

        public static DateTime ConvertToTimeZone(DateTime utcTime, string idTimeZone)
        {
            TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById(idTimeZone);
            return TimeZoneInfo.ConvertTimeFromUtc(utcTime, cstZone);
        }

        public static DateTime TimeZoneConvertToUTC(DateTime time, string idTimeZone)
        {
            TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById(idTimeZone);
            return TimeZoneInfo.ConvertTimeToUtc(time, cstZone);
        }

        public static double ConvertToTimeStamp(DateTime time)
        {
            DateTime baseDate = new DateTime(1970, 01, 01);
            return time.Subtract(baseDate).TotalSeconds;
        }

        public static DateTime GetDateTimeFromTimeStamp(double timeStamp)
        {
            DateTime baseDate = new DateTime(1970, 01, 01);
            return baseDate.AddSeconds(timeStamp);
        }
    }


    public enum NotificationType
    {
        Success = 1,
        Error = 0
    }
}
