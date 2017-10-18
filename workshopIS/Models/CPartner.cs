using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace workshopIS.Models
{
    public class CPartner : IPartner
    {
        // unique ids container
        private static List<int> ids = new List<int>();

        private int id;
        private string name;
        private int? ico;
        private DateTime? validFrom;
        private DateTime? validTo;
        private List<ICustomer> customers;
        private Byte[] fileData;
        private bool? isActive;

        public virtual int Id { get => id; set => id = value; }
        public virtual string Name { get => name; set => name = value; }
        public virtual int? ICO { get => ico; set => ico = value; }
        public virtual DateTime? ValidFrom { get => validFrom; set => validFrom = value; }
        public virtual DateTime? ValidTo { get => validTo; set => validTo = value; }
        public virtual Byte[] FileData { get => fileData; set => fileData = value; }
        public virtual List<ICustomer> Customers { get => customers; set => customers = value; }
        public virtual bool? IsActive { get => isActive; set => isActive = value; }

        // constructors
        public CPartner() { }

        public CPartner(int id, string name, int? ICO, 
            DateTime validFrom, DateTime? validTo = null, bool? isActive = true, 
            Byte[] fileData = null)
        {
            if (ids.Contains(id))
                throw new Exception("Partner with such id already exists!");
            Id = id;
            Name = name;
            this.ICO = ICO;
            ValidTo = validTo;
            IsActive = isActive;
            FileData = fileData;
        }

        public void AddCustomer(ICustomer customer)
        {
            customers.Add(customer);
        }

        // Unique ID generator
        private int GenerateId()
        {
            int i = 1;
            while (ids.Contains(i))
                i++;
            ids.Add(i);
            return i;
        }
    }
}