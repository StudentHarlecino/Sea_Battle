namespace Sea_Battle
{

    public partial class StowageShips : Form
    {

        //Размер ячейки
        public static int cellSize = 30;

        //Доп. переменная для размещения поля ИИ
        public static int loc;

        //Массивы представляющие размерность поля Игрока и Бота. Прибавляем 1 к значениям, чтобы сделать поля с координатами
        private int[,] myMap;
        private int[,] enemyMap;

        //Алфавит для верхенего ряда
        public static string alphabet = "АБВГДЕЖЗИКИЙКЛМНО";

        //Хранение размера корабля
        private int shipSize = 1; // Чтобы до использование numeric у нас было минимальное значение, то есть однопалубный корабль
        private bool checkedHorizontal;
        public StowageShips()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            // Пересоздаем массивы с новыми размерами
            myMap = new int[StartWindow.mapSizeHeight + 1, StartWindow.mapSizeWidth + 1];
            enemyMap = new int[StartWindow.mapSizeHeight + 1, StartWindow.mapSizeWidth + 1];
            loc = 350;
            CreateMaps();
        }


        public void CreateMaps()
        {

            //Подстраиваем размерность окна под размеры заданные пользователем
            //10
            if (StartWindow.mapSizeHeight == 10)
            {
                this.Height = (StartWindow.mapSizeHeight + 1) * cellSize + 150;
            }
            if (StartWindow.mapSizeWidth == 10)
            {
                this.Width = StartWindow.mapSizeWidth * 2 * cellSize + 155;
            }

            //11
            //Добавляется три одноклеточных корабля
            if (StartWindow.mapSizeHeight == 11)
            {
                this.Height = (StartWindow.mapSizeHeight + 1) * cellSize + 155;
            }
            //Добавляется три одноклеточных корабля
            if (StartWindow.mapSizeWidth == 11)
            {
                loc += 30;
                this.Width = StartWindow.mapSizeWidth * 2 * cellSize + 160;
            }

            //12
            //Добавляется два двухклеточных корабля
            if (StartWindow.mapSizeHeight == 12)
            {
                this.Height = (StartWindow.mapSizeHeight + 1) * cellSize + 160;
            }
            //Добавляется два двухклеточных корабля
            if (StartWindow.mapSizeWidth == 12)
            {
                loc += 60;
                this.Width = StartWindow.mapSizeWidth * 2 * cellSize + 165;
            }

            //13
            //Добавляется два трехклеточных корабля
            if (StartWindow.mapSizeHeight == 13)
            {
                this.Height = (StartWindow.mapSizeHeight + 1) * cellSize + 165;
            }
            //Добавляется два трехклеточных корабля
            if (StartWindow.mapSizeWidth == 13)
            {
                loc += 90;
                this.Width = StartWindow.mapSizeWidth * 2 * cellSize + 170;
            }

            //14
            //Добавляется два трехклеточных корабля и два одноклеточных корабля
            if (StartWindow.mapSizeHeight == 14)
            {
                this.Height = (StartWindow.mapSizeHeight + 1) * cellSize + 170;
            }
            //Добавляется два трехклеточных корабля и два одноклеточных корабля
            if (StartWindow.mapSizeWidth == 14)
            {
                loc += 120;
                this.Width = StartWindow.mapSizeWidth * 2 * cellSize + 175;
            }

            //15
            //Добавляется два четырехклеточных корабля и 2 двухклеточных корабля
            if (StartWindow.mapSizeHeight == 15)
            {
                this.Height = (StartWindow.mapSizeHeight + 1) * cellSize + 175;
            }
            //Добавляется два четырехклеточных корабля и 2 двухклеточных корабля
            if (StartWindow.mapSizeWidth == 15)
            {
                loc += 150;
                this.Width = StartWindow.mapSizeWidth * 2 * cellSize + 180;
            }


            // Конфигурация карты игрока
            for (int i = 0; i < StartWindow.mapSizeHeight + 1; i++)
            {
                for (int j = 0; j < StartWindow.mapSizeWidth + 1; j++)
                {
                    myMap[i, j] = 0;

                    // Создаем кнопки с определенными координатами
                    Button button = new Button();
                    button.Location = new Point(j * cellSize + cellSize, i * cellSize + cellSize);
                    button.Size = new Size(cellSize, cellSize);
                    button.Click += new EventHandler(ConfigurationShips);

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
                    }
                    else
                    {
                        button.BackColor = SystemColors.ControlLight;
                    }
                    this.Controls.Add(button);
                }
            }

            // Конфигурация карты бота
            for (int i = 0; i < StartWindow.mapSizeHeight + 1; i++)
            {
                for (int j = 0; j < StartWindow.mapSizeWidth + 1; j++)
                {
                    myMap[i, j] = 0;
                    enemyMap[i, j] = 0;

                    Button button = new Button();
                    button.Location = new Point(loc + j * cellSize + cellSize, i * cellSize + cellSize);
                    button.Size = new Size(cellSize, cellSize);


                    //Создание кнопок для кординат(Не кликабельных, для макета)
                    if (j == 0 || i == 0)
                    {
                        button.BackColor = Color.Gray;



                        if (i == 0 && j > 0)
                        {
                            button.Text = alphabet[j - 1].ToString();
                        }
                        if (j == 0 && i > 0)
                        {
                            button.Text = i.ToString();
                        }
                    }
                    this.Controls.Add(button);

                    //Делаем кнопки на карте бота некликабельными, чтобы пользователь не мог разместить свои корабли на карте бота
                    button.Enabled = false;
                }
            }


            //Подпись карты игрока
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

            //Кнопка чтобы завершить расстановку и начать игру. Тут нам нужно сделать проверку, чтобы не было ошибок и пользователь начал игру со всеми доступными кораблями
            Button startButton = new Button();
            startButton.Size = new Size(130, 40);
            startButton.Text = "Начать";
            startButton.Click += new EventHandler(StartButton_Click);
            startButton.Location = new Point(this.ClientSize.Width - startButton.Width-100, StartWindow.mapSizeHeight * cellSize + 75 + 10);
            this.Controls.Add(startButton);

            Label labelShipSize = new Label();
            labelShipSize.Text = "Размер корабля:";
            labelShipSize.Location = new Point(cellSize, StartWindow.mapSizeHeight * cellSize + 65); ; // Под полем игрока
            labelShipSize.BackColor = Color.Transparent;
            this.Controls.Add(labelShipSize);

            // Создание NumericUpDown для выбора размера корабля
            NumericUpDown numericUpDownShipSize = new NumericUpDown();
            numericUpDownShipSize.Minimum = 1;
            numericUpDownShipSize.Maximum = 4;
            numericUpDownShipSize.Value = 1; // Установка начального значения
            numericUpDownShipSize.Location = new Point(cellSize, StartWindow.mapSizeHeight * cellSize + 88); // Ниже лейбла
            numericUpDownShipSize.ValueChanged += (s, e) => { shipSize = (int)numericUpDownShipSize.Value; }; // Обновляем значение переменной в которой хранится размер корабля
            this.Controls.Add(numericUpDownShipSize);

            // Создание чекбоксов чтобы пользователь мог выбирать какой сторон располагать корабль
            CheckBox horizontalCheckBox = new CheckBox();
            CheckBox verticalCheckBox = new CheckBox();

            horizontalCheckBox.Text = "Горизонтальное";
            horizontalCheckBox.Location = new Point(cellSize, StartWindow.mapSizeHeight * cellSize + 116);
            horizontalCheckBox.Checked = true;
            horizontalCheckBox.AutoSize = true;
            checkedHorizontal = true;
            horizontalCheckBox.CheckedChanged += (s, e) =>
            {
                if (horizontalCheckBox.Checked)
                {
                    verticalCheckBox.Checked = false;
                    checkedHorizontal = true;
                }
            };
            this.Controls.Add(horizontalCheckBox);


            verticalCheckBox.Text = "Вертикальное";
            verticalCheckBox.Location = new Point(cellSize + 125, StartWindow.mapSizeHeight * cellSize + 113);
            horizontalCheckBox.AutoSize = true;
            verticalCheckBox.CheckedChanged += (s, e) =>
            {
                if (verticalCheckBox.Checked)
                {
                    horizontalCheckBox.Checked = false;
                    checkedHorizontal = false;
                }
            };
            this.Controls.Add(verticalCheckBox);

            // кнопка очистки поля от кораблей
            Button clearButton = new Button();
            clearButton.Size = new Size(100, 27);
            clearButton.Text = "Очистить поле";
            clearButton.Click += new EventHandler(ClearButton_Click); // Обработчик события
            clearButton.Location = new Point((this.ClientSize.Width - clearButton.Width) / 3 + 50 , StartWindow.mapSizeHeight * cellSize + 70);
            this.Controls.Add(clearButton);
        }



        public void ConfigurationShips(object sender, EventArgs e)
        {
            Button pressedButton = sender as Button;

            // Индексы для местоположения кнопок
            int rowIndex = ((pressedButton.Location.Y) / cellSize) - 1;
            int colIndex = ((pressedButton.Location.X) / cellSize) - 1;

            // Получаем текущие ограничения на количество кораблей
            var shipCounts = GetShipCounts();

            // Проверяем, можно ли разместить корабль данного размера
            if (placedShips[shipSize] >= shipCounts[shipSize])
            {
                MessageBox.Show($"Вы уже разместили максимальное количество кораблей размером {shipSize}.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Проверяем, можно ли разместить корабль на карте
            if (CanPlaceShip(rowIndex, colIndex, shipSize, checkedHorizontal))
            {
                // Размещаем корабль
                PlaceShipOnMap(rowIndex, colIndex, shipSize, checkedHorizontal);
                pressedButton.BackColor = Color.BlueViolet;
                myMap[rowIndex, colIndex] = 1;

                // Увеличиваем счетчик размещенных кораблей
                placedShips[shipSize]++;
            }
            else
            {
                MessageBox.Show("Невозможно разместить корабль в этом месте.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //-------------------------------------------------------------

        private Dictionary<int, int> GetShipCounts()
        {
            var shipCounts = new Dictionary<int, int>
            {
                { 1, 4 }, // 4 однопалубных корабля
                { 2, 3 }, // 3 двухпалубных корабля
                { 3, 2 }, // 2 трехпалубных корабля
                { 4, 1 }  // 1 четырехпалубный корабль
            };

            // Дополнительные корабли для больших карт
            if (StartWindow.mapSizeHeight == 11)
            {
                shipCounts[1] += 3; // Добавляем 3 однопалубных корабля
            }

            if (StartWindow.mapSizeWidth == 11)
            {
                shipCounts[1] += 3; // Добавляем 3 однопалубных корабля
            }

            if (StartWindow.mapSizeHeight == 12)
            {
                shipCounts[2] += 2; // Добавляем 2 двухпалубных корабля
            }

            if (StartWindow.mapSizeWidth == 12)
            {
                shipCounts[2] += 2; // Добавляем 2 двухпалубных корабля
            }

            if (StartWindow.mapSizeHeight == 13)
            {
                shipCounts[3] += 2; // Добавляем 2 трехпалубных корабля
            }

            if (StartWindow.mapSizeWidth == 13)
            {
                shipCounts[3] += 2; // Добавляем 2 трехпалубных корабля
            }

            if (StartWindow.mapSizeHeight == 14)
            {
                shipCounts[3] += 2; // Добавляем 2 трехпалубных корабля
                shipCounts[4] += 1; // Добавляем 1 четырехпалубный корабль
            }

            if (StartWindow.mapSizeWidth == 14)
            {
                shipCounts[3] += 2; // Добавляем 2 трехпалубных корабля
                shipCounts[4] += 1; // Добавляем 1 четырехпалубный корабль
            }

            if (StartWindow.mapSizeHeight == 15)
            {
                shipCounts[2] += 2; // Добавляем 2 двухпалубных корабля
                shipCounts[4] += 2; // Добавляем 2 четырехпалубных корабля
            }

            if (StartWindow.mapSizeWidth == 15)
            {
                shipCounts[2] += 2; // Добавляем 2 двухпалубных корабля
                shipCounts[4] += 2; // Добавляем 2 четырехпалубных корабля
            }

            return shipCounts;
        }

        private Dictionary<int, int> placedShips = new Dictionary<int, int>
        {
            { 1, 0 }, // Однопалубные
            { 2, 0 }, // Двухпалубные
            { 3, 0 }, // Трехпалубные
            { 4, 0 }  // Четырехпалубные
        };

        private bool CanPlaceShip(int startRow, int startCol, int length, bool horizontal)
        {
            for (int i = 0; i < length; i++)
            {
                int checkRow = horizontal ? startRow : startRow + i;
                int checkCol = horizontal ? startCol + i : startCol;

                // Проверяем границы карты
                if (checkRow < 1 || checkRow >= myMap.GetLength(0) || checkCol < 1 || checkCol >= myMap.GetLength(1))
                {
                    return false; // Корабль выходит за границы карты
                }

                // Проверяем, занята ли клетка
                if (myMap[checkRow, checkCol] != 0)
                {
                    return false; // Клетка уже занята
                }

                // Проверяем соседние клетки
                for (int dy = -1; dy <= 1; dy++)
                {
                    for (int dx = -1; dx <= 1; dx++)
                    {
                        int neighborRow = checkRow + dy;
                        int neighborCol = checkCol + dx;

                        if (neighborRow >= 1 && neighborRow < myMap.GetLength(0) && neighborCol >= 1 && neighborCol < myMap.GetLength(1))
                        {
                            if (myMap[neighborRow, neighborCol] != 0)
                            {
                                return false; // Рядом с кораблем уже есть другой корабль
                            }
                        }
                    }
                }
            }
            return true; // Корабль можно разместить
        }

        private void PlaceShipOnMap(int startRow, int startCol, int length, bool horizontal)
        {
            for (int i = 0; i < length; i++)
            {
                int row = horizontal ? startRow : startRow + i;
                int col = horizontal ? startCol + i : startCol;

                myMap[row, col] = 1; // Указываем, что клетка занята кораблем

                // Обновляем цвет кнопки
                Button button = this.Controls.OfType<Button>().FirstOrDefault(b => b.Location == new Point((col + 1) * cellSize, (row + 1) * cellSize));
                if (button != null)
                {
                    button.BackColor = Color.BlueViolet;
                }
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            // Очищаем карту игрока
            for (int i = 1; i < myMap.GetLength(0); i++)
            {
                for (int j = 1; j < myMap.GetLength(1); j++)
                {
                    myMap[i, j] = 0; // Сбрасываем значение клетки на 0 (пустая клетка)
                }
            }

            // Обновляем визуальное отображение кнопок
            for (int i = 1; i < StartWindow.mapSizeHeight + 1; i++)
            {
                for (int j = 1; j < StartWindow.mapSizeWidth + 1; j++)
                {
                    Button button = this.Controls.OfType<Button>().FirstOrDefault(b => b.Location == new Point(j * cellSize + cellSize, i * cellSize + cellSize));
                    if (button != null)
                    {
                        button.BackColor = SystemColors.ControlLight; // Возвращаем стандартный цвет кнопки
                    }
                }
            }

            // Сбрасываем счетчик размещенных кораблей
            placedShips = new Dictionary<int, int>
            {
                { 1, 0 }, // Однопалубные
                { 2, 0 }, // Двухпалубные
                { 3, 0 }, // Трехпалубные
                { 4, 0 }  // Четырехпалубные
            };

            MessageBox.Show("Поле очищено от кораблей.", "Очистка поля", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            var shipCounts = GetShipCounts();

            // Проверяем, все ли корабли размещены
            foreach (var key in shipCounts.Keys)
            {
                if (placedShips[key] < shipCounts[key])
                {
                    MessageBox.Show($"Вы должны разместить все корабли. Осталось разместить {shipCounts[key] - placedShips[key]} кораблей размером {key}.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Если все корабли размещены, начинаем игру
            SeaBattleGame seaBattleGameForm = new SeaBattleGame(myMap, enemyMap, cellSize, alphabet);
            seaBattleGameForm.Width = this.Width;
            seaBattleGameForm.Height = this.Height;
            seaBattleGameForm.Show();
            this.Hide();
        }




        private void StowageShips_Load(object sender, EventArgs e)
        {

        }

        private void StowageShips_FormClosed(object sender, FormClosedEventArgs e)
        {
            StartWindow startWindow = new StartWindow();
            startWindow.Show();
        }
    }
}
