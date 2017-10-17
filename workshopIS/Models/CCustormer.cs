using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace workshopIS.Models
{
    public class CCustormer : ICustomer
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
    }

    public enum ECallState
    {
        SomeState,
        SomeOtherState
    }
}