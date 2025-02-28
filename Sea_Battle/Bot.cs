using System.Runtime.CompilerServices;

namespace Sea_Battle
{
    public class Bot
    {
        public int[,] botMap = new int[StartWindow.mapSizeHeight + 1, StartWindow.mapSizeWidth + 1]; 
        public int[,] playerMap = new int[StartWindow.mapSizeHeight + 1, StartWindow.mapSizeWidth + 1]; 

        public Button[,] botButtons = new Button[StartWindow.mapSizeHeight + 1, StartWindow.mapSizeWidth + 1];
        public Button[,] playerButtons = new Button[StartWindow.mapSizeHeight + 1, StartWindow.mapSizeWidth + 1];
        

        //Конструктор  
        public Bot(int[,] botMap, int[,] playerMap, Button[,] botButtons, Button[,] playerButtons)
        {
            this.botMap = botMap;
            this.playerMap = playerMap;
            this.botButtons = botButtons;
            this.playerButtons = playerButtons;
        }

        // Функция для того чтобы предусмотреть выходы за границы массива
        public bool IsInsideMap(int i, int j)
        {
            if (i < 0 || j < 0 || i >= StartWindow.mapSizeWidth + 1 || j >= StartWindow.mapSizeHeight + 1)
            {
                return false;
            }
            return true;
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
        //Генерация кораблей бота
        public void CongfigureShips()
        {

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
            } while (playerMap[buttonIndexY, buttonIndexX] == -1 ); // Проверяем, не стреляли ли мы уже в эту клетку

            bool hit = false;

            if (playerMap[buttonIndexY, buttonIndexX] == 1)
            {
                hit = true;

                // Убираем наличие корабля в клетке
                playerMap[buttonIndexY, buttonIndexX] = -1;

                playerButtons[buttonIndexY, buttonIndexX].BackgroundImage = Image.FromFile("Resources/BreakShip.png"); // Изображение попадания
                playerButtons[buttonIndexY, buttonIndexX].BackgroundImageLayout = ImageLayout.Stretch;
                playerButtons[buttonIndexY, buttonIndexX].BackColor = Color.Black;
            }
            else
            {
                hit = false;
                playerMap[buttonIndexY, buttonIndexX] = -1;
                playerButtons[buttonIndexY, buttonIndexX].BackgroundImage = Image.FromFile("Resources/MissHit.png"); // Изображение промаха
                playerButtons[buttonIndexY, buttonIndexX].BackgroundImageLayout = ImageLayout.Stretch;
                playerButtons[buttonIndexY, buttonIndexX].BackColor = Color.Gray;

            }
            if (hit)
            {
                BotShoot();
            }
            return hit;
        }
    }
}
