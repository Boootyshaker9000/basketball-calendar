using basketball_calendar.Models;
using basketball_calendar.Services;

namespace basketball_calendar.Forms;

public partial class MainForm : Form
{
    private /*readonly*/ EventRepository Repository { get; set; }
    private List<Event> AllEvents { get; set; }

    public MainForm()
    {
        AllEvents = new();
        InitializeComponent();
    }

    private void MainForm_Load(object sender, EventArgs eventArgs)
    {
        Repository = new EventRepository("events.json");
        AllEvents = Repository.LoadEvents() ?? new List<Event>();

        var allTags = AllEvents
            .Where(ev => ev != null)
            .SelectMany(ev => ev.Tags ?? Enumerable.Empty<string>())
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
    private void RefreshEventList(DateTime dateTime)
    {
        var filtered = AllEvents
            .Where(ev => ev.Start.Date == dateTime.Date)
            .OrderBy(ev => ev.Start)
            .ToList();

        ListBoxEvents.DataSource = filtered;
        ListBoxEvents.DisplayMember = nameof(Event.Title);
    }

    private void monthCalendar_DateChanged(object sender, DateRangeEventArgs eventArgs)
    {
        RefreshEventList(eventArgs.Start);
    }

    private void buttonAdd_Click(object sender, EventArgs eventArgs)
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

    private void buttonEdit_Click(object sender, EventArgs eventArgs)
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

    private void buttonDelete_Click(object sender, EventArgs eventArgs)
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
    
    private void listBoxEvents_DoubleClick(object sender, EventArgs eventArgs)
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
            .Where(ev => !ev.ReminderSent 
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
}