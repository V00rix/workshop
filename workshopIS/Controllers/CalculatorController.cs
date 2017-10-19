using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using workshopIS.Models;

namespace workshopIS.Controllers
{
    // Controller
    public class CalculatorController : ApiController
    {
        // POST: api/calculator
        /// <summary>
        /// Create new customer from post
        /// </summary>
        /// <param name="loanData">Requested data</param>
        /// <returns>Status</returns>
        public IHttpActionResult Post([FromBody]LoanData loanData)
        {
            CCustomer customer;
            CPartner partner;
            CLoan loan;

            if (loanData.partnerId == null)
                return BadRequest("Partner's ID was empty!");

            // Check if partner exists
            if ((partner = (CPartner)Data.Partners.Find(p => p.Id == loanData.partnerId))
                    == null)
                return BadRequest("Could not find selected partner!");

            // Check if Customer already exists
            if ((customer = (CCustomer)partner.Customers
            .Find(c => c.Phone == loanData.phone
                    && c.FirstName == loanData.firstName
                    && c.Surname == loanData.surname)) == null)
            {
                // if Customer doesn't exist
                // Create new Customer... 
                customer = new CCustomer(loanData.partnerId ?? 0, loanData.phone, 
                    loanData.firstName, loanData.surname, loanData.email);
                // ... and customer to partner
                partner.AddCustomer(customer);
            }
            // ...and new Loan
            loan = new CLoan(customer.Id, loanData.amount, loanData.duration, loanData.note);
            // add to customer
            customer.AddLoan(loan);

            // return status
            return Ok();
        }

        // GET: api/calculator
        /// <summary>
        /// Simple GET request to display all customers throughout all partners
        /// </summary>
        /// <returns>List of list of customers.</returns>
        public List<IPartner> Get()
        {
            return Data.Partners.ToList();
        }
    }

    // Post message class
    public class LoanData
    {
        public int? partnerId;
        public string phone;
        public decimal amount;
        public int duration;
        public string firstName = null;
        public string surname = null;
        public string email = null;
        public string note = null;
    }
}
