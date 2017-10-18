using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using workshopIS.Helpers;
using workshopIS.Models;

namespace workshopIS.Controllers
{
    public class RegistrationController : ApiController
    {
        // GET: api/Registration
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Registration/5
        public string Get(int id)
        {
            return "value";
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
        public void Delete(int id)
        {
        }


        static void GetPartner (int id)
            {
            //ico of partner
            }


    }
}
