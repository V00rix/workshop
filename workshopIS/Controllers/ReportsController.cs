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
    public class ReportsController : ApiController
    {
        public List<CCustomer> GetMessages()
        {
            NHibernate.ISession session = NHibernateHelper.GetCurrentSession();

            var results = session.Query<CCustomer>();

            return results.ToList<CCustomer>();
        }
    }
}
