using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using workshopIS.Models;

namespace workshopIS.Controllers
{
    public class CalculatorController : ApiController
    {
        // Read from DB every time on 
        // every new controller instance
        List<ICustomer> customers = new List<ICustomer>
        {
            new CCustomer()
            {
                Name = "Cus1"
            },
            new CCustomer
            {
                Name = "Cus2"
            }
        };
        List<IPartner> partners = new List<IPartner>
        {
            new CPartner()
            {
                Id = 1,
                Name = "Partner John",
                ICO = 7891
            },
            new CPartner()
            {
                Id = 2,
                Name = "Partner Lawn",
                ICO = 32213
            }
        };

        // POST: api/calculator
        /// <summary>
        /// 
        /// </summary>
        /// <param name="partnerId"></param>
        /// <param name="amount"></param>
        /// <param name="duration"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Post(int partnerId, decimal amount, int duration,
            string phoneNumber, string name="", string email="", string note="")
        {
            // 1. check if partners exist
            // to se tady ma byt RegistrationController.GetPartner(partnerId);
            return Json(partnerId);

        }


        // simple data test function
        // GET: api/Calculator
        [HttpGet]
        public List<string> Get()
        {
            return customers.Select(
                cus => cus.Name
                ).ToList();
        }



    }
}
