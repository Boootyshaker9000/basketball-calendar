namespace basketball_calendar.Models;

/// <summary>
/// Represents the result of an NBA game.
/// </summary>
public class NbaGame
{
    /// <summary>
    /// Gets or sets the unique identifier of the game.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the abbreviation of the home team.
    /// </summary>
    public string HomeTeam { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the abbreviation of the away team.
    /// </summary>
    public string AwayTeam { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the home team's score.
    /// </summary>
    public int HomeScore { get; set; }

    /// <summary>
    /// Gets or sets the away team's score.
    /// </summary>
    public int AwayScore { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the game is played.
    /// </summary>
    public DateTime GameDate { get; set; }

    /// <summary>
    /// Gets or sets the status of the game (e.g., "Final", "In Progress").
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Returns a string representation of the game result, including time, teams, score, and status.
    /// </summary>
    /// <returns>
    /// A formatted string in the form "HH:mm HomeTeam HomeScore - AwayScore AwayTeam (Status)".
    /// </returns>
    public override string ToString()
    {
        return $"{GameDate:HH:mm} {HomeTeam} {HomeScore} - {AwayScore} {AwayTeam} ({Status})";
    }
}
