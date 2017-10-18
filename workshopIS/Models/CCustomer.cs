using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace workshopIS.Models
{
    public class CCustomer : ICustomer
    {
        private int id;
        private string phone;
        private ECallState contactState;
        private string firstName;
        private string surname;
        private string email;
        private string note;
        private int state;
        private int partnerId;
        private DateTime creationDate;
        private List<ILoan> loans;

        public virtual int Id { get => id; set => id = value; }
        public virtual string Phone { get => phone; set => phone = value; }
        public virtual ECallState ContactState { get => contactState; set => contactState = value; }
        public virtual string FirstName { get => firstName; set => firstName = value; }
        public virtual string Surname { get => surname; set => surname = value; }
        public virtual string Email { get => email; set => email = value; }
        public virtual string Note { get => note; set => note = value; }
        public virtual int State { get => state; set => state = value; }
        public virtual int PartnerId { get => partnerId; set => partnerId = value; }
        public virtual DateTime CreationDate { get => creationDate; set => creationDate = value; }
        public virtual List<ILoan> Loans { get => loans; set => loans = value; }

        public CCustomer() { }
    }

    public enum ECallState
    {
        SomeState,
        SomeOtherState
    }
}