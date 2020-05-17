namespace KorytoView
{
    partial class FormClientStat
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
            this.textBoxAllCars = new System.Windows.Forms.TextBox();
            this.textBoxAverage = new System.Windows.Forms.TextBox();
            this.textBoxPopular = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxAllCars
            // 
            this.textBoxAllCars.Location = new System.Drawing.Point(15, 52);
            this.textBoxAllCars.Name = "textBoxAllCars";
            this.textBoxAllCars.Size = new System.Drawing.Size(348, 20);
            this.textBoxAllCars.TabIndex = 0;
            // 
            // textBoxAverage
            // 
            this.textBoxAverage.Location = new System.Drawing.Point(15, 131);
            this.textBoxAverage.Name = "textBoxAverage";
            this.textBoxAverage.Size = new System.Drawing.Size(348, 20);
            this.textBoxAverage.TabIndex = 1;
            // 
            // textBoxPopular
            // 
            this.textBoxPopular.Location = new System.Drawing.Point(15, 215);
            this.textBoxPopular.Name = "textBoxPopular";
            this.textBoxPopular.Size = new System.Drawing.Size(348, 20);
            this.textBoxPopular.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 188);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Самая заказываемая машина";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Средний чек ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(158, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Всего автомобилей заказано";
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(255, 263);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(108, 37);
            this.buttonOk.TabIndex = 6;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // FormClientStat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 317);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxPopular);
            this.Controls.Add(this.textBoxAverage);
            this.Controls.Add(this.textBoxAllCars);
            this.Name = "FormClientStat";
            this.Text = "FormClientStat";
            this.Load += new System.EventHandler(this.FormClientStat_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxAllCars;
        private System.Windows.Forms.TextBox textBoxAverage;
        private System.Windows.Forms.TextBox textBoxPopular;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonOk;
    }
}