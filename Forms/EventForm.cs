using basketball_calendar.Models;

namespace basketball_calendar.Forms;

public partial class EventForm : Form
{
    public Event Event { get; private set; }
    private /*readonly*/ bool IsEditMode { get; set; }

    public EventForm()
    {
        InitializeComponent();
        IsEditMode = false;
        this.Text = "Nová událost";
        var predefinedTags = new[] { "zápas", "trénink", "streetball", "soustředění" };
        checkedListBoxTags.Items.AddRange(predefinedTags);
    }

    public EventForm(Event existingEvent) : this()
    {
        if (existingEvent == null)
            throw new ArgumentNullException(nameof(existingEvent));

        IsEditMode = true;
        Event = existingEvent;
        this.Text = "Upravit událost";

        // Naplnění polí existujícími daty
        textBoxTitle.Text = Event.Title;
        richTextBoxDescription.Text = Event.Description;
        dateTimePickerStart.Value = Event.Start;
        dateTimePickerEnd.Value = Event.End;
        checkedListBoxTags.Text = string.Join(",", Event.Tags);
        numericUpDownReminder.Value = Event.ReminderOffset.HasValue
            ? (decimal)Event.ReminderOffset.Value.TotalMinutes
            : numericUpDownReminder.Minimum;
        
        for (int i = 0; i < checkedListBoxTags.Items.Count; i++)
        {
            if (existingEvent.Tags.Contains(checkedListBoxTags.Items[i].ToString()))
                checkedListBoxTags.SetItemChecked(i, true);
        }
    }

    private void buttonOK_Click(object sender, EventArgs eventArgs)
    {
        // Validace
        if (string.IsNullOrWhiteSpace(textBoxTitle.Text))
        {
            MessageBox.Show("Název události je povinný.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        if (dateTimePickerEnd.Value <= dateTimePickerStart.Value)
        {
            MessageBox.Show("Konec události musí být po začátku.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        // Vytvoření nebo aktualizace Event objektu
        if (!IsEditMode)
        {
            Event = new Event();
        }

        Event.Title = textBoxTitle.Text.Trim();
        Event.Description = richTextBoxDescription.Text.Trim();
        Event.Start = dateTimePickerStart.Value;
        Event.End = dateTimePickerEnd.Value;
        Event.Tags = checkedListBoxTags.CheckedItems
            .OfType<string>()
            .ToList();

        var reminderMinutes = (int)numericUpDownReminder.Value;
        Event.ReminderOffset = reminderMinutes > 0 ? TimeSpan.FromMinutes(reminderMinutes) : (TimeSpan?)null;
        Event.ReminderSent = false;

        this.DialogResult = DialogResult.OK;
        this.Close();
    }

    private void buttonCancel_Click(object sender, EventArgs eventArgs)
    {
        this.DialogResult = DialogResult.Cancel;
        this.Close();
    }
}