using Microsoft.AspNetCore.Mvc;
using SmartBite.KitchenManagement.Application.Common.Wrapper;
using SmartBite.KitchenManagement.Application.Dtos;
using SmartBite.KitchenManagement.Application.Interfaces;

namespace SmartBite.KitchenManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KitchenController(IKitchenService kitchenService, ILogger<KitchenController> logger) : ControllerBase
    {
        private readonly IKitchenService _kitchenService = kitchenService;
        private readonly ILogger<KitchenController> _logger = logger;

        [HttpGet("GetAllKitchens")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SBResponse<List<KitchenDto>?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SBResponse<List<KitchenDto>?>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SBResponse<List<KitchenDto>?>>> GetAllKitchens(CancellationToken cancellation)
        {
            try
            {
                var result = await _kitchenService.GetAllKitchens(cancellation);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    _logger.LogError("An error occurred while fetching all kitchen information: {errorMessage}", result.Message);
                    return StatusCode((int)result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching all kitchen information.");
                return StatusCode(StatusCodes.Status500InternalServerError, SBResponse<List<KitchenDto>?>.Failure("An unexpected error occurred.", System.Net.HttpStatusCode.InternalServerError));
            }
        }

        [HttpGet("GetKitchen/{kitchenId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SBResponse<KitchenDto?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SBResponse<KitchenDto?>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SBResponse<KitchenDto?>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SBResponse<KitchenDto?>>> GetKitchen(int kitchenId, CancellationToken cancellation)
        {
            if (kitchenId <= 0)
            {
                _logger.LogWarning("Invalid kitchenId provided: {kitchenId}. It must be a positive integer.", kitchenId);
                return BadRequest(SBResponse<KitchenDto?>.Failure("Invalid kitchenId. It must be a positive integer.", System.Net.HttpStatusCode.BadRequest));
            }
            try
            {
                var result = await _kitchenService.GetKitchen(kitchenId, cancellation);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    _logger.LogError("An error occurred while fetching kitchen information for KitchenId: {kitchenId}. Error: {errorMessage}", kitchenId, result.Message);
                    return StatusCode((int)result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching kitchen information for KitchenId: {kitchenId}.", kitchenId);
                return StatusCode(StatusCodes.Status500InternalServerError, SBResponse<KitchenDto?>.Failure("An unexpected error occurred.", System.Net.HttpStatusCode.InternalServerError));
            }
        }

        [HttpPost("CreateKitchen")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SBResponse<KitchenDto?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SBResponse<KitchenDto?>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SBResponse<KitchenDto?>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SBResponse<KitchenDto?>>> CreateKitchen(KitchenDto dto, CancellationToken cancellation)
        {
            if (dto == null || !ModelState.IsValid)
            {
                _logger.LogWarning("Received invalid KitchenDto for creation.");
                return BadRequest(SBResponse<KitchenDto?>.Failure("KitchenDto is invalid.", System.Net.HttpStatusCode.BadRequest));
            }
            try
            {
                var result = await _kitchenService.CreateKitchen(dto, cancellation);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    _logger.LogError("An error occurred while creating kitchen information: {errorMessage}", result.Message);
                    return StatusCode((int)result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while creating kitchen information.");
                return StatusCode(StatusCodes.Status500InternalServerError, SBResponse<KitchenDto?>.Failure("An unexpected error occurred.", System.Net.HttpStatusCode.InternalServerError));
            }
        }

        [HttpPut("UpdateKitchen")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SBResponse<bool>>> UpdateKitchen(KitchenDto dto, CancellationToken cancellation)
        {
            if (dto == null || !ModelState.IsValid)
            {
                _logger.LogWarning("Received invalid KitchenDto for update.");
                return BadRequest(SBResponse<bool>.Failure("KitchenDto is invalid.", System.Net.HttpStatusCode.BadRequest));
            }
            try
            {
                var result = await _kitchenService.UpdateKitchen(dto, cancellation);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    _logger.LogError("An error occurred while updating kitchen information: {errorMessage}", result.Message);
                    return StatusCode((int)result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while updating kitchen information.");
                return StatusCode(StatusCodes.Status500InternalServerError, SBResponse<bool>.Failure("An unexpected error occurred.", System.Net.HttpStatusCode.InternalServerError));
            }
        }

        [HttpDelete("DeleteKitchen/{kitchenId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SBResponse<bool>>> DeleteKitchen(int kitchenId, CancellationToken cancellation)
        {
            if (kitchenId <= 0)
            {
                _logger.LogWarning("Invalid kitchenId provided for deletion: {kitchenId}. It must be a positive integer.", kitchenId);
                return BadRequest(SBResponse<bool>.Failure("Invalid kitchenId. It must be a positive integer.", System.Net.HttpStatusCode.BadRequest));
            }
            try
            {
                var result = await _kitchenService.DeleteKitchen(kitchenId, cancellation);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    _logger.LogError("An error occurred while deleting kitchen information for KitchenId: {kitchenId}. Error: {errorMessage}", kitchenId, result.Message);
                    return StatusCode((int)result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while deleting kitchen information for KitchenId: {kitchenId}.", kitchenId);
                return StatusCode(StatusCodes.Status500InternalServerError, SBResponse<bool>.Failure("An unexpected error occurred.", System.Net.HttpStatusCode.InternalServerError));
            }
        }

    }
}
