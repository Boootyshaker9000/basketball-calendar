namespace basketball_calendar.Forms;

partial class EventForm
{
    private System.ComponentModel.IContainer Components;
    private System.Windows.Forms.TableLayoutPanel TableLayout;
    private System.Windows.Forms.Label LabelTitle;
    private System.Windows.Forms.TextBox TextBoxTitle;
    private System.Windows.Forms.Label LabelDescription;
    private System.Windows.Forms.RichTextBox RichTextBoxDescription;
    private System.Windows.Forms.Label LabelStart;
    private System.Windows.Forms.DateTimePicker DateTimePickerStart;
    private System.Windows.Forms.Label LabelEnd;
    private System.Windows.Forms.DateTimePicker DateTimePickerEnd;
    private System.Windows.Forms.Label LabelTags;
    private System.Windows.Forms.CheckedListBox CheckedListBoxTags;
    private System.Windows.Forms.Label LabelReminder;
    private System.Windows.Forms.NumericUpDown NumericUpDownReminder;
    private System.Windows.Forms.FlowLayoutPanel ButtonPanel;
    private System.Windows.Forms.Button ButtonOK;
    private System.Windows.Forms.Button ButtonCancel;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (Components != null))
        {
            Components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        this.Components = new System.ComponentModel.Container();
        this.TableLayout = new System.Windows.Forms.TableLayoutPanel();
        this.LabelTitle = new System.Windows.Forms.Label();
        this.TextBoxTitle = new System.Windows.Forms.TextBox();
        this.LabelDescription = new System.Windows.Forms.Label();
        this.RichTextBoxDescription = new System.Windows.Forms.RichTextBox();
        this.LabelStart = new System.Windows.Forms.Label();
        this.DateTimePickerStart = new System.Windows.Forms.DateTimePicker();
        this.LabelEnd = new System.Windows.Forms.Label();
        this.DateTimePickerEnd = new System.Windows.Forms.DateTimePicker();
        this.LabelTags = new System.Windows.Forms.Label();
        this.CheckedListBoxTags = new System.Windows.Forms.CheckedListBox();
        this.LabelReminder = new System.Windows.Forms.Label();
        this.NumericUpDownReminder = new System.Windows.Forms.NumericUpDown();
        this.ButtonPanel = new System.Windows.Forms.FlowLayoutPanel();
        this.ButtonOK = new System.Windows.Forms.Button();
        this.ButtonCancel = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)(this.NumericUpDownReminder)).BeginInit();
        this.ButtonPanel.SuspendLayout();
        this.SuspendLayout();
        // 
        // TableLayout
        // 
        this.TableLayout.ColumnCount = 2;
        this.TableLayout.RowCount = 6;
        this.TableLayout.Dock = System.Windows.Forms.DockStyle.Top;
        this.TableLayout.AutoSize = true;
        this.TableLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        this.TableLayout.ColumnStyles.Clear();
        this.TableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize));
        this.TableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
        for (int i = 0; i < 6; i++)
            this.TableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
        this.TableLayout.Controls.Add(this.LabelTitle, 0, 0);
        this.TableLayout.Controls.Add(this.TextBoxTitle, 1, 0);
        this.TableLayout.Controls.Add(this.LabelDescription, 0, 1);
        this.TableLayout.Controls.Add(this.RichTextBoxDescription, 1, 1);
        this.TableLayout.Controls.Add(this.LabelStart, 0, 2);
        this.TableLayout.Controls.Add(this.DateTimePickerStart, 1, 2);
        this.TableLayout.Controls.Add(this.LabelEnd, 0, 3);
        this.TableLayout.Controls.Add(this.DateTimePickerEnd, 1, 3);
        this.TableLayout.Controls.Add(this.LabelTags, 0, 4);
        this.TableLayout.Controls.Add(this.CheckedListBoxTags, 1, 4);
        this.TableLayout.Controls.Add(this.LabelReminder, 0, 5);
        this.TableLayout.Controls.Add(this.NumericUpDownReminder, 1, 5);
        // 
        // LabelTitle
        // 
        this.LabelTitle.AutoSize = true;
        this.LabelTitle.Text = "Název";
        this.LabelTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
        // 
        // TextBoxTitle
        // 
        this.TextBoxTitle.Dock = System.Windows.Forms.DockStyle.Fill;
        this.TextBoxTitle.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
        // 
        // LabelDescription
        // 
        this.LabelDescription.AutoSize = true;
        this.LabelDescription.Text = "Popis";
        this.LabelDescription.Anchor = System.Windows.Forms.AnchorStyles.Left;
        // 
        // RichTextBoxDescription
        // 
        this.RichTextBoxDescription.Dock = System.Windows.Forms.DockStyle.Fill;
        this.RichTextBoxDescription.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
        this.RichTextBoxDescription.Height = 100;
        // 
        // LabelStart
        // 
        this.LabelStart.AutoSize = true;
        this.LabelStart.Text = "Začátek";
        this.LabelStart.Anchor = System.Windows.Forms.AnchorStyles.Left;
        // 
        // DateTimePickerStart
        // 
        this.DateTimePickerStart.Dock = System.Windows.Forms.DockStyle.Left;
        this.DateTimePickerStart.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
        this.DateTimePickerStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
        this.DateTimePickerStart.CustomFormat = "dd.MM.yyyy HH:mm";
        // 
        // LabelEnd
        // 
        this.LabelEnd.AutoSize = true;
        this.LabelEnd.Text = "Konec";
        this.LabelEnd.Anchor = System.Windows.Forms.AnchorStyles.Left;
        // 
        // DateTimePickerEnd
        // 
        this.DateTimePickerEnd.Dock = System.Windows.Forms.DockStyle.Left;
        this.DateTimePickerEnd.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
        this.DateTimePickerEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
        this.DateTimePickerEnd.CustomFormat = "dd.MM.yyyy HH:mm";
        // 
        // LabelTags
        // 
        this.LabelTags.AutoSize = true;
        this.LabelTags.Text = "Štítky";
        this.LabelTags.Anchor = System.Windows.Forms.AnchorStyles.Left;
        // 
        // CheckedListBoxTags
        // 
        this.CheckedListBoxTags.Dock = System.Windows.Forms.DockStyle.Fill;
        this.CheckedListBoxTags.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
        this.CheckedListBoxTags.CheckOnClick = true;
        this.CheckedListBoxTags.Height = 80;
        // 
        // LabelReminder
        // 
        this.LabelReminder.AutoSize = true;
        this.LabelReminder.Text = "Upozornění";
        this.LabelReminder.Anchor = System.Windows.Forms.AnchorStyles.Left;
        // 
        // NumericUpDownReminder
        // 
        this.NumericUpDownReminder.Maximum = new decimal(new int[] {1440,0,0,0});
        this.NumericUpDownReminder.Value = new decimal(new int[] {30,0,0,0});
        this.NumericUpDownReminder.Dock = System.Windows.Forms.DockStyle.Left;
        this.NumericUpDownReminder.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
        // 
        // ButtonPanel
        // 
        this.ButtonPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
        this.ButtonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
        this.ButtonPanel.AutoSize = false;
        this.ButtonPanel.Height = 40;
        this.ButtonPanel.Controls.Add(this.ButtonCancel);
        this.ButtonPanel.Controls.Add(this.ButtonOK);
        // 
        // ButtonOK
        // 
        this.ButtonOK.Text = "OK";
        this.ButtonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
        this.ButtonOK.Click += new System.EventHandler(this.ButtonOkOnClick);
        this.ButtonOK.AutoSize = true;
        // 
        // ButtonCancel
        // 
        this.ButtonCancel.Text = "Zrušit";
        this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.ButtonCancel.AutoSize = true;
        // 
        // EventForm
        // 
        this.AcceptButton = this.ButtonOK;
        this.CancelButton = this.ButtonCancel;
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.AutoSize = false;
        this.ClientSize = new System.Drawing.Size(500, 400);
        this.Controls.Add(this.TableLayout);
        this.Controls.Add(this.ButtonPanel);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "Událost";
        ((System.ComponentModel.ISupportInitialize)(this.NumericUpDownReminder)).EndInit();
        this.ButtonPanel.ResumeLayout(false);
        this.ButtonPanel.PerformLayout();
        this.ResumeLayout(false);
    }

    #endregion
}
