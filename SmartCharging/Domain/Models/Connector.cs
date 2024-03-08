using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCharging.API.Domain.Models
{
    
    public class Connector
    {
        public Connector()
        {
            MaxCurrentInAmps = 0;
        }
        public Guid ChargeStationId { get; set; }
        public int ConnectorId { get; set; }
        public int MaxCurrentInAmps  { get; set; }
    }
}
