using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using workshopIS.Helpers;
using workshopIS.Models;

namespace workshopIS.Controllers
{
    public class ReportsController : ApiController
    {
        public void GetMessages()
        {
            NHibernate.ISession session = NHibernateHelper.GetCurrentSession();
            /*
            var results = session.Query<CCustomer>();

            return results.ToList<CCustomer>();*/
            ITransaction tx = session.BeginTransaction();

            CPartner customer = new CPartner();
            customer.Name = "John Snow";
            customer.ICO = 85753;


            session.Save(customer);
            tx.Commit();

            NHibernateHelper.CloseSession();

        }
    }
}
