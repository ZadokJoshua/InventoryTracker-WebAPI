using AutoMapper;
using InventoryTracker.WebAPI.DTOs;
using InventoryTracker.WebAPI.Models;
using InventoryTracker.WebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Runtime.InteropServices;

namespace InventoryTracker.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItemsController : ControllerBase
{
    private readonly IItemRepository _repository;
    private readonly IMapper _mapper;

    public ItemsController(IItemRepository repository, IMapper mapper)
	{
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ItemDto>>> GetItems(string? searchQuery)
    {
        var items = await _repository.GetItemsAsync(searchQuery);

        return Ok(_mapper.Map<IEnumerable<ItemDto>>(items));
    }

    [HttpGet("{itemId}", Name = "GetItem")]
    public async Task<ActionResult<ItemDto>> GetItem(int itemId)
    {
        var item = await _repository.GetItemByIdAsync(itemId);

        return item is null ? NotFound($"Item with Id {itemId} not found.") : Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> AddItem(CreateItemDto item)
    {
        var itemToAdd = _mapper.Map<Item>(item);
        _repository.AddItemAsync(itemToAdd);
        await _repository.SaveChangesAsync();

        var createdItemToReturn = _mapper.Map<ItemDto>(itemToAdd);
        return CreatedAtRoute("GetItem", new
        {
            itemId = createdItemToReturn.Id
        },
        createdItemToReturn);
    }

    [HttpPut("{itemId}")]
    public async Task<ActionResult> UpdateItem(int itemId, UpdateItemDto item)
    {
        var ItemEntity = await _repository.GetItemByIdAsync(itemId);
        if (ItemEntity is null)
        {
            return NotFound();
        }
        _mapper.Map(item, ItemEntity);
        await _repository.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{itemId}")]
    public async Task<IActionResult> DeleteItem(int itemId)
    {
        var itemEntity = await _repository.GetItemByIdAsync(itemId);

        if (itemEntity is null)
        {
            return NotFound();
        }

        _repository.DeleteItem(itemEntity);
        await _repository.SaveChangesAsync();

        return NoContent();
    }
}
