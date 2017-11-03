using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace workshopIS.Controllers
{
    public class MainController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Loan()
        {
            return View("~/Views/Main/Loan/Loan.cshtml");
        }

        public ActionResult Partners()
        {
            return View("~/Views/Main/Partners/Partners.cshtml");
        }


        public ActionResult NotFound()
        {
            return View("~/Views/Main/NotFound.cshtml");
        }

    }
}