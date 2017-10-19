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

        public IHttpActionResult GetLoanInfo()
        {
            ISession session = NHibernateHelper.GetCurrentSession();




            IList<CLoan> test = session.QueryOver<CLoan>()
                                    .JoinQueryOver(l => l.Customer)
                                    .JoinQueryOver(l => l.Partner).List();

            var loanByPartner = test.GroupBy(t => t.Customer.Partner).Select(t => new {Partner = t.Key.Name, Cnt = t.Count()});

            return Ok(loanByPartner);

        }

        [System.Web.Http.Route("api/reports/Loan/{dateFrom}/{dateTo}")]

        public IHttpActionResult GetLoanInfoByDate(string dateFrom,string dateTo)
        {
            return Ok(dateFrom);
        }


        [System.Web.Http.Route("api/reports/CallCentre")]

        public IHttpActionResult GetCallCentrumInfo()
        {
            return Ok();
        }



        [System.Web.Http.Route("api/reports/Partner")]

        public IHttpActionResult GetPartnerInfo()
        {
            return Ok();
        }

        [System.Web.Http.Route("api/reports/{partnerId:int}/Loan")]

        public IHttpActionResult GetLoanByPartnerInfo(int partnerId)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            IList<CLoan> test = session.QueryOver<CLoan>()
                .JoinQueryOver(l => l.Customer)
                .JoinQueryOver(l => l.Partner).List();

            //var vysledek=test.Select(x => new {customer=x.Customer,loan=x});

            var loanByPartner = test.GroupBy(t => t.Customer.Partner).Select(t => new { Partner = t.Key.Id, Cnt = t.Count() }).Where(x=>x.Partner==partnerId);
            return Ok(loanByPartner);
        }


    }
}
