using CsvHelper.Configuration;
using InventoryTracker.WebAPI.Models;

namespace InventoryTracker.WebAPI.Profiles;

public class ItemClassMap : ClassMap<Item>
{
    public ItemClassMap()
    {
        Map(i => i.Id).Index(0).Name("id");
        Map(i => i.Name).Index(1).Name("item_name");
        Map(i => i.SerialNumber).Index(2).Name("serial_number");
        Map(i => i.Value).Index(3).Name("value($)");
    }
}

