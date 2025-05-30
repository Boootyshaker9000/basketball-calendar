using basketball_calendar.Models;

namespace basketball_calendar.Forms;

public partial class EventForm : Form
{
    public Event Event { get; private set; }
    private bool IsEditMode { get; set; }

    public EventForm()
    {
        InitializeComponent();
        IsEditMode = false;
        Text = "Nová událost";
        var predefinedTags = new[] { "Zápas NBA", "Zápas", "Trénink", "Streetball", "Soustředění" };
        CheckedListBoxTags.Items.AddRange(predefinedTags);
    }

    public EventForm(Event existingEvent) : this()
    {
        if (existingEvent == null)
            throw new ArgumentNullException(nameof(existingEvent));

        IsEditMode = true;
        Event = existingEvent;
        Text = "Upravit událost";

        TextBoxTitle.Text = Event.Title;
        RichTextBoxDescription.Text = Event.Description;
        DateTimePickerStart.Value = Event.Start;
        DateTimePickerEnd.Value = Event.End;
        CheckedListBoxTags.Text = string.Join(",", Event.Tags);
        NumericUpDownReminder.Value = Event.ReminderOffset.HasValue
            ? (decimal)Event.ReminderOffset.Value.TotalMinutes
            : NumericUpDownReminder.Minimum;
        
        for (int i = 0; i < CheckedListBoxTags.Items.Count; i++)
        {
            if (existingEvent.Tags.Contains(CheckedListBoxTags.Items[i].ToString()))
                CheckedListBoxTags.SetItemChecked(i, true);
        }
    }

    private void ButtonOkOnClick(object sender, EventArgs eventArgs)
    {
        if (string.IsNullOrWhiteSpace(TextBoxTitle.Text))
        {
            MessageBox.Show("Název události je povinný.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        if (DateTimePickerEnd.Value <= DateTimePickerStart.Value)
        {
            MessageBox.Show("Konec události musí být po začátku.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (!IsEditMode)
        {
            Event = new Event();
        }

        Event.Title = TextBoxTitle.Text.Trim();
        Event.Description = RichTextBoxDescription.Text.Trim();
        Event.Start = DateTimePickerStart.Value;
        Event.End = DateTimePickerEnd.Value;
        Event.Tags = CheckedListBoxTags.CheckedItems
            .OfType<string>()
            .ToList();

        var reminderMinutes = (int)NumericUpDownReminder.Value;
        Event.ReminderOffset = reminderMinutes > 0 ? TimeSpan.FromMinutes(reminderMinutes) : (TimeSpan?)null;
        Event.ReminderSent = false;

        DialogResult = DialogResult.OK;
        Close();
    }

    private void ButtonCancelOnClick(object sender, EventArgs eventArgs)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}