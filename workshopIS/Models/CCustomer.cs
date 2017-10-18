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
        private string name;
        private string email;
        private string note;
        private List<ILoan> loans;

        public int Id { get => id; set => id = value; }
        public string Phone { get => phone; set => phone = value; }
        public ECallState ContactState { get => contactState; set => contactState = value; }
        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public string Note { get => note; set => note = value; }
        public List<ILoan> Loans { get => loans; set => loans = value; }

        public CCustomer() { }
    }

    public enum ECallState
    {
        SomeState,
        SomeOtherState
    }
}