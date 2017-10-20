using NHibernate;
using System;
using System.Collections.Generic;
using workshopIS.Helpers;
using workshopIS.Models;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Net;

namespace workshopIS
{
    public static class Data
    {
        public static List<IPartner> Partners;

        // Runs on startup
        public static void Initialize()
        {
            ReadDataFromDatabase();
        }

        /// <summary>
        /// Read data from database into static field (Partners)
        /// </summary>
        public static void ReadDataFromDatabase()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            var results = session.Query<CPartner>();
            Partners = results.ToList<IPartner>();
            foreach (IPartner partner in Partners)
            {
                partner.Customers = session.Query<CCustomer>().ToList<ICustomer>();
                foreach (ICustomer customer in partner.Customers)
                {
                    customer.Loans = session.Query<CLoan>().ToList<ILoan>();
                }
            }
            session.Close();
        }

        /// <summary>
        /// Save object into DB
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>id of saved object</returns>
        internal static int SaveToDB(Object obj)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                ITransaction tx = session.BeginTransaction();
                int index = (int)session.Save(obj);
                tx.Commit();
                return index;
            }
            catch(Exception e)
            {
                throw new Exception("exeption: ",e);
            }
            finally
            {
                session.Close();
            }
        }
    }
}