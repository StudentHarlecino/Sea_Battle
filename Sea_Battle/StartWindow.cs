namespace Sea_Battle
{
    public partial class StartWindow : Form
    {
        //Создаем переменные для размерности поля
        public static int mapSizeHeight;
        public static int mapSizeWidth;

        public StartWindow()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            //Устанавливаем размерность поля, которую написал пользователь
            int sizeH = (int)(sizeHeightNumeric.Value);
            int sizeW = (int)(sizeWidthNumeric.Value);

            //Проверяем значения пользователя
            if (sizeH > 15 || sizeH < 10)
            {
                MessageBox.Show("Вы ввели недопустимую размерность высоты");
            }
            else
            {
                mapSizeHeight = sizeH;
            }
            if (sizeW > 15 || sizeW < 10)
            {
                MessageBox.Show("Вы ввели недопустимую размерность ширины");
            }
            else
            {
                mapSizeWidth = sizeW;
            }
            if ((sizeW <= 15 && sizeW >= 10) && (sizeH <= 15 && sizeH >= 10))
            {
                StowageShips stowageForm = new StowageShips();
                stowageForm.ShowDialog();
                this.Hide();
            }


        }

        public void ResetGame()
        {
            // Сбросить размеры карты
            mapSizeHeight = 0;
            mapSizeWidth = 0;

            // Очистить статические кнопки
            SeaBattleGame.playerButtons = new Button[StartWindow.mapSizeHeight + 1, StartWindow.mapSizeWidth + 1];
            SeaBattleGame.botButtons = new Button[StartWindow.mapSizeHeight + 1, StartWindow.mapSizeWidth + 1];
        }

        private void StartWindow_Load(object sender, EventArgs e)
        {

        }

        private void startButton_MouseMove(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void startButton_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void StartWindow_FormClosed(object sender, FormClosedEventArgs e)
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
