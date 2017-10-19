using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace workshopIS.Models
{
    public class CCustomer : ICustomer
    {
        // unique ids container
        private static List<int> ids = new List<int>();

        private int? id;
        private string phone;
        private string firstName;
        private string surname;
        private string email;
        private int? state;
        private int? partnerId;
        private DateTime creationDate;
        private List<ILoan> loans;

        public virtual int? Id { get => id; set => id = value; }
        public virtual string Phone { get => phone; set => phone = value; }
        public virtual string FirstName { get => firstName; set => firstName = value; }
        public virtual string Surname { get => surname; set => surname = value; }
        public virtual string Email { get => email; set => email = value; }
        public virtual int? State { get => state; set => state = value; }
        public virtual int? PartnerId { get => partnerId; set => partnerId = value; }
        public virtual string Name { get; }
        public virtual DateTime CreationDate { get => creationDate; set => creationDate = value; }
        public virtual List<ILoan> Loans { get => loans; set => loans = value; }

        public CCustomer() {
            id = GenerateId(); // Some Unique ID
        }

        public CCustomer(int id, int? partnerId, 
                        string phone, string firstName = "",
                        string surname = "", string email = "")
        {
            if (id == -1)
                this.id = GenerateId();
            else
            {
                // this shouldn't be assigned implicitly...
                if (ids.Contains(id))
                    throw new Exception("Customer with such id already exists!");
                this.id = id;
            }
            //id = GenerateId();
            this.partnerId = partnerId;
            this.phone = phone;
            this.firstName = firstName;
            this.surname = surname;
            loans = new List<ILoan>();
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

        public virtual void AddLoan(ILoan loan)
        {
            loans.Add(loan);
        }

    }
}