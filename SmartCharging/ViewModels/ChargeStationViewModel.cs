using SmartCharging.API.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace SmartCharging.API.ViewModels
{
    public class ChargeStationViewModel
    {
        public ChargeStationViewModel()
        {
            Name = string.Empty;
        }

        public Guid ChargeStationId { get; set; }
        public Guid SmartGroupId { get; set; }

        public string Name { get; set; }

        public List<ConnectorViewModel> Connectors { get; set; }
    }
}
