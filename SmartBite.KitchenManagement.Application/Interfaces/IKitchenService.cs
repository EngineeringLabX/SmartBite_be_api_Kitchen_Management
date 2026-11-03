using SmartBite.KitchenManagement.Application.Common.Wrapper;
using SmartBite.KitchenManagement.Application.Dtos;

namespace SmartBite.KitchenManagement.Application.Interfaces
{
    public interface IKitchenService
    {
        Task<SBResponse<List<KitchenDto>?>> GetAllKitchens(CancellationToken cancellation);
        Task<SBResponse<KitchenDto?>> GetKitchen(int kitchenId, CancellationToken cancellation);
        Task<SBResponse<KitchenDto?>> CreateKitchen(KitchenDto dto, CancellationToken cancellation);
        Task<SBResponse<bool>> UpdateKitchen(KitchenDto dto, CancellationToken cancellation);
        Task<SBResponse<bool>> DeleteKitchen(int kitchenId, CancellationToken cancellation);

    }
}
