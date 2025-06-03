using basketball_calendar.Models;

namespace basketball_calendar.Forms;

/// <summary>
/// Form used to create or edit an Event. Provides inputs for title, description, start/end times,
/// tag selection, and reminder settings.
/// </summary>
public partial class EventForm : Form
{
    /// <summary>
    /// Gets the <see cref="Event"/> instance being created or edited.
    /// </summary>
    public Event Event { get; private set; }

    /// <summary>
    /// Indicates whether the form is in edit mode (true) or creation mode (false).
    /// </summary>
    private bool IsEditMode { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="EventForm"/> class for creating a new event.
    /// </summary>
    public EventForm()
    {
        InitializeComponent();
        IsEditMode = false;
        Text = "New Event";
        PopulateTags();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EventForm"/> class for editing an existing event.
    /// Populates all input controls with the values from <paramref name="existingEvent"/>.
    /// </summary>
    /// <param name="existingEvent">The existing <see cref="Event"/> to edit.</param>
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

    /// <summary>
    /// Handles the click event of the OK button. Validates user input, constructs or updates the <see cref="Event"/>,
    /// sets its properties from form controls, and marks the reminder flag appropriately.
    /// </summary>
    /// <param name="sender">The source of the click event.</param>
    /// <param name="eventArgs">The <see cref="EventArgs"/> instance containing event data.</param>
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
    /// Applies the specified color theme to all controls on the form, including background and foreground colors.
    /// </summary>
    /// <param name="backgroundColor">The background color to apply (primary).</param>
    /// <param name="foregroundColor">The foreground color to apply (secondary).</param>
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

    /// <summary>
    /// Populates the tag selection panel with a set of predefined radio buttons representing different event tags.
    /// </summary>
    private void PopulateTags()
    {
        var predefinedTags = new[] { "NBA Game", "Game", "Practice", "Streetball", "Training Camp" };
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
