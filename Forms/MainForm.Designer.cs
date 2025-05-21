namespace basketball_calendar.Views;

public partial class MainForm
{
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.MonthCalendar = new System.Windows.Forms.MonthCalendar();
            this.ListBoxEvents = new System.Windows.Forms.ListBox();
            this.ButtonAdd = new System.Windows.Forms.Button();
            this.ButtonEdit = new System.Windows.Forms.Button();
            this.ButtonDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // MonthCalendar
            // 
            this.MonthCalendar.Location = new System.Drawing.Point(12, 12);
            this.MonthCalendar.MaxSelectionCount = 1;
            this.MonthCalendar.Name = "MonthCalendar";
            this.MonthCalendar.TabIndex = 0;
            this.MonthCalendar.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar_DateChanged);
            // 
            // ListBoxEvents
            // 
            this.ListBoxEvents.FormattingEnabled = true;
            this.ListBoxEvents.ItemHeight = 16;
            this.ListBoxEvents.Location = new System.Drawing.Point(260, 12);
            this.ListBoxEvents.Name = "ListBoxEvents";
            this.ListBoxEvents.Size = new System.Drawing.Size(300, 340);
            this.ListBoxEvents.TabIndex = 1;
            this.ListBoxEvents.DoubleClick += new System.EventHandler(this.listBoxEvents_DoubleClick);
            // 
            // ButtonAdd
            // 
            this.ButtonAdd.Location = new System.Drawing.Point(260, 365);
            this.ButtonAdd.Name = "buttonAdd";
            this.ButtonAdd.Size = new System.Drawing.Size(90, 30);
            this.ButtonAdd.TabIndex = 2;
            this.ButtonAdd.Text = "Přidat";
            this.ButtonAdd.UseVisualStyleBackColor = true;
            this.ButtonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // ButtonEdit
            // 
            this.ButtonEdit.Location = new System.Drawing.Point(370, 365);
            this.ButtonEdit.Name = "buttonEdit";
            this.ButtonEdit.Size = new System.Drawing.Size(90, 30);
            this.ButtonEdit.TabIndex = 3;
            this.ButtonEdit.Text = "Upravit";
            this.ButtonEdit.UseVisualStyleBackColor = true;
            this.ButtonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // ButtonDelete
            // 
            this.ButtonDelete.Location = new System.Drawing.Point(470, 365);
            this.ButtonDelete.Name = "buttonDelete";
            this.ButtonDelete.Size = new System.Drawing.Size(90, 30);
            this.ButtonDelete.TabIndex = 4;
            this.ButtonDelete.Text = "Smazat";
            this.ButtonDelete.UseVisualStyleBackColor = true;
            this.ButtonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
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
            /*this.ReminderTimer = new System.Windows.Forms.Timer(this.components);
            this.ReminderTimer.Interval = 60000; // 60_000 ms = 1 minuta
            this.ReminderTimer.Tick += ReminderTimer_Tick;
            this.ReminderTimer.Start();*/
        }

        #endregion

        private System.Windows.Forms.MonthCalendar MonthCalendar { get; set; }
        private System.Windows.Forms.ListBox ListBoxEvents { get; set; }
        private System.Windows.Forms.Button ButtonAdd { get; set; }
        private System.Windows.Forms.Button ButtonEdit { get; set; }
        private System.Windows.Forms.Button ButtonDelete { get; set; }
        private System.Windows.Forms.NotifyIcon NotifyIcon { get; set; }
        //private System.Windows.Forms.Timer ReminderTimer { get; set; }
}