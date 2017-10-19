using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace workshopIS.Models
{
    public class PutCustomer 
    {

        private int? id;
        private int? state;


        public virtual int? Id { get => id; set => id = value; }

        public virtual int? State { get => state; set => state = value; }

        
    }
}