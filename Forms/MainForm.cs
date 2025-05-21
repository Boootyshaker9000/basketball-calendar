using basketball_calendar.Models;
using basketball_calendar.Services;

namespace basketball_calendar.Views;

public partial class MainForm : Form
{
    private /*readonly*/ EventRepository Repository { get; set; }
    private List<Event> AllEvents { get; set; }

    public MainForm()
    {
        InitializeComponent();
    }

    private void MainForm_Load(object sender, EventArgs eventArgs)
    {
        // Inicializace repository s pevnou cestou k JSON souboru
        Repository = new EventRepository("events.json");

        // Načtení všech událostí
        AllEvents = Repository.LoadEvents();

        // Naplnění seznamu událostí pro dnešní datum
        RefreshEventList(MonthCalendar.SelectionStart);
        
        //ReminderTimer.Start();
    }

    /// <summary>
    /// Aktualizuje zobrazený seznam událostí podle vybraného data.
    /// </summary>
    private void RefreshEventList(DateTime dateTime)
    {
        // Filtrování událostí podle data
        var eventsForDate = AllEvents
            .Where(@event => @event.Start.Date == dateTime.Date)
            .OrderBy(@event => @event.Start)
            .ToList();

        // Zobrazení v listBox
        ListBoxEvents.DataSource = eventsForDate;
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