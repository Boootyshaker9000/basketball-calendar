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
        { "Golden State Warriors", (Color.FromArgb(29, 66, 138), Color.FromArgb(253, 185, 39)) },
        { "Boston Celtics", (Color.FromArgb(0, 122, 51), Color.White) },
        { "Miami Heat", (Color.FromArgb(152, 0, 46), Color.FromArgb(255, 184, 28)) }
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