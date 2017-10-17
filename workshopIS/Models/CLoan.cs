using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace workshopIS.Models
{
    public class CLoan : ILoan // , IComparable
    {
        // should be read-only? 
        public int Id;
        public int Duration; // Days

        // financial
        public decimal Amount;
        public decimal Interest;
        public decimal MonthlyCharge;
        public decimal AnnualCharge;

        // custromer reference
        public ICustomer Custromer;

        public CLoan() { }

        // counting methods, formatting output, compare etc

        // public int CompareTo(object obj)
        // {
        //     throw new NotImplementedException();
        // }
    }
}