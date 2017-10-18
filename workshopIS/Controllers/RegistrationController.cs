using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Http;
using NHibernate.Linq;
using workshopIS.Helpers;
using workshopIS.Models;

namespace workshopIS.Controllers
{
    public class RegistrationController : ApiController
    {
        // GET: api/Registration
        public List<CPartner> Get()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            var results = session.Query<CPartner>();

            return results.ToList<CPartner>();
        }


        // POST: api/Registration
        public IHttpActionResult Post([FromBody]CPartner parner)
        {

            ISession session = NHibernateHelper.GetCurrentSession();

            ITransaction tx = session.BeginTransaction();
            session.Save(parner);
            tx.Commit();

            using (var webClient = new WebClient())
            {


            }
            return Ok();
        }

        // PUT: api/Registration/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Registration/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                ISession session = NHibernateHelper.GetCurrentSession();

                ITransaction tx = session.BeginTransaction();
                var partner = session.Query<CPartner>().FirstOrDefault(x => x.Id.Equals(id));
                if (partner == null)
                {
                    HttpResponseMessage result = Request.CreateResponse(HttpStatusCode.BadRequest,
                        "Bad request, partner wasnt deleted");
                    return result;
                }
                else
                {
                    session.Delete(partner);
                    tx.Commit();
                    HttpResponseMessage result = Request.CreateResponse(HttpStatusCode.OK, "Partner deleted");
                    return result;
                }
            }
            catch
            {
                HttpResponseMessage result = Request.CreateResponse(HttpStatusCode.BadRequest);
                return result;

            }

        }


        static void GetPartner (int id)
            {
            //ico of partner
            }


    }
}
