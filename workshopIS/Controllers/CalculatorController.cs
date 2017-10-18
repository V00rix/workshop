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
            // if (RegistrationController.GetPartner(partnerId))

            // alternative
            if (Data.GetPartners().Select(p => p.Id).Contains(partnerId) && 
                // 2. check if loan is ok
                amount >= 20000 && amount <= 60000 && duration >= 6 && duration <= 96)
            {
                //ICustomer customer;
                //if ((customer = CustomerExists(phoneNumber)) == null)
                //    CreateNewcustomer();

            }
            return Json(partnerId);
        }


        // simple data test function
        // GET: api/Calculator
        [HttpGet]
        public List<string> Get()
        {
            return Data.GetCustomers().Select(
                cus => cus.Name
                ).ToList();
        }



    }
}
