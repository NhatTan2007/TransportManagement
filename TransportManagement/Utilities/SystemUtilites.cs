using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
    }

    public enum NotificationType
    {
        Success = 1,
        Error = 0
    }
}
