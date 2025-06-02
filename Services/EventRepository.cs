using System.Text.Json;
using System.Text.Json.Serialization;
using basketball_calendar.Models;

namespace basketball_calendar.Services;

/// <summary>
/// Manages saving and loading events to/from a JSON file.
/// </summary>
public class EventRepository
{
    private string FilePath { get; }
    private JsonSerializerOptions JsonOptions { get; }

    /// <summary>
    /// Repository constructor.
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
    /// Loads all events from the file.
    /// If the file doesn't exist or is empty, returns an empty list.
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
            Console.WriteLine($"Error deserializing JSON: {ex.Message}");
            return new List<Event>();
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Unexpected error loading events: {exception.Message}");
            return new List<Event>();
        }
    }

    /// <summary>
    /// Saves all events to the file.
    /// </summary>
    private void SaveEvents(List<Event> events)
    {
        try
        {
            var eventsToSave = events;

            var directory = Path.GetDirectoryName(FilePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var json = JsonSerializer.Serialize(eventsToSave, JsonOptions);

            if (File.Exists(FilePath))
            {
                File.Copy(FilePath, FilePath + ".bak", true);
            }

            File.WriteAllText(FilePath, json);
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"Access error when saving events: {ex.Message}");
            throw new InvalidOperationException("Insufficient permissions to write to file.", ex);
        }
        catch (IOException ex)
        {
            Console.WriteLine($"I/O error when saving events: {ex.Message}");
            throw new InvalidOperationException("File is being used by another process or directory cannot be created.", ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error when saving events: {ex.Message}");
            throw new InvalidOperationException("Error saving events.", ex);
        }
    }

    /// <summary>
    /// Adds a new event and saves changes.
    /// </summary>
    public void AddEvent(Event newEvent)
    {
        var events = LoadEvents();
        events.Add(newEvent);
        SaveEvents(events);
    }

    /// <summary>
    /// Updates an existing event and saves changes.
    /// </summary>
    public void UpdateEvent(Event updatedEvent)
    {
        var events = LoadEvents();

        Console.WriteLine($"Looking for event with ID: {updatedEvent.Id}");
        bool found = false;

        for (int i = 0; i < events.Count; i++)
        {
            Console.WriteLine($"Checking event {i}: ID={events[i].Id}, Title={events[i].Title}");
            Console.WriteLine($"Comparison: {events[i].Id == updatedEvent.Id}");
            Console.WriteLine($"ToString comparison: {events[i].Id.ToString() == updatedEvent.Id.ToString()}");

            if (events[i].Id.ToString() == updatedEvent.Id.ToString())
            {
                Console.WriteLine($"ID match found (by ToString): {events[i].Id}");
                events[i] = updatedEvent;
                SaveEvents(events);
                found = true;
                break;
            }
        }

        if (!found)
        {
            Console.WriteLine("No event with matching ID was found");
        }
    }

    /// <summary>
    /// Deletes an event by Id and saves changes.
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
    }
}