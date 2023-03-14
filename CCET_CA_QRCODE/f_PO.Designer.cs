namespace CCET_CA_QRCODE
{
    partial class f_PO
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(f_PO));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pic_LOGOUT = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_LOGOUT)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Agency FB", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(416, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 97);
            this.label1.TabIndex = 5;
            this.label1.Text = "PO";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Agency FB", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(58, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 50);
            this.label2.TabIndex = 6;
            this.label2.Text = "PO NO :";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Agency FB", 24F, System.Drawing.FontStyle.Bold);
            this.textBox1.Location = new System.Drawing.Point(200, 140);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(420, 55);
            this.textBox1.TabIndex = 7;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Agency FB", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(21, 229);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(162, 50);
            this.label3.TabIndex = 8;
            this.label3.Text = "PC NAME :";
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Agency FB", 24F, System.Drawing.FontStyle.Bold);
            this.textBox2.Location = new System.Drawing.Point(200, 226);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(420, 55);
            this.textBox2.TabIndex = 9;
            this.textBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox2_KeyDown);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(30, 304);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(756, 326);
            this.dataGridView1.TabIndex = 10;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CCET_CA_QRCODE.Properties.Resources.responsive;
            this.pictureBox1.Location = new System.Drawing.Point(273, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(137, 116);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // pic_LOGOUT
            // 
            this.pic_LOGOUT.Image = global::CCET_CA_QRCODE.Properties.Resources.excel;
            this.pic_LOGOUT.Location = new System.Drawing.Point(649, 153);
            this.pic_LOGOUT.Name = "pic_LOGOUT";
            this.pic_LOGOUT.Size = new System.Drawing.Size(137, 111);
            this.pic_LOGOUT.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_LOGOUT.TabIndex = 13;
            this.pic_LOGOUT.TabStop = false;
            this.pic_LOGOUT.Click += new System.EventHandler(this.pic_LOGOUT_Click);
            // 
            // f_PO
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(815, 642);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pic_LOGOUT);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "f_PO";
            this.Text = "f_PO";
            this.Load += new System.EventHandler(this.f_PO_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_LOGOUT)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.PictureBox pic_LOGOUT;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}