using SmartCharging.API.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace SmartCharging.API.ViewModels
{
    public class ConnectorViewModel
    {
        public Guid ChargeStationId { get; set; }
        public int ConnectorId { get; set; }
        public int MaxCurrentInAmps { get; set; }
    }
}
