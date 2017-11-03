using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Http;
using System.Web.UI.WebControls;
using NHibernate.Linq;
using workshopIS.Helpers;
using workshopIS.Models;

namespace workshopIS.Controllers
{
    public class RegistrationController : ApiController
    {
        // GET: api/Registration
        [Route("data/registration/")]
        [HttpGet]
        public List<IPartner> Get()
        {
            return Data.Partners;
        }

        // POST: api/Registration
        [Route("data/registration/post")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]List<CPartner> partners)
        {
            //partner.IsActive = true;
            //partner.ValidFrom = DateTime.Now;
            // check this

            foreach (CPartner partner in partners)
            {
                try
                {
                    Data.SaveToDB(partner);
                }
                catch
                {
                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not write to DB"));
                }
                Data.Partners.Add(partner);
            }
            return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.OK, "úspěšně vloženo"));
        }

        // PUT: api/Registration
        [Route("data/registration/put")]
        [HttpPut]
        public IHttpActionResult Put([FromBody] CPartner partner)
        {
            int pid;
            // update data if partner exists (id)
            if ((pid = Data.Partners.FindIndex(p => p.Id == partner.Id)) >= 0)
                Data.Partners[pid] = partner;
            else
                try
                {
                    Data.Partners.Add(new CPartner(partner));
                }
                catch
                {
                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "špatné parametry"));
                }
            return Content(HttpStatusCode.OK, Data.Partners[Data.Partners.Count - 1].Id);
        }


        // DELETE: api/Registration/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("data/registration/delete")]
        [HttpPut]
        public IHttpActionResult Delete([FromBody]int id)
        {

            try
            {
                ISession session = NHibernateHelper.GetCurrentSession();

                var partner = session.Query<CPartner>().First(x => x.Id == id);
                if (partner == null)
                {
                    return BadRequest("Partner (id) wasn't found!");
                }
                Data.Partners.RemoveAt(Data.Partners.FindIndex(p => p.Id == id));
                //if (partner.IsActive == false)
                //{
                //    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Partner is already inactive"));
                //}
                //ITransaction tx = session.BeginTransaction();
                // DATA.DELETEPARTNERS
                session.Delete(session.Get<CPartner>(id));
                session.Flush();
                //partner.IsActive = false;
                //partner.ValidTo = DateTime.Now;
                //tx.Commit();
                session.Close();
                return Ok("Partner removed");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
