using InventoryTracker.WebAPI.DbContexts;
using InventoryTracker.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryTracker.WebAPI.Services;

public class ItemRepository : IItemRepository
{
    private readonly InventoryTrackerDbContext _context;

    public ItemRepository(InventoryTrackerDbContext context)
    {
        _context = context;
    }
    public void AddItemAsync(Item item)
    {
        _context.Items.Add(item);
    }

    public void DeleteItem(Item item)
    {
        _context.Items.Remove(item);
    }

    public async Task<Item?> GetItemByIdAsync(int id)
    {
        return await _context.Items.Where(i => i.Id.Equals(id)).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Item>> GetItemsAsync(string searchQuery)
    {
        var collection = _context.Items as IQueryable<Item>;

        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            collection = collection.Where(i => i.Name.Contains(searchQuery) || (i.Description != null && i.Description.Contains(searchQuery)));
        }

        return await collection.OrderBy(i => i.Name).ToListAsync();
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() >= 0;
    }
}
