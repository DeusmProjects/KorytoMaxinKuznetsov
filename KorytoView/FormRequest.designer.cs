namespace KorytoView
{
    partial class FormRequest
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
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.buttonDeleteElement = new System.Windows.Forms.Button();
            this.buttonAddElement = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxDate = new System.Windows.Forms.TextBox();
            this.label_CNAME = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonChangeElement = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(500, 287);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(184, 41);
            this.buttonUpdate.TabIndex = 29;
            this.buttonUpdate.Text = "Обновить";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // buttonDeleteElement
            // 
            this.buttonDeleteElement.Location = new System.Drawing.Point(500, 193);
            this.buttonDeleteElement.Name = "buttonDeleteElement";
            this.buttonDeleteElement.Size = new System.Drawing.Size(184, 41);
            this.buttonDeleteElement.TabIndex = 27;
            this.buttonDeleteElement.Text = "Удалить";
            this.buttonDeleteElement.UseVisualStyleBackColor = true;
            this.buttonDeleteElement.Click += new System.EventHandler(this.buttonDeleteElement_Click);
            // 
            // buttonAddElement
            // 
            this.buttonAddElement.Location = new System.Drawing.Point(500, 146);
            this.buttonAddElement.Name = "buttonAddElement";
            this.buttonAddElement.Size = new System.Drawing.Size(184, 41);
            this.buttonAddElement.TabIndex = 26;
            this.buttonAddElement.Text = "Добавить";
            this.buttonAddElement.UseVisualStyleBackColor = true;
            this.buttonAddElement.Click += new System.EventHandler(this.buttonAddElement_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(497, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Детали :";
            // 
            // dataGridView
            // 
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.GridColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridView.Location = new System.Drawing.Point(12, 12);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(469, 426);
            this.dataGridView.TabIndex = 24;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(656, 406);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(132, 32);
            this.buttonCancel.TabIndex = 23;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(500, 406);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(132, 32);
            this.buttonSave.TabIndex = 22;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // textBoxDate
            // 
            this.textBoxDate.Location = new System.Drawing.Point(571, 37);
            this.textBoxDate.Multiline = true;
            this.textBoxDate.Name = "textBoxDate";
            this.textBoxDate.Size = new System.Drawing.Size(203, 27);
            this.textBoxDate.TabIndex = 31;
            // 
            // label_CNAME
            // 
            this.label_CNAME.AutoSize = true;
            this.label_CNAME.Location = new System.Drawing.Point(497, 12);
            this.label_CNAME.Name = "label_CNAME";
            this.label_CNAME.Size = new System.Drawing.Size(64, 13);
            this.label_CNAME.TabIndex = 30;
            this.label_CNAME.Text = "Заявка от :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(497, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "дд/мм/гг";
            // 
            // buttonChangeElement
            // 
            this.buttonChangeElement.Location = new System.Drawing.Point(500, 240);
            this.buttonChangeElement.Name = "buttonChangeElement";
            this.buttonChangeElement.Size = new System.Drawing.Size(184, 41);
            this.buttonChangeElement.TabIndex = 28;
            this.buttonChangeElement.Text = "Изменить";
            this.buttonChangeElement.UseVisualStyleBackColor = true;
            this.buttonChangeElement.Click += new System.EventHandler(this.buttonChangeElement_Click);
            // 
            // FormRequest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxDate);
            this.Controls.Add(this.label_CNAME);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.buttonChangeElement);
            this.Controls.Add(this.buttonDeleteElement);
            this.Controls.Add(this.buttonAddElement);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Name = "FormRequest";
            this.Text = "Заявка";
            this.Load += new System.EventHandler(this.FormRequest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Button buttonDeleteElement;
        private System.Windows.Forms.Button buttonAddElement;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textBoxDate;
        private System.Windows.Forms.Label label_CNAME;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonChangeElement;
    }
}