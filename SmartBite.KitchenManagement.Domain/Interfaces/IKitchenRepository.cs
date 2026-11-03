using SmartBite.KitchenManagement.Domain.Entities;

namespace SmartBite.KitchenManagement.Domain.Interfaces
{
    public interface IKitchenRepository
    {
        Task<List<Kitchens>?> GetAllKitchensAsync(CancellationToken cancellation);
        Task<Kitchens?> GetKitchenAsync(int kitchenId, CancellationToken cancellation);
        Task<Kitchens?> CreateKitchenAsync(Kitchens kitchen, CancellationToken cancellation);
        Task<bool> UpdateKitchenAsync(Kitchens kitchen, CancellationToken cancellation);
        Task<bool> DeleteKitchenAsync(int kitchenId, CancellationToken cancellation);

    }
}
