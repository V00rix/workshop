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
            Data.Partners.Add(partner);
            return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.OK, "úspěšně vloženo"));
        }


        // DELETE: api/Registration/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                ISession session = NHibernateHelper.GetCurrentSession();

                var partner = session.Query<CPartner>().FirstOrDefault(x => x.Id == id);
                if (partner == null)
                {
                    HttpResponseMessage result = Request.CreateResponse(HttpStatusCode.BadRequest,
                        "Bad request, partner wasnt found");
                    return result;
                }
                else if (partner.IsActive == false)
                {
                    HttpResponseMessage result = Request.CreateResponse(HttpStatusCode.BadRequest,
                        "Bad request, partner is already innactive");
                    return result;
                }
                else
                {
                    ITransaction tx = session.BeginTransaction();
                    partner.IsActive = false;
                    partner.ValidTo = DateTime.Now;
                    tx.Commit();
                    HttpResponseMessage result = Request.CreateResponse(HttpStatusCode.OK, "Partner moved to inactive state");
                    return result;

                }
            }
            catch
            {
                HttpResponseMessage result = Request.CreateResponse(HttpStatusCode.BadRequest);
                return result;

            }
        }
    }
}
