namespace Sea_Battle
{
    public partial class SeaBattleGame : Form
    {
        private int[,] playerMap;
        private int[,] botMap;
        private int cellSize;
        private string alphabet;

        public static Button[,] playerButtons = new Button[StartWindow.mapSizeHeight + 1, StartWindow.mapSizeWidth + 1];
        public static Button[,] botButtons = new Button[StartWindow.mapSizeHeight + 1, StartWindow.mapSizeWidth + 1];

        public Bot bot;

        public SeaBattleGame(int[,] map, int cellSize, string alphabet)
        {
            InitializeComponent();

            playerMap = map;
            botMap = StowageShips.enemyMap;
            this.cellSize = cellSize;
            this.alphabet = alphabet;

            

            GenerateMap();
        }

        private void GenerateMap()
        {
            // Конфигурация карты игрока
            for (int i = 0; i < StartWindow.mapSizeHeight + 1; i++)
            {
                for (int j = 0; j < StartWindow.mapSizeWidth + 1; j++)
                {
                    // Создаем кнопки с определенными координатами
                    Button button = new Button();
                    button.Location = new Point(j * cellSize + cellSize, i * cellSize + cellSize);
                    button.Size = new Size(cellSize, cellSize);
                    

                    //Создание кнопок для кординат
                    if (j == 0 || i == 0)
                    {
                        button.BackColor = Color.Gray;

                        //Делаем кнопки для обозначения координат некликабельными
                        button.Enabled = false;

                        if (i == 0 && j > 0)
                        {
                            button.Text = alphabet[j - 1].ToString();
                        }
                        if (j == 0 && i > 0)
                        {
                            button.Text = i.ToString();
                        }
                        playerMap[i, j] = -1;
                    }
                    else
                    {
                        button.BackColor = (playerMap[i, j] == 1) ? Color.BlueViolet : SystemColors.ControlLight;
                        playerButtons[i, j] = button;
                    }
                    this.Controls.Add(button);
                    button.Enabled = false;
                }
            }

            // Конфигурация карты бота
            for (int i = 0; i < StartWindow.mapSizeHeight + 1; i++)
            {
                for (int j = 0; j < StartWindow.mapSizeWidth + 1; j++)
                {

                    Button button = new Button();
                    button.Location = new Point(StowageShips.loc + j * cellSize + cellSize, i * cellSize + cellSize);
                    button.Size = new Size(cellSize, cellSize);

                    //Создание кнопок для кординат(Не кликабельных, для макета)
                    if (j == 0 || i == 0)
                    {
                        button.BackColor = Color.Gray;

                        //Делаем кнопки для обозначения координат некликабельными
                        button.Enabled = false;

                        if (i == 0 && j > 0)
                        {
                            button.Text = alphabet[j - 1].ToString();
                        }
                        if (j == 0 && i > 0)
                        {
                            button.Text = i.ToString();
                        }
                    }
                    else
                    {
                        button.Click += new EventHandler(GameStatus);
                        botButtons[i, j] = button;
                    }

                    this.Controls.Add(button);
                }
            }
        }

        public void GameStatus(object sender, EventArgs e)
        {
            bot = new Bot(botMap, playerMap, botButtons, playerButtons);
            Button pressedButton = sender as Button;
            bool playerTurn = Shoot(botMap,pressedButton);
            if (!playerTurn)
            {
                bot.BotShoot();
            }
            
            /* if (CheckIfMapIsNotEmpty())
                {
                    this.Controls.Clear();
                    InitializeComponent();
                }*/
        }

        public bool Shoot(int[,] map, Button pressedButton)
        {
            // Получаем координаты нажатой кнопки
            int buttonIndexX = (pressedButton.Location.X - (StowageShips.loc + StowageShips.cellSize)) / StowageShips.cellSize; // X-координата
            int buttonIndexY = (pressedButton.Location.Y - cellSize) / cellSize; // Y-координата

            bool hit = false;

            if (map[buttonIndexY, buttonIndexX] != 0)
            {
                hit = true;

                // Теперь при попадании наличие корабля в клетке убирается
                map[buttonIndexY, buttonIndexX] = 0;

                pressedButton.BackgroundImage = Image.FromFile("Resources/BreakShip.png"); // Изображение попадания
                pressedButton.BackgroundImageLayout = ImageLayout.Stretch;
                pressedButton.Enabled = false;
            }
            else
            {
                hit = false;
                pressedButton.BackgroundImage = Image.FromFile("Resources/MissHit.png"); // Изображение промаха
                pressedButton.BackgroundImageLayout = ImageLayout.Stretch;
                pressedButton.Enabled = false;
            }
            return hit;
        }

        //Проверка наличия оставшихся кораблей
        public bool CheckIfMapIsNotEmpty()
        {
            bool isEmpty1 = true;
            bool isEmpty2 = true;
            for (int i = 0; i < StartWindow.mapSizeWidth + 1; i++)
            {
                for (int j = 0; j < StartWindow.mapSizeHeight + 1; j++)
                {
                    if (playerMap[i, j] != 0)
                    {
                        isEmpty1 = false;
                    }
                    if (botMap[i, j] != 0)
                    {
                        isEmpty2 = false;
                    }
                }
            }
            if (isEmpty1 || isEmpty2)
            {
                return false;
            }
            else return true;
        }
    }
}
