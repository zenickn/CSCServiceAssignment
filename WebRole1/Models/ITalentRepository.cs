using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRole1.Models
{
    interface ITalentRepository
    {
        IEnumerable<Talent> GetAllTalents();
    }
}
