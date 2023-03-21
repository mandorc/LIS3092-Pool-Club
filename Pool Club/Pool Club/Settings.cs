using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pool_Club
{
    public partial class Settings : Form
    {
        public static bool easy, normal, hard;
        private Configuration settings;

        private void startButtom_Click(object sender, EventArgs e)
        {
            settings.Easy = radioButton1.Checked;
            settings.Normal = radioButton2.Checked;
            settings.Hard = radioButton3.Checked;

            this.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public Settings()
        {
            InitializeComponent();

            settings = Configuration.Default;

            easy = settings.Easy; normal = settings.Normal; hard = settings.Hard;

            radioButton1.Checked = easy ? true : false;
            radioButton2.Checked = !easy && normal ? true : false;
            radioButton3.Checked = !easy && !normal ? true : false;

        }

        private void Settings_Load(object sender, EventArgs e)
        {

        }
    }
}
