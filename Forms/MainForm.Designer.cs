﻿using System.Windows.Forms.VisualStyles;
using Timer = System.Windows.Forms.Timer;

namespace basketball_calendar.Forms;

public partial class MainForm
{
    /// <summary>
    /// Container for designer-managed components.
    /// </summary>
    private System.ComponentModel.IContainer Components { get; set; }

    /// <summary>
    /// Calendar control for selecting dates.
    /// </summary>
    private MonthCalendar MonthCalendar { get; set; }

    /// <summary>
    /// List box that displays events for the selected date.
    /// </summary>
    private ListBox ListBoxEvents { get; set; }

    /// <summary>
    /// Button to add a new event.
    /// </summary>
    private Button ButtonAdd { get; set; }

    /// <summary>
    /// Button to edit the currently selected event.
    /// </summary>
    private Button ButtonEdit { get; set; }

    /// <summary>
    /// Button to delete the currently selected event.
    /// </summary>
    private Button ButtonDelete { get; set; }

    /// <summary>
    /// Icon that appears in the system tray to show reminders.
    /// </summary>
    private NotifyIcon NotifyIcon { get; set; }

    /// <summary>
    /// Timer that triggers periodic checks for event reminders.
    /// </summary>
    private Timer ReminderTimer { get; set; }

    /// <summary>
    /// Label indicating the tag filter.
    /// </summary>
    private Label LabelFilter { get; set; }

    /// <summary>
    /// ComboBox that allows the user to filter events by tag.
    /// </summary>
    private ComboBox ComboBoxFilter { get; set; }

    /// <summary>
    /// Button to open the settings dialog.
    /// </summary>
    private Button ButtonSettings { get; set; }

    /// <summary>
    /// List box that displays NBA game results.
    /// </summary>
    private ListBox ListBoxNbaGames { get; set; }

    /// <summary>
    /// Button to toggle the visibility of NBA results.
    /// </summary>
    private Button ButtonToggleNba { get; set; }

    /// <summary>
    /// Label displaying the "NBA results:" text.
    /// </summary>
    private Label LabelNbaGames { get; set; }

    /// <summary>
    /// Button to refresh NBA game results from the API.
    /// </summary>
    private Button ButtonRefreshNba { get; set; }

    /// <summary>
    /// Clean up any resources being used by the form.
    /// </summary>
    /// <param name="disposing">
    /// true if managed resources should be disposed; otherwise, false.
    /// </param>
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
    /// Initializes and configures all form controls and their properties.
    /// Do not modify the contents of this method with the code editor.
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
        this.ListBoxEvents.Location = new System.Drawing.Point(250, 12);
        this.ListBoxEvents.Name = "ListBoxEvents";
        this.ListBoxEvents.Size = new System.Drawing.Size(300, 340);
        this.ListBoxEvents.TabIndex = 1;
        this.ListBoxEvents.DoubleClick += new System.EventHandler(this.ListBoxEventsOnDoubleClick);
        // 
        // ButtonSettings
        // 
        this.ButtonSettings.Location = new System.Drawing.Point(75, 365);
        this.ButtonSettings.Name = "ButtonSettings";
        this.ButtonSettings.Size = new System.Drawing.Size(90, 30);
        this.ButtonSettings.TabIndex = 4;
        this.ButtonSettings.Text = "Settings";
        this.ButtonSettings.UseVisualStyleBackColor = true;
        this.ButtonSettings.Click += new System.EventHandler(this.ButtonSettingsOnClick);
        // 
        // ButtonAdd
        // 
        this.ButtonAdd.Location = new System.Drawing.Point(250, 365);
        this.ButtonAdd.Name = "ButtonAdd";
        this.ButtonAdd.Size = new System.Drawing.Size(90, 30);
        this.ButtonAdd.TabIndex = 2;
        this.ButtonAdd.Text = "Add";
        this.ButtonAdd.UseVisualStyleBackColor = true;
        this.ButtonAdd.Click += new System.EventHandler(this.ButtonAddOnClick);
        // 
        // ButtonEdit
        // 
        this.ButtonEdit.Location = new System.Drawing.Point(355, 365);
        this.ButtonEdit.Name = "ButtonEdit";
        this.ButtonEdit.Size = new System.Drawing.Size(90, 30);
        this.ButtonEdit.TabIndex = 3;
        this.ButtonEdit.Text = "Edit";
        this.ButtonEdit.UseVisualStyleBackColor = true;
        this.ButtonEdit.Click += new System.EventHandler(this.ButtonEditOnClick);
        // 
        // ButtonDelete
        // 
        this.ButtonDelete.Location = new System.Drawing.Point(460, 365);
        this.ButtonDelete.Name = "ButtonDelete";
        this.ButtonDelete.Size = new System.Drawing.Size(90, 30);
        this.ButtonDelete.TabIndex = 4;
        this.ButtonDelete.Text = "Delete";
        this.ButtonDelete.UseVisualStyleBackColor = true;
        this.ButtonDelete.Click += new System.EventHandler(this.ButtonDeleteOnClick);
        // 
        // NotifyIcon
        // 
        this.NotifyIcon.Icon = System.Drawing.SystemIcons.Application;
        this.NotifyIcon.Text = "Basketball Calendar";
        this.NotifyIcon.Visible = true;
        // 
        // ReminderTimer
        // 
        this.ReminderTimer.Interval = 60000;
        this.ReminderTimer.Tick += new System.EventHandler(this.ReminderTimerOnTick);
        this.ReminderTimer.Start();
        // 
        // ListBoxNbaGames
        // 
        this.ListBoxNbaGames.FormattingEnabled = true;
        this.ListBoxNbaGames.ItemHeight = 16;
        this.ListBoxNbaGames.Location = new System.Drawing.Point(8, 255);
        this.ListBoxNbaGames.Name = "ListBoxNbaGames";
        this.ListBoxNbaGames.Size = new System.Drawing.Size(220, 100);
        this.ListBoxNbaGames.TabIndex = 7;
        // 
        // LabelNbaGames
        // 
        this.LabelNbaGames.AutoSize = true;
        this.LabelNbaGames.Location = new System.Drawing.Point(8, 235);
        this.LabelNbaGames.Name = "LabelNbaGames";
        this.LabelNbaGames.Size = new System.Drawing.Size(95, 17);
        this.LabelNbaGames.TabIndex = 8;
        this.LabelNbaGames.Text = "NBA results:";
        // 
        // ButtonToggleNba
        // 
        this.ButtonToggleNba.Location = new System.Drawing.Point(90, 235);
        this.ButtonToggleNba.Name = "ButtonToggleNba";
        this.ButtonToggleNba.Size = new System.Drawing.Size(55, 23);
        this.ButtonToggleNba.TabIndex = 9;
        this.ButtonToggleNba.Text = "Hide";
        this.ButtonToggleNba.UseVisualStyleBackColor = true;
        this.ButtonToggleNba.Click += new System.EventHandler(this.ButtonToggleNbaOnClick);
        // 
        // ButtonRefreshNba
        // 
        this.ButtonRefreshNba.Location = new System.Drawing.Point(148, 235);
        this.ButtonRefreshNba.Name = "ButtonRefreshNba";
        this.ButtonRefreshNba.Size = new System.Drawing.Size(80, 23);
        this.ButtonRefreshNba.TabIndex = 10;
        this.ButtonRefreshNba.Text = "Refresh";
        this.ButtonRefreshNba.UseVisualStyleBackColor = true;
        this.ButtonRefreshNba.Click += new System.EventHandler(this.ButtonRefreshNbaOnClick);
        // 
        // MainForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(570, 410);
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.Controls.Add(this.LabelFilter);
        this.Controls.Add(this.ButtonDelete);
        this.Controls.Add(this.ButtonEdit);
        this.Controls.Add(this.ButtonAdd);
        this.Controls.Add(this.ButtonSettings);
        this.Controls.Add(this.ListBoxEvents);
        this.Controls.Add(this.MonthCalendar);
        this.Controls.Add(this.ListBoxNbaGames);
        this.Controls.Add(this.LabelNbaGames);
        this.Controls.Add(this.ButtonToggleNba);
        this.Controls.Add(this.ButtonRefreshNba);
        this.Name = "MainForm";
        this.Text = "Basketball Calendar";
        this.Load += async (sender, eventArgs) => await MainForm_Load(sender, eventArgs);
        this.ResumeLayout(false);
    }

    #endregion
}
