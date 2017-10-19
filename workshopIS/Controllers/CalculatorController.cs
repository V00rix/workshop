﻿using System;
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
        /// <param name="phone"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        [HttpPost]
        public string Post(int partnerId, decimal amount, int duration,
            string phone, string name = "", string surname = "", string email = "", string note = "")
        {
            // Check if partners exist
            IPartner partner;
            try
            {
                partner = Data.Partners[partnerId];
            }
            // If not found
            catch
            {
                // No Partner with such ID
                return "No partner with id " + partnerId + " exists!";
            }

            // Check if customer exists
            ICustomer customer;
            try
            {
                customer = partner.Customers.First(c => c.Phone == phone);
            }
            // if not found
            catch
            {
                // customer with such phone number 
                // is not found on selected partner
                customer = new CCustomer(-1, null, phone);
                partner.Customers.Add(customer);
            }

            customer.Loans.Add(new CLoan(amount, duration));
            return "Success!";
        }

        // simple data test function
        // GET: api/Calculator
        [HttpGet]
        public List<string> Get()
        {
            List<string> partnersAndCustomers = new List<string>();
            foreach (IPartner partner in Data.Partners)
            {
                partnersAndCustomers.Add("[P] " + partner.Name + ": ");
                foreach (ICustomer customer in partner.Customers)
                {
                    partnersAndCustomers.Add("[C] " + customer.FirstName);
                }
            }
            return partnersAndCustomers;
        }
    }
}
