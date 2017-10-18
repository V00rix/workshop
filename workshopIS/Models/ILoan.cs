using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace workshopIS.Models
{
    interface ILoan
    {
        int Id { get; set; }
        int Duration { get; set; }
        decimal Amount { get; set; }
        decimal Interest { get; set; }
        decimal MonthlyCharge { get; set; }
        decimal AnnualCharge { get; set; }
        ICustomer Custromer { get; set; }
    }
}
