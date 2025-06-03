namespace basketball_calendar.Models;

/// <summary>
/// Represents a calendar event with properties for title, description, timing, tag, and reminders.
/// </summary>
public class Event
{
    /// <summary>
    /// Unique identifier of the event.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Event title (e.g., "LA Lakers Game").
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Detailed description or note about the event.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Event start time.
    /// </summary>
    public DateTime Start { get; set; }

    /// <summary>
    /// Event end time.
    /// </summary>
    public DateTime End { get; set; }

    /// <summary>
    /// Event tag or type (e.g., "game", "practice").
    /// </summary>
    public string Tag { get; set; }

    /// <summary>
    /// Time span before the event start when a reminder should be triggered (optional).
    /// </summary>
    public TimeSpan? ReminderOffset { get; set; }

    /// <summary>
    /// Indicates whether the reminder has already been sent.
    /// </summary>
    public bool ReminderSent { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Event"/> class.
    /// Generates a new GUID for <see cref="Id"/>, initializes <see cref="Tag"/> as an empty string,
    /// and sets <see cref="ReminderSent"/> to false.
    /// </summary>
    public Event()
    {
        Id = Guid.NewGuid();
        Title = string.Empty;
        Description = string.Empty;
        Tag = string.Empty;
        ReminderSent = false;
    }
}