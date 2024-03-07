namespace SmartCharging.API.DTOs
{
    public class ChargeStationDTO
    {
        public ChargeStationDTO() 
        { 
            Name = string.Empty;
            Connectors = 1;
        }
        public string Name { get; set; }
        public int Connectors { get; set; }
    }
}
