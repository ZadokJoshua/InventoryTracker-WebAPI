namespace InventoryTracker.WebAPI.Services;

public interface ICsvService
{
    void WriteCSV<T>(IEnumerable<T> records);
}
