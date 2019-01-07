using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PharmAssistant.Filters
{
    public class ActionLoggingFilter: ActionFilterAttribute
    {
        private string _actionName, _controllerName, _userName;
        private DateTime _timeStamp;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _actionName = filterContext.ActionDescriptor.ActionName;
            _controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            _userName = filterContext.HttpContext.User.Identity.Name;
            _timeStamp = DateTime.Now;
                        
            Debug.WriteLine("Action Started: ");
            Debug.WriteLine("User: " + _userName + ", Controller Name: " + _controllerName + ", Aciton: " + _actionName + ", Timestamp: " + _timeStamp.ToString("dd-MM-yyyy hh:mm:ss.fff tt"));            
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _actionName = filterContext.ActionDescriptor.ActionName;
            _controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            _userName = filterContext.HttpContext.User.Identity.Name;
            _timeStamp = DateTime.Now;
                        
            Debug.WriteLine("Action Executed: ");
            Debug.WriteLine("User: " + _userName + ", Controller Name: " + _controllerName + ", Aciton: " + _actionName + ", Timestamp: " + _timeStamp.ToString("dd-MM-yyyy hh:mm:ss.fff tt"));            
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            _actionName = filterContext.Result.ToString();
            _controllerName = filterContext.Controller.ToString();
            _userName = filterContext.HttpContext.User.Identity.Name;
            _timeStamp = DateTime.Now;

            Debug.WriteLine("Result Executing: ");
            Debug.WriteLine("User: " + _userName + ", Controller Name: " + _controllerName + ", Aciton: " + _actionName + ", Timestamp: " + _timeStamp.ToString("dd-MM-yyyy hh:mm:ss.fff tt"));
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            _actionName = filterContext.Result.ToString();
            _controllerName = filterContext.Controller.ToString();
            _userName = filterContext.HttpContext.User.Identity.Name;
            _timeStamp = DateTime.Now;

            Debug.WriteLine("Result Executed: ");
            Debug.WriteLine("User: " + _userName + ", Controller Name: " + _controllerName + ", Aciton: " + _actionName + ", Timestamp: " + _timeStamp.ToString("dd-MM-yyyy hh:mm:ss.fff tt"));
        }
    }
}