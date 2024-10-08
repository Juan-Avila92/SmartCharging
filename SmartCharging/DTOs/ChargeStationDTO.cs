﻿using SmartCharging.API.Domain.Models;

namespace SmartCharging.API.DTOs
{
    public class ChargeStationDTO
    {
        public ChargeStationDTO() 
        { 
            Name = string.Empty;
            Connectors = new List<Connector>();
        }
        public string Name { get; set; }

        public List<Connector> Connectors { get; set; }
    }
}
