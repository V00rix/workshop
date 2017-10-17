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

        // Same for Partners, etc

        [HttpPost]
        public void Post()
        {
            // localhost:PORT/api/calculator
            // TODO
        }


        // simple data test function
        [HttpGet]
        public List<string> Get()
        {
            return customers.Select(
                cus => cus.GetName()
                ).ToList();
        }



    }
}
