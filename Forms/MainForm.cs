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
        AllEvents = new();
        InitializeComponent();
    }

    private void MainForm_Load(object sender, EventArgs eventArgs)
    {
        AllEvents = Repository.LoadEvents()
            .Where(ev => ev != null)
            .ToList();

        var allTags = AllEvents
            .Where(ev => ev != null)
            .SelectMany(ev => ev.Tags)  
            .Distinct()
            .OrderBy(t => t)
            .ToList();
        allTags.Insert(0, "Všechny");
        ComboBoxFilter.DataSource = allTags;

        MonthCalendar.SelectionStart = DateTime.Today;
        RefreshEventList(DateTime.Today);
        ReminderTimer.Start();
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
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Repository.UpdateEvent(form.Event);
                    AllEvents = Repository.LoadEvents();
                    RefreshEventList(MonthCalendar.SelectionStart);
                }
            }
        }
    }
    
    private void ReminderTimer_Tick(object sender, EventArgs eventArgs)
    {
        var now = DateTime.Now;
        var toRemind = AllEvents
            .Where(ev => ev != null && !ev.ReminderSent 
                         && ev.ReminderOffset.HasValue
                         && now >= ev.Start - ev.ReminderOffset.Value)
            .ToList();
        foreach (var ev in toRemind)
        {
            NotifyIcon.BalloonTipText = $"{ev.Title} začíná v {ev.Start:HH:mm}";
            NotifyIcon.ShowBalloonTip(5000);
            ev.ReminderSent = true;
            Repository.UpdateEvent(ev);
        }
        if (toRemind.Any())
            AllEvents = Repository.LoadEvents();
    }
    
    private void ButtonSettingsOnClick(object sender, EventArgs eventsArgs)
    {
        using var form = new SettingsForm();
        if (form.ShowDialog() == DialogResult.OK)
        {
            BackColor = form.SelectedPrimary;
            ForeColor = form.SelectedSecondary;

            MonthCalendar.BackColor = BackColor;
            MonthCalendar.ForeColor = ForeColor;

            ListBoxEvents.BackColor = BackColor;
            ListBoxEvents.ForeColor = ForeColor;

            LabelFilter.ForeColor = ForeColor;
            ComboBoxFilter.BackColor = BackColor;
            ComboBoxFilter.ForeColor = ForeColor;

            foreach (Control control in new Control[] { ButtonAdd, ButtonEdit, ButtonDelete, ButtonSettings })
            {
                if (control is Button button)
                {
                    button.BackColor = BackColor;
                    button.ForeColor = ForeColor;
                }
            }
        }
    }
}