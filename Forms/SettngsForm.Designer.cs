namespace basketball_calendar.Forms;

partial class SettingsForm
    {
        private System.ComponentModel.IContainer Components;
        private System.Windows.Forms.ComboBox ComboTeams;
        private System.Windows.Forms.Panel PanelPreview;
        private System.Windows.Forms.Button ButtonOk;
        private System.Windows.Forms.Button ButtonCancel;

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
            this.PanelPreview = new System.Windows.Forms.Panel();
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
            // PanelPreview
            // 
            this.PanelPreview.Location = new System.Drawing.Point(20, 60);
            this.PanelPreview.Name = "PanelPreview";
            this.PanelPreview.Size = new System.Drawing.Size(340, 50);
            this.PanelPreview.TabIndex = 1;
            // 
            // ButtonOk
            // 
            this.ButtonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ButtonOk.Location = new System.Drawing.Point(200, 130);
            this.ButtonOk.Name = "ButtonOk";
            this.ButtonOk.Size = new System.Drawing.Size(75, 30);
            this.ButtonOk.TabIndex = 2;
            this.ButtonOk.Text = "OK";
            this.ButtonOk.UseVisualStyleBackColor = true;
            this.ButtonOk.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonCancel.Location = new System.Drawing.Point(285, 130);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 30);
            this.ButtonCancel.TabIndex = 3;
            this.ButtonCancel.Text = "Storno";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.ButtonOk;
            this.CancelButton = this.ButtonCancel;
            this.ClientSize = new System.Drawing.Size(384, 181);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonOk);
            this.Controls.Add(this.PanelPreview);
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