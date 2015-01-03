namespace BB_Artist {
	partial class Main {
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Verwendete Ressourcen bereinigen.
		/// </summary>
		/// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Vom Windows Form-Designer generierter Code

		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung.
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent() {
			this.b_con = new System.Windows.Forms.Button();
			this.tb_mail = new System.Windows.Forms.TextBox();
			this.nu_delay = new System.Windows.Forms.NumericUpDown();
			this.tb_pass = new System.Windows.Forms.TextBox();
			this.tb_wid = new System.Windows.Forms.TextBox();
			this.tb_code = new System.Windows.Forms.TextBox();
			this.b_draw = new System.Windows.Forms.Button();
			this.l_mail = new System.Windows.Forms.Label();
			this.l_wid = new System.Windows.Forms.Label();
			this.l_code = new System.Windows.Forms.Label();
			this.l_pass = new System.Windows.Forms.Label();
			this.l_delay = new System.Windows.Forms.Label();
			this.l_credits = new System.Windows.Forms.Label();
			this.ofd = new System.Windows.Forms.OpenFileDialog();
			this.gb_settings = new System.Windows.Forms.GroupBox();
			this.rb_over = new System.Windows.Forms.RadioButton();
			this.rb_att = new System.Windows.Forms.RadioButton();
			this.label1 = new System.Windows.Forms.Label();
			this.nu_xp = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.nu_yp = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.nu_delay)).BeginInit();
			this.gb_settings.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nu_xp)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nu_yp)).BeginInit();
			this.SuspendLayout();
			// 
			// b_con
			// 
			this.b_con.Location = new System.Drawing.Point(71, 95);
			this.b_con.Name = "b_con";
			this.b_con.Size = new System.Drawing.Size(100, 23);
			this.b_con.TabIndex = 5;
			this.b_con.Text = "Connect";
			this.b_con.UseVisualStyleBackColor = true;
			this.b_con.Click += new System.EventHandler(this.b_con_Click);
			// 
			// tb_mail
			// 
			this.tb_mail.Location = new System.Drawing.Point(71, 2);
			this.tb_mail.Name = "tb_mail";
			this.tb_mail.Size = new System.Drawing.Size(100, 21);
			this.tb_mail.TabIndex = 1;
			// 
			// nu_delay
			// 
			this.nu_delay.Location = new System.Drawing.Point(71, 121);
			this.nu_delay.Maximum = new decimal(new int[] {
			60,
			0,
			0,
			0});
			this.nu_delay.Minimum = new decimal(new int[] {
			5,
			0,
			0,
			0});
			this.nu_delay.Name = "nu_delay";
			this.nu_delay.Size = new System.Drawing.Size(100, 21);
			this.nu_delay.TabIndex = 6;
			this.nu_delay.Value = new decimal(new int[] {
			10,
			0,
			0,
			0});
			this.nu_delay.ValueChanged += new System.EventHandler(this.nu_delay_ValueChanged);
			// 
			// tb_pass
			// 
			this.tb_pass.Location = new System.Drawing.Point(71, 25);
			this.tb_pass.Name = "tb_pass";
			this.tb_pass.PasswordChar = '*';
			this.tb_pass.Size = new System.Drawing.Size(100, 21);
			this.tb_pass.TabIndex = 2;
			// 
			// tb_wid
			// 
			this.tb_wid.Location = new System.Drawing.Point(71, 48);
			this.tb_wid.Name = "tb_wid";
			this.tb_wid.Size = new System.Drawing.Size(100, 21);
			this.tb_wid.TabIndex = 3;
			// 
			// tb_code
			// 
			this.tb_code.Location = new System.Drawing.Point(71, 71);
			this.tb_code.Name = "tb_code";
			this.tb_code.Size = new System.Drawing.Size(100, 21);
			this.tb_code.TabIndex = 4;
			// 
			// b_draw
			// 
			this.b_draw.Location = new System.Drawing.Point(71, 145);
			this.b_draw.Name = "b_draw";
			this.b_draw.Size = new System.Drawing.Size(100, 23);
			this.b_draw.TabIndex = 7;
			this.b_draw.Text = "Draw";
			this.b_draw.UseVisualStyleBackColor = true;
			this.b_draw.Click += new System.EventHandler(this.b_draw_Click);
			// 
			// l_mail
			// 
			this.l_mail.AutoSize = true;
			this.l_mail.Location = new System.Drawing.Point(12, 5);
			this.l_mail.Name = "l_mail";
			this.l_mail.Size = new System.Drawing.Size(35, 13);
			this.l_mail.TabIndex = 8;
			this.l_mail.Text = "Email:";
			// 
			// l_wid
			// 
			this.l_wid.AutoSize = true;
			this.l_wid.Location = new System.Drawing.Point(12, 51);
			this.l_wid.Name = "l_wid";
			this.l_wid.Size = new System.Drawing.Size(53, 13);
			this.l_wid.TabIndex = 10;
			this.l_wid.Text = "World-Id:";
			// 
			// l_code
			// 
			this.l_code.AutoSize = true;
			this.l_code.Location = new System.Drawing.Point(12, 74);
			this.l_code.Name = "l_code";
			this.l_code.Size = new System.Drawing.Size(36, 13);
			this.l_code.TabIndex = 11;
			this.l_code.Text = "Code:";
			// 
			// l_pass
			// 
			this.l_pass.AutoSize = true;
			this.l_pass.Location = new System.Drawing.Point(12, 28);
			this.l_pass.Name = "l_pass";
			this.l_pass.Size = new System.Drawing.Size(57, 13);
			this.l_pass.TabIndex = 9;
			this.l_pass.Text = "Password:";
			// 
			// l_delay
			// 
			this.l_delay.AutoSize = true;
			this.l_delay.Location = new System.Drawing.Point(11, 124);
			this.l_delay.Name = "l_delay";
			this.l_delay.Size = new System.Drawing.Size(38, 13);
			this.l_delay.TabIndex = 12;
			this.l_delay.Text = "Delay:";
			// 
			// l_credits
			// 
			this.l_credits.AutoSize = true;
			this.l_credits.Location = new System.Drawing.Point(185, 150);
			this.l_credits.Name = "l_credits";
			this.l_credits.Size = new System.Drawing.Size(90, 13);
			this.l_credits.TabIndex = 13;
			this.l_credits.Text = "Created by Krock";
			// 
			// ofd
			// 
			this.ofd.Filter = "Bitmap|*.bmp|JPEG|*.jpeg;*.jpe;*.jpg|GIF|*.gif|PNG|*.png|All Files|*.*";
			// 
			// gb_settings
			// 
			this.gb_settings.Controls.Add(this.nu_yp);
			this.gb_settings.Controls.Add(this.label3);
			this.gb_settings.Controls.Add(this.label2);
			this.gb_settings.Controls.Add(this.nu_xp);
			this.gb_settings.Controls.Add(this.label1);
			this.gb_settings.Location = new System.Drawing.Point(177, 42);
			this.gb_settings.Name = "gb_settings";
			this.gb_settings.Size = new System.Drawing.Size(103, 100);
			this.gb_settings.TabIndex = 16;
			this.gb_settings.TabStop = false;
			this.gb_settings.Text = "Settings";
			// 
			// rb_over
			// 
			this.rb_over.AutoSize = true;
			this.rb_over.Checked = true;
			this.rb_over.Location = new System.Drawing.Point(194, 1);
			this.rb_over.Name = "rb_over";
			this.rb_over.Size = new System.Drawing.Size(67, 17);
			this.rb_over.TabIndex = 14;
			this.rb_over.TabStop = true;
			this.rb_over.Text = "Override";
			this.rb_over.UseVisualStyleBackColor = true;
			this.rb_over.CheckedChanged += new System.EventHandler(this.rb_over_CheckedChanged);
			// 
			// rb_att
			// 
			this.rb_att.AutoSize = true;
			this.rb_att.Location = new System.Drawing.Point(194, 19);
			this.rb_att.Name = "rb_att";
			this.rb_att.Size = new System.Drawing.Size(57, 17);
			this.rb_att.TabIndex = 15;
			this.rb_att.Text = "Attach";
			this.rb_att.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 43);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(17, 13);
			this.label1.TabIndex = 20;
			this.label1.Text = "X:";
			// 
			// nu_xp
			// 
			this.nu_xp.Location = new System.Drawing.Point(29, 39);
			this.nu_xp.Maximum = new decimal(new int[] {
			1000,
			0,
			0,
			0});
			this.nu_xp.Name = "nu_xp";
			this.nu_xp.Size = new System.Drawing.Size(66, 21);
			this.nu_xp.TabIndex = 17;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 69);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(17, 13);
			this.label2.TabIndex = 21;
			this.label2.Text = "Y:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(8, 19);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(87, 13);
			this.label3.TabIndex = 19;
			this.label3.Text = "Attach image at:";
			// 
			// nu_yp
			// 
			this.nu_yp.Location = new System.Drawing.Point(29, 65);
			this.nu_yp.Maximum = new decimal(new int[] {
			1000,
			0,
			0,
			0});
			this.nu_yp.Name = "nu_yp";
			this.nu_yp.Size = new System.Drawing.Size(66, 21);
			this.nu_yp.TabIndex = 18;
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(289, 175);
			this.Controls.Add(this.rb_att);
			this.Controls.Add(this.rb_over);
			this.Controls.Add(this.gb_settings);
			this.Controls.Add(this.l_credits);
			this.Controls.Add(this.l_delay);
			this.Controls.Add(this.l_pass);
			this.Controls.Add(this.l_code);
			this.Controls.Add(this.l_wid);
			this.Controls.Add(this.l_mail);
			this.Controls.Add(this.b_draw);
			this.Controls.Add(this.tb_code);
			this.Controls.Add(this.tb_wid);
			this.Controls.Add(this.tb_pass);
			this.Controls.Add(this.nu_delay);
			this.Controls.Add(this.tb_mail);
			this.Controls.Add(this.b_con);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "Main";
			this.Text = "EE CM Artist";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_Closing);
			this.Load += new System.EventHandler(this.Main_Load);
			((System.ComponentModel.ISupportInitialize)(this.nu_delay)).EndInit();
			this.gb_settings.ResumeLayout(false);
			this.gb_settings.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nu_xp)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nu_yp)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button b_con;
		private System.Windows.Forms.TextBox tb_mail;
		private System.Windows.Forms.NumericUpDown nu_delay;
		private System.Windows.Forms.TextBox tb_pass;
		private System.Windows.Forms.TextBox tb_wid;
		private System.Windows.Forms.TextBox tb_code;
		private System.Windows.Forms.Button b_draw;
		private System.Windows.Forms.Label l_mail;
		private System.Windows.Forms.Label l_wid;
		private System.Windows.Forms.Label l_code;
		private System.Windows.Forms.Label l_pass;
		private System.Windows.Forms.Label l_delay;
		private System.Windows.Forms.Label l_credits;
		private System.Windows.Forms.OpenFileDialog ofd;
		private System.Windows.Forms.GroupBox gb_settings;
		private System.Windows.Forms.NumericUpDown nu_yp;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown nu_xp;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.RadioButton rb_over;
		private System.Windows.Forms.RadioButton rb_att;
	}
}