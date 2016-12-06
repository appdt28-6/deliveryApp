using DeliveryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DeliveryApp.Controllers
{
    public class HomeController : Controller
    {
        private AppDTEntities db = new AppDTEntities();
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserModel model, string returnUrl)
        {

            var role_id = IsValidUser(model.UserName, model.Password);
            var user_id = model.UserName;

            if (ModelState.IsValid && Session["role_id"] != null)
            {
                FormsAuthentication.SetAuthCookie(model.UserName, true);
                //return RedirectToLocal(returnUrl);
                return RedirectToAction(actionName: "AssignRoute", controllerName: "AssignRoute", routeValues: new { role_id, user_id });
                //IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
            }
            else
            {
                // Si llegamos a este punto, es que se ha producido un error y volvemos a mostrar el formulario
                ModelState.AddModelError(key: "", errorMessage: "El nombre de usuario o la contraseña especificados son incorrectos o la conexión no se pudo establecer.\n Verifique conexón/Usario y password");
            }

            //if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))            

            return View(model);
        }
        protected string IsValidUser(string username, string password)
        {
            var role_id = "";

            try
            {

                //Creamos la conexion con la cadena especificada en el Context
                using (db)
                {
                    //Recuperamos los datos del SP
                    var user = db.USUARIO.Where(u => u.Usua_Login == username && u.Usua_Password == password);
                    //Recorremos el resultado para validar la informacion
                    foreach (var result in user)
                    {
                        if (result.Usua_nombre != "")
                        {
                            Session["user_id"] = username;
                            Session["Usua_id"] = result.Usua_Id;
                            Session["role_id"] = result.Usua_nombre;
                            Session["comp_identifier"] = result.Usua_Id.ToString();
                            role_id = result.Usua_nombre;
                        }
                    }
                }

            }
            catch (Exception q)
            {
                role_id = null;

            }
            return role_id;
        }

        public ActionResult LogOff()
        {
            Session.Clear();
            return RedirectToAction("Login", "Home");
        }
    }
}
