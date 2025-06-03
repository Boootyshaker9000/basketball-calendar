using System.Text.Json;
using basketball_calendar.Models;
using basketball_calendar.Services;

namespace basketball_calendar.Forms;

/// <summary>
/// The main form of the application, responsible for displaying and managing events
/// and NBA game results, as well as applying user-selected themes.
/// </summary>
public partial class MainForm : Form
{
    /// <summary>
    /// Repository for loading, adding, updating, and deleting events.
    /// </summary>
    private EventRepository Repository { get; }

    /// <summary>
    /// All loaded events from the repository.
    /// </summary>
    private List<Event> AllEvents { get; set; }

    /// <summary>
    /// Service for fetching NBA game results from an external API.
    /// </summary>
    private NbaGameService NbaService { get; }

    /// <summary>
    /// Indicates whether NBA results are currently visible in the UI.
    /// </summary>
    private bool AreNbaResultsVisible { get; set; }

    /// <summary>
    /// Currently applied background color for the form and its controls.
    /// </summary>
    private Color BackgroundColor { get; set; }

    /// <summary>
    /// Currently applied foreground color for the form and its controls.
    /// </summary>
    private Color ForegroundColor { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MainForm"/> class.
    /// Loads events from the repository and initializes the NBA service.
    /// </summary>
    public MainForm()
    {
        Repository = new();
        AllEvents = Repository.LoadEvents();
        NbaService = new NbaGameService();
        AreNbaResultsVisible = true;
        InitializeComponent();
    }

    /// <summary>
    /// Handles the Load event of the form. Populates the filter dropdown, initializes
    /// the calendar selection, starts the reminder timer, loads today's NBA games,
    /// and applies saved user settings (theme and team colors).
    /// </summary>
    /// <param name="sender">The source of the Load event.</param>
    /// <param name="eventArgs">The <see cref="EventArgs"/> instance containing event data.</param>
    private async Task MainForm_Load(object sender, EventArgs eventArgs)
    {
        List<string> allTags = new List<string> { "All" };

        allTags.AddRange(["NBA Game", "Game", "Practice", "Streetball", "Training Camp"]);

        var distinctTags = allTags.Distinct().ToList();
        var sortedTags = new List<string> { "All" };
        sortedTags.AddRange(distinctTags.Where(tag => tag != "All").OrderBy(tag => tag));

        ComboBoxFilter.DataSource = sortedTags;
        ComboBoxFilter.SelectedIndexChanged += ComboBoxFilter_SelectedIndexChanged;

        MonthCalendar.SelectionStart = DateTime.Today;
        RefreshEventList(DateTime.Today);
        ReminderTimer.Start();

        var games = await NbaService.GetGamesByDateAsync(DateTime.Today);
        ListBoxNbaGames.DataSource = games;
        
        var json = await File.ReadAllTextAsync("settings.json");

        UserSettings? settings = null;
        try
        {
            settings = JsonSerializer.Deserialize<UserSettings>(json);
        }
        catch (JsonException)
        {
            // ignored
        }

        if (settings != null)
        {
            if (settings.TeamName != null && SettingsForm.TeamThemes.TryGetValue(settings.TeamName, out var colors))
            {
                ApplyTheme(Color.FromArgb(settings.SecondaryColorArgb), Color.FromArgb(settings.PrimaryColorArgb));
            }
        }
    }

    /// <summary>
    /// Updates the displayed list of events based on the specified date and the currently selected tag filter.
    /// </summary>
    /// <param name="date">The date for which to load and display events.</param>
    private void RefreshEventList(DateTime date)
    {
        var filtered = AllEvents
            .Where(@event => @event.Start.Date == date.Date)
            .OrderBy(ev => ev.Start)
            .ToList();

        if (ComboBoxFilter.SelectedItem is string selectedTag && selectedTag != "All")
        {
            filtered = filtered.Where(@event => @event.Tag == selectedTag).ToList();
        }
        
        ListBoxEvents.DataSource = filtered;
        ListBoxEvents.DisplayMember = nameof(Event.Title);
    }

    /// <summary>
    /// Handles the DateChanged event of the MonthCalendar. Refreshes the event list
    /// and, if NBA results are visible, refreshes NBA game results for the new date.
    /// </summary>
    /// <param name="sender">The source of the DateChanged event.</param>
    /// <param name="eventArgs">The <see cref="DateRangeEventArgs"/> instance containing event data.</param>
    private void MonthCalendarOnDateChanged(object sender, DateRangeEventArgs eventArgs)
    {
        RefreshEventList(eventArgs.Start);

        if (AreNbaResultsVisible)
        {
            _ = RefreshNbaGamesAsync(eventArgs.Start);
        }
    }

    /// <summary>
    /// Handles the Click event of the Add button. Opens the event creation form,
    /// applies the current theme, and if the new event is saved, reloads and refreshes the event list.
    /// </summary>
    /// <param name="sender">The source of the Click event.</param>
    /// <param name="eventArgs">The <see cref="EventArgs"/> instance containing event data.</param>
    private void ButtonAddOnClick(object sender, EventArgs eventArgs)
    {
        using var form = new EventForm();
        form.ApplyTheme(BackgroundColor, ForegroundColor);
        if (form.ShowDialog() == DialogResult.OK)
        {
            Repository.AddEvent(form.Event);
            AllEvents = Repository.LoadEvents();
            RefreshEventList(MonthCalendar.SelectionStart);
        }
    }

    /// <summary>
    /// Handles the Click event of the Edit button. If an event is selected, opens the event form
    /// populated with the selected event's data, applies the current theme, and if saved, reloads and refreshes the event list.
    /// </summary>
    /// <param name="sender">The source of the Click event.</param>
    /// <param name="eventArgs">The <see cref="EventArgs"/> instance containing event data.</param>
    private void ButtonEditOnClick(object sender, EventArgs eventArgs)
    {
        if (ListBoxEvents.SelectedItem is Event selectedEvent)
        {
            using var form = new EventForm(selectedEvent);
            form.ApplyTheme(BackgroundColor, ForegroundColor);
            if (form.ShowDialog() == DialogResult.OK)
            {
                Repository.UpdateEvent(form.Event);
                AllEvents = Repository.LoadEvents();
                RefreshEventList(MonthCalendar.SelectionStart);
            }
        }
    }

    /// <summary>
    /// Handles the Click event of the Delete button. If an event is selected, asks for confirmation,
    /// then deletes the event, reloads, and refreshes the event list.
    /// </summary>
    /// <param name="sender">The source of the Click event.</param>
    /// <param name="eventArgs">The <see cref="EventArgs"/> instance containing event data.</param>
    private void ButtonDeleteOnClick(object sender, EventArgs eventArgs)
    {
        if (ListBoxEvents.SelectedItem is Event selectedEvent)
        {
            var result = MessageBox.Show(
                $"Are you sure you want to delete the event '{selectedEvent.Title}'?",
                "Delete Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                Repository.DeleteEvent(selectedEvent.Id);
                AllEvents = Repository.LoadEvents();
                RefreshEventList(MonthCalendar.SelectionStart);
            }
        }
    }

    /// <summary>
    /// Handles the DoubleClick event of the ListBoxEvents. Invokes the edit logic for the selected event.
    /// </summary>
    /// <param name="sender">The source of the DoubleClick event.</param>
    /// <param name="eventArgs">The <see cref="EventArgs"/> instance containing event data.</param>
    private void ListBoxEventsOnDoubleClick(object sender, EventArgs eventArgs)
    {
        ButtonEditOnClick(sender, eventArgs);
    }

    /// <summary>
    /// Handles the Tick event of the ReminderTimer. Checks for events whose reminder time has arrived,
    /// shows a notification balloon, marks them as reminded, and updates the repository.
    /// </summary>
    /// <param name="sender">The source of the Tick event.</param>
    /// <param name="eventArgs">The <see cref="EventArgs"/> instance containing event data.</param>
    private void ReminderTimerOnTick(object sender, EventArgs eventArgs)
    {
        var now = DateTime.Now;
        AllEvents = Repository.LoadEvents();
        var toRemind = AllEvents
            .Where(@event => !@event.ReminderSent
                                  && @event.ReminderOffset.HasValue
                                  && now >= @event.Start - @event.ReminderOffset.Value
                                  && now <= @event.Start)
            .ToList();
        
        foreach (var @event in toRemind)
        {
            NotifyIcon.BalloonTipText = $"{@event.Title} starts at {@event.Start:HH:mm}";
            NotifyIcon.ShowBalloonTip(5000);
            
            @event.ReminderSent = true;
            Repository.UpdateEvent(@event);
        }

        if (toRemind.Any())
        {
            AllEvents = Repository.LoadEvents();
        }
    }

    /// <summary>
    /// Applies the specified foreground and background colors to the form and all relevant controls.
    /// </summary>
    /// <param name="foreground">The foreground color to apply.</param>
    /// <param name="background">The background color to apply.</param>
    private void ApplyTheme(Color foreground, Color background)
    {
        BackgroundColor = background;
        ForegroundColor = foreground;
        
        BackColor = BackgroundColor;
        ForeColor = ForegroundColor;

        MonthCalendar.BackColor = BackgroundColor;
        MonthCalendar.ForeColor = ForegroundColor;
        MonthCalendar.TitleBackColor = BackgroundColor;
        MonthCalendar.TitleForeColor = ForegroundColor;
            
        ListBoxEvents.BackColor = BackgroundColor;
        ListBoxEvents.ForeColor = ForegroundColor;

        LabelFilter.BackColor = BackgroundColor;
        LabelFilter.ForeColor = ForegroundColor;
        ComboBoxFilter.BackColor = BackgroundColor;
        ComboBoxFilter.ForeColor = ForegroundColor;

        ListBoxNbaGames.BackColor = BackgroundColor;
        ListBoxNbaGames.ForeColor = ForegroundColor;
        LabelNbaGames.BackColor = BackgroundColor;
        LabelNbaGames.ForeColor = ForegroundColor;

        foreach (Control control in new Control[] { ButtonAdd, ButtonEdit, ButtonDelete, ButtonSettings, ButtonToggleNba, ButtonRefreshNba })
        {
            if (control is Button button)
            {
                button.BackColor = BackgroundColor;
                button.ForeColor = ForegroundColor;
            }
        }
    }

    /// <summary>
    /// Handles the Click event of the Settings button. Opens the settings form,
    /// applies the current theme, and if saved, applies the selected colors to this form.
    /// </summary>
    /// <param name="sender">The source of the Click event.</param>
    /// <param name="eventsArgs">The <see cref="EventArgs"/> instance containing event data.</param>
    private void ButtonSettingsOnClick(object sender, EventArgs eventsArgs)
    {
        using var form = new SettingsForm();
        form.ApplyTheme(ForegroundColor, BackgroundColor);
        if (form.ShowDialog() == DialogResult.OK)
        {
            ApplyTheme(form.SelectedSecondary, form.SelectedPrimary);
        }
    }

    /// <summary>
    /// Handles the SelectedIndexChanged event of the ComboBoxFilter.
    /// Refreshes the event list for the currently selected date, applying the new tag filter.
    /// </summary>
    /// <param name="sender">The source of the SelectedIndexChanged event.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing event data.</param>
    private void ComboBoxFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        RefreshEventList(MonthCalendar.SelectionStart);
    }

    /// <summary>
    /// Handles the Click event of the Toggle NBA button. Shows or hides the NBA results list
    /// and updates the button text accordingly.
    /// </summary>
    /// <param name="sender">The source of the Click event.</param>
    /// <param name="eventArgs">The <see cref="EventArgs"/> instance containing event data.</param>
    private void ButtonToggleNbaOnClick(object sender, EventArgs eventArgs)
    {
        AreNbaResultsVisible = !AreNbaResultsVisible;

        ListBoxNbaGames.Visible = AreNbaResultsVisible;
        ButtonToggleNba.Text = AreNbaResultsVisible ? "Hide" : "Show";
    }

    /// <summary>
    /// Handles the Click event of the Refresh NBA button. Asynchronously reloads NBA game results for the selected date.
    /// </summary>
    /// <param name="sender">The source of the Click event.</param>
    /// <param name="eventArgs">The <see cref="EventArgs"/> instance containing event data.</param>
    private async void ButtonRefreshNbaOnClick(object sender, EventArgs eventArgs)
    {
        await RefreshNbaGamesAsync(MonthCalendar.SelectionStart);
    }

    /// <summary>
    /// Asynchronously loads and displays NBA game results from the external API for the given date.
    /// Disables the Refresh button while loading and handles any errors that occur.
    /// </summary>
    /// <param name="date">The date for which to fetch NBA games.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    private async Task RefreshNbaGamesAsync(DateTime date)
    {
        ButtonRefreshNba.Enabled = false;
        ButtonRefreshNba.Text = "Loading...";
        LabelNbaGames.Text = "NBA results:";

        try
        {
            var games = await NbaService.GetGamesByDateAsync(date);

            ListBoxNbaGames.DataSource = games;

            ListBoxNbaGames.BackColor = BackgroundColor;
            ListBoxNbaGames.ForeColor = ForegroundColor;
        }
        catch (HttpRequestException exception)
        {
            MessageBox.Show($"Too many requests. Please, try again later.: {exception.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (Exception exception)
        {
            MessageBox.Show($"Error while loading NBA games: {exception.Message}", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);

            LabelNbaGames.Text = "NBA results: Error while loading";
        }
        finally
        {
            ButtonRefreshNba.Enabled = true;
            ButtonRefreshNba.Text = "Refresh";
        }
    }
}
