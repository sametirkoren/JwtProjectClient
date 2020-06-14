using System.Net.Http;
using System.Net.Http.Headers;
using JwtProjectClient.Builders.Concrete;
using JwtProjectClient.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace JwtProjectClient.CustomFilters
{
    public class JwtAuthorizeHelper
    {
        /// <summary>
        /// Active Userın rol bazlı durumunu kontrol eder
        /// </summary>
        /// <param name="activeUser"></param>
        /// <param name="roles"></param>
        /// <param name="context"></param>
        public static void CheckUserRole(AppUser activeUser, string roles, ActionExecutingContext context)
        {
            if (!string.IsNullOrWhiteSpace(roles))
            {
                Status status = null;
                if (roles.Contains(","))
                {
                    StatusBuilderDirector director = new StatusBuilderDirector(new MultiRoleStatusBuilder());
                    status = director.GenerateStatus(activeUser, roles);

                }
                else
                {
                    StatusBuilderDirector director = new StatusBuilderDirector(new SingleRoleStatusBuilder());
                    status = director.GenerateStatus(activeUser, roles);
                }

                CheckStatus(status, context);


            }
        }

        private static void CheckStatus(Status status, ActionExecutingContext context)

        {
            if (!status.AccessStatus)
            {
                context.Result = new RedirectToActionResult("AccessDenied", "Account", null);
            }
        }

        /// <summary>
        /// Response üzerinden Active User'ın bilgilerini getirir.
        /// </summary>
        /// <param name="responseMessage"></param>
        /// <returns></returns>
        public static AppUser GetActiveUser(HttpResponseMessage responseMessage)
        {
            return JsonConvert.DeserializeObject<AppUser>(responseMessage.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// Sessionda  jwt tokenin varlığını kontrol eder.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool CheckToken(ActionExecutingContext context, out string token)
        {
            token = context.HttpContext.Session.GetString("token");
            if (string.IsNullOrWhiteSpace(token))
            {
                return true;
            }
            context.Result = new RedirectToActionResult("SignIn", "Account", null);
            return false;
        }

        /// <summary>
        /// ActiveUser endpoint'ine istek yapar.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static HttpResponseMessage GetActiveUserResponseMessage(string token)
        {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return httpClient.GetAsync("http://localhost:54702/api/Auth/ActiveUser").Result;
        }
    }
}