using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace workshopIS.Models
{
    public class CCustomer : ICustomer
    {
        private int id;
        private int phone;
        private ECallState contactState;
        private IPartner partner;
        string name;
        private string email;
        private string note;

        public int Id { get => id; set => id = value; }
        public int Phone { get => phone; set => phone = value; }
        public ECallState ContactState { get => contactState; set => contactState = value; }
        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public string Note { get => note; set => note = value; }
        public IPartner Partner { get => partner; set => partner = value; }

        public CCustomer() { }
    }

    public enum ECallState
    {
        SomeState,
        SomeOtherState
    }
}