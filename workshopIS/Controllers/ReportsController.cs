using NHibernate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
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
using NHibernate.Util;
using workshopIS.Helpers;

namespace workshopIS.Controllers
{
    public class ReportsController : ApiController
    {

        /// <summary>
        /// return all loans by partner grouped by state
        /// </summary>
        /// <returns>return all loans by partner grouped by state </returns>
        [System.Web.Http.Route("api/reports/Loan/Partner")]
        
        public IHttpActionResult GetLoanInfo()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                IList<CLoan> test = session.QueryOver<CLoan>()
                    .JoinQueryOver(l => l.Customer)
                    .JoinQueryOver(l => l.Partner).List();

                var loanByPartner = test.GroupBy(t => t.Customer.Partner)
                    .Select(t => new
                    {
                        Partner = t.Key.Id,
                        LoansByState = t.GroupBy(x => x.Customer.ContactState)
                            .Select(x => new {ContactState = x.Key, Loans = x.ToList()})
                    });

                return Ok(loanByPartner);
            }
            catch
            {
                return BadRequest("Bad request");
            }
            finally
            {
                session.Close();
            }

        }
        /// <summary>
        /// return all loans by partner grouped by state, not works correctly
        /// </summary>
        /// <returns>return all loans by partner grouped by state in CSV</returns>
        [System.Web.Http.Route("api/reports/Loan/Partner/Csv")]

        public IHttpActionResult GetLoanByCsv()
        {
            ISession session = NHibernateHelper.GetCurrentSession();

            IList<CLoan> test = session.QueryOver<CLoan>()
                .JoinQueryOver(l => l.Customer)
                .JoinQueryOver(l => l.Partner).List();

            var loanByPartner = test.GroupBy(t => t.Customer.Partner)
                .Select(t => new { Partner = t.Key.Id, LoansByState = t.GroupBy(x => x.Customer.ContactState).Select(x => new { ContactState = x.Key, Loans = x.ToList() }) });

            JavaScriptSerializer js = new JavaScriptSerializer();
            string json = js.Serialize(loanByPartner);
            JsonToCsv.jsonStringToCSV(json);

            return Ok();

        }
        /// <summary>
        /// return loans by createdate
        /// </summary>
        /// <param name="dateFrom">dateFrom for filter</param>
        /// <param name="dateTo">dateTo for filter</param>
        /// <returns></returns>
        [System.Web.Http.Route("api/reports/Loan")]

        public IHttpActionResult GetLoanInfoByDate(string dateFrom,string dateTo)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                DateTime DateFrom = DateTime.Parse(dateFrom);
                DateTime DateTo = DateTime.Parse(dateTo);


                IList<CLoan> test = session.QueryOver<CLoan>()
                    .JoinQueryOver(l => l.Customer)
                    .JoinQueryOver(l => l.Partner).List();
                var query = test.Where(x => x.Customer.CreationDate >= DateFrom && x.Customer.CreationDate <= DateTo);
                return Ok(query);
            }
            catch
            {
                return BadRequest("bad request");
            }
            finally
            {
                session.Close();
            }
        }

        /// <summary>
        /// Count loans by statuses in callcentre
        /// </summary>
        /// <returns>return counter loans by statuses</returns>
        [System.Web.Http.Route("api/reports/CallCentre/status")]

        public IHttpActionResult GetCallCentrumInfo()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            var result = session.Query<CCustomer>().GroupBy(x => x.ContactState).Select(x => new {Status=x.Key, counter = x.Count()}).ToList();
            return Ok(result);

        }
        /// <summary>
        /// Count loans by unique status in callcentre
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [System.Web.Http.Route("api/reports/CallCentre/status/{status}")]

        public IHttpActionResult GetCallCentrumInfoById(int status)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                if (status == 1 || status == 2 || status == 3)
                {
                    var result = session.Query<CCustomer>().GroupBy(x => x.ContactState)
                        .Select(x => new {Status = x.Key, counter = x.Count()}).Where(x => x.Status.Value == status)
                        .ToList();
                    if (result.Count == 0)
                    {
                        return Ok("Loans with this status was not found");
                    }
                    return Ok(result);

                }
                return BadRequest("Status doesnt exist");
            }
            catch
            {
                return BadRequest("Bad request");
            }
            finally
            {
                session.Close();
            }


        }

        /// <summary>
        /// count all partners
        /// </summary>
        /// <returns>return counted partners</returns>
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

        /// <summary>
        /// count all loans
        /// </summary>
        /// <returns>counted loans</returns>
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
        /// <summary>
        /// Count all loans by partner
        /// </summary>
        /// <param name="partnerId">partnerID</param>
        /// <returns>counted loans by partnerID</returns>
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
                    .Select(t => new {Partner = t.Key.Id, Cnt = t.Count()}).FirstOrDefault(x => x.Partner == partnerId);

                if (loanByPartner==null)
                {
                    return BadRequest("Selected partner doesnt exist");
                }

                return Ok(loanByPartner);

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


    }
}
