using System.Text.Json;
using System.Text.Json.Serialization;
using basketball_calendar.Models;

namespace basketball_calendar.Services;

/// <summary>
/// Spravuje ukládání a načítání událostí do/z JSON souboru.
/// </summary>
public class EventRepository
{
    private string FilePath { get; }
    private JsonSerializerOptions JsonOptions { get; }

    /// <summary>
    /// Konstruktor repository.
    /// </summary>
    public EventRepository()
    {
        FilePath = "events.json";
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
            {
                return new List<Event>();
            }

            var json = File.ReadAllText(FilePath);

            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<Event>();
            }

            var events = JsonSerializer.Deserialize<List<Event>>(json, JsonOptions);
            return events ?? new List<Event>();
        }
        catch (FileNotFoundException)
        {
            return new List<Event>();
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Chyba při deserializaci JSON: {ex.Message}");
            // Při chybě formátu JSON raději vrátíme prázdný seznam než chybný
            return new List<Event>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Neočekávaná chyba při načítání událostí: {ex.Message}");
            return new List<Event>();
        }
    }

    /// <summary>
    /// Uloží všechny události do souboru.
    /// </summary>
    private void SaveEvents(List<Event> events)
    {
        try
        {
            // Zajistíme, že events není null
            var eventsToSave = events ?? new List<Event>();

            // Vytvoříme adresář, pokud neexistuje
            var directory = Path.GetDirectoryName(FilePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var json = JsonSerializer.Serialize(eventsToSave, JsonOptions);

            // Vytvoříme záložní soubor před zápisem
            if (File.Exists(FilePath))
            {
                File.Copy(FilePath, FilePath + ".bak", true);
            }

            File.WriteAllText(FilePath, json);
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"Chyba přístupu při ukládání událostí: {ex.Message}");
            throw new InvalidOperationException("Nedostatečná oprávnění pro zápis do souboru.", ex);
        }
        catch (IOException ex)
        {
            Console.WriteLine($"I/O chyba při ukládání událostí: {ex.Message}");
            throw new InvalidOperationException("Soubor je používán jiným procesem nebo nelze vytvořit adresář.", ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Neočekávaná chyba při ukládání událostí: {ex.Message}");
            throw new InvalidOperationException("Chyba při ukládání událostí.", ex);
        }
    }

    /// <summary>
    /// Přidá novou událost a uloží změny.
    /// </summary>
    public void AddEvent(Event newEvent)
    {
        var events = LoadEvents();
        events.Add(newEvent);
        SaveEvents(events);
    }

    /// <summary>
    /// Aktualizuje existující událost a uloží změny.
    /// </summary>
    public void UpdateEvent(Event updatedEvent)
    {
        var events = LoadEvents();

        Console.WriteLine($"Hledám událost s ID: {updatedEvent.Id}");
        bool found = false;

        for (int i = 0; i < events.Count; i++)
        {
            Console.WriteLine($"Kontroluji událost {i}: ID={events[i].Id}, Název={events[i].Title}");
            Console.WriteLine($"Porovnání: {events[i].Id == updatedEvent.Id}");
            Console.WriteLine($"ToString porovnání: {events[i].Id.ToString() == updatedEvent.Id.ToString()}");

            // Porovnání podle řetězcového zápisu místo přímého porovnání objektů
            if (events[i].Id.ToString() == updatedEvent.Id.ToString())
            {
                Console.WriteLine($"Nalezena shoda ID (podle ToString): {events[i].Id}");
                events[i] = updatedEvent;
                SaveEvents(events);
                found = true;
                break;
            }
        }

        if (!found)
        {
            Console.WriteLine("Žádná událost s odpovídajícím ID nebyla nalezena");
        }
    }

    /// <summary>
    /// Odstraní událost podle Id a uloží změny.
    /// </summary>
    public void DeleteEvent(Guid id)
    {
        List<Event> events = LoadEvents();
        string idString = id.ToString();
        Event? eventToRemove = events.FirstOrDefault(@event => @event.Id.ToString() == idString);

        if (eventToRemove != null)
        {
            events.Remove(eventToRemove);
            SaveEvents(events);
        }
        else
        {
            Console.WriteLine($"Událost s ID {id} nebyla nalezena při mazání");
        }
    }
}