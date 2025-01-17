using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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

        //Массивы представляющие размерность поля Игрока и Бота
        public static int[,] myMap = new int[StartWindow.mapSizeHight, StartWindow.mapSizeWidth];
        public static int[,] enemyMap = new int[StartWindow.mapSizeHight, StartWindow.mapSizeWidth];

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
            if (StartWindow.mapSizeHight == 10)
            {
                this.Width = StartWindow.mapSizeWidth * 2 * cellSize + 70;
                this.Height = (StartWindow.mapSizeHight + 1) * cellSize + 20;
            }
            if (StartWindow.mapSizeHight == 11)
            {
                loc += 30;
                this.Width = StartWindow.mapSizeWidth * 2 * cellSize + 120;
                this.Height = (StartWindow.mapSizeHight + 1) * cellSize + 50;
            }
            if (StartWindow.mapSizeHight == 12)
            {
                loc += 60;
                this.Width = StartWindow.mapSizeWidth * 2 * cellSize + 70;
                this.Height = (StartWindow.mapSizeHight + 1) * cellSize + 20;
            }
            if (StartWindow.mapSizeHight == 13)
            {
                loc += 90;
                this.Width = StartWindow.mapSizeWidth * 2 * cellSize + 70;
                this.Height = (StartWindow.mapSizeHight + 1) * cellSize + 20;
            }
            if (StartWindow.mapSizeHight == 14)
            {
                loc += 120;
                this.Width = StartWindow.mapSizeWidth * 2 * cellSize + 70;
                this.Height = (StartWindow.mapSizeHight + 1) * cellSize + 20;
            }
            if (StartWindow.mapSizeHight == 15)
            {
                loc += 150;
                this.Width = StartWindow.mapSizeWidth * 2 * cellSize + 70;
                this.Height = (StartWindow.mapSizeHight + 1) * cellSize + 20;
            }
            // Конфигурация карты игрока
            for (int i = 0; i < StartWindow.mapSizeHight; i++)
            {
                for (int j = 0; j < StartWindow.mapSizeWidth; j++)
                {
                    myMap[i, j] = 0;

                    // Создаем кнопки с определенными координатами
                    Button button = new Button();
                    button.Location = new Point(j * cellSize, i * cellSize);
                    button.Size = new Size(cellSize, cellSize);
                    this.Controls.Add(button);
                }
            }

            // Конфигурация карты бота
            for (int i = 0; i < StartWindow.mapSizeHight; i++)
            {
                for (int j = 0; j < StartWindow.mapSizeWidth; j++)
                {
                    myMap[i, j] = 0;
                    enemyMap[i, j] = 0;

                    Button button = new Button();
                    button.Location = new Point(loc + j * cellSize, i * cellSize);
                    button.Size = new Size(cellSize, cellSize);
                    this.Controls.Add(button);
                }
            }
        }

        private void StowageShips_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }
    }
}
