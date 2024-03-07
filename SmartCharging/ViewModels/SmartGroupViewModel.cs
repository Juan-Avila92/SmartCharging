using System.ComponentModel.DataAnnotations;

namespace SmartCharging.API.ViewModels
{
    public class SmartGroupViewModel
    {
        public SmartGroupViewModel()
        {
            Name = string.Empty;
            CapacityInAmps = 0;
        }

        public Guid SmartGroupId { get; set; }

        public string Name { get; set; }

        public int CapacityInAmps { get; set; }
    }
}
