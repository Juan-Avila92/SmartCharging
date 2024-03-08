namespace SmartCharging.API.DTOs
{
    public class ConnectorDTO
    {
        public ConnectorDTO() 
        {
            MaxCurrentInAmps = 0;
        }
        public int MaxCurrentInAmps { get; set; }
    }
}
