//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Facebook;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.Extensions.Configuration;
//using System.IO;
//using IS220.O11.HTCL.Models;
//using System.Text.Json;
//using Microsoft.AspNetCore.Http;

//namespace WebApplication.Controllers
//{
//    public class FacebookController : Controller
//    {
//        string appid = string.Empty;
//        string appsecret = string.Empty;

//        public FacebookController()
//        {
//            //var configuration = GetConfiguration();
//            appid = "678518867456921";
//            appsecret = "c83b94a3bba48d441d700ecd5c6766c6";
//        }

//        private Uri RedirectUri
//        {
//            get
//            {
//                var uriBuilder = new UriBuilder(Request.Headers["Referer"].ToString());
//                uriBuilder.Query = null;
//                uriBuilder.Fragment = null;
//                uriBuilder.Path = Url.Action("FacebookCallback");
//                return uriBuilder.Uri;
//            }
//        }

//        public IActionResult Facebook()
//        {
//            var fb = new FacebookClient();
//            var loginurl = fb.GetLoginUrl(
//                new
//                {
//                    client_id = appid,
//                    client_secret = appsecret,
//                    redirect_uri = RedirectUri.AbsoluteUri,
//                    response_type= "code",
//                    scope="email",
//                });
//            return Redirect(loginurl.AbsoluteUri);
//        }


//        public IActionResult FacebookCallback(string code)
//        {
//            var fb = new FacebookClient();

//            dynamic result = fb.Post("oauth/access_token", new { 
//                client_id = appid,
//                client_secret = appsecret,
//                redirect_uri = RedirectUri.AbsoluteUri,
//                code = code
//            });

//            var accesstoken = result.access_token;
//            fb.AccessToken = accesstoken;
//            StoreContext context = HttpContext.RequestServices.GetService(typeof(WebApplication.Models.StoreContext)) as StoreContext;
//            dynamic data = fb.Get("me?fields=id,link,first_name,currency,last_name,email,gender,locale,timezone,verified,picture,age_range");
//            if(data.gender==null)
//            {
//                Console.WriteLine(data);
//                data.gender = "Chưa cập nhật";
//            }
//            client_accounts res = context.CheckLoginFacebook(Convert.ToString(data.id), Convert.ToString(data.first_name), Convert.ToString(data.last_name), Convert.ToString(data.email), Convert.ToString(data.gender));
//            if (res != null)
//            {
//                ViewBag.status = "Success";
//                ViewBag.infor = res;
//                ViewBag.avatar = Convert.ToString(data.picture.data.url);
//                string temp= Convert.ToString(data.picture.data.url);
//                HttpContext.Session.SetString("Avatar", temp);
//                HttpContext.Session.SetString("UserSession", JsonSerializer.Serialize(res));
//            }
//            else
//            {
//                ViewBag.status = "Fail";
//            }
//            return Redirect("/Home/Index");
//        }

//        public IActionResult Index()
//        {
//            return View();
//        }

//        public IConfiguration GetConfiguration()
//        {
//            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsetings.json", optional: true, reloadOnChange: true);
//            return builder.Build();
//        }


//    }
//}
