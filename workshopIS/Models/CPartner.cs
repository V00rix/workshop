using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace workshopIS.Models
{
    public class CPartner : IPartner
    {
        private int id;
        private string name;
        private int ico;
        private string documentPath;
        private DateTime validFrom;
        private DateTime validTo;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int ICO { get => ico; set => ico = value; }
        public string DocumentPath { get => documentPath; set => documentPath = value; }
        public DateTime ValidFrom { get => validFrom; set => validFrom = value; }
        public DateTime ValidTo { get => validTo; set => validTo = value; }

        public CPartner() { }
    }
}