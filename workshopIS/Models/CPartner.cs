using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace workshopIS.Models
{
    public class CPartner : IPartner, IIsValid
    {
        // mandatory
        private int id;
        private string name;
        private int? ico;
        // mandatory, generated on initializaton
        private DateTime? validFrom;
        // optional
        private bool? isActive;
        private DateTime? validTo;
        private Byte[] fileData;
        private List<CCustomer> customers;

        [JsonProperty("id")]
        public virtual int Id { get => id; set => id = value; }
        [JsonProperty("name")]
        public virtual string Name { get => name; set => name = value; }
        [JsonProperty("ico")]
        public virtual int? ICO { get => ico; set => ico = value; }
        [JsonProperty("validFrom")]
        public virtual DateTime? ValidFrom { get => validFrom; set => validFrom = value; }
        [JsonProperty("validTo")]
        public virtual DateTime? ValidTo { get => validTo; set => validTo = value; }
        [JsonIgnore]
        public virtual Byte[] FileData { get => fileData; set => fileData = value; }
        [JsonProperty("customers")]
        public virtual List<CCustomer> Customers { get => customers; set => customers = value; }
        [JsonProperty("isActive")]
        public virtual bool? IsActive { get => isActive; set => isActive = value; }

        public CPartner()
        {
            customers = new List<CCustomer>();
        }

        // constructors
        /// <summary>
        /// Create new instance of CPartner
        /// </summary>
        /// <param name="name">Partner's name</param>
        /// <param name="ICO">ICO</param>
        /// <param name="validFrom">Date of validity start</param>
        /// <param name="validTo">Date of validity end</param>
        /// <param name="isActive">State of partner's validity</param>
        /// <param name="fileData">da</param>
        /// <param name="customers">Customers of the partner</param>
        public CPartner(string name, int ICO,
            DateTime? validFrom = null, DateTime? validTo = null, bool isActive = true,
            Byte[] fileData = null, List<CCustomer> customers = null)
        {
            this.name = name;
            this.ICO = ICO;
            this.validTo = validTo;
            this.isActive = isActive;
            this.fileData = fileData;
            // check if fields are valid
            try
            {
                IsValid();
            }
            catch (Exception ex)
            {
                throw new Exception("Wrong values specified in new CPartner creation!",
                    ex);
            }
            // set ValidFrom
            this.validFrom = validFrom ?? DateTime.Now;
            // save to DB and get id
            this.id = Data.SaveToDB(this);
            // create new list if argument is null
            this.customers = customers ?? new List<CCustomer>();
            // link each loan from list to this customer
            foreach (CCustomer customer in this.customers)
                customer.Partner = this;
        }

        /// <summary>
        /// Copy from another CPartner
        /// </summary>
        /// <param name="p">CPartner</param>
        public CPartner(CPartner p)
        {
            this.name = p.name;
            this.ICO = p.ICO;
            this.validTo = p.validTo;
            this.isActive = p.isActive;
            this.fileData = p.fileData;
            // check if fields are valid
            try
            {
                IsValid();
            }
            catch (Exception ex)
            {
                throw new Exception("Wrong values specified in new CPartner creation!",
                    ex);
            }
            // set ValidFrom
            this.validFrom = p.validFrom ?? DateTime.Now;
            // save to DB and get id
            this.id = Data.SaveToDB(this);
            // create new list if argument is null
            this.customers = p.customers ?? new List<CCustomer>();
            // link each loan from list to this customer
            foreach (CCustomer customer in this.customers)
                customer.Partner = this;
        }

        // Add new customer to list
        public virtual void AddCustomer(CCustomer customer)
        {
            customer.Partner = this;
            customers.Add(customer);
        }

        // check for fields validity
        public virtual bool IsValid()
        {
            // ICO check
            /*
            if (ICO  null)
                throw new ArgumentNullException("Name is empty!");
                */
            if (Name == null)
                throw new ArgumentNullException("Name is empty!");
            // some file check
            return true;
        }
    }
}