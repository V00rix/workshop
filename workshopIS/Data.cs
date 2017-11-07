﻿using NHibernate;
using System;
using System.Collections.Generic;
using System.Data;
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

            /* FakePartners();
            foreach (var partner in Partners)
            {

                SaveToDB(partner);
            } */
            ReadDataFromDatabase();
        }

        private static void FakePartners()
        {
            Partners = new List<IPartner>
            {
                new CPartner("hello", 321, DateTime.Now, DateTime.MaxValue, true),
                new CPartner("312", 320001, DateTime.Now, DateTime.MaxValue, true),
                new CPartner("eqweqwe", 1321, DateTime.Now, DateTime.MaxValue, true),
                new CPartner("qweeqwe", 32331, DateTime.Now, DateTime.MaxValue, true),
                new CPartner("ewe", 312333, DateTime.Now, DateTime.MaxValue, true),
            };
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
            int index = -1;
            try
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    index = (int) session.Save(obj);
                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                if (session.Connection.State == ConnectionState.Closed)
                {
                    try
                    {
                        session.Connection.Open();
                        ITransaction transaction = session.BeginTransaction();
                        index = (int) session.Save(obj);
                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        throw new Exception("exeption: ", e);
                    }
                }
            }
            finally
            {
                //session.Close();
            }
            return index;
        }
    }
}