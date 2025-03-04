namespace Sea_Battle
{
    partial class Victory
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
            panel1 = new Panel();
            playAgainButton = new Button();
            label1 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackgroundImage = Properties.Resources.VictoryImage;
            panel1.BackgroundImageLayout = ImageLayout.Stretch;
            panel1.Controls.Add(playAgainButton);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 450);
            panel1.TabIndex = 0;
            // 
            // playAgainButton
            // 
            playAgainButton.FlatAppearance.MouseDownBackColor = Color.PaleTurquoise;
            playAgainButton.FlatAppearance.MouseOverBackColor = Color.MediumTurquoise;
            playAgainButton.FlatStyle = FlatStyle.Flat;
            playAgainButton.Font = new Font("Arial Black", 15F);
            playAgainButton.Location = new Point(453, 189);
            playAgainButton.Name = "playAgainButton";
            playAgainButton.Size = new Size(187, 54);
            playAgainButton.TabIndex = 1;
            playAgainButton.Text = "Начать заново";
            playAgainButton.UseVisualStyleBackColor = true;
            playAgainButton.Click += playAgainButton_Click;
            playAgainButton.MouseLeave += playAgainButton_MouseLeave;
            playAgainButton.MouseMove += playAgainButton_MouseMove;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Sitka Heading", 60F, FontStyle.Bold);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(367, 57);
            label1.Name = "label1";
            label1.Size = new Size(364, 116);
            label1.TabIndex = 0;
            label1.Text = "ПОБЕДА";
            // 
            // Victory
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Victory";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Вы выиграли!";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private Button playAgainButton;
    }
}