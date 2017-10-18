using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace workshopIS.Models
{
    public interface IPartner
    {
        int Id { get; set; }
        string Name { get; set; }
        int ICO { get; set; }
        string DocumentPath { get; set; }
        DateTime ValidFrom { get; set; }
        DateTime ValidTo { get; set; }
    }
}
