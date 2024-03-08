using System.ComponentModel.DataAnnotations;

namespace SmartCharging.API.Domain.Models
{
    public class SmartGroup
    {
        public SmartGroup()
        {
            Name = string.Empty;
            CapacityInAmps = 0;
        }  

        public Guid SmartGroupId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int CapacityInAmps { get; set; }

        public List<ChargeStation> ChargeStations { get; set; } = new List<ChargeStation>();
    }
}
