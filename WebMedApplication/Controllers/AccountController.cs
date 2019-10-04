namespace WebMedApplication.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;
    using WebMedApplication.Models;
    public class AccountController : Controller
    {
        MedDBContext context = new MedDBContext(); // Создание объекта класса контекста базы данных.
        [HttpGet]
        public ActionResult SignIn() // Get-версия метода, возвращающего в виде представления страницу авторизации.
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(User us) //Post-версия метода авторизации.
        {

            foreach (USERS user in context.USERS) // Перебор всех данных таблицы USERS.
            {
                string str = Encoding.ASCII.GetString(user.PASS);
                if (String.Compare(user.LOG_IN, us.LogIn) == 0 && String.Compare(str, us.Pass) == 0)
                {
                    FormsAuthentication.SetAuthCookie(us.LogIn, true);
                    return RedirectToAction("Index", "Home");
                }

            }
            return RedirectToAction("SignIn", "Account");
        }

    }
}