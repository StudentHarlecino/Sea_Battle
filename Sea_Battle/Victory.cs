using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sea_Battle
{
    public partial class Victory : Form
    {
        public Victory()
        {
            InitializeComponent();
        }

        private void playAgainButton_Click(object sender, EventArgs e)
        {
            // Скрыть текущую форму
            this.Hide();

            // Создать новое начальное окно
            StartWindow startForm = new StartWindow();

            // Сбросить данные игры
            startForm.ResetGame();

            // Показать начальное окно
            startForm.Show();
        }


        private void playAgainButton_MouseMove(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void playAgainButton_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void Victory_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        //Убираем возможность раскрывать окна двойным кликом по заголовку
        protected override void WndProc(ref Message m)
        {
            const int WM_NCLBUTTONDBLCLK = 0x00A3; // Сообщение о двойном нажатии на заголовок

            if (m.Msg == WM_NCLBUTTONDBLCLK)
            {
                // Игнорируем двойное нажатие на заголовок
                return;
            }

            base.WndProc(ref m); // Продолжаем стандартную обработку других сообщений
        }
    }
}
