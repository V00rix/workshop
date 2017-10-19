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

        public List<CCustomer> Get()
        {
            ISession session = NHibernateHelper.GetCurrentSession();


            var results = session.Query<CCustomer>();

            return results.ToList<CCustomer>();
        }

        public IHttpActionResult Put([FromBody]CCustomer customer)
        {
            ISession session = NHibernateHelper.GetCurrentSession();

            if (customer.Id < 1)
            {
                try
                {
                    ITransaction tx = session.BeginTransaction();
                    session.Query<CCustomer>();
                    tx.Commit();
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
