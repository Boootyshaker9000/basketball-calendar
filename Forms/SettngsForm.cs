using System.Text.Json;
using basketball_calendar.Services;

namespace basketball_calendar.Forms;

/// <summary>
/// Form for selecting color theme based on NBA team.
/// </summary>
public partial class SettingsForm : Form
{
    /// <summary>
    /// Selected primary color.
    /// </summary>
    public Color SelectedPrimary { get; private set; }

    /// <summary>
    /// Selected secondary color.
    /// </summary>
    public Color SelectedSecondary { get; private set; }

    public static readonly Dictionary<string, (Color primary, Color secondary)> TeamThemes =
        new()
    {
        { "Los Angeles Lakers", (Color.FromArgb(85, 37, 130), Color.FromArgb(253, 185, 39)) },
    { "Chicago Bulls", (Color.FromArgb(206, 17, 65), Color.Black) },
    { "Golden State Warriors", (Color.FromArgb(29, 66, 138), Color.FromArgb(255, 199, 44)) },
    { "Boston Celtics", (Color.FromArgb(0, 122, 51), Color.White) },
    { "Miami Heat", (Color.FromArgb(152, 0, 46), Color.FromArgb(255, 184, 28)) },
    { "Atlanta Hawks", (Color.FromArgb(200, 16, 46), Color.FromArgb(253, 185, 39)) },
    { "Brooklyn Nets", (Color.Black, Color.White) },
    { "Charlotte Hornets", (Color.FromArgb(29, 17, 96), Color.FromArgb(0, 120, 140)) },
    { "Cleveland Cavaliers", (Color.FromArgb(134, 0, 56), Color.FromArgb(4, 30, 66)) },
    { "Dallas Mavericks", (Color.FromArgb(0, 83, 188), Color.FromArgb(0, 40, 94)) },
    { "Denver Nuggets", (Color.FromArgb(13, 34, 64), Color.FromArgb(255, 198, 39)) },
    { "Detroit Pistons", (Color.FromArgb(200, 16, 46), Color.FromArgb(29, 66, 138)) },
    { "Houston Rockets", (Color.FromArgb(206, 17, 65), Color.Black) },
    { "Indiana Pacers", (Color.FromArgb(0, 45, 98), Color.FromArgb(255, 198, 39)) },
    { "Los Angeles Clippers", (Color.FromArgb(200, 16, 46), Color.FromArgb(29, 66, 138)) },
    { "Memphis Grizzlies", (Color.FromArgb(93, 118, 169), Color.FromArgb(18, 23, 63)) },
    { "Milwaukee Bucks", (Color.FromArgb(0, 71, 27), Color.FromArgb(240, 235, 210)) },
    { "Minnesota Timberwolves", (Color.FromArgb(12, 35, 64), Color.FromArgb(35, 97, 146)) },
    { "New Orleans Pelicans", (Color.FromArgb(0, 22, 65), Color.FromArgb(227, 24, 55)) },
    { "New York Knicks", (Color.FromArgb(0, 107, 182), Color.FromArgb(245, 132, 38)) },
    { "Oklahoma City Thunder", (Color.FromArgb(0, 125, 195), Color.FromArgb(239, 59, 36)) },
    { "Orlando Magic", (Color.FromArgb(0, 125, 197), Color.FromArgb(196, 206, 211)) },
    { "Philadelphia 76ers", (Color.FromArgb(0, 107, 182), Color.FromArgb(237, 23, 76)) },
    { "Phoenix Suns", (Color.FromArgb(29, 17, 96), Color.FromArgb(229, 95, 32)) },
    { "Portland Trail Blazers", (Color.FromArgb(224, 58, 62), Color.Black) },
    { "Sacramento Kings", (Color.FromArgb(91, 43, 130), Color.FromArgb(99, 113, 122)) },
    { "San Antonio Spurs", (Color.FromArgb(196, 206, 211), Color.Black) },
    { "Toronto Raptors", (Color.FromArgb(206, 17, 65), Color.Black) },
    { "Utah Jazz", (Color.FromArgb(0, 43, 92), Color.FromArgb(0, 71, 27)) },
    { "Washington Wizards", (Color.FromArgb(0, 43, 92), Color.FromArgb(227, 24, 55)) }
    };

    public SettingsForm()
    {
        InitializeComponent();

        ComboTeams.Items.AddRange(TeamThemes.Keys.ToArray<object>());
        if (ComboTeams.Items.Count > 0)
            ComboTeams.SelectedIndex = 0;
    }

    private void ComboTeams_SelectedIndexChanged(object sender, EventArgs eventsArgs)
    {
        if (ComboTeams.SelectedItem is string team && TeamThemes.TryGetValue(team, out var colors))
        {
            PanelPrimary.BackColor = colors.primary;
            PanelSecondary.BackColor = colors.secondary;
        }
    }

    private void ButtonOkOnClick(object sender, EventArgs eventsArgs)
    {
        if (ComboTeams.SelectedItem is string team && TeamThemes.TryGetValue(team, out var colors))
        {
            SelectedPrimary = colors.primary;
            SelectedSecondary = colors.secondary;
            
            var settings = new UserSettings {
                TeamName = ComboTeams.SelectedItem as string,
                PrimaryColorArgb = PanelPrimary.BackColor.ToArgb(),
                SecondaryColorArgb = PanelSecondary.BackColor.ToArgb()
            };
            File.WriteAllText("settings.json", JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true }));
        }
    }

    public void ApplyTheme(Color foreground, Color background)
    {
        BackColor = background;
        ForeColor = foreground;

        ComboTeams.BackColor = background;
        ComboTeams.ForeColor = foreground;

        LabelPrimary.BackColor = background;
        LabelPrimary.ForeColor = foreground;
        
        LabelSecondary.BackColor = background;
        LabelSecondary.ForeColor = foreground;

        foreach (Control control in new Control[] { ButtonOk, ButtonCancel })
        {
            if (control is Button button)
            {
                button.BackColor = background;
                button.ForeColor = foreground;
            }
        }
    }
}