namespace Sea_Battle
{
    public partial class StartWindow : Form
    {

        //������� ���������� ��� ����������� ����
        public static int mapSizeHight;
        public static int mapSizeWidth;

        public StartWindow()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            //������������� ����������� ����, ������� ������� ������������
            int sizeH = (int)(sizeHeightNumeric.Value);
            int sizeW = (int)(sizeWidthNumeric.Value);

            //��������� �������� ������������
            if (sizeH > 15 || sizeH < 10)
            {
                MessageBox.Show("�� ����� ������������ ����������� ������");
            }
            else
            {
                mapSizeHight = sizeH;
            }
            if (sizeW > 15 || sizeW < 10)
            {
                MessageBox.Show("�� ����� ������������ ����������� ������");
            }
            else
            {
                mapSizeWidth = sizeW;
            }
            if ((sizeW <= 15 && sizeW >= 10) && (sizeH <= 15 && sizeH >= 10))
            {
                StowageShips stowageForm = new StowageShips();
                stowageForm.ShowDialog();
                this.Close();
            }

        }

        private void StartWindow_Load(object sender, EventArgs e)
        {

        }

        private void startButton_MouseMove(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void startButton_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }
    }
}
