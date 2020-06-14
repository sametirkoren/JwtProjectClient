using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using JwtProjectClient.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace JwtProjectClient.CustomFilters
{
    public class JwtAuthorize : ActionFilterAttribute
    {
        public string Roles {get;set;}

        public override void OnActionExecuting(ActionExecutingContext context){
            var token = context.HttpContext.Session.GetString("token");
            if(string.IsNullOrWhiteSpace(token)){
                context.Result = new RedirectToActionResult("SignIn","Account",null);
            }

            else{
                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);

                var responseMessage = httpClient.GetAsync("http://localhost:54702/api/Auth/ActiveUser").Result;

                if(responseMessage.StatusCode == HttpStatusCode.OK){
                    var activeUser = JsonConvert.DeserializeObject<AppUser> (responseMessage.Content.ReadAsStringAsync().Result);
                    // [JwtAuthorize(Roles="Admin,Member,....")]
                    // [JwtAuthorize(Roles="Admin")]
                    // [JwtAuthorize]
                    if(!string.IsNullOrWhiteSpace(Roles)){
                        bool accessStatus = false ; 
                        if(Roles.Contains(",")){
                            var acceptedRoles =  Roles.Split(',');
                            foreach(var role in acceptedRoles){
                                if(activeUser.Roles.Contains(role)){
                                    accessStatus = true;
                                }
                            }
                        }else{
                            if(activeUser.Roles.Contains(Roles)){
                                accessStatus = true; 
                            }
                        }

                        if(!accessStatus){
                            context.Result = new RedirectToActionResult("AccessDenied","Account",null);
                        }
                    }
                }
                else if(responseMessage.StatusCode == HttpStatusCode.Unauthorized){
                    context.HttpContext.Session.Remove("token");
                    context.Result = new RedirectToActionResult("SignIn","Account",null);
                }

                else{
                    var statusCode = responseMessage.StatusCode.ToString();
                    context.HttpContext.Session.Remove("token");
                    context.Result = new RedirectToActionResult("ApiError","Account",new {code=statusCode});
                }
            }
           
        }
    }
}