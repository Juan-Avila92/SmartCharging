namespace SmartCharging.API.Domain.Models
{
    public class ChargeStation
    {
        public ChargeStation()
        {
            Name = string.Empty;
            Connectors = 1;
        }

        public Guid ChargeStationId { get; set; }
        public Guid SmartGroupId { get; set; }
        public string Name { get; set; }
        public int Connectors { get; set; }
    }
}
