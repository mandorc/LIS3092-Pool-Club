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
        private List<Pelota> pelotas; // lista para almacenar las pelotas
        private Brush[] colores = { Brushes.Yellow, Brushes.Blue, Brushes.Red, Brushes.Purple, Brushes.Orange, Brushes.Green, Brushes.Maroon, Brushes.Black, Brushes.DarkCyan, Brushes.Pink, Brushes.Gray, Brushes.DeepSkyBlue, Brushes.WhiteSmoke, Brushes.SaddleBrown, Brushes.MediumSpringGreen }; // array de colores para las pelotas

        private bool[] pelotaDesaparecida = { false, false, false, false, false, false, false, false }; // array para determinar si cada pelota ha desaparecido

        public GameScene()
        {
            InitializeComponent();

            // Dibujar la mesa de billar
            DrawPoolTable(pictureBox1.CreateGraphics());

            pelotas = new List<Pelota>();

            // Crear una pelota verde en el centro de la mesa
            int ballRadius = 20;
            int ballPosX = pictureBox1.Width / 2;
            int ballPosY = pictureBox1.Height / 2;

            Pelota p = new Pelota(ballPosX, ballPosY, ballRadius, 0, 0, Brushes.Green);
            pelotas.Add(p);

        }

        private void GameScene_Load(object sender, EventArgs e)
        {

        }

        private bool EstaEnAgujero(Pelota p, int agujero)
        {
            // Determinar si la pelota está dentro del agujero
            int holeSize = 50;
            int xSpacing = 25;
            int ySpacing = 25;
            int holeX, holeY;

            switch (agujero)
            {
                case 0: // esquina superior izquierda
                    holeX = xSpacing;
                    holeY = ySpacing;
                    break;
                case 1: // esquina superior derecha
                    holeX = pictureBox1.Width - xSpacing - holeSize;
                    holeY = ySpacing;
                    break;
                case 2: // esquina inferior izquierda
                    holeX = xSpacing;
                    holeY = pictureBox1.Height - ySpacing - holeSize;
                    break;
                case 3: // esquina inferior derecha
                    holeX = pictureBox1.Width - xSpacing - holeSize;
                    holeY = pictureBox1.Height - ySpacing - holeSize;
                    break;
                case 4: // agujero superior central
                    holeX = (pictureBox1.Width - holeSize) / 2;
                    holeY = ySpacing;
                    break;
                case 5: // agujero inferior central
                    holeX = (pictureBox1.Width - holeSize) / 2;
                    holeY = pictureBox1.Height - ySpacing - holeSize;
                    break;
                default:
                    return false;
            }

            return p.PosX + p.radio > holeX && p.PosX - p.radio < holeX + holeSize &&
                   p.PosY + p.radio > holeY && p.PosY - p.radio < holeY + holeSize;
        }

        private void DrawPoolTable(Graphics g)
        {
            // Dibujar la mesa de billar
            g.FillRectangle(Brushes.Green, 20, 20, pictureBox1.Width - 40, pictureBox1.Height - 40); // dibujar el paño verde
            g.FillRectangle(Brushes.Brown, 50, 50, pictureBox1.Width - 100, pictureBox1.Height - 100); // dibujar el borde de madera

            // Dibujar los agujeros
            int holeSize = 50;
            int xSpacing = 25;
            int ySpacing = 25;
            g.FillEllipse(Brushes.Black, xSpacing, ySpacing, holeSize, holeSize); // esquina superior izquierda
            g.FillEllipse(Brushes.Black, pictureBox1.Width - xSpacing - holeSize, ySpacing, holeSize, holeSize); // esquina superior derecha
            g.FillEllipse(Brushes.Black, xSpacing, pictureBox1.Height - ySpacing - holeSize, holeSize, holeSize); // esquina inferior izquierda
            g.FillEllipse(Brushes.Black, pictureBox1.Width - xSpacing - holeSize, pictureBox1.Height - ySpacing - holeSize, holeSize, holeSize); // esquina inferior derecha
            g.FillEllipse(Brushes.Black, (pictureBox1.Width - holeSize) / 2, ySpacing, holeSize, holeSize); // agujero superior central
            g.FillEllipse(Brushes.Black, (pictureBox1.Width - holeSize) / 2, pictureBox1.Height - ySpacing - holeSize, holeSize, holeSize); // agujero inferior central
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Mover las pelotas
            foreach (Pelota p in pelotas)
            {
                p.Mover(pictureBox1.Width, pictureBox1.Height, pelotas);

                // Comprobar si la pelota ha desaparecido en un agujero
                for (int i = 0; i < 6; i++)
                {
                    if (!pelotaDesaparecida[i] && EstaEnAgujero(p, i))
                    {
                        pelotaDesaparecida[i] = true;
                        p.PosX = -100; // establecer la posición de la pelota fuera del área visible del PictureBox
                        p.PosY = -100;
                    }
                }
            }

            // Redibujar el PictureBox
            pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            // Dibujar la mesa de billar
            DrawPoolTable(e.Graphics);

            // Dibujar las pelotas
            foreach (Pelota p in pelotas)
            {
                p.Dibujar(e.Graphics);
            }

        }
    }
}
