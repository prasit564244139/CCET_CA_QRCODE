namespace CCET_CA_QRCODE
{
    partial class Form1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnQR = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btTYPE = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lbl_USER = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_SHOW_ALL = new System.Windows.Forms.Button();
            this.btn_EXCEL = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.pic_LOGOUT = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_LOGOUT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnQR
            // 
            this.btnQR.BackColor = System.Drawing.Color.DarkOrchid;
            this.btnQR.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnQR.Font = new System.Drawing.Font("Agency FB", 18F, System.Drawing.FontStyle.Bold);
            this.btnQR.ForeColor = System.Drawing.SystemColors.Control;
            this.btnQR.Location = new System.Drawing.Point(379, 129);
            this.btnQR.Name = "btnQR";
            this.btnQR.Size = new System.Drawing.Size(150, 56);
            this.btnQR.TabIndex = 0;
            this.btnQR.Text = "QRCODE";
            this.btnQR.UseVisualStyleBackColor = false;
            this.btnQR.Click += new System.EventHandler(this.btnQR_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Agency FB", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dataGridView1.Location = new System.Drawing.Point(20, 106);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(808, 463);
            this.dataGridView1.TabIndex = 1;
            // 
            // btTYPE
            // 
            this.btTYPE.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btTYPE.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btTYPE.Font = new System.Drawing.Font("Agency FB", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btTYPE.ForeColor = System.Drawing.SystemColors.Control;
            this.btTYPE.Location = new System.Drawing.Point(26, 129);
            this.btTYPE.Name = "btTYPE";
            this.btTYPE.Size = new System.Drawing.Size(140, 56);
            this.btTYPE.TabIndex = 2;
            this.btTYPE.Text = "SPEC";
            this.btTYPE.UseVisualStyleBackColor = false;
            this.btTYPE.Click += new System.EventHandler(this.btTYPE_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.RoyalBlue;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Agency FB", 18F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.SystemColors.Control;
            this.button1.Location = new System.Drawing.Point(548, 129);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(147, 56);
            this.button1.TabIndex = 3;
            this.button1.Text = "STATUS";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbl_USER
            // 
            this.lbl_USER.AutoSize = true;
            this.lbl_USER.Font = new System.Drawing.Font("Agency FB", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_USER.ForeColor = System.Drawing.Color.White;
            this.lbl_USER.Location = new System.Drawing.Point(408, 46);
            this.lbl_USER.Name = "lbl_USER";
            this.lbl_USER.Size = new System.Drawing.Size(75, 39);
            this.lbl_USER.TabIndex = 4;
            this.lbl_USER.Text = "USER";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Agency FB", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(86, 50);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(531, 35);
            this.textBox1.TabIndex = 5;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // btn_SHOW_ALL
            // 
            this.btn_SHOW_ALL.BackColor = System.Drawing.Color.OrangeRed;
            this.btn_SHOW_ALL.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_SHOW_ALL.Font = new System.Drawing.Font("Agency FB", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_SHOW_ALL.ForeColor = System.Drawing.Color.White;
            this.btn_SHOW_ALL.Location = new System.Drawing.Point(636, 33);
            this.btn_SHOW_ALL.Name = "btn_SHOW_ALL";
            this.btn_SHOW_ALL.Size = new System.Drawing.Size(192, 63);
            this.btn_SHOW_ALL.TabIndex = 8;
            this.btn_SHOW_ALL.Text = "ALL STORE";
            this.btn_SHOW_ALL.UseVisualStyleBackColor = false;
            this.btn_SHOW_ALL.Click += new System.EventHandler(this.btn_SHOW_ALL_Click);
            // 
            // btn_EXCEL
            // 
            this.btn_EXCEL.BackColor = System.Drawing.Color.ForestGreen;
            this.btn_EXCEL.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_EXCEL.Font = new System.Drawing.Font("Agency FB", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_EXCEL.ForeColor = System.Drawing.SystemColors.Control;
            this.btn_EXCEL.Location = new System.Drawing.Point(714, 129);
            this.btn_EXCEL.Name = "btn_EXCEL";
            this.btn_EXCEL.Size = new System.Drawing.Size(156, 56);
            this.btn_EXCEL.TabIndex = 10;
            this.btn_EXCEL.Text = "EXCEL";
            this.btn_EXCEL.UseVisualStyleBackColor = false;
            this.btn_EXCEL.Click += new System.EventHandler(this.btn_EXCEL_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Agency FB", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(24, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 39);
            this.label1.TabIndex = 13;
            this.label1.Text = "QR:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.btn_SHOW_ALL);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Agency FB", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(19, 191);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(854, 588);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "INFOMATION";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Agency FB", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(326, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 39);
            this.label2.TabIndex = 16;
            this.label2.Text = "EMP :";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Gray;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Agency FB", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.SystemColors.Control;
            this.button2.Location = new System.Drawing.Point(188, 129);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(168, 56);
            this.button2.TabIndex = 17;
            this.button2.Text = "INFORMATION";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // pic_LOGOUT
            // 
            this.pic_LOGOUT.Image = global::CCET_CA_QRCODE.Properties.Resources.logout;
            this.pic_LOGOUT.Location = new System.Drawing.Point(752, 22);
            this.pic_LOGOUT.Name = "pic_LOGOUT";
            this.pic_LOGOUT.Size = new System.Drawing.Size(95, 83);
            this.pic_LOGOUT.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_LOGOUT.TabIndex = 12;
            this.pic_LOGOUT.TabStop = false;
            this.pic_LOGOUT.Click += new System.EventHandler(this.pic_LOGOUT_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::CCET_CA_QRCODE.Properties.Resources.profile__1_;
            this.pictureBox2.Location = new System.Drawing.Point(219, 26);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(91, 82);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 11;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CCET_CA_QRCODE.Properties.Resources.CA_TEAM_FACTORY4;
            this.pictureBox1.Location = new System.Drawing.Point(50, 15);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(133, 98);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(895, 788);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pic_LOGOUT);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.btn_EXCEL);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lbl_USER);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btTYPE);
            this.Controls.Add(this.btnQR);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(913, 835);
            this.MinimumSize = new System.Drawing.Size(913, 835);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "INFOMATION V1.41";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_LOGOUT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnQR;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btTYPE;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbl_USER;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn_SHOW_ALL;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_EXCEL;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pic_LOGOUT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
    }
}

