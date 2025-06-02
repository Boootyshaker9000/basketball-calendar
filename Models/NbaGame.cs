namespace basketball_calendar.Models;

/// <summary>
/// Reprezentuje výsledek NBA zápasu
/// </summary>
public class NbaGame
{
    /// <summary>
    /// ID zápasu
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Název domácího týmu
    /// </summary>
    public string HomeTeam { get; set; } = string.Empty;

    /// <summary>
    /// Název hostujícího týmu
    /// </summary>
    public string AwayTeam { get; set; } = string.Empty;

    /// <summary>
    /// Skóre domácího týmu
    /// </summary>
    public int HomeScore { get; set; }

    /// <summary>
    /// Skóre hostujícího týmu
    /// </summary>
    public int AwayScore { get; set; }

    /// <summary>
    /// Datum a čas zápasu
    /// </summary>
    public DateTime GameDate { get; set; }

    /// <summary>
    /// Status zápasu (např. dokončeno, probíhá, atd.)
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Textová reprezentace výsledku zápasu
    /// </summary>
    public override string ToString()
    {
        return $"{GameDate:HH:mm} {HomeTeam} {HomeScore} - {AwayScore} {AwayTeam} ({Status})";
    }
}
