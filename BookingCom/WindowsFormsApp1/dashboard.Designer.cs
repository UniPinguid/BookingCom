namespace BookingCom
{
    partial class dashboard
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
            this.btnLuuTru = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDatXe = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnLuuTru
            // 
            this.btnLuuTru.Font = new System.Drawing.Font("Inter SemiBold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuuTru.Location = new System.Drawing.Point(278, 209);
            this.btnLuuTru.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnLuuTru.Name = "btnLuuTru";
            this.btnLuuTru.Size = new System.Drawing.Size(258, 124);
            this.btnLuuTru.TabIndex = 0;
            this.btnLuuTru.Text = "Lưu trú";
            this.btnLuuTru.UseVisualStyleBackColor = true;
            this.btnLuuTru.Click += new System.EventHandler(this.btnLuuTru_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Inter", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Booking.com";
            // 
            // btnDatXe
            // 
            this.btnDatXe.Font = new System.Drawing.Font("Inter SemiBold", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnDatXe.Location = new System.Drawing.Point(699, 209);
            this.btnDatXe.Name = "btnDatXe";
            this.btnDatXe.Size = new System.Drawing.Size(220, 124);
            this.btnDatXe.TabIndex = 2;
            this.btnDatXe.Text = "Đặt xe";
            this.btnDatXe.UseVisualStyleBackColor = true;
            this.btnDatXe.Click += new System.EventHandler(this.btnDatXe_Click);
            // 
            // dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1222, 654);
            this.Controls.Add(this.btnDatXe);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnLuuTru);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "dashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dashboard";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLuuTru;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDatXe;
    }
}

