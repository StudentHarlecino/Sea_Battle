namespace Sea_Battle
{
    partial class Defeat
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
            label1 = new Label();
            playAgainButton = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackgroundImage = Properties.Resources.DefeatImage;
            panel1.Controls.Add(label1);
            panel1.Controls.Add(playAgainButton);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 450);
            panel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Edirne Normal", 60F);
            label1.ForeColor = Color.WhiteSmoke;
            label1.Location = new Point(427, 60);
            label1.Name = "label1";
            label1.Size = new Size(346, 96);
            label1.TabIndex = 1;
            label1.Text = "ПОРАЖЕНИЕ";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // playAgainButton
            // 
            playAgainButton.FlatAppearance.MouseDownBackColor = Color.Brown;
            playAgainButton.FlatAppearance.MouseOverBackColor = Color.Maroon;
            playAgainButton.FlatStyle = FlatStyle.Flat;
            playAgainButton.Font = new Font("Arial Black", 15F);
            playAgainButton.Location = new Point(507, 202);
            playAgainButton.Name = "playAgainButton";
            playAgainButton.Size = new Size(187, 54);
            playAgainButton.TabIndex = 0;
            playAgainButton.Text = "Начать заново";
            playAgainButton.UseVisualStyleBackColor = true;
            playAgainButton.Click += playAgainButton_Click;
            playAgainButton.MouseLeave += playAgainButton_MouseLeave;
            playAgainButton.MouseMove += playAgainButton_MouseMove;
            // 
            // Defeat
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Defeat";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Вы проиграли!";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button playAgainButton;
        private Label label1;
    }
}