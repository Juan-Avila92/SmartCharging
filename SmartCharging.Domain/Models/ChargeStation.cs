using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCharging.Domain.Models
{
    public class ChargeStation
    {
        public ChargeStation()
        {
            Name = string.Empty;
        }  

        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
