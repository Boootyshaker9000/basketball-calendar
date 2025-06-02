using basketball_calendar.Models;

namespace basketball_calendar.Forms;

public partial class EventForm : Form
{
    public Event Event { get; private set; }
    private bool IsEditMode { get; }

    public EventForm()
    {
        InitializeComponent();
        IsEditMode = false;
        Text = "New Event";
        PopulateTags();
    }

    public EventForm(Event existingEvent) : this()
    {
        IsEditMode = true;
        Event = existingEvent;
        Text = "Edit Event";

        TextBoxTitle.Text = Event.Title;
        RichTextBoxDescription.Text = Event.Description;
        DateTimePickerStart.Value = Event.Start;
        DateTimePickerEnd.Value = Event.End;
        PanelTags.Text = string.Join(",", Event.Tag);
        NumericUpDownReminder.Value = Event.ReminderOffset.HasValue
            ? (decimal)Event.ReminderOffset.Value.TotalMinutes
            : NumericUpDownReminder.Minimum;
        
        foreach (RadioButton radioButton in PanelTags.Controls.OfType<RadioButton>())
        {
            if (radioButton.Text == existingEvent.Tag)
            {
                radioButton.Checked = true;
                break;
            }
        }
    }

    private void ButtonOkOnClick(object sender, EventArgs eventArgs)
    {
        if (string.IsNullOrWhiteSpace(TextBoxTitle.Text))
        {
            MessageBox.Show("Event title is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        if (DateTimePickerEnd.Value < DateTimePickerStart.Value)
        {
            MessageBox.Show("Event end time must be after start time.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        var checkedRb = PanelTags.Controls.OfType<RadioButton>().FirstOrDefault(radioButton => radioButton.Checked);

        if (checkedRb == null)
        {
            MessageBox.Show("Please select a tag.", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        Event.Tag = checkedRb.Text;

        var reminderMinutes = (int)NumericUpDownReminder.Value;
        Event.ReminderOffset = reminderMinutes > 0 ? TimeSpan.FromMinutes(reminderMinutes) : null;
        
        if (IsEditMode && (Event.Start != OriginalStart || Event.ReminderOffset != OriginalOffset))
        {
            Event.ReminderSent = false;
        }
        else if (!IsEditMode)
        {
            Event.ReminderSent = false;
        }

        DialogResult = DialogResult.OK;
        Close();
    }
    
    /// <summary>
    /// Applies a color theme (primary/secondary) to the entire EventForm.
    /// </summary>
    /// <param name="backgroundColor">Background color (primary)</param>
    /// <param name="foregroundColor">Foreground color (secondary)</param>
    public void ApplyTheme(Color backgroundColor, Color foregroundColor)
    {
        BackColor = backgroundColor;
        ForeColor = foregroundColor;

        foreach (Control control in Controls)
        {
            control.BackColor = backgroundColor;
            control.ForeColor = foregroundColor;

            if (control is TableLayoutPanel tableLayoutPanel)
            {
                foreach (Control child in tableLayoutPanel.Controls)
                {
                    child.BackColor = backgroundColor;
                    child.ForeColor = foregroundColor;

                    if (child is RichTextBox richTextBox)
                        richTextBox.BackColor = backgroundColor;
                    if (child is CheckedListBox checkedListBox)
                        checkedListBox.BackColor = backgroundColor;
                }
            }
        }

        DateTimePickerStart.CalendarForeColor = foregroundColor;
        DateTimePickerStart.CalendarMonthBackground = backgroundColor;
        DateTimePickerEnd.CalendarForeColor = foregroundColor;
        DateTimePickerEnd.CalendarMonthBackground = backgroundColor;

        NumericUpDownReminder.BackColor = backgroundColor;
        NumericUpDownReminder.ForeColor = foregroundColor;

        foreach (Control control in ButtonPanel.Controls)
        {
            if (control is Button button)
            {
                button.BackColor = backgroundColor;
                button.ForeColor = foregroundColor;
            }
        }
    }
    
    private void PopulateTags()
    {
        var predefinedTags = new[] {"NBA Game", "Game", "Practice", "Streetball", "Training Camp"};
        foreach (var tag in predefinedTags)
        {
            var radioButton = new RadioButton
            {
                Text = tag,
                AutoSize = true,
                Margin = new Padding(3),
                UseVisualStyleBackColor = true
            };
            PanelTags.Controls.Add(radioButton);
        }
    }
}