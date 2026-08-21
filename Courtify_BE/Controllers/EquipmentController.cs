using CourtifyBE.Models;
using CourtifyBE.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CourtifyBE.Controllers
{
    [ApiController]
    [Route("api/equipment-add-ons")]
    public class EquipmentController
    {
        private readonly IEquipmentService _equipmentService;

        public EquipmentController(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        [HttpGet]
        public async Task<IActionResult>GetEquipment(
            [FromQuery]string? search,
            [FromQuery]int page = 1,
            [FromQuery]int limit = 10)
        {
            var equipments = await _equipmentService.GetAllAsync(search, page, limit);
            return new OkObjectResult(equipments);
        }

    }
}
