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
            var a = 4;

        }

        // Fake DB
        public static void FakeDatabase()
        {
            // initialize fake list of partners,
            // customers and loans - all related
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