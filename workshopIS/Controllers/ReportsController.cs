﻿using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using workshopIS.Helpers;
using workshopIS.Models;
using ServiceStack.Text;

namespace workshopIS.Controllers
{/// <summary>
/// Reports class
/// </summary>
    public class ReportsController : ApiController
    {

        /// <summary>
        /// return all loans count by partner grouped by state
        /// </summary>
        /// <returns>return all loans by partner grouped by state </returns>
        [System.Web.Http.Route("api/reports/loan/partner")]
        
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
                            .Select(x => new {ContactState = x.Key, LoansCount = x.ToList().Count})
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
        [System.Web.Http.Route("api/reports/loan/partner/csv")]

        public IHttpActionResult GetLoanByCsv()
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
                            .Select(x => new {ContactState = x.Key, Loans = x.ToList().Count })
                    });


                var report = CsvSerializer.SerializeToCsv(loanByPartner);

                return BadRequest(report);

            }
            catch
            {
                return BadRequest("Proste fail");
            }
            finally
            {
                session.Close();
            }

        }
        /// <summary>
        /// return loans by createdate
        /// </summary>
        /// <param name="dateFrom">dateFrom for filter</param>
        /// <param name="dateTo">dateTo for filter</param>
        /// <returns></returns>
        [System.Web.Http.Route("api/reports/loan")]

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
                var query = test.Where(x => x.Customer.CreationDate >= DateFrom && x.Customer.CreationDate <= DateTo).ToList();
                if (query.Count == 0)
                {
                    return BadRequest("Loans between these dates was not found");
                }
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
        /// return loans by createdate in csv
        /// </summary>
        /// <param name="dateFrom">dateFrom for filter</param>
        /// <param name="dateTo">dateTo for filter</param>
        /// <returns></returns>
        [System.Web.Http.Route("api/reports/loan/csv")]

        public IHttpActionResult GetLoanInfoByDateCsv(string dateFrom, string dateTo)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                DateTime DateFrom = DateTime.Parse(dateFrom);
                DateTime DateTo = DateTime.Parse(dateTo);


                IList<CLoan> test = session.QueryOver<CLoan>()
                    .JoinQueryOver(l => l.Customer)
                    .JoinQueryOver(l => l.Partner).List();
                var query = test.Where(x => x.Customer.CreationDate >= DateFrom && x.Customer.CreationDate <= DateTo).ToList();
                if (query.Count == 0)
                {
                    return BadRequest("Loans between these dates was not found");
                }
                var report = CsvSerializer.SerializeToCsv(query);
                return Ok(report);
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
        [System.Web.Http.Route("api/reports/callcentre/status")]

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
        [System.Web.Http.Route("api/reports/callcentre/status/{status}")]

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
        [System.Web.Http.Route("api/reports/partners/count")]

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
        [System.Web.Http.Route("api/reports/loan/count")]

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
        [System.Web.Http.Route("api/reports/loan/partner/{partnerId:int}")]

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
