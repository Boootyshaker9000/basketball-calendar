using System.Text.Json;
using System.Text.Json.Serialization;
using basketball_calendar.Models;

namespace basketball_calendar.Services;

/// <summary>
/// Spravuje ukládání a načítání událostí do/z JSON souboru.
/// </summary>
public class EventRepository
{
    private string FilePath { get; set; }
    private JsonSerializerOptions JsonOptions { get; set; }

    /// <summary>
    /// Konstruktor repository.
    /// </summary>
    /// <param name="filePath">Cesta k JSON souboru pro ukládání událostí.</param>
    public EventRepository(string filePath)
    {
        FilePath = filePath;
        JsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters =
            {
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
            }
        };
    }

    /// <summary>
    /// Načte všechny události ze souboru.
    /// Pokud soubor neexistuje nebo je prázdný, vrátí prázdný seznam.
    /// </summary>
    public List<Event> LoadEvents()
    {
        try
        {
            if (!File.Exists(FilePath))
                return new List<Event>();

            var json = File.ReadAllText(FilePath);
            if (string.IsNullOrWhiteSpace(json))
                return new List<Event>();

            return JsonSerializer.Deserialize<List<Event>>(json, JsonOptions)
                   ?? new List<Event>();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Chyba při načítání událostí.", ex);
        }
    }

    /// <summary>
    /// Uloží všechny události do souboru.
    /// </summary>
    public void SaveEvents(List<Event> events)
    {
        try
        {
            var json = JsonSerializer.Serialize(events, JsonOptions);
            File.WriteAllText(FilePath, json);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Chyba při ukládání událostí.", ex);
        }
    }

    /// <summary>
    /// Přidá novou událost a uloží změny.
    /// </summary>
    public void AddEvent(Event newEvent)
    {
        var events = LoadEvents();
        if (newEvent != null)
        {
            events.Add(newEvent);
            SaveEvents(events);
        }
    }

    /// <summary>
    /// Aktualizuje existující událost a uloží změny.
    /// </summary>
    public void UpdateEvent(Event updatedEvent)
    {

        var events = LoadEvents();
        var index = events.FindIndex(ev => ev.Id == updatedEvent.Id);
        if (index >= 0)
        {
            events[index] = updatedEvent;
            SaveEvents(events);
        }
    }

    /// <summary>
    /// Odstraní událost podle Id a uloží změny.
    /// </summary>
    public void DeleteEvent(Guid id)
    {
        var events = LoadEvents();
        var removed = events.RemoveAll(@event => @event.Id == id);
        if (removed > 0)
        {
            SaveEvents(events);
        }
    }
}