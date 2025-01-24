using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sea_Battle
{

    public partial class StowageShips : Form
    {
        //Размер ячейки
        public static int cellSize = 30;

        //Массивы представляющие размерность поля Игрока и Бота. Прибавляем 1 к значениям, чтобы сделать поля с координатами
        public static int[,] myMap = new int[StartWindow.mapSizeHight+1, StartWindow.mapSizeWidth+1];
        public static int[,] enemyMap = new int[StartWindow.mapSizeHight+1, StartWindow.mapSizeWidth+1];

        //Алфавит для верхенего ряда
        public static string alphabet = "АБВГДЕЖЗИКИЙКЛМНО";

        public StowageShips()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            CreateMap();
        }

        public void CreateMap()
        {

            //Доп. переменная для размещения поля ИИ
            int loc = 350;

            // Количество кораблей по умолчанию при размере 10 x 10
            byte countShipOne = 4;
            byte countShipTwo = 3;
            byte countShipThree = 2;
            byte countShipFour = 1;

            //Подстраиваем размерность окна под размеры заданные пользователем
            //10
            if (StartWindow.mapSizeHight == 10)
            {
                this.Height = (StartWindow.mapSizeHight + 1) * cellSize + 150;
            }
            if (StartWindow.mapSizeWidth == 10)
            {
                this.Width = StartWindow.mapSizeWidth * 2 * cellSize + 155;
            }

            //11
            //Добавляется три одноклеточных корабля
            if (StartWindow.mapSizeHight == 11)
            {
                this.Height = (StartWindow.mapSizeHight + 1) * cellSize + 155;
            }
            //Добавляется три одноклеточных корабля
            if (StartWindow.mapSizeWidth == 11)
            {
                loc += 30;
                this.Width = StartWindow.mapSizeWidth * 2 * cellSize + 160;
            }

            //12
            //Добавляется два двухклеточных корабля
            if (StartWindow.mapSizeHight == 12)
            {
                this.Height = (StartWindow.mapSizeHight + 1) * cellSize + 160;
            }
            //Добавляется два двухклеточных корабля
            if (StartWindow.mapSizeWidth == 12)
            {
                loc += 60;
                this.Width = StartWindow.mapSizeWidth * 2 * cellSize + 165;
            }

            //13
            //Добавляется два трехклеточных корабля
            if (StartWindow.mapSizeHight == 13)
            {
                this.Height = (StartWindow.mapSizeHight + 1) * cellSize + 165;
            }
            //Добавляется два трехклеточных корабля
            if (StartWindow.mapSizeWidth == 13)
            {
                loc += 90;
                this.Width = StartWindow.mapSizeWidth * 2 * cellSize + 170;
            }

            //14
            //Добавляется два трехклеточных корабля и два одноклеточных корабля
            if (StartWindow.mapSizeHight == 14)
            {
                this.Height = (StartWindow.mapSizeHight + 1) * cellSize + 170;
            }
            //Добавляется два трехклеточных корабля и два одноклеточных корабля
            if (StartWindow.mapSizeWidth == 14)
            {
                loc += 120;
                this.Width = StartWindow.mapSizeWidth * 2 * cellSize + 175;
            }

            //15
            //Добавляется два четырехклеточных корабля и 2 двухклеточных корабля
            if (StartWindow.mapSizeHight == 15)
            {
                this.Height = (StartWindow.mapSizeHight + 1) * cellSize + 175;
            }
            //Добавляется два четырехклеточных корабля и 2 двухклеточных корабля
            if (StartWindow.mapSizeWidth == 15)
            {
                loc += 150;
                this.Width = StartWindow.mapSizeWidth * 2 * cellSize + 180;
            }


            // Конфигурация карты игрока
            for (int i = 0; i < StartWindow.mapSizeHight+1; i++)
            {
                for (int j = 0; j < StartWindow.mapSizeWidth+1; j++)
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
                            button.Text = alphabet[j-1].ToString();
                        }
                        if(j==0 && i>0)
                        {
                            button.Text = i.ToString();
                        }
                    }
                    this.Controls.Add(button);
                }
            }

            // Конфигурация карты бота
            for (int i = 0; i < StartWindow.mapSizeHight+1; i++)
            {
                for (int j = 0; j < StartWindow.mapSizeWidth+1; j++)
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
            map1.Location = new Point((StartWindow.mapSizeWidth * cellSize) / 2 - (map1.Width / 2) +70, 10);
            this.Controls.Add(map1);

            //Подпись карты противника 
            Label map2 = new Label();
            map2.Text = "Карта противника";
            map2.AutoSize = true;
            map2.Location = new Point((StartWindow.mapSizeWidth * cellSize) / 2 - (map1.Width / 2) + (400 + cellSize*(StartWindow.mapSizeWidth-10)), 10);
            this.Controls.Add(map2);

            //Кнопка чтобы завершить расстановку и начать игру. Тут нам нужно сделать проверку, чтобы не было ошибок и пользователь начал игру со всеми доступными кораблями
            Button startButton = new Button();
            startButton.Size = new Size(130,40);
            startButton.Text = "Начать";
            startButton.Click += new EventHandler(StartButton_Click);
            startButton.Location = new Point((this.ClientSize.Width-startButton.Width)/2,StartWindow.mapSizeHight * cellSize + 75 + 10);
            this.Controls.Add(startButton);


        }
        public void ConfigurationShips(object sender, EventArgs e)
        {
            Button pressedButton = sender as Button;

            // Индексы для местоположения кнопок
            int rowIndex = (pressedButton.Location.Y - cellSize) / cellSize;
            int colIndex = (pressedButton.Location.X - cellSize) / cellSize;

            if (myMap[rowIndex,colIndex] == 0)
            {
                pressedButton.BackColor = Color.BlueViolet;
                myMap[rowIndex, colIndex] = 1;
            }
            else
            {
                pressedButton.BackColor = SystemColors.ControlLight;
                myMap[rowIndex, colIndex] = 0;
            }

        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            SeaBattleGame seaBattleGameForm = new SeaBattleGame();
            seaBattleGameForm.Show();

        }

        private void StowageShips_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }
    }
}
