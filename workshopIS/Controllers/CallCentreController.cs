using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using workshopIS.Helpers;
using workshopIS.Models;
using workshopIS.Repository;

namespace workshopIS.Controllers
{
    public class CallCentreController : ApiController
    {
        public ICallCentreRepository CallCentreRepository { get; set; }

        /// <summary>
        /// Get all loans with joined custumers and parners
        /// </summary>
        /// <returns>HTTP response OK and Ilist of loans</returns>
        public HttpResponseMessage Get()
        {
            //uncoment when you need date in revert format and delete [JsonIgnore] from CCustomer
            //ISession session = NHibernateHelper.GetCurrentSession(); //open or get seasion

            //IList<CLoan> query = session.QueryOver<CLoan>() //select from LOAN
            //            .JoinQueryOver(l => l.Customer) //join CUSTOMER
            //            .JoinQueryOver(l => l.Partner) //join PARTNER
            //            .List();
            //var results = query.Select(x => new { loan = x, customer = x.Customer });
            //session.Close();

            // and comment this out
            Data.ReadDataFromDatabase(); //read data from DB
            List<IPartner> results = Data.Partners;
            //
            return Request.CreateResponse(HttpStatusCode.OK, results);
        }

        /// <summary>
        /// Update Customer Contact State
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="ContactState">Contact State from 1 to 3</param>
        /// <returns>HttpStatusCode</returns>
        public IHttpActionResult Put([FromBody] PutCustomer customer)
        {
            if (customer.Id > 0 && customer.ContactState > 0 && customer.ContactState < 4) //verificate input data
            {
                ISession session = NHibernateHelper.GetCurrentSession();
                try
                {
                    ITransaction tx = session.BeginTransaction();

                    CCustomer customerToUpdate = session.Query<CCustomer>() //select current customer from DB
                        .Where(p => p.Id == customer.Id)
                        .FirstOrDefault();

                    customerToUpdate.ContactState =
                        customer.ContactState; //set ContactState from PUT entity to DB entity

                    session.Update(customerToUpdate); //update customer

                    tx.Commit();

                }
                catch (Exception e)
                {
                    //throw new Exception("",e);
                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "špatné parametry"));
                }
                finally
                {
                    session.Close();
                }
            }
            else
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "neplatné parametry"));

            }
            return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.OK, "změna proběhla v pořádku"));
        }

        // POST: api/Registration
        [Route("data/callcentre/state")]
        [HttpPost]
        public IHttpActionResult Put([FromBody] StateData stateData)
        {
            // validation
            if (stateData.state < 0 || stateData.state > 4)
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "State is out of boundaries!"));

            int pid;
            int cid;
            if ((pid = Data.Partners.FindIndex(p => p.Id == stateData.pid)) < 0)
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No partner found with such ID!"));
            if ((cid = Data.Partners[pid].Customers.FindIndex(c => c.Id == stateData.cid)) < 0)
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "No customer found with such ID!"));

            // all checks passed - update customer's state
            Data.UpdateState(Data.Partners[pid].Customers[cid], stateData.state);
            Data.CloseSession();

            return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.OK, 
                string.Format("Set '{1}'\'s '{0}'\'s contact state to '{2}'", 
                Data.Partners[pid].Customers[cid].FirstName + " " + Data.Partners[pid].Customers[cid].Surname, 
                Data.Partners[pid].Name,
                Data.Partners[pid].Customers[cid].ContactState)));
        }
    }

    public class StateData
    {
        public int pid;
        public int cid;
        public int state;
    }
}