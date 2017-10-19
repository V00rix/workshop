using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace workshopIS.Models
{
    public interface ICustomer
    {
        int Id { get; set; }
        string Phone { get; set; }
        List<ILoan> Loans { get; set; }

        int? State { get; set; }
        DateTime? CreationDate { get; set; }


        // optional
        string FirstName { get; set; }
        string Surname { get; set; }
        string Email { get; set; }
    }
}
