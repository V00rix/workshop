using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace workshopIS.Models
{
    public class CPartner : IPartner
    {
        string MyTest;
        private int Id;
        public string Name;
        public string Surname;
        public string DokumentPath;
        public DateTime ValidFrom;
        public DateTime ValidTo;
        public CPartner() { }
    }
}