using InventoryTracker.WebAPI.Models;

namespace InventoryTracker.WebAPI.Services;

public interface IItemRepository
{
    Task<IEnumerable<Item>> GetItemsAsync(string searchQuery);
    Task<Item?> GetItemByIdAsync(int id);
    void AddItemAsync(Item item);
    void DeleteItem(Item item);
    Task<bool> SaveChangesAsync();
}
