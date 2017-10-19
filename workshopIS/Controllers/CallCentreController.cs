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
        
            ISession session = NHibernateHelper.GetCurrentSession();
            /*
            var results = session.QueryOver<CCustomer>()
                .Fetch(t => t.Partner).Eager
                .List();

            var result2 = results.GroupBy(c => c.Partner).Select(c => new { Partner = c.Key, Cnt = c.Count() });
            session.Close();*/
            //var result = session.QueryOver<CLoan>()
            //    .Fetch(t => t.Customer).Eager
            //    .List();

            IList<CLoan> query = session.QueryOver<CLoan>()
                        .JoinQueryOver(l => l.Customer)
                        .JoinQueryOver(l => l.Partner).List();
            var result = query.Select(x => new { loan = x, customer = x.Customer });


            return Request.CreateResponse(HttpStatusCode.OK, result);
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
