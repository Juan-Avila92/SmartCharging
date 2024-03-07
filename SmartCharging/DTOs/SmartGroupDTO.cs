namespace SmartCharging.API.DTOs
{
    public class SmartGroupDTO
    {
        public SmartGroupDTO() 
        { 
            Name = string.Empty;
            
        }
        public string Name { get; set; }
        public int CapacityInAmps { get; set; }
    }
}
