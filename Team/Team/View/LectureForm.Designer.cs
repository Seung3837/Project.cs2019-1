namespace Team.View
{
    partial class LectureForm
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
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.LectureView = new System.Windows.Forms.DataGridView();
            this.roomSchedule = new System.Windows.Forms.DataGridView();
            this.roomSelectCombo = new System.Windows.Forms.ComboBox();
            this.roomNameLabel = new System.Windows.Forms.Label();
            this.lectureViewSelectButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.LectureView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.roomSchedule)).BeginInit();
            this.SuspendLayout();
            // 
            // LectureView
            // 
            this.LectureView.AllowUserToResizeRows = false;
            this.LectureView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.LectureView.Location = new System.Drawing.Point(10, 73);
            this.LectureView.Name = "LectureView";
            this.LectureView.RowTemplate.Height = 23;
            this.LectureView.Size = new System.Drawing.Size(780, 495);
            this.LectureView.TabIndex = 1;
            // 
            // roomSchedule
            // 
            this.roomSchedule.AllowUserToAddRows = false;
            this.roomSchedule.AllowUserToDeleteRows = false;
            this.roomSchedule.AllowUserToResizeColumns = false;
            this.roomSchedule.AllowUserToResizeRows = false;
            this.roomSchedule.ColumnHeadersHeight = 40;
            this.roomSchedule.Cursor = System.Windows.Forms.Cursors.Help;
            this.roomSchedule.Location = new System.Drawing.Point(10, 73);
            this.roomSchedule.MultiSelect = false;
            this.roomSchedule.Name = "roomSchedule";
            this.roomSchedule.ReadOnly = true;
            this.roomSchedule.RowHeadersVisible = false;
            this.roomSchedule.RowTemplate.Height = 50;
            this.roomSchedule.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.roomSchedule.Size = new System.Drawing.Size(780, 495);
            this.roomSchedule.TabIndex = 2;
            this.roomSchedule.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.roomSchedule_CellDoubleClick);
            // 
            // roomSelectCombo
            // 
            this.roomSelectCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.roomSelectCombo.FormattingEnabled = true;
            this.roomSelectCombo.Location = new System.Drawing.Point(103, 14);
            this.roomSelectCombo.Name = "roomSelectCombo";
            this.roomSelectCombo.Size = new System.Drawing.Size(84, 20);
            this.roomSelectCombo.TabIndex = 3;
            this.roomSelectCombo.SelectedIndexChanged += new System.EventHandler(this.roomSelectCombo_SelectedIndexChanged);
            // 
            // roomNameLabel
            // 
            this.roomNameLabel.Font = new System.Drawing.Font("굴림", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.roomNameLabel.Location = new System.Drawing.Point(257, 14);
            this.roomNameLabel.Name = "roomNameLabel";
            this.roomNameLabel.Size = new System.Drawing.Size(300, 40);
            this.roomNameLabel.TabIndex = 4;
            this.roomNameLabel.Text = "전체 강의실";
            this.roomNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lectureViewSelectButton
            // 
            this.lectureViewSelectButton.Location = new System.Drawing.Point(12, 12);
            this.lectureViewSelectButton.Name = "lectureViewSelectButton";
            this.lectureViewSelectButton.Size = new System.Drawing.Size(85, 55);
            this.lectureViewSelectButton.TabIndex = 5;
            this.lectureViewSelectButton.Text = "button1";
            this.lectureViewSelectButton.UseVisualStyleBackColor = true;
            this.lectureViewSelectButton.Click += new System.EventHandler(this.lectureViewSelectButton_Click);
            // 
            // LectureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 580);
            this.Controls.Add(this.lectureViewSelectButton);
            this.Controls.Add(this.roomNameLabel);
            this.Controls.Add(this.roomSelectCombo);
            this.Controls.Add(this.roomSchedule);
            this.Controls.Add(this.LectureView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "LectureForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "LectureForm";
            ((System.ComponentModel.ISupportInitialize)(this.LectureView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.roomSchedule)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.DataGridView LectureView;
        private System.Windows.Forms.DataGridView roomSchedule;
        private System.Windows.Forms.ComboBox roomSelectCombo;
        private System.Windows.Forms.Label roomNameLabel;
        private System.Windows.Forms.Button lectureViewSelectButton;
    }
}