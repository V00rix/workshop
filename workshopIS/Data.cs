using System;
using System.Collections.Generic;
using workshopIS.Models;

namespace workshopIS
{
    public static class Data
    {
        static List<IPartner> partners;

        // Runs on startup
        public static void Initialize()
        {
            // ReadDataFromDatabase(); 
            FakeDatabase();
        }

        // DATABASE CODE BELOW
        private static void ReadDataFromDatabase()
        {
            // TODO: read from db to partners
            //
            // partners.add(*FROM DATABASE*)
        }

        // Fake DB
        public static void FakeDatabase()
        {
            // initialize fake list of partners,
            // customers and loans - all related
            partners = new List<IPartner>
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
                            Name = "Customer Bob",
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
                            Name = "Customer Cob"
                        },
                        new CCustomer
                        {
                            Id = 3,
                            Name = "Customer Hello World"
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
                            Name = "Second Partner Customer"
                        },
                        new CCustomer
                        {
                            Id = 13,
                            Name = "Friday"
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