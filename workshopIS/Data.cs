using NHibernate;
using System;
using System.Collections.Generic;
using workshopIS.Helpers;
using workshopIS.Models;
using System.Linq;

namespace workshopIS
{
    public static class Data
    {
        public static List<IPartner> Partners;
        private const decimal MIN_LOAN_AMOUNT = 20000;
        private const decimal MAX_LOAN_AMOUNT = 500000;
        private const decimal MIN_LOAN_DURATION = 6;
        private const decimal MAX_LOAN_DURATION = 96;

        // Runs on startup
        public static void Initialize()
        {
            ReadDataFromDatabase(); 

            //FakeDatabase();
        }

        // DATABASE CODE BELOW
        public static void ReadDataFromDatabase()
        {
            ISession session = NHibernateHelper.GetCurrentSession();

            //ITransaction tx = session.BeginTransaction();
            //session.Save(new Message { Body = "dsfsdfsdf", CreationTime = DateTime.Now });
            //tx.Commit();
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
        /// save object into DB
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



            /*
            if (obj.GetType() == typeof(CPartner))
                return counterP++;
            if (obj.GetType() == typeof(CCustomer))
                return counterC++;
            if (obj.GetType() == typeof(CLoan))
                return counterL++;*/

        }

        // Partners Methods
        public static List<IPartner> GetPartners()
        {
            // TODO
            return null;
        }

        public static IPartner GetPartner(int id)
        {
            // TODO
            return null;
        }

        // Customers methods
        internal static List<ICustomer> GetCustomers()
        {
            // TODO
            return null;
        }
        // ...
    }
}