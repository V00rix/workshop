﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

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
        public void Post([FromBody]string value)
        {

            string url = "http://";

            using (var webClient = new WebClient())
            {

               





            }
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
