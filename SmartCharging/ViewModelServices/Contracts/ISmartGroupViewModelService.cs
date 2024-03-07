using SmartCharging.API.Domain.Models;
using SmartCharging.API.ViewModels;

namespace SmartCharging.API.ViewModelServices.Contracts
{
    public interface ISmartGroupViewModelService
    {
        List<SmartGroupViewModel> ConvertToViewModel(List<SmartGroup> smartGroups);
    }
}
