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

        private void button1_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.Show();
        }
    }
}