namespace Sea_Battle
{
    public partial class SeaBattleGame : Form
    {
        private int[,] playerMap;
        private int[,] botMap;
        private int cellSize;
        private string alphabet;

        public SeaBattleGame(int[,] map, int cellSize, string alphabet)
        {
            InitializeComponent();

            this.playerMap = map;
            this.botMap = StowageShips.enemyMap;
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
                    }
                    else
                    {
                        button.BackColor = (playerMap[i, j] == 1) ? Color.BlueViolet : SystemColors.ControlLight;
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
                    button.Location = new Point(350 + j * cellSize + cellSize, i * cellSize + cellSize);
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
                }
            }
        }

        public bool Shoot(int[,] map, Button pressedButton)
        {
            int rowIndex = (pressedButton.Location.Y - StowageShips.cellSize) / StowageShips.cellSize;
            int colIndex = (pressedButton.Location.X - StowageShips.cellSize) / StowageShips.cellSize;
            bool hit = false;
            if (map[pressedButton.Location.Y / StowageShips.cellSize, pressedButton.Location.X / StowageShips.cellSize] == 0)
            {
                hit = true;

                pressedButton.BackgroundImage = Image.FromFile("Resources/BreakShips.png"); // Изображение попадания
                pressedButton.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else 
            {
                hit = false;
                pressedButton.BackgroundImage = Image.FromFile("Resources/MissHit.png"); // Изображение промаха
                pressedButton.BackgroundImageLayout = ImageLayout.Stretch;
            }

            return hit;
        }

        private void SeaBattleGame_Load(object sender, EventArgs e)
        {

        }
    }
}
