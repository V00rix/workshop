using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace workshopIS.Models
{
    public class CCustomer : ICustomer
    {
        // all params - public?
        public int Id;
        public int Phone; // code? +420 etc -> need to use string?
        public ECallState ContactState; // Used by CallCentre and Reports

        // reference to partner
        public IPartner Partner;

        // optional params
        public string Name;
        public string Surname;
        public string Email;
        public string Note;

        public CCustomer() { } 

        string ICustomer.GetName()
        {
            return this.Name;
        }
        public string GetSurname()
        {
            return this.Surname;
        }
    }

    public enum ECallState
    {
        SomeState,
        SomeOtherState
    }
}