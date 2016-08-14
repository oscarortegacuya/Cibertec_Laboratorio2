using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebDeveloper.Filters
{
    public class AuditControl: ActionFilterAttribute
    {
        private static ILog Log { get; set; }

        ILog log = LogManager.GetLogger
            (
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
            );
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Message(filterContext, "OnActionExecuted");
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        =>
            Message(filterContext, "OnResultExecuted");

        private void Message(ControllerContext filterContext, string filterMethod)
        {
            var controller = filterContext.RouteData.Values["controller"].ToString();
            var action = filterContext.RouteData.Values["action"].ToString();
            log.Info($"{controller} in action {action} on {filterMethod}");
        }
    }
}