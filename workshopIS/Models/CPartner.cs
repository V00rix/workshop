using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace workshopIS.Models
{
    public class CPartner : IPartner
    {
        // all params - public?
        public int Id { get; set; }
        public string Name { get; set; }
        public int ICO { get; set; }
        public string DokumentPath { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public CPartner() { }
    }
}