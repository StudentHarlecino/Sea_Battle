namespace Sea_Battle
{
    public partial class SeaBattleGame : Form
    {
        private int[,] playerMap;
        private int[,] botMap;
        private int cellSize;
        private string alphabet;

        public static Button[,] playerButtons;
        public static Button[,] botButtons;

        public Bot bot;

        public SeaBattleGame(int[,] playerMap, int[,] botMap, int cellSize, string alphabet)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            this.playerMap = playerMap;
            this.botMap = botMap;
            this.cellSize = cellSize;
            this.alphabet = alphabet;
            playerButtons = new Button[StartWindow.mapSizeHeight + 1, StartWindow.mapSizeWidth + 1];
            botButtons = new Button[StartWindow.mapSizeHeight + 1, StartWindow.mapSizeWidth + 1];
            GenerateMap();
        }

        private void GenerateMap()
        {
            bot = new Bot(botMap, playerMap, botButtons, playerButtons);
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
                        button.BackColor = Color.DimGray;

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
                        button.BackColor = (playerMap[i, j] == 1) ? Color.BlueViolet : SystemColors.ScrollBar;
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
                        button.BackColor = Color.DimGray;

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
            bot.ConfigureShips();
            botMap = bot.botMap;

            Label map1 = new Label();
            map1.Text = "Ваша карта";
            map1.Location = new Point((StartWindow.mapSizeWidth * cellSize) / 2 - (map1.Width / 2) + 70, 10);
            this.Controls.Add(map1);

            //Подпись карты противника 
            Label map2 = new Label();
            map2.Text = "Карта противника";
            map2.AutoSize = true;
            map2.Location = new Point((StartWindow.mapSizeWidth * cellSize) / 2 - (map1.Width / 2) + (400 + cellSize * (StartWindow.mapSizeWidth - 10)), 10);
            this.Controls.Add(map2);
        }

        public void GameStatus(object sender, EventArgs e)
        {
            bot = new Bot(botMap, playerMap, botButtons, playerButtons);
            Button pressedButton = sender as Button;
            bool playerTurn = Shoot(botMap, pressedButton);
            if (!playerTurn)
            {
                bot.BotShoot();
            }

            // Проверка на победу или поражение
            if (CheckIfAllShipsDestroyed(botMap))
            {
                // Переход на форму победы
                Victory winForm = new Victory();
                winForm.Show();
                this.Hide();
            }
            else if (CheckIfAllShipsDestroyed(playerMap))
            {
                // Переход на форму поражения
                Defeat loseForm = new Defeat();
                loseForm.Show();
                this.Hide();
            }
        }

        public bool Shoot(int[,] map, Button pressedButton)
        {
            int buttonIndexX = (pressedButton.Location.X - (StowageShips.loc + StowageShips.cellSize)) / StowageShips.cellSize;
            int buttonIndexY = (pressedButton.Location.Y - cellSize) / cellSize;

            bool hit = false;

            if (map[buttonIndexY, buttonIndexX] > 0) // Проверяем, является ли клетка частью корабля
            {
                hit = true;
                int originalValue = map[buttonIndexY, buttonIndexX];

                // Помечаем клетку как подбитую (-2)
                map[buttonIndexY, buttonIndexX] = -2;
                pressedButton.BackgroundImage = Image.FromFile("Resources/BreakShip.png");
                pressedButton.BackgroundImageLayout = ImageLayout.Stretch;
                pressedButton.BackColor = Color.Black;
                pressedButton.Enabled = false;

                // Проверяем, уничтожен ли корабль
                List<Tuple<int, int>> shipCells = FindShipCells(map, buttonIndexY, buttonIndexX, originalValue);
                bool isShipDestroyed = shipCells.All(cell => map[cell.Item1, cell.Item2] == -2);

                if (isShipDestroyed)
                {
                    foreach (var cell in shipCells)
                    {
                        BlockAdjacentCells(map, cell.Item1, cell.Item2);
                    }
                }
            }
            else if (map[buttonIndexY, buttonIndexX] == 0)
            {
                map[buttonIndexY, buttonIndexX] = -1; // Помечаем промах
                pressedButton.BackgroundImage = Image.FromFile("Resources/MissHit.png");
                pressedButton.BackgroundImageLayout = ImageLayout.Stretch;
                pressedButton.BackColor = SystemColors.ControlDark;
                pressedButton.Enabled = false;
            }

            return hit;
        }

        private List<Tuple<int, int>> FindShipCells(int[,] map, int startY, int startX, int originalValue)
        {
            var shipCells = new List<Tuple<int, int>>();
            var queue = new Queue<Tuple<int, int>>();
            var visited = new HashSet<Tuple<int, int>>();

            queue.Enqueue(Tuple.Create(startY, startX));

            while (queue.Count > 0)
            {
                var cell = queue.Dequeue();
                int y = cell.Item1;
                int x = cell.Item2;

                if (y < 1 || y >= map.GetLength(0) || x < 1 || x >= map.GetLength(1)) continue;
                if (visited.Contains(cell)) continue;
                if (map[y, x] != originalValue && map[y, x] != -2) continue; // Учитываем подбитые клетки

                visited.Add(cell);
                shipCells.Add(cell);

                // Поиск соседних клеток корабля
                queue.Enqueue(Tuple.Create(y - 1, x));
                queue.Enqueue(Tuple.Create(y + 1, x));
                queue.Enqueue(Tuple.Create(y, x - 1));
                queue.Enqueue(Tuple.Create(y, x + 1));
            }

            return shipCells;
        }

        static public void BlockAdjacentCells(int[,] map, int y, int x)
        {
            for (int i = y - 1; i <= y + 1; i++)
            {
                for (int j = x - 1; j <= x + 1; j++)
                {
                    // Проверяем границы карты
                    if (i >= 1 && i < map.GetLength(0) && j >= 1 && j < map.GetLength(1))
                    {
                        // Блокируем только пустые клетки (0)
                        if (map[i, j] == 0)
                        {
                            map[i, j] = -1; // Помечаем как промах
                            Button button = botButtons[i, j];
                            if (button != null && button.Enabled)
                            {
                                button.BackgroundImage = Image.FromFile("Resources/MissHit.png");
                                button.BackgroundImageLayout = ImageLayout.Stretch;
                                button.BackColor = SystemColors.ControlDark;
                                button.Enabled = false;
                            }
                        }
                    }
                }
            }
        }



        //Проверка на подбитие всех кораблей
        private bool CheckIfAllShipsDestroyed(int[,] map)
        {
            for (int i = 1; i < map.GetLength(0); i++)
            {
                for (int j = 1; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == 1) // Если на карте остались неподбитые корабли
                    {
                        return false;
                    }
                }
            }
            return true; // Все корабли уничтожены
        }

        private void SeaBattleGame_FormClosed(object sender, FormClosedEventArgs e)
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
