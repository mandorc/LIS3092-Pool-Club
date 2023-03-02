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
    public partial class GameScene : Form
    {
        public GameScene()
        {
            InitializeComponent();
            DrawPoolTable();
        }

        private void GameScene_Load(object sender, EventArgs e)
        {

        }

        private void DrawPoolTable()
        {
            // Crear un objeto Bitmap del tamaño del PictureBox
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            // Crear un objeto Graphics a partir del Bitmap
            Graphics g = Graphics.FromImage(bmp);

            // Dibujar la mesa de billar
            g.FillRectangle(Brushes.Green, 20, 20, bmp.Width - 40, bmp.Height - 40); // dibujar el paño verde
            g.FillRectangle(Brushes.Brown, 50, 50, bmp.Width - 100, bmp.Height - 100); // dibujar el borde de madera

            // Dibujar los agujeros
            int holeSize = 50;
            int xSpacing = 25;
            int ySpacing = 25;
            g.FillEllipse(Brushes.Black, xSpacing, ySpacing, holeSize, holeSize); // esquina superior izquierda
            g.FillEllipse(Brushes.Black, bmp.Width - xSpacing - holeSize, ySpacing, holeSize, holeSize); // esquina superior derecha
            g.FillEllipse(Brushes.Black, xSpacing, bmp.Height - ySpacing - holeSize, holeSize, holeSize); // esquina inferior izquierda
            g.FillEllipse(Brushes.Black, bmp.Width - xSpacing - holeSize, bmp.Height - ySpacing - holeSize, holeSize, holeSize); // esquina inferior derecha
            g.FillEllipse(Brushes.Black, (bmp.Width - holeSize) / 2, ySpacing, holeSize, holeSize); // agujero superior central
            g.FillEllipse(Brushes.Black, (bmp.Width - holeSize) / 2, bmp.Height - ySpacing - holeSize, holeSize, holeSize); // agujero inferior central

            // Liberar recursos
            g.Dispose();

            // Asignar el Bitmap al PictureBox
            pictureBox1.Image = bmp;
        }
    }
}
