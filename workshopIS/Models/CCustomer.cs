using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace workshopIS.Models
{
    public class CCustomer : ICustomer, IIsValid
    {
        // id is assigned when saved to DB
        private int id;
        // mandatory fields, set from constructor parameters
        private string phone;
        // mandatory, generated on initializaton
        private DateTime? creationDate;
        private CPartner partner;
        // optional fields
        private string firstName = null;
        private string surname = null;
        private string email = null;
        // state of contact from CallCentre
        private int? contactState = 0;  // 0 - not contacted yet (default)  | E
                                        // 1 - could not make contact       | N
                                        // 2 - contacted, confirmed         | U
                                        // 3 - contacted, rejected          | M
                                        // reference to customer related loans
        private List<ILoan> loans;

        public virtual int Id { get => id; set => id = value; }
        public virtual string Phone { get => phone; set => phone = value; }
        public virtual string FirstName { get => firstName; set => firstName = value; }
        public virtual string Surname { get => surname; set => surname = value; }
        public virtual string Email { get => email; set => email = value; }
        public virtual int? ContactState { get => contactState; set => contactState = value; }
        public virtual DateTime? CreationDate { get => creationDate; set => creationDate = value; }
        public virtual List<ILoan> Loans { get => loans; set => loans = value; }
        public virtual CPartner Partner { get => partner; set => partner = value; }

        // Constructors
        /// <summary>
        /// Create new instance of CCustomer
        /// </summary>
        /// <param name="partnerId">ID of a related partner</param>
        /// <param name="phone">Customer's phone number</param>
        /// <param name="firstName">Customer's first name</param>
        /// <param name="surname">Customer's surname</param>
        /// <param name="email">Customer's e-mail adress</param>
         /*
        public CCustomer(CPartner partner, string phone, string firstName = null,
                        string surname = null, string email = null, 
                        List<ILoan> loans = null)
        {
            this.partner = partner;
            this.phone = phone;
            this.firstName = firstName;
            this.surname = surname;
            this.email = email;
            // check if fields are valid
            try
            {
                IsValid();
            }
            catch (Exception ex)
            {
                throw new Exception("Wrong values specified in new CCustomer creation!",
                    ex);
            }
            // set creation date
            this.CreationDate = DateTime.Now;
            // save to DB and get id
            this.id = Data.SaveToDB(this);
            // create new list if argument is null
            this.loans = loans ?? new List<ILoan>();
            // link each loan from list to this customer
            foreach (ILoan loan in this.loans)
                loan.Customer = this;
        }*/

        // Add Loan to list
        public virtual void AddLoan(ILoan loan)
        {
            // Set child's id
            loan.Customer = this;
            loans.Add(loan);
        }

        // check for fields validity
        public virtual bool IsValid()
        {
            // Phone
            // valid phone format ?
            if (Phone == null)
                throw new ArgumentNullException("No phone number!");
            if (Phone.Length < 9)
                throw new ArgumentException("Phone number is too short!");
            if (Phone.Length > 16)
                throw new ArgumentException("Phone number is too long!");
            // other checks ?
            return true;
        }
    }
}