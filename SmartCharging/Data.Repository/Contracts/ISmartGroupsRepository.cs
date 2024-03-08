using SmartCharging.API.Domain.Models;
using System.Linq.Expressions;

namespace SmartCharging.API.Data.Repository.Contracts
{
    public interface ISmartGroupsRepository
    {
        List<SmartGroup> GetAllWithChargeStations();
        List<ChargeStation> GetChargeStationsById(Guid id);
        int GetSumOfAllConnectorsAmpValuesById(Guid id);
    }
}
