using System.Windows.Forms.VisualStyles;
using Timer = System.Windows.Forms.Timer;

namespace basketball_calendar.Forms;

public partial class MainForm
{
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer Components;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (Components != null))
            {
                Components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Components = new System.ComponentModel.Container();
            this.MonthCalendar = new System.Windows.Forms.MonthCalendar();
            this.ListBoxEvents = new System.Windows.Forms.ListBox();
            this.ButtonAdd = new System.Windows.Forms.Button();
            this.ButtonEdit = new System.Windows.Forms.Button();
            this.ButtonDelete = new System.Windows.Forms.Button();
            this.LabelFilter = new System.Windows.Forms.Label();
            this.ComboBoxFilter = new System.Windows.Forms.ComboBox();
            this.ButtonSettings = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // MonthCalendar
            // 
            this.MonthCalendar.Location = new System.Drawing.Point(12, 12);
            this.MonthCalendar.MaxSelectionCount = 1;
            this.MonthCalendar.Name = "MonthCalendar";
            this.MonthCalendar.TabIndex = 0;
            this.MonthCalendar.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.MonthCalendarOnDateChanged);
            // 
            // LabelFilter
            //
            this.LabelFilter.AutoSize = true;
            this.LabelFilter.Location = new System.Drawing.Point(12, 365);
            this.LabelFilter.Name = "LabelFilter";
            this.LabelFilter.Size = new System.Drawing.Size(52, 17);
            this.LabelFilter.TabIndex = 5;
            this.LabelFilter.Text = "Štítek:";
            // 
            // ListBoxEvents
            // 
            this.ListBoxEvents.FormattingEnabled = true;
            this.ListBoxEvents.ItemHeight = 16;
            this.ListBoxEvents.Location = new System.Drawing.Point(260, 12);
            this.ListBoxEvents.Name = "ListBoxEvents";
            this.ListBoxEvents.Size = new System.Drawing.Size(300, 340);
            this.ListBoxEvents.TabIndex = 1;
            this.ListBoxEvents.DoubleClick += new System.EventHandler(this.ListBoxEventsOnDoubleClick);
            //
            // ButtonSettings
            //
            this.ButtonSettings.Location = new System.Drawing.Point(12, 365);
            this.ButtonSettings.Name = "ButtonSettings";
            this.ButtonSettings.Size = new System.Drawing.Size(90, 30);
            this.ButtonSettings.TabIndex = 4;
            this.ButtonSettings.Text = "Nastavení";
            this.ButtonSettings.UseVisualStyleBackColor = true;
            this.ButtonSettings.Click += new System.EventHandler(this.ButtonSettingsOnClick);
            this.Controls.Add(this.ButtonSettings);
            // 
            // ButtonAdd
            // 
            this.ButtonAdd.Location = new System.Drawing.Point(260, 365);
            this.ButtonAdd.Name = "ButtonAdd";
            this.ButtonAdd.Size = new System.Drawing.Size(90, 30);
            this.ButtonAdd.TabIndex = 2;
            this.ButtonAdd.Text = "Přidat";
            this.ButtonAdd.UseVisualStyleBackColor = true;
            this.ButtonAdd.Click += new System.EventHandler(this.ButtonAddOnClick);
            // 
            // ButtonEdit
            // 
            this.ButtonEdit.Location = new System.Drawing.Point(365, 365);
            this.ButtonEdit.Name = "ButtonEdit";
            this.ButtonEdit.Size = new System.Drawing.Size(90, 30);
            this.ButtonEdit.TabIndex = 3;
            this.ButtonEdit.Text = "Upravit";
            this.ButtonEdit.UseVisualStyleBackColor = true;
            this.ButtonEdit.Click += new System.EventHandler(this.ButtonEditOnClick);
            // 
            // ButtonDelete
            // 
            this.ButtonDelete.Location = new System.Drawing.Point(470, 365);
            this.ButtonDelete.Name = "ButtonDelete";
            this.ButtonDelete.Size = new System.Drawing.Size(90, 30);
            this.ButtonDelete.TabIndex = 4;
            this.ButtonDelete.Text = "Smazat";
            this.ButtonDelete.UseVisualStyleBackColor = true;
            this.ButtonDelete.Click += new System.EventHandler(this.ButtonDeleteOnClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 410);
            this.Controls.Add(this.ButtonDelete);
            this.Controls.Add(this.ButtonEdit);
            this.Controls.Add(this.ButtonAdd);
            this.Controls.Add(this.ListBoxEvents);
            this.Controls.Add(this.MonthCalendar);
            this.Name = "MainForm";
            this.Text = "Basketball Calendar";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            //
            // NotifyIcon
            //
            this.ReminderTimer = new System.Windows.Forms.Timer(this.Components);
            this.ReminderTimer.Interval = 60000;
            this.ReminderTimer.Tick += ReminderTimer_Tick;
            this.ReminderTimer.Start();
        }

        #endregion

        private MonthCalendar MonthCalendar { get; set; }
        private ListBox ListBoxEvents { get; set; }
        private Button ButtonAdd { get; set; }
        private Button ButtonEdit { get; set; }
        private Button ButtonDelete { get; set; }
        private NotifyIcon NotifyIcon { get; set; }
        private Timer ReminderTimer { get; set; }
        private Label LabelFilter { get; set; }
        private ComboBox ComboBoxFilter { get; set; }
        private Button ButtonSettings { get; set; }
}