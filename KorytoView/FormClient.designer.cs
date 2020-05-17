namespace KorytoView
{
    partial class FormClient
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
            this.textBoxMail = new System.Windows.Forms.TextBox();
            this.label_Mail = new System.Windows.Forms.Label();
            this.button_CANCEL = new System.Windows.Forms.Button();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label_CNAME = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxLogin = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxMail
            // 
            this.textBoxMail.Location = new System.Drawing.Point(57, 85);
            this.textBoxMail.Multiline = true;
            this.textBoxMail.Name = "textBoxMail";
            this.textBoxMail.Size = new System.Drawing.Size(304, 27);
            this.textBoxMail.TabIndex = 11;
            // 
            // label_Mail
            // 
            this.label_Mail.AutoSize = true;
            this.label_Mail.Location = new System.Drawing.Point(9, 88);
            this.label_Mail.Name = "label_Mail";
            this.label_Mail.Size = new System.Drawing.Size(37, 13);
            this.label_Mail.TabIndex = 10;
            this.label_Mail.Text = "Почта";
            // 
            // button_CANCEL
            // 
            this.button_CANCEL.Location = new System.Drawing.Point(229, 317);
            this.button_CANCEL.Name = "button_CANCEL";
            this.button_CANCEL.Size = new System.Drawing.Size(132, 32);
            this.button_CANCEL.TabIndex = 9;
            this.button_CANCEL.Text = "Отмена";
            this.button_CANCEL.UseVisualStyleBackColor = true;
            this.button_CANCEL.Click += new System.EventHandler(this.button_CANCEL_Click);
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(57, 22);
            this.textBoxName.Multiline = true;
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(304, 27);
            this.textBoxName.TabIndex = 7;
            // 
            // label_CNAME
            // 
            this.label_CNAME.AutoSize = true;
            this.label_CNAME.Location = new System.Drawing.Point(12, 25);
            this.label_CNAME.Name = "label_CNAME";
            this.label_CNAME.Size = new System.Drawing.Size(34, 13);
            this.label_CNAME.TabIndex = 6;
            this.label_CNAME.Text = "ФИО";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(91, 317);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(132, 32);
            this.buttonSave.TabIndex = 12;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(57, 216);
            this.textBoxPassword.Multiline = true;
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(304, 27);
            this.textBoxPassword.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 219);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Пароль";
            // 
            // textBoxLogin
            // 
            this.textBoxLogin.Location = new System.Drawing.Point(57, 153);
            this.textBoxLogin.Multiline = true;
            this.textBoxLogin.Name = "textBoxLogin";
            this.textBoxLogin.Size = new System.Drawing.Size(304, 27);
            this.textBoxLogin.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 156);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Логин";
            // 
            // FormClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 361);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxLogin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxMail);
            this.Controls.Add(this.label_Mail);
            this.Controls.Add(this.button_CANCEL);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.label_CNAME);
            this.Name = "FormClient";
            this.Text = "FormClient";
            this.Load += new System.EventHandler(this.FormClient_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxMail;
        private System.Windows.Forms.Label label_Mail;
        private System.Windows.Forms.Button button_CANCEL;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label_CNAME;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxLogin;
        private System.Windows.Forms.Label label2;
    }
}