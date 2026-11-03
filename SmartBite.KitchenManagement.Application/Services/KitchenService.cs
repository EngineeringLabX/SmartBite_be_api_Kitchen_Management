using AutoMapper;
using Microsoft.Extensions.Logging;
using SmartBite.KitchenManagement.Application.Common.Exceptions;
using SmartBite.KitchenManagement.Application.Common.Wrapper;
using SmartBite.KitchenManagement.Application.Dtos;
using SmartBite.KitchenManagement.Application.Interfaces;
using SmartBite.KitchenManagement.Domain.Entities;
using SmartBite.KitchenManagement.Domain.Interfaces;
using System.Net;

namespace SmartBite.KitchenManagement.Application.Services
{
    public class KitchenService(IKitchenRepository repository, IMapper mapper, ILogger<KitchenService> logger) : IKitchenService
    {
        private readonly IKitchenRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<KitchenService> _logger = logger;

        public async Task<SBResponse<List<KitchenDto>?>> GetAllKitchens(CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("{className} - {methodName} Fetching Kitchen information ", nameof(KitchenService), nameof(GetAllKitchens));
                var result = await _repository.GetAllKitchensAsync(cancellation);
                if (result == null || result.Count <= 0)
                {
                    return SBResponse<List<KitchenDto>?>.Success(new List<KitchenDto>(), "No Records Found");
                }
                return SBResponse<List<KitchenDto>?>.Success(_mapper.Map<List<KitchenDto>>(result), "Kitchen information fetched successfully.");
            }
            catch (BusinessException bEx)
            {
                _logger.LogError(bEx, "An Error Occured {className} - {methodName} While Fetching Kitchen information ", nameof(KitchenService), nameof(GetAllKitchens));
                return SBResponse<List<KitchenDto>?>.Failure(bEx.Message, bEx.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An Error Occured {className} - {methodName} While Fetching Kitchen information ", nameof(KitchenService), nameof(GetAllKitchens));
                return SBResponse<List<KitchenDto>?>.Failure(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<SBResponse<KitchenDto?>> GetKitchen(int kitchenId, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("{className} - {methodName} Fetching Kitchen information for KitchenId: {kitchenId}", nameof(KitchenService), nameof(GetKitchen), kitchenId);
                var result = await _repository.GetKitchenAsync(kitchenId, cancellation);
                if (result == null)
                {
                    return SBResponse<KitchenDto?>.Success(null, "No Records Found");
                }
                return SBResponse<KitchenDto?>.Success(_mapper.Map<KitchenDto>(result), "Kitchen information fetched successfully.");
            }
            catch (BusinessException bEx)
            {
                _logger.LogError(bEx, "An Error Occured {className} - {methodName} While Fetching Kitchen information ", nameof(KitchenService), nameof(GetKitchen));
                return SBResponse<KitchenDto?>.Failure(bEx.Message, bEx.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An Error Occured {className} - {methodName} While Fetching Kitchen information ", nameof(KitchenService), nameof(GetKitchen));
                return SBResponse<KitchenDto?>.Failure(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<SBResponse<KitchenDto?>> CreateKitchen(KitchenDto dto, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("{className} - {methodName} Creating Kitchen information ", nameof(KitchenService), nameof(CreateKitchen));
                var result = await _repository.CreateKitchenAsync(_mapper.Map<Kitchens>(dto), cancellation);
                if (result == null)
                {
                    return SBResponse<KitchenDto?>.Failure("Failed to create kitchen information.", HttpStatusCode.InternalServerError);
                }
                return SBResponse<KitchenDto?>.Success(_mapper.Map<KitchenDto>(result), "Kitchen information created successfully.");
            }
            catch (BusinessException bEx)
            {
                _logger.LogError(bEx, "An Error Occured {className} - {methodName} While Creating Kitchen information ", nameof(KitchenService), nameof(CreateKitchen));
                return SBResponse<KitchenDto?>.Failure(bEx.Message, bEx.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An Error Occured {className} - {methodName} While Creating Kitchen information ", nameof(KitchenService), nameof(CreateKitchen));
                return SBResponse<KitchenDto?>.Failure(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<SBResponse<bool>> UpdateKitchen(KitchenDto dto, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("{className} - {methodName} Updating Kitchen information ", nameof(KitchenService), nameof(UpdateKitchen));
                var result = await _repository.UpdateKitchenAsync(_mapper.Map<Kitchens>(dto), cancellation);
                if (!result)
                {
                    return SBResponse<bool>.Failure("Failed to update kitchen information.", HttpStatusCode.InternalServerError);
                }
                return SBResponse<bool>.Success(result, "Kitchen information updated successfully.");
            }
            catch (BusinessException bEx)
            {
                _logger.LogError(bEx, "An Error Occured {className} - {methodName} While Updating Kitchen information ", nameof(KitchenService), nameof(UpdateKitchen));
                return SBResponse<bool>.Failure(bEx.Message, bEx.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An Error Occured {className} - {methodName} While Updating Kitchen information ", nameof(KitchenService), nameof(UpdateKitchen));
                return SBResponse<bool>.Failure(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<SBResponse<bool>> DeleteKitchen(int kitchenId, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("{className} - {methodName} Deleting Kitchen information for KitchenId: {kitchenId}", nameof(KitchenService), nameof(DeleteKitchen), kitchenId);
                var result = await _repository.DeleteKitchenAsync(kitchenId, cancellation);
                if (!result)
                {
                    return SBResponse<bool>.Failure("Failed to delete kitchen information.", HttpStatusCode.InternalServerError);
                }
                return SBResponse<bool>.Success(result, "Kitchen information deleted successfully.");
            }
            catch (BusinessException bEx)
            {
                _logger.LogError(bEx, "An Error Occured {className} - {methodName} While Deleting Kitchen information ", nameof(KitchenService), nameof(DeleteKitchen));
                return SBResponse<bool>.Failure(bEx.Message, bEx.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An Error Occured {className} - {methodName} While Deleting Kitchen information ", nameof(KitchenService), nameof(DeleteKitchen));
                return SBResponse<bool>.Failure(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
