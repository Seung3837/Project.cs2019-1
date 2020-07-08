namespace Team
{
    partial class LoginForm
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
            this.idLoginLabel = new System.Windows.Forms.Label();
            this.secondIdLoginTextBox = new System.Windows.Forms.TextBox();
            this.idLoginCompleteButton = new System.Windows.Forms.Button();
            this.idLoginCancelButton = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.firstIdLoginLabel = new System.Windows.Forms.Label();
            this.secondIdErrorMessageLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // idLoginLabel
            // 
            this.idLoginLabel.AutoSize = true;
            this.idLoginLabel.Font = new System.Drawing.Font("굴림", 10F);
            this.idLoginLabel.Location = new System.Drawing.Point(71, 54);
            this.idLoginLabel.Name = "idLoginLabel";
            this.idLoginLabel.Size = new System.Drawing.Size(30, 14);
            this.idLoginLabel.TabIndex = 0;
            this.idLoginLabel.Text = "ID :";
            // 
            // secondIdLoginTextBox
            // 
            this.secondIdLoginTextBox.Location = new System.Drawing.Point(151, 51);
            this.secondIdLoginTextBox.Name = "secondIdLoginTextBox";
            this.secondIdLoginTextBox.Size = new System.Drawing.Size(93, 21);
            this.secondIdLoginTextBox.TabIndex = 2;
            this.secondIdLoginTextBox.TextChanged += new System.EventHandler(this.secondIdLoginTextBox_TextChanged);
            // 
            // idLoginCompleteButton
            // 
            this.idLoginCompleteButton.Location = new System.Drawing.Point(284, 167);
            this.idLoginCompleteButton.Name = "idLoginCompleteButton";
            this.idLoginCompleteButton.Size = new System.Drawing.Size(69, 22);
            this.idLoginCompleteButton.TabIndex = 3;
            this.idLoginCompleteButton.Text = "완료";
            this.idLoginCompleteButton.UseVisualStyleBackColor = true;
            this.idLoginCompleteButton.Click += new System.EventHandler(this.idLoginCompleteButton_Click);
            // 
            // idLoginCancelButton
            // 
            this.idLoginCancelButton.Location = new System.Drawing.Point(370, 167);
            this.idLoginCancelButton.Name = "idLoginCancelButton";
            this.idLoginCancelButton.Size = new System.Drawing.Size(69, 22);
            this.idLoginCancelButton.TabIndex = 3;
            this.idLoginCancelButton.Text = "취소";
            this.idLoginCancelButton.UseVisualStyleBackColor = true;
            this.idLoginCancelButton.Click += new System.EventHandler(this.idLoginCancelButton_Click);
            // 
            // firstIdLoginLabel
            // 
            this.firstIdLoginLabel.AutoSize = true;
            this.firstIdLoginLabel.Font = new System.Drawing.Font("굴림", 10F);
            this.firstIdLoginLabel.Location = new System.Drawing.Point(107, 53);
            this.firstIdLoginLabel.Name = "firstIdLoginLabel";
            this.firstIdLoginLabel.Size = new System.Drawing.Size(47, 14);
            this.firstIdLoginLabel.TabIndex = 0;
            this.firstIdLoginLabel.Text = "Empty";
            // 
            // secondIdErrorMessageLabel
            // 
            this.secondIdErrorMessageLabel.AutoSize = true;
            this.secondIdErrorMessageLabel.ForeColor = System.Drawing.Color.Red;
            this.secondIdErrorMessageLabel.Location = new System.Drawing.Point(108, 78);
            this.secondIdErrorMessageLabel.Name = "secondIdErrorMessageLabel";
            this.secondIdErrorMessageLabel.Size = new System.Drawing.Size(89, 12);
            this.secondIdErrorMessageLabel.TabIndex = 4;
            this.secondIdErrorMessageLabel.Text = "Error Message";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 198);
            this.Controls.Add(this.secondIdErrorMessageLabel);
            this.Controls.Add(this.idLoginCancelButton);
            this.Controls.Add(this.idLoginCompleteButton);
            this.Controls.Add(this.secondIdLoginTextBox);
            this.Controls.Add(this.firstIdLoginLabel);
            this.Controls.Add(this.idLoginLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "LoginForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label idLoginLabel;
        private System.Windows.Forms.TextBox secondIdLoginTextBox;
        private System.Windows.Forms.Button idLoginCompleteButton;
        private System.Windows.Forms.Button idLoginCancelButton;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Label firstIdLoginLabel;
        private System.Windows.Forms.Label secondIdErrorMessageLabel;
    }
}