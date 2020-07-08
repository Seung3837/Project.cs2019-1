namespace Team.View
{
    partial class ChangeTeacherForm
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
            this.TeachersView = new System.Windows.Forms.DataGridView();
            this.selectedTeacherView = new System.Windows.Forms.DataGridView();
            this.ChangeButton = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.TeachersView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.selectedTeacherView)).BeginInit();
            this.SuspendLayout();
            // 
            // TeachersView
            // 
            this.TeachersView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TeachersView.Location = new System.Drawing.Point(7, 24);
            this.TeachersView.Name = "TeachersView";
            this.TeachersView.RowTemplate.Height = 23;
            this.TeachersView.Size = new System.Drawing.Size(220, 190);
            this.TeachersView.TabIndex = 0;
            // 
            // selectedTeacherView
            // 
            this.selectedTeacherView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.selectedTeacherView.Location = new System.Drawing.Point(287, 24);
            this.selectedTeacherView.Name = "selectedTeacherView";
            this.selectedTeacherView.RowTemplate.Height = 23;
            this.selectedTeacherView.Size = new System.Drawing.Size(220, 190);
            this.selectedTeacherView.TabIndex = 1;
            // 
            // ChangeButton
            // 
            this.ChangeButton.Location = new System.Drawing.Point(237, 97);
            this.ChangeButton.Name = "ChangeButton";
            this.ChangeButton.Size = new System.Drawing.Size(40, 42);
            this.ChangeButton.TabIndex = 2;
            this.ChangeButton.Text = "변경";
            this.ChangeButton.UseVisualStyleBackColor = true;
            this.ChangeButton.Click += new System.EventHandler(this.ChangeButton_Click);
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(394, 220);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 3;
            this.OKButton.Text = "완료";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // ChangeTeacherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 252);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.ChangeButton);
            this.Controls.Add(this.selectedTeacherView);
            this.Controls.Add(this.TeachersView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ChangeTeacherForm";
            this.Text = "ChangeTeacherForm";
            ((System.ComponentModel.ISupportInitialize)(this.TeachersView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.selectedTeacherView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView TeachersView;
        private System.Windows.Forms.DataGridView selectedTeacherView;
        private System.Windows.Forms.Button ChangeButton;
        private System.Windows.Forms.Button OKButton;
    }
}