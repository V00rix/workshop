using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace workshopIS.Models
{
    public class CLoan : ILoan // , IComparable
    {
        private int id;
        private int duration;
        private decimal amount;
        private decimal interest;
        private decimal monthlyCharge;
        private decimal annualCharge;
        private int customerID;

        public virtual int Id { get => id; set => id = value; }
        public virtual int Duration { get => duration; set => duration = value; }
        public virtual int CustomerID { get => customerID; set => customerID = value; }
        public virtual decimal Amount { get => amount; set => amount = value; }
        public virtual decimal Interest { get => interest; set => interest = value; }
        public virtual decimal MonthlyCharge { get => monthlyCharge; set => monthlyCharge = value; }
        public virtual decimal AnnualCharge { get => annualCharge; set => annualCharge = value; }
 
        public CLoan() { }
    }
}