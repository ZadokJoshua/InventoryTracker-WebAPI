using InventoryTracker.WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace InventoryTracker.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FilesController : ControllerBase
{
    private readonly ICsvService _csvService;
    private readonly IItemRepository _itemRepository;

    public FilesController(ICsvService csvService, IItemRepository itemRepository)
    {
        _csvService = csvService;
        _itemRepository = itemRepository;
    }

    [HttpGet("write-items-csv")]
    public async Task<IActionResult> GetItems()
    {
        var items = await _itemRepository.GetItemsAsync(searchQuery: String.Empty);
        _csvService.WriteCSV(items);
        return Ok(items);
    }
}
