using Commons;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QTS.Commons
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AuthorizeToken : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                bool temp = false;
                // check token is valid here
                if (!temp)
                {
                    return;
                }
                else
                {
                    context.HttpContext.Response.Headers.Add("AuthStatus", "UnAuthorized");
                    context.HttpContext.Response.StatusCode = Functions.ParseInt(HttpStatusCode.Forbidden);
                    context.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "UnAuthorized";
                    context.Result = new JsonResult("UnAuthorized")
                    {
                        Value = new
                        {
                            MessageCode = MessageCode.Unauthorized,
                            message = "Unauthorized",
                            content = "Unauthorized"
                        },
                    };
                }
            }
            catch (Exception ex)
            {
                context.Result = new JsonResult("UnAuthorized")
                {
                    Value = new
                    {
                        MessageCode = MessageCode.Error,
                        message = Functions.ToString(ex.Message),
                        content = Functions.ToString(ex.Message)
                    },
                };
            }
        }
    }
}
