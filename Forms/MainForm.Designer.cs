using System.Windows.Forms.VisualStyles;
using Timer = System.Windows.Forms.Timer;

namespace basketball_calendar.Forms;

public partial class MainForm
{
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer Components  { get; set; }
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
        private ListBox ListBoxNbaGames { get; set; }
        private Button ButtonToggleNba { get; set; }
        private Label LabelNbaGames { get; set; }
        private Button ButtonRefreshNba { get; set; }

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
            this.NotifyIcon = new System.Windows.Forms.NotifyIcon(this.Components);
            this.ReminderTimer = new System.Windows.Forms.Timer(this.Components);
            this.ListBoxNbaGames = new System.Windows.Forms.ListBox();
            this.ButtonToggleNba = new System.Windows.Forms.Button();
            this.LabelNbaGames = new System.Windows.Forms.Label();
            this.ButtonRefreshNba = new System.Windows.Forms.Button();
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
            this.LabelFilter.Location = new System.Drawing.Point(12, 195);
            this.LabelFilter.Name = "LabelFilter";
            this.LabelFilter.Size = new System.Drawing.Size(52, 17);
            this.LabelFilter.TabIndex = 5;
            this.LabelFilter.Text = "Tag:";
            this.Controls.Add(this.LabelFilter);
            //
            // ComboBoxFilter
            //
            this.ComboBoxFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxFilter.FormattingEnabled = true;
            this.ComboBoxFilter.Location = new System.Drawing.Point(70, 192);
            this.ComboBoxFilter.Name = "ComboBoxFilter";
            this.ComboBoxFilter.Size = new System.Drawing.Size(150, 24);
            this.ComboBoxFilter.TabIndex = 6;
            this.Controls.Add(this.ComboBoxFilter);
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
            this.ButtonSettings.Location = new System.Drawing.Point(120, 365);
            this.ButtonSettings.Name = "ButtonSettings";
            this.ButtonSettings.Size = new System.Drawing.Size(90, 30);
            this.ButtonSettings.TabIndex = 4;
            this.ButtonSettings.Text = "Settings";
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
            this.ButtonAdd.Text = "Add";
            this.ButtonAdd.UseVisualStyleBackColor = true;
            this.ButtonAdd.Click += new System.EventHandler(this.ButtonAddOnClick);
            // 
            // ButtonEdit
            // 
            this.ButtonEdit.Location = new System.Drawing.Point(365, 365);
            this.ButtonEdit.Name = "ButtonEdit";
            this.ButtonEdit.Size = new System.Drawing.Size(90, 30);
            this.ButtonEdit.TabIndex = 3;
            this.ButtonEdit.Text = "Edit";
            this.ButtonEdit.UseVisualStyleBackColor = true;
            this.ButtonEdit.Click += new System.EventHandler(this.ButtonEditOnClick);
            // 
            // ButtonDelete
            // 
            this.ButtonDelete.Location = new System.Drawing.Point(470, 365);
            this.ButtonDelete.Name = "ButtonDelete";
            this.ButtonDelete.Size = new System.Drawing.Size(90, 30);
            this.ButtonDelete.TabIndex = 4;
            this.ButtonDelete.Text = "Delete";
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
            this.NotifyIcon.Icon = System.Drawing.SystemIcons.Application;
            this.NotifyIcon.Text = "Basketball Calendar";
            this.NotifyIcon.Visible = true;
            // 
            // ReminderTimer
            // 
            this.ReminderTimer.Interval = 60_000;
            this.ReminderTimer.Tick += new System.EventHandler(this.ReminderTimerOnTick);
            this.ReminderTimer.Start();
            //
            // ListBoxNbaGames
            //
            this.ListBoxNbaGames.FormattingEnabled = true;
            this.ListBoxNbaGames.ItemHeight = 16;
            this.ListBoxNbaGames.Location = new System.Drawing.Point(12, 230);
            this.ListBoxNbaGames.Name = "ListBoxNbaGames";
            this.ListBoxNbaGames.Size = new System.Drawing.Size(230, 100);
            this.ListBoxNbaGames.TabIndex = 7;
            this.Controls.Add(this.ListBoxNbaGames);
            //
            // LabelNbaGames
            //
            this.LabelNbaGames.AutoSize = true;
            this.LabelNbaGames.Location = new System.Drawing.Point(12, 210);
            this.LabelNbaGames.Name = "LabelNbaGames";
            this.LabelNbaGames.Size = new System.Drawing.Size(95, 17);
            this.LabelNbaGames.TabIndex = 8;
            this.LabelNbaGames.Text = "NBA results:";
            this.Controls.Add(this.LabelNbaGames);
            //
            // ButtonToggleNba
            //
            this.ButtonToggleNba.Location = new System.Drawing.Point(130, 210);
            this.ButtonToggleNba.Name = "ButtonToggleNba";
            this.ButtonToggleNba.Size = new System.Drawing.Size(55, 23);
            this.ButtonToggleNba.TabIndex = 9;
            this.ButtonToggleNba.Text = "Hide";
            this.ButtonToggleNba.UseVisualStyleBackColor = true;
            this.ButtonToggleNba.Click += new System.EventHandler(this.ButtonToggleNbaOnClick);
            this.Controls.Add(this.ButtonToggleNba);
            //
            // ButtonRefreshNba
            //
            this.ButtonRefreshNba.Location = new System.Drawing.Point(190, 210);
            this.ButtonRefreshNba.Name = "ButtonRefreshNba";
            this.ButtonRefreshNba.Size = new System.Drawing.Size(55, 23);
            this.ButtonRefreshNba.TabIndex = 10;
            this.ButtonRefreshNba.Text = "Refresh";
            this.ButtonRefreshNba.UseVisualStyleBackColor = true;
            this.ButtonRefreshNba.Click += new System.EventHandler(this.ButtonRefreshNbaOnClick);
            this.Controls.Add(this.ButtonRefreshNba);
        }

        #endregion
}