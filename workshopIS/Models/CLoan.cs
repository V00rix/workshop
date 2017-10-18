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
        private ICustomer custromer;

        public int Id { get => id; set => id = value; }
        public int Duration { get => duration; set => duration = value; }
        public decimal Amount { get => amount; set => amount = value; }
        public decimal Interest { get => interest; set => interest = value; }
        public decimal MonthlyCharge { get => monthlyCharge; set => monthlyCharge = value; }
        public decimal AnnualCharge { get => annualCharge; set => annualCharge = value; }
        public ICustomer Custromer { get => custromer; set => custromer = value; }

        public CLoan() { }
    }
}