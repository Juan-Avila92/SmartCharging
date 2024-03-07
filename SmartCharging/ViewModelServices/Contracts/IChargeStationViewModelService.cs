using SmartCharging.API.Domain.Models;
using SmartCharging.API.ViewModels;

namespace SmartCharging.API.ViewModelServices.Contracts
{
    public interface IChargeStationViewModelService
    {
        List<ChargeStationViewModel> ConvertToViewModel(List<ChargeStation> chargeStations);
    }
}
