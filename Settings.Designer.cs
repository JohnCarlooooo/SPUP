namespace SPUP
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.Selected_lbl = new System.Windows.Forms.Label();
            this.Ports_cb = new System.Windows.Forms.ComboBox();
            this.Search_btn = new System.Windows.Forms.Button();
            this.Apply_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Selected_lbl
            // 
            this.Selected_lbl.AutoSize = true;
            this.Selected_lbl.Location = new System.Drawing.Point(12, 9);
            this.Selected_lbl.Name = "Selected_lbl";
            this.Selected_lbl.Size = new System.Drawing.Size(80, 13);
            this.Selected_lbl.TabIndex = 0;
            this.Selected_lbl.Text = "Selected Port : ";
            // 
            // Ports_cb
            // 
            this.Ports_cb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Ports_cb.FormattingEnabled = true;
            this.Ports_cb.Location = new System.Drawing.Point(121, 50);
            this.Ports_cb.Name = "Ports_cb";
            this.Ports_cb.Size = new System.Drawing.Size(168, 21);
            this.Ports_cb.TabIndex = 1;
            // 
            // Search_btn
            // 
            this.Search_btn.Location = new System.Drawing.Point(15, 48);
            this.Search_btn.Name = "Search_btn";
            this.Search_btn.Size = new System.Drawing.Size(100, 23);
            this.Search_btn.TabIndex = 2;
            this.Search_btn.Text = "Search Devices";
            this.Search_btn.UseVisualStyleBackColor = true;
            this.Search_btn.Click += new System.EventHandler(this.Search_btn_Click);
            // 
            // Apply_btn
            // 
            this.Apply_btn.Location = new System.Drawing.Point(111, 98);
            this.Apply_btn.Name = "Apply_btn";
            this.Apply_btn.Size = new System.Drawing.Size(86, 43);
            this.Apply_btn.TabIndex = 3;
            this.Apply_btn.Text = "Apply and Exit";
            this.Apply_btn.UseVisualStyleBackColor = true;
            this.Apply_btn.Click += new System.EventHandler(this.Apply_btn_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 153);
            this.Controls.Add(this.Apply_btn);
            this.Controls.Add(this.Search_btn);
            this.Controls.Add(this.Ports_cb);
            this.Controls.Add(this.Selected_lbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Selected_lbl;
        private System.Windows.Forms.ComboBox Ports_cb;
        private System.Windows.Forms.Button Search_btn;
        private System.Windows.Forms.Button Apply_btn;
    }
}