using WebVPP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

    namespace WebVPP.Controllers
    {
        public class BaseController : Controller
        {
            public object CurrentUser
            {
                get
                {
                    var userObject = HttpContext.Session.GetString("UserName");
                    if (!string.IsNullOrEmpty(userObject))
                    {
                        return JsonSerializer.Deserialize<Account>(userObject);
                    }
                    return null;
                }
                set
                {
                    HttpContext.Session.SetString("UserName", JsonSerializer.Serialize(value));
                }
            }
            public bool IsLogin
            {
                get
                {
                    return CurrentUser != null;
                }
            }

            public override void OnActionExecuted(ActionExecutedContext context)
            {
                base.OnActionExecuted(context);
            }
        }

    }
