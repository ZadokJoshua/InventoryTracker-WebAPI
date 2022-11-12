using System.ComponentModel.DataAnnotations;

namespace InventoryTracker.WebAPI.DTOs
{
    public class CreateItemDto
    {

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [StringLength(150)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [StringLength(15)]
        public string SerialNumber { get; set; } = string.Empty;

        public decimal Value { get; set; } = 0.00M;
    }
}
