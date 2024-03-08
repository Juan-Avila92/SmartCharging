using SmartCharging.API.Domain.Contracts;
using SmartCharging.API.Domain.Models;
using SmartCharging.API.ViewModels;
using SmartCharging.API.ViewModelServices.Contracts;

namespace SmartCharging.API.ViewModelServices
{
    public class ChargeStationViewModelService : IChargeStationViewModelService
    {
        public List<ChargeStationViewModel> ConvertToViewModel(List<ChargeStation> chargeStations)
        {
            return chargeStations.Select(chargeStation => new ChargeStationViewModel()
            {
                Name = chargeStation.Name,
                SmartGroupId = chargeStation.SmartGroupId,
                ChargeStationId = chargeStation.ChargeStationId,
                Connectors = chargeStation.Connectors.Select
                (
                    x => new ConnectorViewModel()
                    {
                        ChargeStationId = x.ChargeStationId,
                        ConnectorId = x.ConnectorId,
                        MaxCurrentInAmps = x.MaxCurrentInAmps
                    }
                ).ToList(),
            }).ToList();
        }
    }
}
