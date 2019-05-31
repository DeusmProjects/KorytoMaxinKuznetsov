namespace KorytoView
{
    partial class FormMain
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
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.справочникToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.клиентыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.машиныToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.деталиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.заявкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отчетыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.покупкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.заявкиToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonTakeOrder = new System.Windows.Forms.Button();
            this.buttonFinish = new System.Windows.Forms.Button();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.buttonOrderView = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(12, 38);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(737, 509);
            this.dataGridView.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.справочникToolStripMenuItem,
            this.отчетыToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1048, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // справочникToolStripMenuItem
            // 
            this.справочникToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.клиентыToolStripMenuItem,
            this.машиныToolStripMenuItem,
            this.деталиToolStripMenuItem,
            this.заявкиToolStripMenuItem});
            this.справочникToolStripMenuItem.Name = "справочникToolStripMenuItem";
            this.справочникToolStripMenuItem.Size = new System.Drawing.Size(87, 20);
            this.справочникToolStripMenuItem.Text = "Справочник";
            // 
            // клиентыToolStripMenuItem
            // 
            this.клиентыToolStripMenuItem.Name = "клиентыToolStripMenuItem";
            this.клиентыToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.клиентыToolStripMenuItem.Text = "Клиенты";
            this.клиентыToolStripMenuItem.Click += new System.EventHandler(this.клиентыToolStripMenuItem_Click);
            // 
            // машиныToolStripMenuItem
            // 
            this.машиныToolStripMenuItem.Name = "машиныToolStripMenuItem";
            this.машиныToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.машиныToolStripMenuItem.Text = "Машины";
            this.машиныToolStripMenuItem.Click += new System.EventHandler(this.машиныToolStripMenuItem_Click);
            // 
            // деталиToolStripMenuItem
            // 
            this.деталиToolStripMenuItem.Name = "деталиToolStripMenuItem";
            this.деталиToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.деталиToolStripMenuItem.Text = "Детали";
            this.деталиToolStripMenuItem.Click += new System.EventHandler(this.деталиToolStripMenuItem_Click);
            // 
            // заявкиToolStripMenuItem
            // 
            this.заявкиToolStripMenuItem.Name = "заявкиToolStripMenuItem";
            this.заявкиToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.заявкиToolStripMenuItem.Text = "Заявки";
            this.заявкиToolStripMenuItem.Click += new System.EventHandler(this.заявкиToolStripMenuItem_Click);
            // 
            // отчетыToolStripMenuItem
            // 
            this.отчетыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.покупкиToolStripMenuItem,
            this.заявкиToolStripMenuItem1});
            this.отчетыToolStripMenuItem.Name = "отчетыToolStripMenuItem";
            this.отчетыToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.отчетыToolStripMenuItem.Text = "Отчеты";
            // 
            // покупкиToolStripMenuItem
            // 
            this.покупкиToolStripMenuItem.Name = "покупкиToolStripMenuItem";
            this.покупкиToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.покупкиToolStripMenuItem.Text = "Покупки";
            this.покупкиToolStripMenuItem.Click += new System.EventHandler(this.покупкиToolStripMenuItem_Click);
            // 
            // заявкиToolStripMenuItem1
            // 
            this.заявкиToolStripMenuItem1.Name = "заявкиToolStripMenuItem1";
            this.заявкиToolStripMenuItem1.Size = new System.Drawing.Size(122, 22);
            this.заявкиToolStripMenuItem1.Text = "Заявки";
            this.заявкиToolStripMenuItem1.Click += new System.EventHandler(this.заявкиToolStripMenuItem1_Click);
            // 
            // buttonTakeOrder
            // 
            this.buttonTakeOrder.Location = new System.Drawing.Point(826, 38);
            this.buttonTakeOrder.Name = "buttonTakeOrder";
            this.buttonTakeOrder.Size = new System.Drawing.Size(163, 35);
            this.buttonTakeOrder.TabIndex = 2;
            this.buttonTakeOrder.Text = "Отправить на исполнение";
            this.buttonTakeOrder.UseVisualStyleBackColor = true;
            this.buttonTakeOrder.Click += new System.EventHandler(this.buttonTakeOrder_Click);
            // 
            // buttonFinish
            // 
            this.buttonFinish.Location = new System.Drawing.Point(826, 91);
            this.buttonFinish.Name = "buttonFinish";
            this.buttonFinish.Size = new System.Drawing.Size(163, 35);
            this.buttonFinish.TabIndex = 3;
            this.buttonFinish.Text = "Окончен";
            this.buttonFinish.UseVisualStyleBackColor = true;
            this.buttonFinish.Click += new System.EventHandler(this.buttonFinish_Click);
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(826, 144);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(163, 35);
            this.buttonUpdate.TabIndex = 4;
            this.buttonUpdate.Text = "Обновить";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // buttonOrderView
            // 
            this.buttonOrderView.Location = new System.Drawing.Point(826, 512);
            this.buttonOrderView.Name = "buttonOrderView";
            this.buttonOrderView.Size = new System.Drawing.Size(163, 35);
            this.buttonOrderView.TabIndex = 5;
            this.buttonOrderView.Text = "Просмотреть заказ";
            this.buttonOrderView.UseVisualStyleBackColor = true;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1048, 559);
            this.Controls.Add(this.buttonOrderView);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.buttonFinish);
            this.Controls.Add(this.buttonTakeOrder);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "Автосалон Корыто";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem справочникToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem клиентыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem машиныToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem деталиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem заявкиToolStripMenuItem;
        private System.Windows.Forms.Button buttonTakeOrder;
        private System.Windows.Forms.ToolStripMenuItem отчетыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem покупкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem заявкиToolStripMenuItem1;
        private System.Windows.Forms.Button buttonFinish;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Button buttonOrderView;
    }
}