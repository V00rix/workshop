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
        private CCustomer customer;
        // counted
        private decimal? monthlyCharge;
        private decimal? apr;
        private decimal? interest;
        // optional
        private string note = null;

        [JsonProperty("id")]
        public virtual int Id { get => id; set => id = value; }
        [JsonProperty("duration")]
        public virtual int? Duration { get => duration; set => duration = value; }
        [JsonProperty("amount")]
        public virtual decimal? Amount { get => amount; set => amount = value; }
        [JsonProperty("interest")]
        public virtual decimal? Interest { get => interest; set => interest = value; }
        [JsonProperty("monthlyCharge")]
        public virtual decimal? MonthlyCharge { get => monthlyCharge; set => monthlyCharge = value; }
        [JsonProperty("apr")]
        public virtual decimal? APR { get => apr; set => apr = value; }
        [JsonProperty("note")]
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
        public CLoan(CCustomer customer, decimal amount, int duration, decimal interest, string note = null)
        {
            this.customer = customer;
            try { this.amount = amount; }
            catch { throw new Exception("Amount was not specified!"); }

            try { this.duration = duration; }
            catch { throw new Exception("Duration was not specified!"); }

            try { this.interest = interest; }
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
            monthlyCharge = (amount / duration) * (1M + this.interest);
            // some formula for annual percentage rate 
            apr = (decimal)((double)(amount * interest * 12 * (decimal)Math.Pow((double)(1M + (this.interest ?? 0.1M)), duration)) /
                (double)Math.Pow((double)(1 + interest), duration));
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