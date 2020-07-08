namespace Team.View
{
    partial class NoticeForm
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
            this.noticeView = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.writerTextBox = new System.Windows.Forms.TextBox();
            this.insertTimeTextBox = new System.Windows.Forms.TextBox();
            this.contentTextBox = new System.Windows.Forms.RichTextBox();
            this.InsertButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.InsertOKButton = new System.Windows.Forms.Button();
            this.InsertNoButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.noticeView)).BeginInit();
            this.SuspendLayout();
            // 
            // noticeView
            // 
            this.noticeView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.noticeView.Location = new System.Drawing.Point(12, 48);
            this.noticeView.Name = "noticeView";
            this.noticeView.RowTemplate.Height = 23;
            this.noticeView.Size = new System.Drawing.Size(386, 248);
            this.noticeView.TabIndex = 0;
            this.noticeView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.noticeView_CellDoubleClick);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(415, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "제      목";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(415, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "작성일자";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(415, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 23);
            this.label3.TabIndex = 3;
            this.label3.Text = "공지내용";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(415, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 23);
            this.label4.TabIndex = 4;
            this.label4.Text = "작 성 자";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new System.Drawing.Point(481, 49);
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(307, 21);
            this.titleTextBox.TabIndex = 5;
            // 
            // writerTextBox
            // 
            this.writerTextBox.Location = new System.Drawing.Point(481, 82);
            this.writerTextBox.Name = "writerTextBox";
            this.writerTextBox.Size = new System.Drawing.Size(307, 21);
            this.writerTextBox.TabIndex = 6;
            // 
            // insertTimeTextBox
            // 
            this.insertTimeTextBox.Location = new System.Drawing.Point(480, 113);
            this.insertTimeTextBox.Name = "insertTimeTextBox";
            this.insertTimeTextBox.Size = new System.Drawing.Size(307, 21);
            this.insertTimeTextBox.TabIndex = 7;
            // 
            // contentTextBox
            // 
            this.contentTextBox.Location = new System.Drawing.Point(417, 171);
            this.contentTextBox.Name = "contentTextBox";
            this.contentTextBox.Size = new System.Drawing.Size(370, 125);
            this.contentTextBox.TabIndex = 8;
            this.contentTextBox.Text = "";
            // 
            // InsertButton
            // 
            this.InsertButton.Location = new System.Drawing.Point(623, 12);
            this.InsertButton.Name = "InsertButton";
            this.InsertButton.Size = new System.Drawing.Size(75, 23);
            this.InsertButton.TabIndex = 9;
            this.InsertButton.Text = "등록";
            this.InsertButton.UseVisualStyleBackColor = true;
            this.InsertButton.Click += new System.EventHandler(this.InsertButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(713, 12);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(75, 23);
            this.DeleteButton.TabIndex = 10;
            this.DeleteButton.Text = "삭제";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // InsertOKButton
            // 
            this.InsertOKButton.Location = new System.Drawing.Point(623, 12);
            this.InsertOKButton.Name = "InsertOKButton";
            this.InsertOKButton.Size = new System.Drawing.Size(75, 23);
            this.InsertOKButton.TabIndex = 11;
            this.InsertOKButton.Text = "완료";
            this.InsertOKButton.UseVisualStyleBackColor = true;
            this.InsertOKButton.Click += new System.EventHandler(this.InsertOKButton_Click);
            // 
            // InsertNoButton
            // 
            this.InsertNoButton.Location = new System.Drawing.Point(712, 12);
            this.InsertNoButton.Name = "InsertNoButton";
            this.InsertNoButton.Size = new System.Drawing.Size(75, 23);
            this.InsertNoButton.TabIndex = 12;
            this.InsertNoButton.Text = "취소";
            this.InsertNoButton.UseVisualStyleBackColor = true;
            this.InsertNoButton.Click += new System.EventHandler(this.InsertNoButton_Click);
            // 
            // NoticeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 307);
            this.Controls.Add(this.InsertNoButton);
            this.Controls.Add(this.InsertOKButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.InsertButton);
            this.Controls.Add(this.contentTextBox);
            this.Controls.Add(this.insertTimeTextBox);
            this.Controls.Add(this.writerTextBox);
            this.Controls.Add(this.titleTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.noticeView);
            this.Name = "NoticeForm";
            this.Text = "NoticeForm";
            ((System.ComponentModel.ISupportInitialize)(this.noticeView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView noticeView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox titleTextBox;
        private System.Windows.Forms.TextBox writerTextBox;
        private System.Windows.Forms.TextBox insertTimeTextBox;
        private System.Windows.Forms.RichTextBox contentTextBox;
        private System.Windows.Forms.Button InsertButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button InsertOKButton;
        private System.Windows.Forms.Button InsertNoButton;
    }
}