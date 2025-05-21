namespace basketball_calendar.Views;

partial class EventForm
{
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.Label labelTitle;
    private System.Windows.Forms.TextBox textBoxTitle;
    private System.Windows.Forms.Label labelDescription;
    private System.Windows.Forms.RichTextBox richTextBoxDescription;
    private System.Windows.Forms.Label labelStart;
    private System.Windows.Forms.DateTimePicker dateTimePickerStart;
    private System.Windows.Forms.Label labelEnd;
    private System.Windows.Forms.DateTimePicker dateTimePickerEnd;
    private System.Windows.Forms.Label labelTags;
    private System.Windows.Forms.CheckedListBox checkedListBoxTags;
    private System.Windows.Forms.Label labelReminder;
    private System.Windows.Forms.NumericUpDown numericUpDownReminder;
    private System.Windows.Forms.Button buttonOK;
    private System.Windows.Forms.Button buttonCancel;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        this.labelTitle = new System.Windows.Forms.Label();
        this.textBoxTitle = new System.Windows.Forms.TextBox();
        this.labelDescription = new System.Windows.Forms.Label();
        this.richTextBoxDescription = new System.Windows.Forms.RichTextBox();
        this.labelStart = new System.Windows.Forms.Label();
        this.dateTimePickerStart = new System.Windows.Forms.DateTimePicker();
        this.labelEnd = new System.Windows.Forms.Label();
        this.dateTimePickerEnd = new System.Windows.Forms.DateTimePicker();
        this.labelTags = new System.Windows.Forms.Label();
        this.checkedListBoxTags = new System.Windows.Forms.CheckedListBox();
        this.labelReminder = new System.Windows.Forms.Label();
        this.numericUpDownReminder = new System.Windows.Forms.NumericUpDown();
        this.buttonOK = new System.Windows.Forms.Button();
        this.buttonCancel = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)(this.numericUpDownReminder)).BeginInit();
        this.SuspendLayout();
        // 
        // labelTitle
        // 
        this.labelTitle.AutoSize = true;
        this.labelTitle.Location = new System.Drawing.Point(12, 15);
        this.labelTitle.Name = "labelTitle";
        this.labelTitle.Size = new System.Drawing.Size(38, 17);
        this.labelTitle.TabIndex = 0;
        this.labelTitle.Text = "Název";
        // 
        // textBoxTitle
        // 
        this.textBoxTitle.Location = new System.Drawing.Point(100, 12);
        this.textBoxTitle.Name = "textBoxTitle";
        this.textBoxTitle.Size = new System.Drawing.Size(300, 22);
        this.textBoxTitle.TabIndex = 1;
        // 
        // labelDescription
        // 
        this.labelDescription.AutoSize = true;
        this.labelDescription.Location = new System.Drawing.Point(12, 50);
        this.labelDescription.Name = "labelDescription";
        this.labelDescription.Size = new System.Drawing.Size(63, 17);
        this.labelDescription.TabIndex = 2;
        this.labelDescription.Text = "Popis";
        // 
        // richTextBoxDescription
        // 
        this.richTextBoxDescription.Location = new System.Drawing.Point(100, 47);
        this.richTextBoxDescription.Name = "richTextBoxDescription";
        this.richTextBoxDescription.Size = new System.Drawing.Size(300, 100);
        this.richTextBoxDescription.TabIndex = 3;
        this.richTextBoxDescription.Text = "";
        // 
        // labelStart
        // 
        this.labelStart.AutoSize = true;
        this.labelStart.Location = new System.Drawing.Point(12, 160);
        this.labelStart.Name = "labelStart";
        this.labelStart.Size = new System.Drawing.Size(58, 17);
        this.labelStart.TabIndex = 4;
        this.labelStart.Text = "Začátek";
        // 
        // dateTimePickerStart
        // 
        this.dateTimePickerStart.CustomFormat = "dd.MM.yyyy HH:mm";
        this.dateTimePickerStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
        this.dateTimePickerStart.Location = new System.Drawing.Point(100, 155);
        this.dateTimePickerStart.Name = "dateTimePickerStart";
        this.dateTimePickerStart.Size = new System.Drawing.Size(200, 22);
        this.dateTimePickerStart.TabIndex = 5;
        // 
        // labelEnd
        // 
        this.labelEnd.AutoSize = true;
        this.labelEnd.Location = new System.Drawing.Point(12, 195);
        this.labelEnd.Name = "labelEnd";
        this.labelEnd.Size = new System.Drawing.Size(56, 17);
        this.labelEnd.TabIndex = 6;
        this.labelEnd.Text = "Konec";
        // 
        // dateTimePickerEnd
        // 
        this.dateTimePickerEnd.CustomFormat = "dd.MM.yyyy HH:mm";
        this.dateTimePickerEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
        this.dateTimePickerEnd.Location = new System.Drawing.Point(100, 190);
        this.dateTimePickerEnd.Name = "dateTimePickerEnd";
        this.dateTimePickerEnd.Size = new System.Drawing.Size(200, 22);
        this.dateTimePickerEnd.TabIndex = 7;
        // 
        // labelTags
        // 
        this.labelTags.AutoSize = true;
        this.labelTags.Location = new System.Drawing.Point(12, 230);
        this.labelTags.Name = "labelTags";
        this.labelTags.Size = new System.Drawing.Size(44, 17);
        this.labelTags.TabIndex = 8;
        this.labelTags.Text = "Štítky";
        //
        // checkedListBoxTags
        //
        this.checkedListBoxTags = new System.Windows.Forms.CheckedListBox();
        this.checkedListBoxTags.CheckOnClick = true;
        this.checkedListBoxTags.FormattingEnabled = true;
        this.checkedListBoxTags.Location = new System.Drawing.Point(100, 227);
        this.checkedListBoxTags.Name = "checkedListBoxTags";
        this.checkedListBoxTags.Size = new System.Drawing.Size(300, 80);
        this.checkedListBoxTags.TabIndex = 9;
        // 
        // labelReminder
        // 
        this.labelReminder.AutoSize = true;
        this.labelReminder.Location = new System.Drawing.Point(12, 265);
        this.labelReminder.Name = "labelReminder";
        this.labelReminder.Size = new System.Drawing.Size(82, 17);
        this.labelReminder.TabIndex = 10;
        this.labelReminder.Text = "Upozornění";
        // 
        // numericUpDownReminder
        // 
        this.numericUpDownReminder.Location = new System.Drawing.Point(100, 263);
        this.numericUpDownReminder.Maximum = new decimal(new int[] {
        1440,
        0,
        0,
        0});
        this.numericUpDownReminder.Name = "numericUpDownReminder";
        this.numericUpDownReminder.Size = new System.Drawing.Size(80, 22);
        this.numericUpDownReminder.TabIndex = 11;
        this.numericUpDownReminder.Value = new decimal(new int[] {
        30,
        0,
        0,
        0});
        // 
        // buttonOK
        // 
        this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
        this.buttonOK.Location = new System.Drawing.Point(220, 305);
        this.buttonOK.Name = "buttonOK";
        this.buttonOK.Size = new System.Drawing.Size(90, 30);
        this.buttonOK.TabIndex = 12;
        this.buttonOK.Text = "OK";
        this.buttonOK.UseVisualStyleBackColor = true;
        // 
        // buttonCancel
        // 
        this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.buttonCancel.Location = new System.Drawing.Point(330, 305);
        this.buttonCancel.Name = "buttonCancel";
        this.buttonCancel.Size = new System.Drawing.Size(90, 30);
        this.buttonCancel.TabIndex = 13;
        this.buttonCancel.Text = "Zrušit";
        this.buttonCancel.UseVisualStyleBackColor = true;
        // 
        // EventForm
        // 
        this.AcceptButton = this.buttonOK;
        this.CancelButton = this.buttonCancel;
        this.ClientSize = new System.Drawing.Size(420, 350);
        this.Controls.Add(this.buttonCancel);
        this.Controls.Add(this.buttonOK);
        this.Controls.Add(this.numericUpDownReminder);
        this.Controls.Add(this.labelReminder);
        this.Controls.Add(this.checkedListBoxTags);
        this.Controls.Add(this.labelTags);
        this.Controls.Add(this.dateTimePickerEnd);
        this.Controls.Add(this.labelEnd);
        this.Controls.Add(this.dateTimePickerStart);
        this.Controls.Add(this.labelStart);
        this.Controls.Add(this.richTextBoxDescription);
        this.Controls.Add(this.labelDescription);
        this.Controls.Add(this.textBoxTitle);
        this.Controls.Add(this.labelTitle);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "EventForm";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "Událost";
        ((System.ComponentModel.ISupportInitialize)(this.numericUpDownReminder)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();
    }
    #endregion
}