namespace Pool_Club
{
    public partial class MenuFrame : Form
    {
        public MenuFrame()
        {
            InitializeComponent();
        }

        private void startButtom_Click(object sender, EventArgs e)
        {
            GameScene gamescene = new GameScene();
            gamescene.Show();
        }
    }
}