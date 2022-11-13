using Microsoft.EntityFrameworkCore;

namespace InventoryTracker.WebAPI.DTOs;

public class ItemDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? SerialNumber { get; set; }
    public decimal Value { get; set; }
}
