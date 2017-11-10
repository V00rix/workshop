using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using NHibernate;
using NHibernate.Util;
using workshopIS.Helpers;
using workshopIS.Models;

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
                new CPartner("ewe", 312333, DateTime.Now, DateTime.MaxValue, true)
            };
        }

        /// <summary>
        /// Read data from database into static field (Partners)
        /// </summary>
        public static void ReadDataFromDatabase()
        {
            try
            {

                ISession session = NHibernateHelper.GetCurrentSession();
                var results = session.Query<CPartner>();
                Partners = results.ToList<IPartner>();
                foreach (IPartner partner in Partners)
                {
                    partner.Customers = session.Query<CCustomer>().Where(c => c.Partner.Id == partner.Id).ToList();
                    foreach (CCustomer customer in partner.Customers)
                    {
                        customer.Loans = session.Query<CLoan>().Where(l => l.Customer.Id == customer.Id).ToList();
                    }
                }
                session.Close();
            }
            catch (Exception e)
            {
                Trace.WriteLine("Error while reading from DB");
                Partners = new List<IPartner>();
            }
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
                    index = (int)session.Save(obj);
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
                        index = (int)session.Save(obj);
                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        throw new Exception("exeption: ", e);
                    }
                }
            }
            return index;
        }

        private static void RemoveFromDb(Object obj)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            session.Delete(obj);
            session.Flush();
        }

        private static void CleanupDb()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            session.Query<CCustomer>().Where(c => c.Partner == null).ToList().ForEach(RemoveFromDb);
            session.Query<CLoan>().Where(l => l.Customer == null).ToList().ForEach(RemoveFromDb);
            session.Flush();
        }

        /// <summary>
        /// Erase selected partner from database
        /// </summary>
        /// <param name="pid">Partner's id</param>
        public static void RemovePartner(int pid)
        {
            RemoveFromDb(Partners[pid]);
            CleanupDb();
            Partners.RemoveAt(pid);
        }

        /// <summary>
        /// Update database entity of selected partner
        /// </summary>
        /// <param name="pid">Id of a partner</param>
        /// <param name="partner">New value</param>
        public static void UpdatePartner(int pid, CPartner partner)
        {
            byte[] tempFile = Partners[pid].FileData;
            Partners[pid] = partner;
            Partners[pid].FileData = partner.FileData ?? tempFile;
            ISession session = NHibernateHelper.GetCurrentSession();
            ITransaction tx = session.BeginTransaction();
            session.Update(Partners[pid]);
            session.Query<CCustomer>()
                .Where(c => c.Partner.Id == Partners[pid].Id).ForEach(c =>
                {
                    int cid;
                    if ((cid = Partners[pid].Customers.FindIndex(cus => cus.Id == c.Id)) == -1)
                    {
                        session.Query<CLoan>().Where(l => l.Customer.Id == c.Id).ForEach(RemoveFromDb);
                        RemoveFromDb(c);
                    }
                    else
                    {
                        session.Query<CLoan>().Where(l => l.Customer.Id == c.Id).ForEach(l =>
                        {
                            if (!Partners[pid].Customers[cid].Loans.Exists(ll => l.Id == ll.Id))
                                RemoveFromDb(l);
                        });

                    }
                });
            CleanupDb();
            tx.Commit();
        }

        /// <summary>
        /// Close session
        /// </summary>
        public static void CloseSession()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            session.Close();
        }

        /// <summary>
        /// Appends File to specified partner, or last if none was specified.
        /// <para>Second use in case of creating new partner.</para>
        /// </summary>
        /// <param name="fileBytes">File Data</param>
        /// <param name="pid">Partner Id</param>
        public static void AppendFile(byte[] fileBytes, int pid = -1)
        {
            pid = pid < 0 ? Partners.Count - 1 : pid;
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                Partners[pid].FileData = fileBytes;
                ITransaction tx = session.BeginTransaction();
                session.Update(Partners[pid]);
                tx.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                session.Close();
            }
        }

        /// <summary>
        /// Update contact state of the selected customer
        /// </summary>
        /// <param name="pid">Partner's id</param>
        /// <param name="cid">Customer's id</param>
        /// <param name="state">New state value</param>
        public static void UpdateState(int pid, int cid, int state)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            ITransaction tx = session.BeginTransaction();
            Partners[pid].Customers[cid].ContactState = state;
            session.Update(Partners[pid].Customers[cid]);
            tx.Commit();
        }

        /// <summary>
        /// Update contact state of the selected customer
        /// </summary>
        /// <param name="customer">Selected customer</param>
        /// <param name="state">New state value</param>
        public static void UpdateState(CCustomer customer, int state)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            ITransaction tx = session.BeginTransaction();
            customer.ContactState = state;
            session.Update(customer);
            tx.Commit();
        }
    }
}