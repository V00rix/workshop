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

        public HttpResponseMessage Get()
        {
            return Data.GetCallCentreResults();
        }

        public IHttpActionResult Put([FromBody]PutCustomer customer)
        {
            ISession session = NHibernateHelper.GetCurrentSession();

            if (customer.Id > 0) //verificate ID from put
            {
                try
                {

                    ITransaction tx = session.BeginTransaction();

                    CCustomer customerToUpdate = session.Query<CCustomer>()
                        .Where(p => p.Id == customer.Id).FirstOrDefault();

                    customerToUpdate.ContactState = customer.ContactState;

                    session.Update(customerToUpdate);

                    tx.Commit();

                }
                catch (Exception e)
                {
                    throw new Exception("",e);
                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "špatné parametry"));
                }
                finally
                {
                    session.Close();
                }
            }
            else
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "neplatně zadané id"));

            }
            return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.OK, "změna proběhla v pořádku"));



        }
    }
}
