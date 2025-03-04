namespace Sea_Battle
{
    partial class StartWindow
    {
        private System.ComponentModel.IContainer components = null;

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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            startButton = new Button();
            panel1 = new Panel();
            panel2 = new Panel();
            sizeWidthNumeric = new NumericUpDown();
            sizeHeightNumeric = new NumericUpDown();
            label5 = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)sizeWidthNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)sizeHeightNumeric).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Sitka Heading", 60F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(218, 9);
            label1.Name = "label1";
            label1.Size = new Size(528, 116);
            label1.TabIndex = 2;
            label1.Text = "Морской бой";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Sitka Heading", 24F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(-1, 0);
            label2.Name = "label2";
            label2.Size = new Size(339, 47);
            label2.TabIndex = 3;
            label2.Text = "Выберете размер поля";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Arial Black", 9F);
            label3.Location = new Point(132, 110);
            label3.Name = "label3";
            label3.Size = new Size(59, 17);
            label3.TabIndex = 4;
            label3.Text = "Высота";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Arial Black", 9F);
            label4.Location = new Point(132, 175);
            label4.Name = "label4";
            label4.Size = new Size(62, 17);
            label4.TabIndex = 5;
            label4.Text = "Ширина";
            // 
            // startButton
            // 
            startButton.BackColor = Color.AliceBlue;
            startButton.FlatAppearance.MouseDownBackColor = Color.PowderBlue;
            startButton.FlatAppearance.MouseOverBackColor = Color.SteelBlue;
            startButton.FlatStyle = FlatStyle.Flat;
            startButton.Font = new Font("Arial Black", 15F);
            startButton.Location = new Point(95, 256);
            startButton.Name = "startButton";
            startButton.Size = new Size(140, 47);
            startButton.TabIndex = 6;
            startButton.Text = "Начать";
            startButton.UseVisualStyleBackColor = false;
            startButton.Click += StartButton_Click;
            startButton.MouseLeave += startButton_MouseLeave;
            startButton.MouseMove += startButton_MouseMove;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Menu;
            panel1.BackgroundImage = Properties.Resources.StartImage;
            panel1.BackgroundImageLayout = ImageLayout.Stretch;
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(969, 553);
            panel1.TabIndex = 7;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.HighlightText;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(sizeWidthNumeric);
            panel2.Controls.Add(sizeHeightNumeric);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(startButton);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label2);
            panel2.Location = new Point(307, 153);
            panel2.Name = "panel2";
            panel2.Size = new Size(336, 339);
            panel2.TabIndex = 7;
            // 
            // sizeWidthNumeric
            // 
            sizeWidthNumeric.Location = new Point(104, 195);
            sizeWidthNumeric.Name = "sizeWidthNumeric";
            sizeWidthNumeric.Size = new Size(120, 23);
            sizeWidthNumeric.TabIndex = 9;
            sizeWidthNumeric.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // sizeHeightNumeric
            // 
            sizeHeightNumeric.Location = new Point(104, 130);
            sizeHeightNumeric.Name = "sizeHeightNumeric";
            sizeHeightNumeric.Size = new Size(120, 23);
            sizeHeightNumeric.TabIndex = 8;
            sizeHeightNumeric.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Book Antiqua", 9F);
            label5.Location = new Point(16, 48);
            label5.Name = "label5";
            label5.Size = new Size(301, 32);
            label5.TabIndex = 7;
            label5.Text = "Минимальные значения для каждой величины: 10\r\nМаксимальные значения для каждой величины: 15";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // StartWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(969, 553);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "StartWindow";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Морской бой";
            FormClosed += StartWindow_FormClosed;
            Load += StartWindow_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)sizeWidthNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)sizeHeightNumeric).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Button startButton;
        private Panel panel1;
        private Panel panel2;
        private Label label5;
        private NumericUpDown sizeHeightNumeric;
        private NumericUpDown sizeWidthNumeric;
    }
}
