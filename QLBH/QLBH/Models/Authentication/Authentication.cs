using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;

namespace QLBH.Models.Authentication
{
    public class Authentication : ActionFilterAttribute
    {
        private readonly List<string> _roles; 

        public Authentication(params string[] roles) 
        {
            _roles = new List<string>(roles);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var username = context.HttpContext.Session.GetString("TaiKhoan");
            var role = context.HttpContext.Session.GetString("Loai");

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(role))
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        {"Controller", "Access"},
                        {"Action", "Login"}
                    }
                );
                return; 
            }

            if (_roles.Count > 0 && !_roles.Contains(role))
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        {"Controller", "Access"},
                        {"Action", "Login"} // Hoặc có thể chuyển đến trang không có quyền
                    }
                );
            }
        }
    }
}
