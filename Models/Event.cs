namespace basketball_calendar.Models;

/// <summary>
/// Reprezentuje událost v kalendáři.
/// </summary>
public class Event
{
    /// <summary>
    /// Jedinečný identifikátor události.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Název události (např. "Zápas LA Lakers").
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Podrobný popis nebo poznámka k události.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Čas začátku události.
    /// </summary>
    public DateTime Start { get; set; }

    /// <summary>
    /// Čas konce události.
    /// </summary>
    public DateTime End { get; set; }

    /// <summary>
    /// Štítky nebo typy události (např. "zápas", "trénink").
    /// </summary>
    public List<string> Tags { get; set; }

    /// <summary>
    /// Posunutí času upozornění před začátkem události (volitelné).
    /// </summary>
    public TimeSpan? ReminderOffset { get; set; }

    /// <summary>
    /// Indikuje, zda již bylo upozornění odesláno.
    /// </summary>
    public bool ReminderSent { get; set; }

    /// <summary>
    /// Výchozí konstruktor, inicializuje Id a kolekci štítků.
    /// </summary>
    public Event()
    {
        Id = Guid.NewGuid();
        Tags = new List<string>();
        ReminderSent = false;
    }
}