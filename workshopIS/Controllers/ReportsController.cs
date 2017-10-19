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

        [System.Web.Http.Route("api/reports/Loan/Partner")]

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
            //dopsat
            return Ok(dateFrom);
        }


        [System.Web.Http.Route("api/reports/CallCentre/status")]

        public IHttpActionResult GetCallCentrumInfo()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            var result = session.Query<CCustomer>().GroupBy(x => x.ContactState).Select(x => new {Status=x.Key, counter = x.Count()}).ToList();
            return Ok(result);

        }
        [System.Web.Http.Route("api/reports/CallCentre/status/{status}")]

        public IHttpActionResult GetCallCentrumInfoById(int status)
        {
            if (status == 1 || status == 2 || status == 3)
            {
                ISession session = NHibernateHelper.GetCurrentSession();
                var result = session.Query<CCustomer>().GroupBy(x => x.ContactState).Select(x => new { Status = x.Key, counter = x.Count() }).Where(x => x.Status.Value == status).ToList();
                if (result.Count == 0)
                {
                    return Ok("Loans with this status was not found");
                }
                return Ok(result);

            }
            return BadRequest("Status doesnt exist");

        }



        [System.Web.Http.Route("api/reports/Partners")]

        public IHttpActionResult GetPartnerInfo()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {

                var result = session.Query<CPartner>().Select(x => new { Partners = x });



                return Ok(result);
            }

                catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            finally
            {
                session.Close();
            }
        }

        [System.Web.Http.Route("api/reports/Partners/Count")]

        public IHttpActionResult GetPartnerCount()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {

                var result = session.Query<CPartner>().Select(x => new {Partners = x}).Count();
                var vysledek = new {soucetPartneru = result};

                return Ok(vysledek);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            finally
            {
                session.Close();
            }

        }

        [System.Web.Http.Route("api/reports/Loan")]

        public IHttpActionResult GetAllLoanInfo()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                var result = session.Query<CLoan>().Select(x => new { Loan = x });

                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            finally
            {
                session.Close();
            }
        }

        [System.Web.Http.Route("api/reports/Loan/Count")]

        public IHttpActionResult GetLoanCount()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                var result = session.Query<CLoan>().Select(x => new {Loan = x}).Count();
                var vysledek = new {soucetPujcek = result};

                return Ok(vysledek);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            finally
            {
                session.Close();
            }

        }

        [System.Web.Http.Route("api/reports/Loan/Partner/{partnerId:int}")]

        public IHttpActionResult GetLoanByPartnerInfo(int partnerId)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                IList<CLoan> test = session.QueryOver<CLoan>()
                    .JoinQueryOver(l => l.Customer)
                    .JoinQueryOver(l => l.Partner).List();

                //var vysledek=test.Select(x => new {customer=x.Customer,loan=x});

                var loanByPartner = test.GroupBy(t => t.Customer.Partner)
                    .Select(t => new {Partner = t.Key.Id, Cnt = t.Count()}).Where(x => x.Partner == partnerId);
                if (loanByPartner.Count() == 0)
                {
                    return Ok("Selected partner doesnt exist");
                }

                return Ok(loanByPartner);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            finally
            {
                
            }

        }


    }
}
