namespace basketball_calendar.Models;

/// <summary>
/// Represents a calendar event.
/// </summary>
public class Event
{
    /// <summary>
    /// Unique identifier of the event.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Event title (e.g. "LA Lakers Game").
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
    /// Event tags or types (e.g. "game", "practice").
    /// </summary>
    public string Tag { get; set; }

    /// <summary>
    /// Reminder offset time before the event start (optional).
    /// </summary>
    public TimeSpan? ReminderOffset { get; set; }

    /// <summary>
    /// Indicates whether the reminder has been sent.
    /// </summary>
    public bool ReminderSent { get; set; }

    /// <summary>
    /// Default constructor, initializes Id and tags collection.
    /// </summary>
    public Event()
    {
        Id = Guid.NewGuid();
        Tag = string.Empty;
        ReminderSent = false;
    }
}