using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCharging.Domain.Models
{
    public class SmartGroup
    {
        public SmartGroup()
        {
            Name = string.Empty;
            ChargeStation = new List<ChargeStation>();
            CapacityInAmps = 0;
        }  

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int CapacityInAmps { get; set; }
        public List<ChargeStation> ChargeStation { get; set; }
    }
}
