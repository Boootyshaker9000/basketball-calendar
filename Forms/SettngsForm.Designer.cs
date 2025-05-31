namespace basketball_calendar.Forms;

partial class SettingsForm
    {
        private System.ComponentModel.IContainer Components { get; set; }
        private System.Windows.Forms.ComboBox ComboTeams { get; set; }
        private System.Windows.Forms.Label LabelPrimary { get; set; }
        private System.Windows.Forms.Panel PanelPrimary { get; set; }
        private System.Windows.Forms.Label LabelSecondary { get; set; }
        private System.Windows.Forms.Panel PanelSecondary { get; set; }
        private System.Windows.Forms.Button ButtonOk { get; set; }
        private System.Windows.Forms.Button ButtonCancel { get; set; }

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

        private void InitializeComponent()
        {
            this.Components = new System.ComponentModel.Container();
            this.ComboTeams = new System.Windows.Forms.ComboBox();
            this.PanelPrimary = new System.Windows.Forms.Panel();
            this.LabelPrimary = new System.Windows.Forms.Label();
            this.PanelSecondary = new System.Windows.Forms.Panel();
            this.LabelSecondary = new System.Windows.Forms.Label();
            this.ButtonOk = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ComboTeams
            // 
            this.ComboTeams.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboTeams.FormattingEnabled = true;
            this.ComboTeams.Location = new System.Drawing.Point(20, 20);
            this.ComboTeams.Name = "ComboTeams";
            this.ComboTeams.Size = new System.Drawing.Size(340, 24);
            this.ComboTeams.TabIndex = 0;
            this.ComboTeams.SelectedIndexChanged += new System.EventHandler(this.ComboTeams_SelectedIndexChanged);
            // 
            // PanelPrimary
            // 
            this.LabelPrimary.AutoSize = true;
            this.LabelPrimary.Location = new System.Drawing.Point(20, 60);
            this.LabelPrimary.Name = "LabelPrimary";
            this.LabelPrimary.Size = new System.Drawing.Size(94, 17);
            this.LabelPrimary.TabIndex = 1;
            this.LabelPrimary.Text = "Primary color:";
            // 
            // PanelPrimary
            // 
            this.PanelPrimary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelPrimary.Location = new System.Drawing.Point(150, 55);
            this.PanelPrimary.Name = "PanelPrimary";
            this.PanelPrimary.Size = new System.Drawing.Size(50, 25);
            this.PanelPrimary.TabIndex = 2;
            // 
            // LabelSecondary
            // 
            this.LabelSecondary.AutoSize = true;
            this.LabelSecondary.Location = new System.Drawing.Point(20, 100);
            this.LabelSecondary.Name = "LabelSecondary";
            this.LabelSecondary.Size = new System.Drawing.Size(120, 17);
            this.LabelSecondary.TabIndex = 3;
            this.LabelSecondary.Text = "Sekundární barva:";
            // 
            // PanelSecondary
            // 
            this.PanelSecondary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelSecondary.Location = new System.Drawing.Point(150, 95);
            this.PanelSecondary.Name = "PanelSecondary";
            this.PanelSecondary.Size = new System.Drawing.Size(50, 25);
            this.PanelSecondary.TabIndex = 4;
            // 
            // ButtonOk
            // 
            this.ButtonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ButtonOk.Location = new System.Drawing.Point(200, 130);
            this.ButtonOk.Name = "ButtonOk";
            this.ButtonOk.Size = new System.Drawing.Size(75, 30);
            this.ButtonOk.TabIndex = 5;
            this.ButtonOk.Text = "OK";
            this.ButtonOk.UseVisualStyleBackColor = true;
            this.ButtonOk.Click += new System.EventHandler(this.ButtonOkOnClick);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonCancel.Location = new System.Drawing.Point(285, 130);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 30);
            this.ButtonCancel.TabIndex = 6;
            this.ButtonCancel.Text = "Cancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.ButtonOk;
            this.CancelButton = this.ButtonCancel;
            this.ClientSize = new System.Drawing.Size(384, 181);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonOk);
            this.Controls.Add(this.PanelPrimary);
            this.Controls.Add(this.LabelPrimary);
            this.Controls.Add(this.PanelSecondary);
            this.Controls.Add(this.LabelSecondary);
            this.Controls.Add(this.ComboTeams);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nastavení motivu";
            this.ResumeLayout(false);
        }

        #endregion
    }