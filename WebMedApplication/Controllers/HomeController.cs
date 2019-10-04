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
    public struct SortDate // Структура, описывающая событие с конкретным лекарственным препаратом.
    {
        public DateTime DateTimeItem { get; set; } // Дата и время события.
        public int Count { get; set; } // Кол-во препарата на складе в результате случившегося событтия. 
        public string EventName { get; set; } // Описание события.

    }
    public class HomeController : Controller
    {

        MedDBContext context = new MedDBContext(); // Создание объекта контекста данных.

        public ActionResult Index() // Метод, возвращающий представление главной страницы.
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return View("~/Views/Home/CheckLoginError.cshtml");
            }
            var preparates = context.MEDICAMENT;
            ViewBag.Preparates = preparates; // Отправка в представления списка препаратов из базы данных.
            return View();
        }

        public ActionResult DrawCHart(Guid id) // Метод, возвращающий представление по рисованию графиков остатков конкретного препарата.
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return View("~/Views/Home/CheckLoginError.cshtml");
            }
            List<SortDate> sortList = new List<SortDate>(); // Список, содержащий события с конкретным препаратом.
            bool flag = false; // Флаг, отвечающий за обнаружение событий конкретного препарата.
            string name_p = null;
            foreach (MEDICAMENT p in context.MEDICAMENT)
            {
                if (id == p.ID)
                    name_p = "График динамики остатков препарата '" + p.TRADE_NAME + "' за последний год использования";
                ViewBag.Id = id;
            }
            List<int> Quantities = new List<int>();
            List<string> Incriptions = new List<string>();
            foreach (MEDICAMENT_EVENT s in context.MEDICAMENT_EVENT)
            {
                if (id == s.MEDICAMENT_ID)
                {
                    flag = true;
                    SortDate sd1 = new SortDate();
                    sd1.DateTimeItem = s.EVENT_DATE;
                    sd1.Count = s.QUANTITY;
                    foreach (EVENTS s2 in context.EVENTS)
                    {
                        if (s2.ID == s.EVENT_ID)
                            sd1.EventName = s2.EVENT_NAME;
                    }
                    sortList.Add(sd1);
                }
            }
            sortList.Sort(delegate (SortDate sortdate1, SortDate sortdate2) { return sortdate1.DateTimeItem.CompareTo(sortdate2.DateTimeItem); }); // Сортировка списка событий
            SortDate l = sortList.Last();
            SortDate f = sortList.First();
            foreach (SortDate item in sortList)
            {
                if ((l.DateTimeItem - item.DateTimeItem).TotalDays <= 365)
                {
                    Incriptions.Add(item.EventName + " " + item.DateTimeItem.ToShortDateString());
                    Quantities.Add(item.Count);
                }
            }
            if (flag == false) return View("~/Views/Home/DrawChartNullError.cshtml");
            ViewBag.Counts = Quantities.ToList(); //Отправка отсортированных данных для графиков в представление.
            ViewBag.Texts = Incriptions.ToList(); //Отправка отсортированных данных для графиков в представление.
            name_p += " (Весь период с " + f.DateTimeItem.ToShortDateString() + " по " + l.DateTimeItem.ToShortDateString() + ")";
            ViewBag.Name = name_p;
            return View();
        }


        public ActionResult pickDate() // Метод, возвращающий представление построения графиков за заданный период.
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return View("~/Views/Home/CheckLoginError.cshtml");
            }
            return View();
        }

        [HttpPost]
        public ActionResult pickDate(ValidationDateControl str)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return View("~/Views/Home/CheckLoginError.cshtml");
            }
            if (str.DOB >= str.DOB2) return RedirectToAction("DrawCHart", "Home", new { id = str.PrepID });
            List<SortDate> sortList = new List<SortDate>();
            List<int> counts = new List<int>();
            List<string> texts = new List<string>();
            foreach (MEDICAMENT_EVENT s in context.MEDICAMENT_EVENT)
            {
                if (str.PrepID == s.MEDICAMENT_ID)
                {
                    SortDate sd1 = new SortDate();
                    sd1.DateTimeItem = s.EVENT_DATE;
                    sd1.Count = s.QUANTITY;
                    foreach (EVENTS s2 in context.EVENTS)
                    {
                        if (s2.ID == s.EVENT_ID)
                            sd1.EventName = s2.EVENT_NAME;
                    }
                    sortList.Add(sd1);
                }
            }
            sortList.Sort(delegate (SortDate sortdate1, SortDate sortdate2) { return sortdate1.DateTimeItem.CompareTo(sortdate2.DateTimeItem); });
            ViewBag.WM = sortList.ToList();
            SortDate fe = sortList.First();
            SortDate le = sortList.Last();
            if ((str.DOB < fe.DateTimeItem) || (str.DOB2 > le.DateTimeItem)) return RedirectToAction("DrawCHart", "Home", new { id = str.PrepID });
            foreach (SortDate item in sortList)
            {
                if ((item.DateTimeItem.Date >= str.DOB.Date) && (item.DateTimeItem.Date <= str.DOB2.Date))
                {
                    counts.Add(item.Count);
                    texts.Add(item.EventName + " " + item.DateTimeItem.ToShortDateString());
                }
            }
            string header = "График изменения остатков препарата в период с " + str.DOB.ToShortDateString() + " по " + str.DOB2.ToShortDateString();
            ViewBag.Counts = counts.ToList(); // Данные для построения графика за заданный период.
            ViewBag.Texts = texts.ToList(); // Данные для построения графика за заданный период.
            ViewBag.Header = header;
            return View();

        }
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("SignIn", "Account");
        }
    }
}