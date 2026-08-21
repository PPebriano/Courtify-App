using CourtifyBE.DTOs;
using CourtifyBE.Models;

namespace CourtifyBE.Services
{
    public interface IEquipmentService
    {
        Task<List<EquimentAddOnsResponse>> GetAllAsync(string? search, int page, int limit);
        EquimentAddOnsResponse ToDetailResponse(EquipmentAddOns? equipmentAddOns);
    }
}
