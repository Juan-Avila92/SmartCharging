using SmartCharging.API.Domain.Models;
using System.Linq.Expressions;

namespace SmartCharging.API.Data.Repository.Contracts
{
    public interface IChargeStationRepository
    {
        ChargeStation GetChargeStationByIdWithConnectors(Guid id);
        List<ChargeStation> GetAllWithConnectors();
    }
}
