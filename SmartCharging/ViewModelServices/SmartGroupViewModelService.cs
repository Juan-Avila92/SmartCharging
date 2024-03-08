using SmartCharging.API.Domain.Contracts;
using SmartCharging.API.Domain.Models;
using SmartCharging.API.ViewModels;
using SmartCharging.API.ViewModelServices.Contracts;

namespace SmartCharging.API.ViewModelServices
{
    public class SmartGroupViewModelService : ISmartGroupViewModelService
    {
        public List<SmartGroupViewModel> ConvertToViewModel(List<SmartGroup> smartGroups)
        {
            return smartGroups.Select(smartGroup => new SmartGroupViewModel()
            {
                Name = smartGroup.Name,
                SmartGroupId = smartGroup.SmartGroupId,
                CapacityInAmps = smartGroup.CapacityInAmps,
                ChargeStationViewModels = smartGroup.ChargeStations.Select(chargeStation => new ChargeStationViewModel()
                {
                    SmartGroupId = chargeStation.SmartGroupId,
                    ChargeStationId = chargeStation.ChargeStationId,
                    Name = chargeStation.Name,
                    Connectors = chargeStation.Connectors.Select(connector => new ConnectorViewModel()
                    {
                        ChargeStationId = connector.ChargeStationId,
                        MaxCurrentInAmps = connector.MaxCurrentInAmps,
                        ConnectorId = connector.ConnectorId
                    }).ToList(),
                }).ToList()
            }).ToList();
        }
    }
}
