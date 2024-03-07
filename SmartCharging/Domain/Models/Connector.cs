namespace SmartCharging.API.Domain.Models
{
    public class Connector
    {
        public Connector()
        {
            MaxCurrentInAmps = 0;
            ConnectorNumber = 1;
        }

        public Guid ChargeStationId { get; set; }
        public int MaxCurrentInAmps  { get; set; }
        public int ConnectorNumber { get; set; }
    }
}
