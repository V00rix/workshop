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

namespace workshopIS.Controllers
{
    [CollectionDataContractAttribute]
    public class ReportsController : ApiController
    {

        [System.Web.Http.Route("api/reports/partners")]

        public IHttpActionResult Get()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            var results = session.Query<CLoan>().ToList();
            var Customers = session.Query<CCustomer>().ToList();

            //var query =
            //    from customer in Customers
            //    from order in results
            //        .Where(o => customer.Id == o.CustomerID)
            //        .DefaultIfEmpty()
            //    select new {Customer = customer, Order = order};

            //foreach (var item in query)
            //{
            //    var nevim = item;
            //    Console.WriteLine(item.Customer.Id);
            //}


        var vysledek = results.GroupBy(x => x.CustomerID)
                .Select(group => new { group.Key, counter=group.Count() }).ToList<object>();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var output = serializer.Serialize(vysledek);
            //return vysledek;
            return Ok(vysledek);
        }

        public static byte[] Serialize(object obj)
        {
            using (var ms = new MemoryStream())
            using (var writer = new BsonWriter(ms))
            {
                new JsonSerializer().Serialize(writer, obj);
                return ms.ToArray();
            }
        }

    }
}
