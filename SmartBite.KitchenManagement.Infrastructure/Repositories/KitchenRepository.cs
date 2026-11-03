using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartBite.KitchenManagement.Application.Common.Exceptions;
using SmartBite.KitchenManagement.Domain.Entities;
using SmartBite.KitchenManagement.Domain.Interfaces;
using SmartBite.KitchenManagement.Infrastructure.Persistance;
using System.Net;

namespace SmartBite.KitchenManagement.Infrastructure.Repositories
{
    public class KitchenRepository(KitchenDbContext context, ILogger<KitchenRepository> logger) : IKitchenRepository
    {
        private readonly KitchenDbContext _context = context;
        private readonly ILogger<KitchenRepository> _logger = logger;

        public async Task<List<Kitchens>?> GetAllKitchensAsync(CancellationToken cancellation)
        {
            try
            {
                return await _context.Kitchens.AsNoTracking().ToListAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred {className} - {methodName} while fetching all kitchen information.", nameof(KitchenRepository), nameof(GetAllKitchensAsync));
                return null;
            }
        }

        public async Task<Kitchens?> GetKitchenAsync(int kitchenId, CancellationToken cancellation)
        {
            try
            {
                return await _context.Kitchens.AsNoTracking().FirstOrDefaultAsync(k => k.KitchenId == kitchenId, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred {className} - {methodName} while fetching kitchen information for KitchenId: {kitchenId}.", nameof(KitchenRepository), nameof(GetKitchenAsync), kitchenId);
                return null;
            }
        }

        public async Task<Kitchens?> CreateKitchenAsync(Kitchens kitchen, CancellationToken cancellation)
        {
            try
            {
                await _context.Kitchens.AddAsync(kitchen, cancellation);
                await _context.SaveChangesAsync(cancellation);
                return kitchen;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred {className} - {methodName} while creating kitchen information.", nameof(KitchenRepository), nameof(CreateKitchenAsync));
                return null;
            }
        }

        public async Task<bool> UpdateKitchenAsync(Kitchens kitchen, CancellationToken cancellation)
        {
            try
            {
                var isExist = await _context.Kitchens.AnyAsync(k => k.KitchenId == kitchen.KitchenId, cancellation);
                if (!isExist)
                {
                    _logger.LogWarning("{className} - {methodName} Kitchen information with KitchenId: {kitchenId} does not exist. Update operation aborted.", nameof(KitchenRepository), nameof(UpdateKitchenAsync), kitchen.KitchenId);
                    throw new BusinessException($"Kitchen information with KitchenId: {kitchen.KitchenId} does not exist. Update operation aborted.", HttpStatusCode.NotFound);
                }
                _context.Kitchens.Update(kitchen);
                await _context.SaveChangesAsync(cancellation);
                return true;
            }
            catch (BusinessException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred {className} - {methodName} while updating kitchen information.", nameof(KitchenRepository), nameof(UpdateKitchenAsync));
                return false;
            }
        }

        public async Task<bool> DeleteKitchenAsync(int kitchenId, CancellationToken cancellation)
        {
            try
            {
                var kitchen = await _context.Kitchens.FirstOrDefaultAsync(k => k.KitchenId == kitchenId, cancellation);
                if (kitchen == null)
                {
                    _logger.LogWarning("{className} - {methodName} Kitchen information with KitchenId: {kitchenId} does not exist. Delete operation aborted.", nameof(KitchenRepository), nameof(DeleteKitchenAsync), kitchenId);
                    throw new BusinessException($"Kitchen information with KitchenId: {kitchenId} does not exist. Delete operation aborted.", HttpStatusCode.NotFound);
                }

                _context.Kitchens.Remove(kitchen);
                await _context.SaveChangesAsync(cancellation);
                return true;
            }
            catch (BusinessException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred {className} - {methodName} while deleting kitchen information.", nameof(KitchenRepository), nameof(DeleteKitchenAsync));
                return false;
            }
        }
    }
}
