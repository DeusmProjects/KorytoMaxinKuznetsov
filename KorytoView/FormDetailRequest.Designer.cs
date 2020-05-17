namespace KorytoView
{
    partial class FormDetailRequest
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
            this.label_DFC_AMOUNT = new System.Windows.Forms.Label();
            this.label_DFC_DNAME = new System.Windows.Forms.Label();
            this.textBoxAmount = new System.Windows.Forms.TextBox();
            this.comboBoxDetails = new System.Windows.Forms.ComboBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label_DFC_AMOUNT
            // 
            this.label_DFC_AMOUNT.AutoSize = true;
            this.label_DFC_AMOUNT.Location = new System.Drawing.Point(14, 73);
            this.label_DFC_AMOUNT.Name = "label_DFC_AMOUNT";
            this.label_DFC_AMOUNT.Size = new System.Drawing.Size(66, 13);
            this.label_DFC_AMOUNT.TabIndex = 23;
            this.label_DFC_AMOUNT.Text = "Количество";
            // 
            // label_DFC_DNAME
            // 
            this.label_DFC_DNAME.AutoSize = true;
            this.label_DFC_DNAME.Location = new System.Drawing.Point(12, 24);
            this.label_DFC_DNAME.Name = "label_DFC_DNAME";
            this.label_DFC_DNAME.Size = new System.Drawing.Size(57, 13);
            this.label_DFC_DNAME.TabIndex = 22;
            this.label_DFC_DNAME.Text = "Название";
            // 
            // textBoxAmount
            // 
            this.textBoxAmount.Location = new System.Drawing.Point(87, 73);
            this.textBoxAmount.Name = "textBoxAmount";
            this.textBoxAmount.Size = new System.Drawing.Size(232, 20);
            this.textBoxAmount.TabIndex = 21;
            // 
            // comboBoxDetails
            // 
            this.comboBoxDetails.FormattingEnabled = true;
            this.comboBoxDetails.Location = new System.Drawing.Point(87, 24);
            this.comboBoxDetails.Name = "comboBoxDetails";
            this.comboBoxDetails.Size = new System.Drawing.Size(232, 21);
            this.comboBoxDetails.TabIndex = 20;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(187, 138);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(132, 32);
            this.buttonCancel.TabIndex = 19;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(49, 138);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(132, 32);
            this.buttonSave.TabIndex = 18;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // FormDetailRequest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 184);
            this.Controls.Add(this.label_DFC_AMOUNT);
            this.Controls.Add(this.label_DFC_DNAME);
            this.Controls.Add(this.textBoxAmount);
            this.Controls.Add(this.comboBoxDetails);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Name = "FormDetailRequest";
            this.Text = "Детали для заявки";
            this.Load += new System.EventHandler(this.FormDetailRequest_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_DFC_AMOUNT;
        private System.Windows.Forms.Label label_DFC_DNAME;
        private System.Windows.Forms.TextBox textBoxAmount;
        private System.Windows.Forms.ComboBox comboBoxDetails;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
    }
}