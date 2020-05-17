namespace KorytoView
{
    partial class FormReport
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonCreateReport = new System.Windows.Forms.Button();
            this.textBoxFrom = new System.Windows.Forms.TextBox();
            this.textBoxTo = new System.Windows.Forms.TextBox();
            this.buttonFrom = new System.Windows.Forms.Button();
            this.buttonTo = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.buttonSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(522, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "От";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(520, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "До";
            // 
            // buttonCreateReport
            // 
            this.buttonCreateReport.Location = new System.Drawing.Point(556, 211);
            this.buttonCreateReport.Name = "buttonCreateReport";
            this.buttonCreateReport.Size = new System.Drawing.Size(232, 42);
            this.buttonCreateReport.TabIndex = 4;
            this.buttonCreateReport.Text = "Создать отчет";
            this.buttonCreateReport.UseVisualStyleBackColor = true;
            this.buttonCreateReport.Click += new System.EventHandler(this.buttonCreateReport_Click);
            // 
            // textBoxFrom
            // 
            this.textBoxFrom.Location = new System.Drawing.Point(556, 22);
            this.textBoxFrom.Name = "textBoxFrom";
            this.textBoxFrom.Size = new System.Drawing.Size(232, 20);
            this.textBoxFrom.TabIndex = 5;
            // 
            // textBoxTo
            // 
            this.textBoxTo.Location = new System.Drawing.Point(556, 113);
            this.textBoxTo.Name = "textBoxTo";
            this.textBoxTo.Size = new System.Drawing.Size(232, 20);
            this.textBoxTo.TabIndex = 6;
            // 
            // buttonFrom
            // 
            this.buttonFrom.Location = new System.Drawing.Point(556, 57);
            this.buttonFrom.Name = "buttonFrom";
            this.buttonFrom.Size = new System.Drawing.Size(232, 27);
            this.buttonFrom.TabIndex = 7;
            this.buttonFrom.Text = "Указать дату";
            this.buttonFrom.UseVisualStyleBackColor = true;
            this.buttonFrom.Click += new System.EventHandler(this.buttonFrom_Click);
            // 
            // buttonTo
            // 
            this.buttonTo.Location = new System.Drawing.Point(556, 149);
            this.buttonTo.Name = "buttonTo";
            this.buttonTo.Size = new System.Drawing.Size(232, 27);
            this.buttonTo.TabIndex = 8;
            this.buttonTo.Text = "Указать дату";
            this.buttonTo.UseVisualStyleBackColor = true;
            this.buttonTo.Click += new System.EventHandler(this.buttonTo_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(12, 12);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(483, 426);
            this.dataGridView.TabIndex = 9;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(556, 396);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(232, 42);
            this.buttonSave.TabIndex = 10;
            this.buttonSave.Text = "Сохранить отчет";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // FormReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.buttonTo);
            this.Controls.Add(this.buttonFrom);
            this.Controls.Add(this.textBoxTo);
            this.Controls.Add(this.textBoxFrom);
            this.Controls.Add(this.buttonCreateReport);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormReport";
            this.Text = "ReportForm";
            this.Load += new System.EventHandler(this.ReportForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonCreateReport;
        private System.Windows.Forms.TextBox textBoxFrom;
        private System.Windows.Forms.TextBox textBoxTo;
        private System.Windows.Forms.Button buttonFrom;
        private System.Windows.Forms.Button buttonTo;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button buttonSave;
    }
}