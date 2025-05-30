namespace basketball_calendar.Forms;

/// <summary>
/// Formulář pro výběr barevného motivu podle NBA týmu.
/// </summary>
public partial class SettingsForm : Form
{
    /// <summary>
    /// Vybraný primární odstín.
    /// </summary>
    public Color SelectedPrimary { get; private set; }

    /// <summary>
    /// Vybraný sekundární odstín.
    /// </summary>
    public Color SelectedSecondary { get; private set; }

    private static readonly Dictionary<string, (Color primary, Color secondary)> TeamThemes =
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
            PanelPreview.BackColor = colors.primary;
            PanelPreview.ForeColor = colors.secondary;
        }
    }

    private void ButtonOkOnClick(object sender, EventArgs eventsArgs)
    {
        if (ComboTeams.SelectedItem is string team && TeamThemes.TryGetValue(team, out var colors))
        {
            SelectedPrimary = colors.primary;
            SelectedSecondary = colors.secondary;
        }
        else
        {
            MessageBox.Show("Vyberte tým.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}