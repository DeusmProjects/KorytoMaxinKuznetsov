namespace KorytoView
{
    partial class FormCreateOrder
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
            this.label_SUMMARY = new System.Windows.Forms.Label();
            this.label_AMOUNT = new System.Windows.Forms.Label();
            this.label_CRNAME = new System.Windows.Forms.Label();
            this.label_CNAME = new System.Windows.Forms.Label();
            this.textBoxSummary = new System.Windows.Forms.TextBox();
            this.textBoxAmount = new System.Windows.Forms.TextBox();
            this.comboBoxCar = new System.Windows.Forms.ComboBox();
            this.comboBoxName = new System.Windows.Forms.ComboBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label_SUMMARY
            // 
            this.label_SUMMARY.AutoSize = true;
            this.label_SUMMARY.Location = new System.Drawing.Point(15, 135);
            this.label_SUMMARY.Name = "label_SUMMARY";
            this.label_SUMMARY.Size = new System.Drawing.Size(41, 13);
            this.label_SUMMARY.TabIndex = 34;
            this.label_SUMMARY.Text = "Сумма";
            // 
            // label_AMOUNT
            // 
            this.label_AMOUNT.AutoSize = true;
            this.label_AMOUNT.Location = new System.Drawing.Point(15, 101);
            this.label_AMOUNT.Name = "label_AMOUNT";
            this.label_AMOUNT.Size = new System.Drawing.Size(66, 13);
            this.label_AMOUNT.TabIndex = 33;
            this.label_AMOUNT.Text = "Количество";
            // 
            // label_CRNAME
            // 
            this.label_CRNAME.AutoSize = true;
            this.label_CRNAME.Location = new System.Drawing.Point(12, 65);
            this.label_CRNAME.Name = "label_CRNAME";
            this.label_CRNAME.Size = new System.Drawing.Size(69, 13);
            this.label_CRNAME.TabIndex = 32;
            this.label_CRNAME.Text = "Автомобиль";
            // 
            // label_CNAME
            // 
            this.label_CNAME.AutoSize = true;
            this.label_CNAME.Location = new System.Drawing.Point(12, 28);
            this.label_CNAME.Name = "label_CNAME";
            this.label_CNAME.Size = new System.Drawing.Size(43, 13);
            this.label_CNAME.TabIndex = 31;
            this.label_CNAME.Text = "Клиент";
            // 
            // textBoxSummary
            // 
            this.textBoxSummary.Location = new System.Drawing.Point(101, 135);
            this.textBoxSummary.Name = "textBoxSummary";
            this.textBoxSummary.Size = new System.Drawing.Size(205, 20);
            this.textBoxSummary.TabIndex = 30;
            this.textBoxSummary.TextChanged += new System.EventHandler(this.textBoxSummary_TextChanged);
            // 
            // textBoxAmount
            // 
            this.textBoxAmount.Location = new System.Drawing.Point(101, 101);
            this.textBoxAmount.Name = "textBoxAmount";
            this.textBoxAmount.Size = new System.Drawing.Size(205, 20);
            this.textBoxAmount.TabIndex = 29;
            // 
            // comboBoxCar
            // 
            this.comboBoxCar.FormattingEnabled = true;
            this.comboBoxCar.Location = new System.Drawing.Point(101, 65);
            this.comboBoxCar.Name = "comboBoxCar";
            this.comboBoxCar.Size = new System.Drawing.Size(205, 21);
            this.comboBoxCar.TabIndex = 28;
            // 
            // comboBoxName
            // 
            this.comboBoxName.FormattingEnabled = true;
            this.comboBoxName.Location = new System.Drawing.Point(101, 25);
            this.comboBoxName.Name = "comboBoxName";
            this.comboBoxName.Size = new System.Drawing.Size(205, 21);
            this.comboBoxName.TabIndex = 27;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(174, 206);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(132, 32);
            this.buttonCancel.TabIndex = 36;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(18, 206);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(132, 32);
            this.buttonSave.TabIndex = 35;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // FormCreateOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 250);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.label_SUMMARY);
            this.Controls.Add(this.label_AMOUNT);
            this.Controls.Add(this.label_CRNAME);
            this.Controls.Add(this.label_CNAME);
            this.Controls.Add(this.textBoxSummary);
            this.Controls.Add(this.textBoxAmount);
            this.Controls.Add(this.comboBoxCar);
            this.Controls.Add(this.comboBoxName);
            this.Name = "FormCreateOrder";
            this.Text = "Заказ";
            this.Load += new System.EventHandler(this.FormCreateOrder_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_SUMMARY;
        private System.Windows.Forms.Label label_AMOUNT;
        private System.Windows.Forms.Label label_CRNAME;
        private System.Windows.Forms.Label label_CNAME;
        private System.Windows.Forms.TextBox textBoxSummary;
        private System.Windows.Forms.TextBox textBoxAmount;
        private System.Windows.Forms.ComboBox comboBoxCar;
        private System.Windows.Forms.ComboBox comboBoxName;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
    }
}