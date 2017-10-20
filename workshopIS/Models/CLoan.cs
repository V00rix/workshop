using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace workshopIS.Models
{
    public class CLoan : ILoan, IIsValid
    {
        // id is null by default, assigned when is saved to DB
        private int id;
        // mandatory fields
        private int? duration;
        private decimal? amount;
        private decimal? percentage;
        private CCustomer customer;
        // counted
        private decimal? monthlyCharge;
        private decimal? annualCharge;
        private decimal? interest;
        // optional
        private string note = null;

        public virtual int Id { get => id; set => id = value; }
        public virtual int? Duration { get => duration; set => duration = value; }
        public virtual decimal? Amount { get => amount; set => amount = value; }
        public virtual decimal? Percentage { get => percentage; set => percentage = value; }
        public virtual decimal? Interest { get => interest; set => interest = value; }
        public virtual decimal? MonthlyCharge { get => monthlyCharge; set => monthlyCharge = value; }
        public virtual decimal? AnnualCharge { get => annualCharge; set => annualCharge = value; }
        public virtual string Note { get => note; set => note = value; }
        [JsonIgnore]
        public virtual CCustomer Customer { get => customer; set => customer = value; }

        // constructors
        /// <summary>
        /// Create new instance of CLoan
        /// </summary>
        /// <param name="amount">Loan amount</param>
        /// <param name="duration">Loan duration</param>
        /// <param name="note">Note(s)</param>

        public CLoan() { }
        public CLoan(CCustomer customer, decimal amount, int duration, decimal percentage, string note = null)
        {
            this.customer = customer;
            try { this.amount = amount; }
            catch { throw new Exception("Amount was not specified!"); }

            try { this.duration = duration; }
            catch { throw new Exception("Duration was not specified!"); }

            try { this.percentage = percentage; }
            catch { throw new Exception("Percentage was not specified!"); }

            this.note = note;
            // check if fields are valid
            try
            {
                IsValid();
            }
            catch (Exception ex)
            {
                throw new Exception("Wrong values specified in new CLoan creation!",
                    ex);
            }
            // count monthly charge, annual charge and interest
            monthlyCharge = (amount / duration) * (1 + this.percentage);
            // some formula for annualCharge 
            interest = 
            // save to DB and get id
            this.id = Data.SaveToDB(this);
        }

        public virtual bool IsValid()
        {
            // get constants from configuration
            decimal minAmount = Decimal.Parse(ConfigurationManager.AppSettings.Get("LOAN_MIN_AMOUNT"));
            decimal maxAmount = Decimal.Parse(ConfigurationManager.AppSettings.Get("LOAN_MAX_AMOUNT"));
            decimal minDuration = Decimal.Parse(ConfigurationManager.AppSettings.Get("LOAN_MIN_DURATION"));
            decimal maxDuration = Decimal.Parse(ConfigurationManager.AppSettings.Get("LOAN_MAX_DURATION"));

            // amount check
            if (Amount < minAmount)
                throw new ArgumentException(
                    "Loan amount is lower than " + minAmount + "!");
            if (Amount > maxAmount)
                throw new ArgumentException(
                    "Loan amount is higher than " + maxAmount + "!");

            // duration check
            if (Duration < minDuration)
                throw new ArgumentException(
                    "Loan duration is lower than " + minDuration + " months!");
            if (Duration > maxDuration)
                throw new ArgumentException(
                    "Loan duration is higher than " + maxDuration + " months!");

            // everything OK
            return true;
        }
    }
}