namespace Sea_Battle
{
    public class Bot
    {
        public int[,] botMap = new int[StartWindow.mapSizeHeight + 1, StartWindow.mapSizeWidth + 1];
        public int[,] playerMap = new int[StartWindow.mapSizeHeight + 1, StartWindow.mapSizeWidth + 1];

        public Button[,] botButtons = new Button[StartWindow.mapSizeHeight + 1, StartWindow.mapSizeWidth + 1];
        public Button[,] playerButtons = new Button[StartWindow.mapSizeHeight + 1, StartWindow.mapSizeWidth + 1];

        Random random = new Random();
        //Конструктор  
        public Bot(int[,] botMap, int[,] playerMap, Button[,] botButtons, Button[,] playerButtons)
        {
            this.botMap = botMap;
            this.playerMap = playerMap;
            this.botButtons = botButtons;
            this.playerButtons = playerButtons;
        }
        public void ConfigureShips()
        {
            PlaceShip(1, 4); // 1 корабль на 4 клетки
            PlaceShip(2, 3); // 2 корабля по 3 клетки
            PlaceShip(3, 2); // 3 корабля по 2 клетки
            PlaceShip(4, 1); // 4 корабля по 1 клетке
            if (StartWindow.mapSizeHeight == 10 && StartWindow.mapSizeWidth == 10)
            {
                PlaceShip(3, 1);
            }

            //11
            //Добавляется три одноклеточных корабля
            if (StartWindow.mapSizeHeight == 11)
            {
                PlaceShip(3, 1);
            }
            //Добавляется три одноклеточных корабля
            if (StartWindow.mapSizeWidth == 11)
            {
                PlaceShip(3, 1);
            }

            //12
            //Добавляется два двухклеточных корабля
            if (StartWindow.mapSizeHeight == 12)
            {
                PlaceShip(2, 2);
            }
            //Добавляется два двухклеточных корабля
            if (StartWindow.mapSizeWidth == 12)
            {
                PlaceShip(2, 2);
            }

            //13
            //Добавляется два трехклеточных корабля
            if (StartWindow.mapSizeHeight == 13)
            {
                PlaceShip(2, 3);
            }
            //Добавляется два трехклеточных корабля
            if (StartWindow.mapSizeWidth == 13)
            {
                PlaceShip(2, 3);
            }

            //14
            //Добавляется два трехклеточных корабля и два одноклеточных корабля
            if (StartWindow.mapSizeHeight == 14)
            {
                PlaceShip(2, 1);
                PlaceShip(1, 4);
            }
            //Добавляется два трехклеточных корабля и два одноклеточных корабля
            if (StartWindow.mapSizeWidth == 14)
            {
                PlaceShip(2, 1);
                PlaceShip(1, 4);
            }

            //15
            //Добавляется два четырехклеточных корабля и 2 двухклеточных корабля
            if (StartWindow.mapSizeHeight == 15)
            {
                PlaceShip(2, 2);
                PlaceShip(2, 4);
            }
            //Добавляется два четырехклеточных корабля и 2 двухклеточных корабля
            if (StartWindow.mapSizeWidth == 15)
            {
                PlaceShip(2, 2);
                PlaceShip(2, 4);
            }
        }

        private void PlaceShip(int count, int length)
        {
            for (int i = 0; i < count; i++)
            {
                bool placed = false;

                while (!placed)
                {
                    // Случайно выбираем, горизонтально или вертикально
                    bool horizontal = random.Next(2) == 0;
                    int x, y;

                    if (horizontal)
                    {
                        // Генерируем случайные координаты для горизонтального размещения
                        y = random.Next(1, StartWindow.mapSizeHeight + 1 + 1);
                        x = random.Next(1, StartWindow.mapSizeWidth + 1 - length + 1); // Обеспечим пространство для длины корабля
                    }
                    else
                    {
                        // Генерируем случайные координаты для вертикального размещения
                        x = random.Next(1, StartWindow.mapSizeWidth + 1 + 1);
                        y = random.Next(1, StartWindow.mapSizeHeight + 1 - length + 1); // Обеспечим пространство для длины корабля
                    }

                    // Проверяем, можно ли разместить корабль
                    if (CanPlaceShip(x, y, length, horizontal))
                    {
                        // Если возможно, размещаем корабль
                        PlaceShipOnMap(x, y, length, horizontal);
                        placed = true; // Устанавливаем флаг о размещении корабля
                    }
                }
            }
        }

        private bool CanPlaceShip(int x, int y, int length, bool horizontal)
        {
            for (int i = 0; i < length; i++)
            {
                // Проверяем ячейку
                int checkX = horizontal ? x + i : x;
                int checkY = horizontal ? y : y + i;

                // Проверяем границы карты
                if (!IsInsideMap(checkX, checkY) || botMap[checkY, checkX] != 0) // Проверяем границы и занятость
                {
                    return false; // Нельзя разместить корабль
                }

                // Проверяем соседние клетки
                for (int dy = -1; dy <= 1; dy++)
                {
                    for (int dx = -1; dx <= 1; dx++)
                    {
                        if ((dy == 0 && dx == 0) || // Пропуск саму клетку
                            !IsInsideMap(checkX + dx, checkY + dy)) // Проверка границ
                        {
                            continue;
                        }

                        if (botMap[checkY + dy, checkX + dx] != 0) // Проверяем, занята ли соседняя клетка
                        {
                            return false;
                        }
                    }
                }
            }
            return true; // Можно разместить корабль
        }

        private bool IsInsideMap(int x, int y)
        {
            return x >= 1 && x < botMap.GetLength(1) && y >= 1 && y < botMap.GetLength(0);
        }

        private void PlaceShipOnMap(int x, int y, int length, bool horizontal)
        {
            for (int i = 0; i < length; i++)
            {
                int shipX = horizontal ? x + i : x;
                int shipY = horizontal ? y : y + i;
                botMap[shipY, shipX] = 1; // Указываем, что клетка занята
            }
        }

        //Вспомогательная фунция для конфигурации карты бота
        public bool IsEmpty(int i, int j, int length)
        {
            bool isEmpty = true;

            for (int k = j; k < j + length; k++)
            {
                if (botMap[i, j] != 0)
                {
                    isEmpty = false;
                    break;
                }
            }

            return isEmpty;
        }

        //Проверка наличия оставшихся кораблей


        //Стрельба бота
        public bool BotShoot()
        {
            int buttonIndexX;
            int buttonIndexY;
            Random random = new Random();
            // Генерируем случайные координаты для стрельбы
            do
            {
                buttonIndexX = random.Next(1, StartWindow.mapSizeWidth + 1); // Генерация X-координаты
                buttonIndexY = random.Next(1, StartWindow.mapSizeHeight + 1); // Генерация Y-координаты
            } while (playerMap[buttonIndexY, buttonIndexX] == -1 || playerMap[buttonIndexY, buttonIndexX] == -2); // Проверяем, не стреляли ли мы уже в эту клетку

            bool hit = false;

            if (playerMap[buttonIndexY, buttonIndexX] == 1)
            {
                hit = true;
                playerMap[buttonIndexY, buttonIndexX] = -2; // Подбитая клетка

                // Проверяем, уничтожен ли корабль игрока
                var shipCells = FindShipCells(playerMap, buttonIndexY, buttonIndexX, 1);
                bool isShipDestroyed = shipCells.All(cell => playerMap[cell.Item1, cell.Item2] == -2);

                if (isShipDestroyed)
                {
                    foreach (var cell in shipCells)
                    {
                        // Блокируем клетки вокруг корабля игрока
                        BlockAdjacentCellsForPlayer(playerMap, cell.Item1, cell.Item2);
                    }
                }

                // Обновляем кнопку на поле игрока
                playerButtons[buttonIndexY, buttonIndexX].BackgroundImage = Image.FromFile("Resources/BreakShip.png");
                playerButtons[buttonIndexY, buttonIndexX].BackgroundImageLayout = ImageLayout.Stretch;
                playerButtons[buttonIndexY, buttonIndexX].BackColor = Color.Black;
                playerButtons[buttonIndexY, buttonIndexX].Enabled = false;
            }
            else
            {
                hit = false;
                playerMap[buttonIndexY, buttonIndexX] = -1; // Помечаем как промах

                // Обновляем кнопку на поле игрока
                playerButtons[buttonIndexY, buttonIndexX].BackgroundImage = Image.FromFile("Resources/MissHit.png");
                playerButtons[buttonIndexY, buttonIndexX].BackgroundImageLayout = ImageLayout.Stretch;
                playerButtons[buttonIndexY, buttonIndexX].BackColor = Color.Gray;
                playerButtons[buttonIndexY, buttonIndexX].Enabled = false;
            }

            if (hit)
            {
                BotShoot(); // Бот стреляет снова, если попал
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

        private void BlockAdjacentCellsForPlayer(int[,] map, int y, int x)
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

                            // Обновляем кнопку на поле игрока
                            Button button = playerButtons[i, j];
                            if (button != null)
                            {
                                button.BackgroundImage = Image.FromFile("Resources/MissHit.png");
                                button.BackgroundImageLayout = ImageLayout.Stretch;
                                button.BackColor = Color.Gray;
                                button.Enabled = false;
                            }
                        }
                    }
                }
            }
        }
    }
}
