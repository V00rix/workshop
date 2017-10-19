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

            IList<CLoan> test = session.QueryOver<CLoan>()
                        .JoinQueryOver(l => l.Customer)
                        .JoinQueryOver(l => l.Partner).List();
            var xlsda = test.Select(x => new { loan = x, customer = x.Customer });
            var loanByPartner = test.GroupBy(t => t.Customer.Partner).Select(t => new { Partner = t.Key.Name, Cnt = t.Count() });

            return Request.CreateResponse(HttpStatusCode.OK, xlsda);
        }

        public IHttpActionResult Put([FromBody]CCustomer customer)
        {
            ISession session = NHibernateHelper.GetCurrentSession();

            if (customer.Id > 0) //verificate ID from put
            {
                try
                {
                    var selectedCustomer = session.QueryOver<CPartner>()
                        .Where(p => p.Id == customer.Id);
                }
                catch (Exception)
                {
                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "chyba při komunikaci s databází"));
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

            try
            {
                ITransaction tx = session.BeginTransaction();
                session.Update(customer);
                tx.Commit();
            }
            catch
            {
                //throw new Exception("{0} Exception caught.", e);
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "transakce neproběhla správně"));
            }
            finally
            {
                session.Close();
            }
            return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.OK, "změna proběhla v pořádku"));
        }

    }
}
