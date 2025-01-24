using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sea_Battle
{
    public partial class SeaBattleGame : Form
    {
        public SeaBattleGame()
        {
            InitializeComponent();
        }

        public bool Shoot(int[,] map, Button pressedButton)
        {
            int rowIndex = (pressedButton.Location.Y - StowageShips.cellSize) / StowageShips.cellSize;
            int colIndex = (pressedButton.Location.X - StowageShips.cellSize) / StowageShips.cellSize;
            bool hit = false;
            if (map[pressedButton.Location.Y / StowageShips.cellSize, pressedButton.Location.X / StowageShips.cellSize] == 0)
            {
                hit = true;
            }
            else hit = false;

            return hit;
        }

        private void SeaBattleGame_Load(object sender, EventArgs e)
        {

        }
    }
}
