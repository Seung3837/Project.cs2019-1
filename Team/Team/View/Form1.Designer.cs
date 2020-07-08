namespace Team
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.startStudentButton = new System.Windows.Forms.Button();
            this.startTeacherButton = new System.Windows.Forms.Button();
            this.startPostButton = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.startLectureButton = new System.Windows.Forms.Button();
            this.colorDialog2 = new System.Windows.Forms.ColorDialog();
            this.SuspendLayout();
            // 
            // startStudentButton
            // 
            this.startStudentButton.Location = new System.Drawing.Point(163, 148);
            this.startStudentButton.Name = "startStudentButton";
            this.startStudentButton.Size = new System.Drawing.Size(135, 150);
            this.startStudentButton.TabIndex = 0;
            this.startStudentButton.Text = "학생";
            this.startStudentButton.UseVisualStyleBackColor = true;
            this.startStudentButton.Click += new System.EventHandler(this.startStudentButton_Click);
            // 
            // startTeacherButton
            // 
            this.startTeacherButton.Location = new System.Drawing.Point(319, 148);
            this.startTeacherButton.Name = "startTeacherButton";
            this.startTeacherButton.Size = new System.Drawing.Size(134, 150);
            this.startTeacherButton.TabIndex = 1;
            this.startTeacherButton.Text = "선생님";
            this.startTeacherButton.UseVisualStyleBackColor = true;
            this.startTeacherButton.Click += new System.EventHandler(this.startTeacherButton_Click);
            // 
            // startPostButton
            // 
            this.startPostButton.Location = new System.Drawing.Point(163, 315);
            this.startPostButton.Name = "startPostButton";
            this.startPostButton.Size = new System.Drawing.Size(290, 66);
            this.startPostButton.TabIndex = 2;
            this.startPostButton.Text = "공지사항";
            this.startPostButton.UseVisualStyleBackColor = true;
            this.startPostButton.Click += new System.EventHandler(this.startPostButton_Click);
            // 
            // startLectureButton
            // 
            this.startLectureButton.Location = new System.Drawing.Point(163, 67);
            this.startLectureButton.Name = "startLectureButton";
            this.startLectureButton.Size = new System.Drawing.Size(290, 66);
            this.startLectureButton.TabIndex = 2;
            this.startLectureButton.Text = "강의";
            this.startLectureButton.UseVisualStyleBackColor = true;
            this.startLectureButton.Click += new System.EventHandler(this.startLectureButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 443);
            this.Controls.Add(this.startLectureButton);
            this.Controls.Add(this.startPostButton);
            this.Controls.Add(this.startTeacherButton);
            this.Controls.Add(this.startStudentButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Location = new System.Drawing.Point(600, 200);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button startStudentButton;
        private System.Windows.Forms.Button startTeacherButton;
        private System.Windows.Forms.Button startPostButton;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button startLectureButton;
        private System.Windows.Forms.ColorDialog colorDialog2;
    }
}

