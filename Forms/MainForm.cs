using System.Text.Json;
using basketball_calendar.Models;
using basketball_calendar.Services;

namespace basketball_calendar.Forms;

public partial class MainForm : Form
{
    private EventRepository Repository { get; }
    private List<Event> AllEvents { get; set; }

    public MainForm()
    {
        Repository = new();
        AllEvents = Repository.LoadEvents();
        InitializeComponent();
    }

    private void MainForm_Load(object sender, EventArgs eventArgs)
    {
        List<string> allTags = new List<string> { "All" };

        allTags.AddRange(["NBA Game", "Game", "Practice", "Streetball", "Training Camp"]);

        var distinctTags = allTags.Distinct().ToList();
        var sortedTags = new List<string> { "All" };
        sortedTags.AddRange(distinctTags.Where(t => t != "All").OrderBy(t => t));

        ComboBoxFilter.DataSource = sortedTags;
        ComboBoxFilter.SelectedIndexChanged += ComboBoxFilter_SelectedIndexChanged;

        MonthCalendar.SelectionStart = DateTime.Today;
        RefreshEventList(DateTime.Today);
        ReminderTimer.Start();
        
        var json = File.ReadAllText("settings.json");

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
    /// Updates the displayed list of events based on the selected date and tag.
    /// </summary>
    private void RefreshEventList(DateTime date)
    {
        var filtered = AllEvents
            .Where(@event => @event.Start.Date == date.Date)
            .OrderBy(ev => ev.Start)
            .ToList();

        if (ComboBoxFilter.SelectedItem is string selectedTag && selectedTag != "All")
        {
            filtered = filtered.Where(e => e.Tag == selectedTag).ToList();
        }
        
        ListBoxEvents.DataSource = filtered;
        ListBoxEvents.DisplayMember = nameof(Event.Title);
    }


    private void MonthCalendarOnDateChanged(object sender, DateRangeEventArgs eventArgs)
    {
        RefreshEventList(eventArgs.Start);
    }

    private void ButtonAddOnClick(object sender, EventArgs eventArgs)
    {
        using var form = new EventForm();
        form.ApplyTheme(BackgroundColor, ForegroundColor);
        if (form.ShowDialog() == DialogResult.OK)
        {
            Console.WriteLine("Spuštěno");
            Repository.AddEvent(form.Event);
            AllEvents = Repository.LoadEvents();
            RefreshEventList(MonthCalendar.SelectionStart);
        }
    }

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
    
    private void ListBoxEventsOnDoubleClick(object sender, EventArgs eventArgs)
    {
        ButtonEditOnClick(sender, eventArgs);
    }
    
    private void ReminderTimerOnTick(object sender, EventArgs eventArgs)
    {
        var now = DateTime.Now;
        AllEvents = Repository.LoadEvents();
        var toRemind = AllEvents
            .Where(@event => !@event.ReminderSent 
                                  && @event.ReminderOffset.HasValue 
                                  && !@event.ReminderSent
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

        foreach (Control control in new Control[] { ButtonAdd, ButtonEdit, ButtonDelete, ButtonSettings })
        {
            if (control is Button button)
            {
                button.BackColor = BackgroundColor;
                button.ForeColor = ForegroundColor;
            }
        }
    }

    private void ButtonSettingsOnClick(object sender, EventArgs eventsArgs)
    {
        using var form = new SettingsForm();
        form.ApplyTheme(ForegroundColor, BackgroundColor);
        if (form.ShowDialog() == DialogResult.OK)
        {
            ApplyTheme(form.SelectedSecondary, form.SelectedPrimary);
        }
    }

    private void ComboBoxFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        RefreshEventList(MonthCalendar.SelectionStart);
    }

    private Color BackgroundColor { get; set; }
    private Color ForegroundColor { get; set; }
}