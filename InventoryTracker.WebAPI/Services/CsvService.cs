using CsvHelper;
using InventoryTracker.WebAPI.Models;
using InventoryTracker.WebAPI.Profiles;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;

namespace InventoryTracker.WebAPI.Services;

public class CsvService : ICsvService
{
    private readonly string csvPath = $"my-inventory-{DateTime.Now.ToFileTime()}.csv";

    /// <summary>
    /// CSV write method
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="records">Records is a list of an object from the database.</param>
    public void WriteCSV<T>(IEnumerable<T> records)
    {
        using (StreamWriter writer = new StreamWriter(csvPath))
        {
            using (CsvWriter csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csvWriter.Context.RegisterClassMap<ItemClassMap>();
                csvWriter.WriteRecords(records);
            }
        }
    }
}

