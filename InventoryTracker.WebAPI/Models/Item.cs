using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryTracker.WebAPI.Models
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(150)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [MaxLength(15)]
        public string SerialNumber { get; set; } = string.Empty;

        [Precision(18, 2)]
        public decimal Value { get; set; }

        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
    }
}
