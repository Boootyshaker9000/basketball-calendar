namespace basketball_calendar.Services;

/// <summary>
/// Represents user-selected theme settings, including team name and colors.
/// </summary>
public class UserSettings
{
    /// <summary>
    /// Gets or sets the name of the selected NBA team.
    /// </summary>
    public string? TeamName { get; set; }

    /// <summary>
    /// Gets or sets the ARGB value of the primary color.
    /// </summary>
    public int PrimaryColorArgb { get; set; }

    /// <summary>
    /// Gets or sets the ARGB value of the secondary color.
    /// </summary>
    public int SecondaryColorArgb { get; set; }
}