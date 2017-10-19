using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace workshopIS.Models
{
    public class CLoan : ILoan // , IComparable
    {
        // unique ids container
        private static List<int> ids = new List<int>();
        private int? id;
        private int? duration;
        private int? customerID;
        private decimal? amount;
        private decimal? interest;
        private decimal? monthlyCharge;
        private decimal? annualCharge;
        private string note;

        public virtual int? Id { get => id; set => id = value; }
        public virtual int? Duration { get => duration; set => duration = value; }
        public virtual int? CustomerID { get => customerID; set => customerID = value; }
        public virtual decimal? Amount { get => amount; set => amount = value; }
        public virtual decimal? Interest { get => interest; set => interest = value; }
        public virtual decimal? MonthlyCharge { get => monthlyCharge; set => monthlyCharge = value; }
        public virtual decimal? AnnualCharge { get => annualCharge; set => annualCharge = value; }
        public virtual string Note { get => note; set => note = value; }
        public virtual CCustomer Customer { get; set; }

        // constructors
        public CLoan() { }

        public CLoan(decimal amount, int duration, string note = "")
        {
            this.amount = amount;
            this.duration = duration;
            this.note = note;
        }
    }
}