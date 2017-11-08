using NHibernate;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using NHibernate.Linq;
using ServiceStack;
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
            try
            {
                // Data.UpdatePartners(partners);
            }
            catch
            {
                Data.CloseSession();
                return BadRequest("Could not write to DB");
            }
            Data.CloseSession();
            return Ok("úspěšně vloženo");
        }

        // PUT: api/Registration
        [Route("data/registration/put")]
        [HttpPut]
        public IHttpActionResult Put([FromBody] CPartner partner)
        {
            int pid;
            // update data if partner exists (id) 
            if ((pid = Data.Partners.FindIndex(p => p.Id == partner.Id)) >= 0)
                Data.UpdatePartner(pid, partner);
            else
                try
                {
                    Data.Partners.Add(new CPartner(partner));
                }
                catch
                {
                    Data.CloseSession();
                    return BadRequest("špatné parametry");
                }
            Data.CloseSession();
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

            int pid;
            // update data if partner exists (id) 
            if ((pid = Data.Partners.FindIndex(p => p.Id == id)) >= 0)
                try
                {
                    Data.RemovePartner(pid);
                    Data.CloseSession();
                    return Ok("Partner removed");
                }
                catch (Exception e)
                {
                    Data.CloseSession();
                    return BadRequest(e.Message);
                }

            return BadRequest("No partner found with such index!");
        }

        [Route("data/registration/file")]
        [HttpPost]
        public async Task<HttpResponseMessage> Post()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = new MultipartMemoryStreamProvider();

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);
                
                byte[] fileBytes = await provider.Contents[0].ReadAsByteArrayAsync();
                string fname = provider.Contents[0].Headers.ContentDisposition.FileName.Trim('\"');
                try
                {
                    int pid = int.Parse(await provider.Contents[1].ReadAsStringAsync());
                    if ((pid = Data.Partners.FindIndex(p => p.Id == pid)) != -1)
                        Data.AppendFile(fileBytes, pid);
                    else 
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "Partner with such id wasn't found!");
                }
                catch (Exception e)
                {
                    Data.AppendFile(fileBytes);
                }
                return Request.CreateResponse(HttpStatusCode.OK, "Successfully added. File name:" + fname);
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}
