using System.Text.Json;
using System.Text.Json.Serialization;
using basketball_calendar.Models;

namespace basketball_calendar.Services
{
    /// <summary>
    /// Manages saving and loading events to/from a JSON file.
    /// </summary>
    public class EventRepository
    {
        /// <summary>
        /// The path to the JSON file where events are stored.
        /// </summary>
        private string FilePath { get; }

        /// <summary>
        /// JSON serialization options, including indentation and enum string conversion.
        /// </summary>
        private JsonSerializerOptions JsonOptions { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventRepository"/> class.
        /// Sets up the file path and JSON serializer options.
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
        /// Loads all events from the JSON file.
        /// If the file does not exist, is empty, or contains invalid JSON, returns an empty list.
        /// </summary>
        /// <returns>A list of <see cref="Event"/> objects read from the file.</returns>
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
            catch (JsonException jsonException)
            {
                Console.WriteLine($"Error deserializing JSON: {jsonException.Message}");
                return new List<Event>();
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Unexpected error loading events: {exception.Message}");
                return new List<Event>();
            }
        }

        /// <summary>
        /// Saves the provided list of events to the JSON file.
        /// </summary>
        /// <param name="events">The list of <see cref="Event"/> objects to save.</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when there is insufficient permission or an I/O error prevents writing to the file.
        /// </exception>
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
                File.WriteAllText(FilePath, json);
            }
            catch (UnauthorizedAccessException unauthorizedAccessException)
            {
                Console.WriteLine($"Access error when saving events: {unauthorizedAccessException.Message}");
                throw new InvalidOperationException("Insufficient permissions to write to file.", unauthorizedAccessException);
            }
            catch (IOException ioException)
            {
                Console.WriteLine($"I/O error when saving events: {ioException.Message}");
                throw new InvalidOperationException("File is being used by another process or directory cannot be created.", ioException);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Unexpected error when saving events: {exception.Message}");
                throw new InvalidOperationException("Error saving events.", exception);
            }
        }

        /// <summary>
        /// Adds a new event to the repository and saves the updated list.
        /// </summary>
        /// <param name="newEvent">The <see cref="Event"/> to add.</param>
        public void AddEvent(Event newEvent)
        {
            var events = LoadEvents();
            events.Add(newEvent);
            SaveEvents(events);
        }

        /// <summary>
        /// Updates an existing event in the repository by matching its ID and saves the changes.
        /// </summary>
        /// <param name="updatedEvent">The <see cref="Event"/> containing updated data. Its ID must match an existing event.</param>
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
        /// Deletes an event with the specified ID from the repository and saves the updated list.
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> identifier of the event to delete.</param>
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
                Console.WriteLine($"Event with ID {id} was not found when deleting");
            }
        }
    }
}
