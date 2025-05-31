using System.Text.Json;
using basketball_calendar.Models;
using basketball_calendar.Services;

namespace basketball_calendar.Forms;

public partial class MainForm : Form
{
    private EventRepository Repository { get; set; }
    private List<Event> AllEvents { get; set; }

    public MainForm()
    {
        Repository = new("events.json");
        AllEvents = Repository.LoadEvents();
        InitializeComponent();
    }

    private void MainForm_Load(object sender, EventArgs eventArgs)
    {
        var allTags = AllEvents
            .SelectMany(@event => @event.Tags)  
            .Distinct()
            .OrderBy(time => time)
            .ToList();
        allTags.Insert(0, "Všechny");
        ComboBoxFilter.DataSource = allTags;

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
    /// Aktualizuje zobrazený seznam událostí podle vybraného data.
    /// </summary>
    private void RefreshEventList(DateTime date)
    {
        var filtered = AllEvents
            .Where(ev => ev != null && ev.Start.Date == date.Date)
            .OrderBy(ev => ev.Start)
            .ToList();
        
        ListBoxEvents.DataSource = filtered;
        ListBoxEvents.DisplayMember = nameof(Event.Title);
    }


    private void MonthCalendarOnDateChanged(object sender, DateRangeEventArgs eventArgs)
    {
        RefreshEventList(eventArgs.Start);
    }

    private void ButtonAddOnClick(object sender, EventArgs eventArgs)
    {
        using (var form = new EventForm())
        {
            form.ApplyTheme(BackgroundColor, ForegroundColor);
            if (form.ShowDialog() == DialogResult.OK)
            {
                Repository.AddEvent(form.Event);
                AllEvents = Repository.LoadEvents();
                RefreshEventList(MonthCalendar.SelectionStart);
            }
        }
    }

    private void ButtonEditOnClick(object sender, EventArgs eventArgs)
    {
        if (ListBoxEvents.SelectedItem is Event selectedEvent)
        {
            using (var form = new EventForm(selectedEvent))
            {
                form.ApplyTheme(BackgroundColor, ForegroundColor);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Repository.UpdateEvent(form.Event);
                    AllEvents = Repository.LoadEvents();
                    RefreshEventList(MonthCalendar.SelectionStart);
                }
            }
        }
    }

    private void ButtonDeleteOnClick(object sender, EventArgs eventArgs)
    {
        if (ListBoxEvents.SelectedItem is Event selectedEvent)
        {
            var result = MessageBox.Show(
                $"Opravdu chcete smazat událost '{selectedEvent.Title}'?",
                "Potvrzení smazání",
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
        if (ListBoxEvents.SelectedItem is Event selectedEvent)
        {
            using (var form = new EventForm(selectedEvent))
            {
                form.ApplyTheme(ForegroundColor, BackgroundColor);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Repository.UpdateEvent(form.Event);
                    AllEvents = Repository.LoadEvents();
                    RefreshEventList(MonthCalendar.SelectionStart);
                }
            }
        }
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
            NotifyIcon.BalloonTipText = $"{@event.Title} začíná v {@event.Start:HH:mm}";
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

    private Color BackgroundColor { get; set; }
    private Color ForegroundColor { get; set; }
}