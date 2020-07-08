namespace Team
{
    partial class InsertForm
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
            this.insertPictureBox = new System.Windows.Forms.PictureBox();
            this.imageRegisterButton = new System.Windows.Forms.Button();
            this.YesButton = new System.Windows.Forms.Button();
            this.NoButton = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label1 = new System.Windows.Forms.Label();
            this.NameTxtBox = new System.Windows.Forms.TextBox();
            this.SchoolTxtBox = new System.Windows.Forms.TextBox();
            this.PhoneTxtBox1 = new System.Windows.Forms.TextBox();
            this.PhoneTxtBox2 = new System.Windows.Forms.TextBox();
            this.PhoneTxtBox3 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.LectureView = new System.Windows.Forms.DataGridView();
            this.schoolComboBox = new System.Windows.Forms.ComboBox();
            this.gradeComboBox = new System.Windows.Forms.ComboBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.schoolLabel = new System.Windows.Forms.Label();
            this.gradeLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.parentLabel = new System.Windows.Forms.Label();
            this.yearComboBox = new System.Windows.Forms.ComboBox();
            this.monthComboBox = new System.Windows.Forms.ComboBox();
            this.dayComboBox = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.parentTextBox3 = new System.Windows.Forms.TextBox();
            this.parentTextBox2 = new System.Windows.Forms.TextBox();
            this.parentTextBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.insertPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LectureView)).BeginInit();
            this.SuspendLayout();
            // 
            // insertPictureBox
            // 
            this.insertPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.insertPictureBox.Location = new System.Drawing.Point(293, 12);
            this.insertPictureBox.Name = "insertPictureBox";
            this.insertPictureBox.Size = new System.Drawing.Size(136, 133);
            this.insertPictureBox.TabIndex = 0;
            this.insertPictureBox.TabStop = false;
            // 
            // imageRegisterButton
            // 
            this.imageRegisterButton.Location = new System.Drawing.Point(293, 151);
            this.imageRegisterButton.Name = "imageRegisterButton";
            this.imageRegisterButton.Size = new System.Drawing.Size(136, 23);
            this.imageRegisterButton.TabIndex = 1;
            this.imageRegisterButton.Text = "이미지 등록";
            this.imageRegisterButton.UseVisualStyleBackColor = true;
            this.imageRegisterButton.Click += new System.EventHandler(this.ImageInsertButton_Click);
            // 
            // YesButton
            // 
            this.YesButton.Location = new System.Drawing.Point(228, 345);
            this.YesButton.Name = "YesButton";
            this.YesButton.Size = new System.Drawing.Size(97, 23);
            this.YesButton.TabIndex = 2;
            this.YesButton.Text = "완 료";
            this.YesButton.UseVisualStyleBackColor = true;
            this.YesButton.Click += new System.EventHandler(this.YesButton_Click);
            // 
            // NoButton
            // 
            this.NoButton.Location = new System.Drawing.Point(333, 345);
            this.NoButton.Name = "NoButton";
            this.NoButton.Size = new System.Drawing.Size(97, 23);
            this.NoButton.TabIndex = 3;
            this.NoButton.Text = "취 소";
            this.NoButton.UseVisualStyleBackColor = true;
            this.NoButton.Click += new System.EventHandler(this.NoButton_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 23);
            this.label1.TabIndex = 4;
            this.label1.Text = "이      름";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NameTxtBox
            // 
            this.NameTxtBox.Location = new System.Drawing.Point(82, 21);
            this.NameTxtBox.Name = "NameTxtBox";
            this.NameTxtBox.Size = new System.Drawing.Size(64, 21);
            this.NameTxtBox.TabIndex = 5;
            this.NameTxtBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // SchoolTxtBox
            // 
            this.SchoolTxtBox.Location = new System.Drawing.Point(82, 51);
            this.SchoolTxtBox.Name = "SchoolTxtBox";
            this.SchoolTxtBox.Size = new System.Drawing.Size(64, 21);
            this.SchoolTxtBox.TabIndex = 7;
            this.SchoolTxtBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // PhoneTxtBox1
            // 
            this.PhoneTxtBox1.Location = new System.Drawing.Point(82, 112);
            this.PhoneTxtBox1.Name = "PhoneTxtBox1";
            this.PhoneTxtBox1.Size = new System.Drawing.Size(35, 21);
            this.PhoneTxtBox1.TabIndex = 14;
            this.PhoneTxtBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PhoneTxtBox1.TextChanged += new System.EventHandler(this.PhoneTxtBox1_TextChanged);
            // 
            // PhoneTxtBox2
            // 
            this.PhoneTxtBox2.Location = new System.Drawing.Point(128, 112);
            this.PhoneTxtBox2.Name = "PhoneTxtBox2";
            this.PhoneTxtBox2.Size = new System.Drawing.Size(39, 21);
            this.PhoneTxtBox2.TabIndex = 15;
            this.PhoneTxtBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PhoneTxtBox2.TextChanged += new System.EventHandler(this.PhoneTxtBox2_TextChanged);
            // 
            // PhoneTxtBox3
            // 
            this.PhoneTxtBox3.Location = new System.Drawing.Point(178, 112);
            this.PhoneTxtBox3.Name = "PhoneTxtBox3";
            this.PhoneTxtBox3.Size = new System.Drawing.Size(39, 21);
            this.PhoneTxtBox3.TabIndex = 16;
            this.PhoneTxtBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PhoneTxtBox3.TextChanged += new System.EventHandler(this.PhoneTxtBox3_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(117, 117);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 12);
            this.label7.TabIndex = 17;
            this.label7.Text = "-";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(167, 117);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 12);
            this.label8.TabIndex = 18;
            this.label8.Text = "-";
            // 
            // LectureView
            // 
            this.LectureView.AllowUserToResizeRows = false;
            this.LectureView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.LectureView.Location = new System.Drawing.Point(12, 180);
            this.LectureView.Name = "LectureView";
            this.LectureView.RowTemplate.Height = 23;
            this.LectureView.Size = new System.Drawing.Size(418, 159);
            this.LectureView.TabIndex = 19;
            this.LectureView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.LectureView_CellDoubleClick);
            // 
            // schoolComboBox
            // 
            this.schoolComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.schoolComboBox.FormattingEnabled = true;
            this.schoolComboBox.Location = new System.Drawing.Point(150, 51);
            this.schoolComboBox.Name = "schoolComboBox";
            this.schoolComboBox.Size = new System.Drawing.Size(35, 20);
            this.schoolComboBox.TabIndex = 20;
            this.schoolComboBox.SelectedIndexChanged += new System.EventHandler(this.schoolComboBox_SelectedIndexChanged);
            // 
            // gradeComboBox
            // 
            this.gradeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gradeComboBox.FormattingEnabled = true;
            this.gradeComboBox.Location = new System.Drawing.Point(244, 51);
            this.gradeComboBox.Name = "gradeComboBox";
            this.gradeComboBox.Size = new System.Drawing.Size(35, 20);
            this.gradeComboBox.TabIndex = 21;
            // 
            // schoolLabel
            // 
            this.schoolLabel.Location = new System.Drawing.Point(12, 51);
            this.schoolLabel.Name = "schoolLabel";
            this.schoolLabel.Size = new System.Drawing.Size(53, 23);
            this.schoolLabel.TabIndex = 22;
            this.schoolLabel.Text = "학      교";
            this.schoolLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gradeLabel
            // 
            this.gradeLabel.Location = new System.Drawing.Point(189, 51);
            this.gradeLabel.Name = "gradeLabel";
            this.gradeLabel.Size = new System.Drawing.Size(53, 23);
            this.gradeLabel.TabIndex = 23;
            this.gradeLabel.Text = "학      년";
            this.gradeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 23);
            this.label4.TabIndex = 24;
            this.label4.Text = "생년월일";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(12, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 23);
            this.label5.TabIndex = 25;
            this.label5.Text = "전화번호";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // parentLabel
            // 
            this.parentLabel.Location = new System.Drawing.Point(4, 141);
            this.parentLabel.Name = "parentLabel";
            this.parentLabel.Size = new System.Drawing.Size(68, 23);
            this.parentLabel.TabIndex = 26;
            this.parentLabel.Text = "보호자번호";
            this.parentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // yearComboBox
            // 
            this.yearComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.yearComboBox.FormattingEnabled = true;
            this.yearComboBox.Location = new System.Drawing.Point(82, 82);
            this.yearComboBox.Name = "yearComboBox";
            this.yearComboBox.Size = new System.Drawing.Size(50, 20);
            this.yearComboBox.TabIndex = 27;
            this.yearComboBox.SelectedIndexChanged += new System.EventHandler(this.yearComboBox_SelectedIndexChanged);
            // 
            // monthComboBox
            // 
            this.monthComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.monthComboBox.FormattingEnabled = true;
            this.monthComboBox.Location = new System.Drawing.Point(151, 82);
            this.monthComboBox.Name = "monthComboBox";
            this.monthComboBox.Size = new System.Drawing.Size(38, 20);
            this.monthComboBox.TabIndex = 28;
            this.monthComboBox.SelectedIndexChanged += new System.EventHandler(this.monthComboBox_SelectedIndexChanged);
            // 
            // dayComboBox
            // 
            this.dayComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dayComboBox.FormattingEnabled = true;
            this.dayComboBox.Location = new System.Drawing.Point(208, 82);
            this.dayComboBox.Name = "dayComboBox";
            this.dayComboBox.Size = new System.Drawing.Size(38, 20);
            this.dayComboBox.TabIndex = 29;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(134, 81);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 23);
            this.label9.TabIndex = 30;
            this.label9.Text = "년";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(190, 81);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 23);
            this.label10.TabIndex = 31;
            this.label10.Text = "월";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(247, 81);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 23);
            this.label11.TabIndex = 32;
            this.label11.Text = "일";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(167, 147);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(11, 12);
            this.label12.TabIndex = 37;
            this.label12.Text = "-";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(117, 147);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(11, 12);
            this.label13.TabIndex = 36;
            this.label13.Text = "-";
            // 
            // parentTextBox3
            // 
            this.parentTextBox3.Location = new System.Drawing.Point(178, 142);
            this.parentTextBox3.Name = "parentTextBox3";
            this.parentTextBox3.Size = new System.Drawing.Size(39, 21);
            this.parentTextBox3.TabIndex = 35;
            this.parentTextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.parentTextBox3.TextChanged += new System.EventHandler(this.parentTextBox3_TextChanged);
            // 
            // parentTextBox2
            // 
            this.parentTextBox2.Location = new System.Drawing.Point(128, 142);
            this.parentTextBox2.Name = "parentTextBox2";
            this.parentTextBox2.Size = new System.Drawing.Size(39, 21);
            this.parentTextBox2.TabIndex = 34;
            this.parentTextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.parentTextBox2.TextChanged += new System.EventHandler(this.parentTextBox2_TextChanged);
            // 
            // parentTextBox1
            // 
            this.parentTextBox1.Location = new System.Drawing.Point(82, 142);
            this.parentTextBox1.Name = "parentTextBox1";
            this.parentTextBox1.Size = new System.Drawing.Size(35, 21);
            this.parentTextBox1.TabIndex = 33;
            this.parentTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.parentTextBox1.TextChanged += new System.EventHandler(this.parentTextBox1_TextChanged);
            // 
            // InsertForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 380);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.parentTextBox3);
            this.Controls.Add(this.parentTextBox2);
            this.Controls.Add(this.parentTextBox1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.dayComboBox);
            this.Controls.Add(this.monthComboBox);
            this.Controls.Add(this.yearComboBox);
            this.Controls.Add(this.parentLabel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.gradeLabel);
            this.Controls.Add(this.schoolLabel);
            this.Controls.Add(this.gradeComboBox);
            this.Controls.Add(this.schoolComboBox);
            this.Controls.Add(this.LectureView);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.PhoneTxtBox3);
            this.Controls.Add(this.PhoneTxtBox2);
            this.Controls.Add(this.PhoneTxtBox1);
            this.Controls.Add(this.SchoolTxtBox);
            this.Controls.Add(this.NameTxtBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.NoButton);
            this.Controls.Add(this.YesButton);
            this.Controls.Add(this.imageRegisterButton);
            this.Controls.Add(this.insertPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "InsertForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "InsertForm";
            ((System.ComponentModel.ISupportInitialize)(this.insertPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LectureView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox insertPictureBox;
        private System.Windows.Forms.Button imageRegisterButton;
        private System.Windows.Forms.Button YesButton;
        private System.Windows.Forms.Button NoButton;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox NameTxtBox;
        private System.Windows.Forms.TextBox SchoolTxtBox;
        private System.Windows.Forms.TextBox PhoneTxtBox1;
        private System.Windows.Forms.TextBox PhoneTxtBox2;
        private System.Windows.Forms.TextBox PhoneTxtBox3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView LectureView;
        private System.Windows.Forms.ComboBox schoolComboBox;
        private System.Windows.Forms.ComboBox gradeComboBox;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Label schoolLabel;
        private System.Windows.Forms.Label gradeLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label parentLabel;
        private System.Windows.Forms.ComboBox yearComboBox;
        private System.Windows.Forms.ComboBox monthComboBox;
        private System.Windows.Forms.ComboBox dayComboBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox parentTextBox3;
        private System.Windows.Forms.TextBox parentTextBox2;
        private System.Windows.Forms.TextBox parentTextBox1;
    }
}