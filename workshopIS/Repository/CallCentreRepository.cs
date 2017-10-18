using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace workshopIS.Repository
{
    public class CallCentreRepository : ICallCentreRepository
    {
        public string test(string nevim)
        {
            return "test" + nevim;
        }
    }
}