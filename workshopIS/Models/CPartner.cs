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
        private List<ICustomer> customers;
        private Byte[] fileData;

        public virtual int Id { get => id; set => id = value; }
        public virtual string Name { get => name; set => name = value; }
        public virtual int ICO { get => ico; set => ico = value; }
        public virtual string DocumentPath { get => documentPath; set => documentPath = value; }
        public virtual DateTime ValidFrom { get => validFrom; set => validFrom = value; }
        public virtual DateTime ValidTo { get => validTo; set => validTo = value; }
        public virtual Byte[] FileData { get => fileData; set => fileData = value; }
        public virtual List<ICustomer> Customers { get => customers; set => customers = value; }

        public CPartner() { }
    }
}