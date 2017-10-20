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
        public List<IPartner> Get()
        {
            /* this should be done in Data.Read
            ISession session = NHibernateHelper.GetCurrentSession();
            var results = session.Query<CPartner>();

            return results.ToList<CPartner>(); */

            return Data.Partners;
        }


        // POST: api/Registration
        public IHttpActionResult Post([FromBody]CPartner partner)
        {
            partner.IsActive = true;
            partner.ValidFrom = DateTime.Now;
            // check this
            try
            {
                Data.SaveToDB(partner);
            }
            catch 
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "špatné parametry"));

            }

            return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.OK, "úspěšně vloženo"));

        }

        //public IHttpActionResult Post([FromBody]int ICO)
        //{
        //    return Ok();
        //}



        // PUT: api/Registration/5
        public void Put(int id, [FromBody]string value)
        {
            // upade existing Partner
            // Data.Update(); - not yet implemented
        }

        // DELETE: api/Registration/5
        public HttpResponseMessage Delete(int id)
        {
            // that should also be done in Data.DeletePartner(); - not yet implemented
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


        static void GetPartner(int id)
        {
            //ico of partner
        }


    }
}
