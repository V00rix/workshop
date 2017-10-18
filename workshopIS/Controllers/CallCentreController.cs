using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using workshopIS.Repository;

namespace workshopIS.Controllers
{
    public class CallCentreController : ApiController
    {
        public ICallCentreRepository CallCentreRepository { get; set; }

        public string Get()
        {
            return CallCentreRepository.test("nevim");
        }

    }
}
