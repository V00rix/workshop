using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace workshopIS.Models
{
    public interface ILoan
    {
        int Id { get; set; }
        int? Duration { get; set; }
        decimal? Amount { get; set; }
        decimal? Interest { get; set; }
        decimal? MonthlyCharge { get; set; }
        decimal? APR { get; set; }
        string Note { get; set; }
        CCustomer Customer { get; set; }
    }
}
