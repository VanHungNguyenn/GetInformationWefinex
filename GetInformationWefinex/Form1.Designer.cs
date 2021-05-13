
namespace GetInformationWefinex
{
    partial class FormMain
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
            this.textboxAccount = new Guna.UI2.WinForms.Guna2TextBox();
            this.textboxPassword = new Guna.UI2.WinForms.Guna2TextBox();
            this.panelLeft = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.btnStop = new Guna.UI2.WinForms.Guna2Button();
            this.btnStart = new Guna.UI2.WinForms.Guna2Button();
            this.panelLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // textboxAccount
            // 
            this.textboxAccount.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(162)))), ((int)(((byte)(210)))));
            this.textboxAccount.BorderRadius = 10;
            this.textboxAccount.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textboxAccount.DefaultText = "";
            this.textboxAccount.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.textboxAccount.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.textboxAccount.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textboxAccount.DisabledState.Parent = this.textboxAccount;
            this.textboxAccount.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textboxAccount.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textboxAccount.FocusedState.Parent = this.textboxAccount;
            this.textboxAccount.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textboxAccount.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textboxAccount.HoverState.Parent = this.textboxAccount;
            this.textboxAccount.Location = new System.Drawing.Point(179, 14);
            this.textboxAccount.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textboxAccount.Name = "textboxAccount";
            this.textboxAccount.PasswordChar = '\0';
            this.textboxAccount.PlaceholderText = "Nhập tài khoản";
            this.textboxAccount.SelectedText = "";
            this.textboxAccount.ShadowDecoration.Parent = this.textboxAccount;
            this.textboxAccount.Size = new System.Drawing.Size(300, 41);
            this.textboxAccount.TabIndex = 0;
            // 
            // textboxPassword
            // 
            this.textboxPassword.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(162)))), ((int)(((byte)(210)))));
            this.textboxPassword.BorderRadius = 10;
            this.textboxPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textboxPassword.DefaultText = "";
            this.textboxPassword.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.textboxPassword.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.textboxPassword.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textboxPassword.DisabledState.Parent = this.textboxPassword;
            this.textboxPassword.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.textboxPassword.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textboxPassword.FocusedState.Parent = this.textboxPassword;
            this.textboxPassword.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textboxPassword.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.textboxPassword.HoverState.Parent = this.textboxPassword;
            this.textboxPassword.Location = new System.Drawing.Point(487, 14);
            this.textboxPassword.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textboxPassword.Name = "textboxPassword";
            this.textboxPassword.PasswordChar = '*';
            this.textboxPassword.PlaceholderText = "Nhập mật khẩu";
            this.textboxPassword.SelectedText = "";
            this.textboxPassword.ShadowDecoration.Parent = this.textboxPassword;
            this.textboxPassword.Size = new System.Drawing.Size(300, 41);
            this.textboxPassword.TabIndex = 1;
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.btnStop);
            this.panelLeft.Controls.Add(this.btnStart);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.panelLeft.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.ShadowDecoration.Parent = this.panelLeft;
            this.panelLeft.Size = new System.Drawing.Size(172, 450);
            this.panelLeft.TabIndex = 2;
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.Transparent;
            this.btnStop.BorderRadius = 10;
            this.btnStop.BorderThickness = 1;
            this.btnStop.CheckedState.Parent = this.btnStop;
            this.btnStop.CustomImages.Parent = this.btnStop;
            this.btnStop.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnStop.ForeColor = System.Drawing.Color.White;
            this.btnStop.HoverState.Parent = this.btnStop;
            this.btnStop.Location = new System.Drawing.Point(12, 263);
            this.btnStop.Name = "btnStop";
            this.btnStop.ShadowDecoration.Parent = this.btnStop;
            this.btnStop.Size = new System.Drawing.Size(149, 40);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "DỪNG LẠI";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.Transparent;
            this.btnStart.BorderRadius = 10;
            this.btnStart.BorderThickness = 1;
            this.btnStart.CheckedState.Parent = this.btnStart;
            this.btnStart.CustomImages.Parent = this.btnStart;
            this.btnStart.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnStart.ForeColor = System.Drawing.Color.White;
            this.btnStart.HoverState.Parent = this.btnStart;
            this.btnStart.Location = new System.Drawing.Point(12, 205);
            this.btnStart.Name = "btnStart";
            this.btnStart.ShadowDecoration.Parent = this.btnStart;
            this.btnStart.Size = new System.Drawing.Size(149, 40);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "LẤY THÔNG TIN";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelLeft);
            this.Controls.Add(this.textboxPassword);
            this.Controls.Add(this.textboxAccount);
            this.Name = "FormMain";
            this.Text = "GET INFORMATION WEFINEX";
            this.panelLeft.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2TextBox textboxAccount;
        private Guna.UI2.WinForms.Guna2TextBox textboxPassword;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel panelLeft;
        private Guna.UI2.WinForms.Guna2Button btnStart;
        private Guna.UI2.WinForms.Guna2Button btnStop;
    }
}

