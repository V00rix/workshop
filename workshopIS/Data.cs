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
            // ReadDataFromDatabase(); 

            FakeDatabase();
        }

        // DATABASE CODE BELOW
        private static void ReadDataFromDatabase()
        {
            ISession session = NHibernateHelper.GetCurrentSession();

            //ITransaction tx = session.BeginTransaction();
            //session.Save(new Message { Body = "dsfsdfsdf", CreationTime = DateTime.Now });
            //tx.Commit();
            var results = session.Query<CPartner>();
            Partners = results.ToList<IPartner>();
        }

        // Fake DB
        public static void FakeDatabase()
        {
            // initialize fake list of partners,
            // customers and loans - all related
            Partners = new List<IPartner>();
            /*
            Partners = new List<IPartner>
            {
                new CPartner
                {
                    Id = 1,
                    Name = "Partner John",
                    ICO = 7891,
                    Customers = new List<ICustomer>
                    {
                        new CCustomer
                        {
                            Id = 1,
                            FirstName = "Customer Bob",
                            Loans = new List<ILoan>
                            {
                                new CLoan
                                {
                                    Id = 1,
                                    Amount = 40000,
                                    Duration = 10
                                },
                                new CLoan
                                {
                                    Id = 10,
                                    Amount = 200000,
                                    Duration = 50
                                },
                                new CLoan
                                {
                                    Id = 7,
                                    Amount = 50000,
                                    Duration = 960
                                }
                            }
                        },
                        new CCustomer
                        {
                            Id = 2,
                            FirstName = "Customer Cob"
                        },
                        new CCustomer
                        {
                            Id = 3,
                            FirstName = "Customer Hello World"
                        }
                    }
                },
                new CPartner
                {
                    Id = 2,
                    Name = "Partner Lawn",
                    ICO = 32213,
                    Customers = new List<ICustomer>
                    {
                        new CCustomer
                        {
                            Id = 6,
                            FirstName = "Second Partner Customer"
                        },
                        new CCustomer
                        {
                            Id = 13,
                            FirstName = "Friday"
                        }
                    }
                }
            };
            */
        }

        private static int counterP = 0;
        private static int counterC = 0;
        private static int counterL = 0;

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