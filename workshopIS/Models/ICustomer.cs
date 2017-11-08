using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace workshopIS.Models
{
    public interface ICustomer
    {
        int Id { get; set; }
        string Phone { get; set; }

        List<CLoan> Loans { get; set; }

        int? ContactState { get; set; }
        DateTime? CreationDate { get; set; }
        [JsonIgnore]
        CPartner Partner { get; set; }

        // optional fields
        string FirstName { get; set; }
        string Surname { get; set; }
        string Email { get; set; }

        void AddLoan(CLoan loan);
    }
}
