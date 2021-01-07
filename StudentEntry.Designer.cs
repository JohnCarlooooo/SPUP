namespace SPUP
{
    partial class StudentEntry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StudentEntry));
            this.FullName_lbl = new System.Windows.Forms.Label();
            this.Address_lbl = new System.Windows.Forms.Label();
            this.Age_lbl = new System.Windows.Forms.Label();
            this.Phone_lbl = new System.Windows.Forms.Label();
            this.Dept_lbl = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Admit_btn = new System.Windows.Forms.Button();
            this.Cancel_btn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.Temperature_tb = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // FullName_lbl
            // 
            this.FullName_lbl.AutoSize = true;
            this.FullName_lbl.Font = new System.Drawing.Font("Open Sans SemiBold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FullName_lbl.ForeColor = System.Drawing.Color.White;
            this.FullName_lbl.Location = new System.Drawing.Point(5, 9);
            this.FullName_lbl.Name = "FullName_lbl";
            this.FullName_lbl.Size = new System.Drawing.Size(113, 28);
            this.FullName_lbl.TabIndex = 0;
            this.FullName_lbl.Text = "Full Name";
            // 
            // Address_lbl
            // 
            this.Address_lbl.AutoSize = true;
            this.Address_lbl.Font = new System.Drawing.Font("Open Sans SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Address_lbl.ForeColor = System.Drawing.Color.White;
            this.Address_lbl.Location = new System.Drawing.Point(5, 94);
            this.Address_lbl.Name = "Address_lbl";
            this.Address_lbl.Size = new System.Drawing.Size(66, 20);
            this.Address_lbl.TabIndex = 1;
            this.Address_lbl.Text = "Address";
            // 
            // Age_lbl
            // 
            this.Age_lbl.Font = new System.Drawing.Font("Open Sans SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Age_lbl.ForeColor = System.Drawing.Color.White;
            this.Age_lbl.Location = new System.Drawing.Point(7, 29);
            this.Age_lbl.Name = "Age_lbl";
            this.Age_lbl.Size = new System.Drawing.Size(73, 30);
            this.Age_lbl.TabIndex = 2;
            this.Age_lbl.Text = "Age";
            this.Age_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Phone_lbl
            // 
            this.Phone_lbl.Font = new System.Drawing.Font("Open Sans SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Phone_lbl.ForeColor = System.Drawing.Color.White;
            this.Phone_lbl.Location = new System.Drawing.Point(5, 114);
            this.Phone_lbl.Name = "Phone_lbl";
            this.Phone_lbl.Size = new System.Drawing.Size(147, 20);
            this.Phone_lbl.TabIndex = 3;
            this.Phone_lbl.Text = "Phone Number";
            this.Phone_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Dept_lbl
            // 
            this.Dept_lbl.AutoSize = true;
            this.Dept_lbl.Font = new System.Drawing.Font("Open Sans SemiBold", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Dept_lbl.ForeColor = System.Drawing.Color.White;
            this.Dept_lbl.Location = new System.Drawing.Point(5, 56);
            this.Dept_lbl.Name = "Dept_lbl";
            this.Dept_lbl.Size = new System.Drawing.Size(97, 22);
            this.Dept_lbl.TabIndex = 4;
            this.Dept_lbl.Text = "Department";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(74)))), ((int)(((byte)(51)))));
            this.panel1.Controls.Add(this.FullName_lbl);
            this.panel1.Controls.Add(this.Dept_lbl);
            this.panel1.Controls.Add(this.Address_lbl);
            this.panel1.Controls.Add(this.Phone_lbl);
            this.panel1.Controls.Add(this.Age_lbl);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(292, 142);
            this.panel1.TabIndex = 7;
            // 
            // Admit_btn
            // 
            this.Admit_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(74)))), ((int)(((byte)(51)))));
            this.Admit_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Admit_btn.FlatAppearance.BorderSize = 0;
            this.Admit_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Admit_btn.Font = new System.Drawing.Font("Calibri", 10F);
            this.Admit_btn.ForeColor = System.Drawing.SystemColors.Control;
            this.Admit_btn.Location = new System.Drawing.Point(311, 68);
            this.Admit_btn.Name = "Admit_btn";
            this.Admit_btn.Size = new System.Drawing.Size(110, 31);
            this.Admit_btn.TabIndex = 63;
            this.Admit_btn.Text = "Admit Student";
            this.Admit_btn.UseVisualStyleBackColor = false;
            this.Admit_btn.Click += new System.EventHandler(this.Admit_btn_Click);
            // 
            // Cancel_btn
            // 
            this.Cancel_btn.BackColor = System.Drawing.Color.DimGray;
            this.Cancel_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Cancel_btn.FlatAppearance.BorderSize = 0;
            this.Cancel_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Cancel_btn.Font = new System.Drawing.Font("Calibri", 10F);
            this.Cancel_btn.ForeColor = System.Drawing.SystemColors.Control;
            this.Cancel_btn.Location = new System.Drawing.Point(328, 103);
            this.Cancel_btn.Name = "Cancel_btn";
            this.Cancel_btn.Size = new System.Drawing.Size(77, 31);
            this.Cancel_btn.TabIndex = 64;
            this.Cancel_btn.Text = "Cancel";
            this.Cancel_btn.UseVisualStyleBackColor = false;
            this.Cancel_btn.Click += new System.EventHandler(this.Cancel_btn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Open Sans SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(314, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 20);
            this.label2.TabIndex = 65;
            this.label2.Text = "Temperature";
            // 
            // Temperature_tb
            // 
            this.Temperature_tb.Font = new System.Drawing.Font("Open Sans SemiBold", 11.25F);
            this.Temperature_tb.Location = new System.Drawing.Point(329, 34);
            this.Temperature_tb.MaxLength = 5;
            this.Temperature_tb.Name = "Temperature_tb";
            this.Temperature_tb.Size = new System.Drawing.Size(74, 28);
            this.Temperature_tb.TabIndex = 66;
            this.Temperature_tb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Temperature_tb_KeyPress);
            // 
            // StudentEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 140);
            this.Controls.Add(this.Temperature_tb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Admit_btn);
            this.Controls.Add(this.Cancel_btn);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StudentEntry";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StudentEntry";
            this.Load += new System.EventHandler(this.StudentEntry_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label FullName_lbl;
        private System.Windows.Forms.Label Address_lbl;
        private System.Windows.Forms.Label Age_lbl;
        private System.Windows.Forms.Label Phone_lbl;
        private System.Windows.Forms.Label Dept_lbl;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Admit_btn;
        private System.Windows.Forms.Button Cancel_btn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Temperature_tb;
    }
}