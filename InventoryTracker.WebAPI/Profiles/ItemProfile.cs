using AutoMapper;
using InventoryTracker.WebAPI.DTOs;
using InventoryTracker.WebAPI.Models;

namespace InventoryTracker.WebAPI.Profiles;

public class ItemProfile : Profile
{
	public ItemProfile()
	{
		CreateMap<Item, ItemDto>();
		CreateMap<CreateItemDto, Item>();
		CreateMap<UpdateItemDto, Item>();
	}
}
