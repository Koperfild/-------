namespace Лаба1
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.citiesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.citiesBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.fromLabel = new System.Windows.Forms.Label();
            this.toLabel = new System.Windows.Forms.Label();
            this.PriceDGW = new System.Windows.Forms.DataGridView();
            this.AirTransport = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroundTransport = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WaterTransport = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.executeButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.citiesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.citiesBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PriceDGW)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.DataSource = this.citiesBindingSource;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(49, 59);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(92, 21);
            this.comboBox1.TabIndex = 0;
            // 
            // citiesBindingSource
            // 
            this.citiesBindingSource.DataSource = typeof(Лаба1.Cities);
            // 
            // comboBox2
            // 
            this.comboBox2.DataSource = this.citiesBindingSource1;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(164, 59);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(92, 21);
            this.comboBox2.TabIndex = 1;
            // 
            // citiesBindingSource1
            // 
            this.citiesBindingSource1.DataSource = typeof(Лаба1.Cities);
            // 
            // fromLabel
            // 
            this.fromLabel.AutoSize = true;
            this.fromLabel.Location = new System.Drawing.Point(82, 24);
            this.fromLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.fromLabel.Name = "fromLabel";
            this.fromLabel.Size = new System.Drawing.Size(27, 13);
            this.fromLabel.TabIndex = 2;
            this.fromLabel.Text = "from";
            // 
            // toLabel
            // 
            this.toLabel.AutoSize = true;
            this.toLabel.Location = new System.Drawing.Point(162, 24);
            this.toLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.toLabel.Name = "toLabel";
            this.toLabel.Size = new System.Drawing.Size(16, 13);
            this.toLabel.TabIndex = 3;
            this.toLabel.Text = "to";
            // 
            // PriceDGW
            // 
            this.PriceDGW.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PriceDGW.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AirTransport,
            this.GroundTransport,
            this.WaterTransport});
            this.PriceDGW.Location = new System.Drawing.Point(49, 132);
            this.PriceDGW.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.PriceDGW.Name = "PriceDGW";
            this.PriceDGW.RowTemplate.Height = 24;
            this.PriceDGW.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.PriceDGW.Size = new System.Drawing.Size(440, 97);
            this.PriceDGW.TabIndex = 4;
            // 
            // AirTransport
            // 
            this.AirTransport.HeaderText = "AirTransport";
            this.AirTransport.Name = "AirTransport";
            this.AirTransport.ReadOnly = true;
            this.AirTransport.Width = 133;
            // 
            // GroundTransport
            // 
            this.GroundTransport.HeaderText = "GroundTransport";
            this.GroundTransport.Name = "GroundTransport";
            this.GroundTransport.ReadOnly = true;
            this.GroundTransport.Width = 133;
            // 
            // WaterTransport
            // 
            this.WaterTransport.HeaderText = "WaterTransport";
            this.WaterTransport.Name = "WaterTransport";
            this.WaterTransport.ReadOnly = true;
            this.WaterTransport.Width = 133;
            // 
            // executeButton
            // 
            this.executeButton.Location = new System.Drawing.Point(121, 95);
            this.executeButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.executeButton.Name = "executeButton";
            this.executeButton.Size = new System.Drawing.Size(64, 19);
            this.executeButton.TabIndex = 5;
            this.executeButton.Text = "Calculate";
            this.executeButton.UseVisualStyleBackColor = true;
            this.executeButton.Click += new System.EventHandler(this.executeButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 316);
            this.Controls.Add(this.executeButton);
            this.Controls.Add(this.PriceDGW);
            this.Controls.Add(this.toLabel);
            this.Controls.Add(this.fromLabel);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Distance Counter";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.citiesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.citiesBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PriceDGW)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.BindingSource citiesBindingSource;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label fromLabel;
        private System.Windows.Forms.Label toLabel;
        private System.Windows.Forms.BindingSource citiesBindingSource1;
        private System.Windows.Forms.DataGridView PriceDGW;
        private System.Windows.Forms.Button executeButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn AirTransport;
        private System.Windows.Forms.DataGridViewTextBoxColumn GroundTransport;
        private System.Windows.Forms.DataGridViewTextBoxColumn WaterTransport;

    }
}

