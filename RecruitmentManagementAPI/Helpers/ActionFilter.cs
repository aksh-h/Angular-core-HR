using Microsoft.AspNetCore.Mvc.Filters;
using RecruitmentManagementDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace RecruitmentManagementAPI.Controllers
{
    public class ActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            CurrentContext.EmployeeID = Convert.ToInt32(context.HttpContext.Request.Headers["UserID"]);
            CurrentContext.RoleName = Convert.ToString(context.HttpContext.Request.Headers["Role"]);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
           
        }
    }
}
