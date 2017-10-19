using NHibernate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Serialization;
using NHibernate.Criterion;
using workshopIS.Helpers;
using workshopIS.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using workshopIS.Helpers;

namespace workshopIS.Controllers
{
    public class ReportsController : ApiController
    {

        [System.Web.Http.Route("api/reports/Loan")]

        public IHttpActionResult Get()
        {
            ISession session = NHibernateHelper.GetCurrentSession();


            var results = session.Query<CLoan>().Select(c=>new {Loan = c, Customer = c.Customer}).ToList();
            // var Customers = session.Query<CCustomer>().ToList();
            // var partners = session.Query<CPartner>().ToList();

            //var test= Customers.FirstOrDefault(x => x.Id == 1).Name;

            // //var query =
            // //    from customer in Customers
            // //    from order in results
            // //        .Where(o => customer.Id == o.CustomerID)
            // //        .DefaultIfEmpty()
            // //    select new { Customer = customer.Name, Order = order.Amount };




            // //var vysledek = session.Query<CLoan>().GroupBy(x => x.CustomerID)
            // //    .Where(t => t.Key.HasValue).Select(group => new { Customers.FirstOrDefault(x => x.Id==group.Key) , counter=group.Count() }).ToList();

            return Ok(results);

        }


    }
}
