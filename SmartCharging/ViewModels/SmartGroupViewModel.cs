using System.ComponentModel.DataAnnotations;

namespace SmartCharging.API.ViewModels
{
    public class SmartGroupViewModel
    {
        public SmartGroupViewModel()
        {
            Name = string.Empty;
            CapacityInAmps = 0;
            ChargeStationViewModels = new List<ChargeStationViewModel>();
        }

        public Guid SmartGroupId { get; set; }

        public string Name { get; set; }

        public int CapacityInAmps { get; set; }

        public List<ChargeStationViewModel> ChargeStationViewModels { get; set; }
    }
}
