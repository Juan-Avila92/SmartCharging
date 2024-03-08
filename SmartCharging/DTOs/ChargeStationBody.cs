using SmartCharging.API.Domain.Models;

namespace SmartCharging.API.DTOs
{
    public class ChargeStationBody
    {
        public ChargeStationBody() 
        { 
            Name = string.Empty;
        }
        public string Name { get; set; }
        public List<ConnectorDTO> Connectors { get; set; }
    }
}
