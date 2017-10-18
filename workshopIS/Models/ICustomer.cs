using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace workshopIS.Models
{
    interface ICustomer
    {
        int Id { get; set; }
        int Phone { get; set; }
        ECallState ContactState { get; set; }
        IPartner Partner { get; set; }

        // optional
        string Name { get; set; }
        string Email { get; set; }
        string Note { get; set; }
    }
}
