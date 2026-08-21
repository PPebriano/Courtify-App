using CourtifyBE.DTOs;
using CourtifyBE.Models;
using CourtifyBE.Repositories;

namespace CourtifyBE.Services
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IRepository<EquipmentAddOns> _equipmentRepository;

        public EquipmentService(IRepository<EquipmentAddOns> equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;
        }

        public async Task<List<EquimentAddOnsResponse>>GetAllAsync(string? search, int page, int limit)
        {
            var equipments = await _equipmentRepository.GetAllAsync();
            if (!string.IsNullOrEmpty(search))
            {
                equipments = equipments
                    .Where(m => m.ItemName.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            var pagedData = equipments
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToList();

            var response = pagedData.Select(e => ToDetailResponse(e)).ToList();

            return response;

        }
        public EquimentAddOnsResponse ToDetailResponse(EquipmentAddOns equipmentAddOns)
        {
            return new EquimentAddOnsResponse
            {
                Id = equipmentAddOns.Id,
                ItemName = equipmentAddOns.ItemName,
                RentalFee = equipmentAddOns.RentalFee,
                Stock = equipmentAddOns.Stock
            };
        }
    }
}
