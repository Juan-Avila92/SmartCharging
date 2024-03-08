namespace SmartCharging.API.Domain.Models
{
    public class ChargeStation
    {
        public ChargeStation()
        {
            Name = string.Empty;
            Connectors = new List<Connector>();
        }

        public Guid ChargeStationId { get; set; }
        public Guid SmartGroupId { get; set; }
        public string Name { get; set; }
        public List<Connector> Connectors { get; set;}
    }
}
