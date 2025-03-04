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
            bot.ConfigureShips();
            botMap = bot.botMap;
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

            /* if (CheckIfMapIsNotEmpty())
                {
                    this.Controls.Clear();
                    InitializeComponent();
                }*/
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
                pressedButton.Enabled = false;

                // Проверяем, уничтожен ли корабль
                List<Tuple<int, int>> shipCells = FindShipCells(map, buttonIndexY, buttonIndexX, originalValue);
                bool isShipDestroyed = shipCells.All(cell => map[cell.Item1, cell.Item2] == -2);

                if (isShipDestroyed)
                {
                    foreach (var cell in shipCells)
                    {
                        // Передаем флаг isBotMap = true, если это карта бота, иначе false
                        BlockAdjacentCells(map, cell.Item1, cell.Item2, true);
                    }
                }
            }
            else if (map[buttonIndexY, buttonIndexX] == 0)
            {
                map[buttonIndexY, buttonIndexX] = -1; // Помечаем промах
                pressedButton.BackgroundImage = Image.FromFile("Resources/MissHit.png");
                pressedButton.BackgroundImageLayout = ImageLayout.Stretch;
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

        static public void BlockAdjacentCells(int[,] map, int y, int x, bool isBotMap)
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
                            Button button = isBotMap ? botButtons[i, j] : playerButtons[i, j];
                            if (button != null && button.Enabled)
                            {
                                button.BackgroundImage = Image.FromFile("Resources/MissHit.png");
                                button.BackgroundImageLayout = ImageLayout.Stretch;
                                button.Enabled = false;
                            }
                        }
                    }
                }
            }
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
