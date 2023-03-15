﻿using System;
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

        private Pelota pelotaBlancaSeleccionada; // variable para almacenar la pelota blanca seleccionada
        private bool arrastrandoPelotaBlanca = false; // bandera para indicar si se está arrastrando la pelota blanca
        private Point posicionAnteriorMouse; // posición anterior del mouse para calcular la dirección y velocidad del movimiento

        public GameScene()
        {
            InitializeComponent();

            // Dibujar la mesa de billar
            DrawPoolTable(pictureBox1.CreateGraphics());

            pelotas = new List<Pelota>();

            // Crear una pelota verde en el centro de la mesa
            int ballRadius = 10;
            int ballPosX = pictureBox1.Width / 2;
            int ballPosY = pictureBox1.Height / 2;

            Random rnd = new Random();
            Pelota pelota1 = new Pelota(ballPosX, ballPosY, ballRadius, rnd.Next(-10, 10), rnd.Next(-10, 10), Brushes.Yellow);
            Pelota pelota2 = new Pelota(ballPosX + (2 * ballRadius), ballPosY - ballRadius, ballRadius, rnd.Next(-10, 10), rnd.Next(-10, 10), Brushes.Blue);
            Pelota pelota3 = new Pelota(ballPosX + (2 * ballRadius), ballPosY + ballRadius, ballRadius, rnd.Next(-10, 10), rnd.Next(-10, 10), Brushes.Red);


            Pelota pelotaBlanca = new Pelota(ballPosX-200, ballPosY, ballRadius, 0, 0, Brushes.White);


            pelotas.Add(pelota1);
            pelotas.Add(pelota2);
            pelotas.Add(pelota3);

            pelotas.Add(pelotaBlanca);

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

                // Comprobar si la pelota ha colisionado con otra pelota
                foreach (Pelota otraPelota in pelotas)
                {
                    if (otraPelota != p) // Evita comprobar la colisión consigo misma
                    {
                        double distancia = Math.Sqrt(Math.Pow(p.PosX - otraPelota.PosX, 2) + Math.Pow(p.PosY - otraPelota.PosY, 2));
                        if (distancia < p.radio + otraPelota.radio)
                        {
                            // Colisión detectada
                            double angulo = Math.Atan2(p.PosY - otraPelota.PosY, p.PosX - otraPelota.PosX);
                            double velocidadX1 = Math.Cos(angulo) * p.velocidadInicial;
                            double velocidadY1 = Math.Sin(angulo) * p.velocidadInicial;
                            double velocidadX2 = Math.Cos(angulo + Math.PI) * otraPelota.velocidadInicial;
                            double velocidadY2 = Math.Sin(angulo + Math.PI) * otraPelota.velocidadInicial;

                            p.velocidadX = Convert.ToInt32(velocidadX1);
                            p.velocidadY = Convert.ToInt32(velocidadY1);
                            otraPelota.velocidadX = Convert.ToInt32(velocidadX2);
                            otraPelota.velocidadY = Convert.ToInt32(velocidadY2);

                            // Mover las pelotas después de la colisión
                            p.Mover(pictureBox1.Width, pictureBox1.Height, pelotas);
                            otraPelota.Mover(pictureBox1.Width, pictureBox1.Height, pelotas);
                        }
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

            // Detectar y resolver colisiones entre pelotas
            for (int i = 0; i < pelotas.Count; i++)
            {
                for (int j = i + 1; j < pelotas.Count; j++)
                {
                    Pelota p1 = pelotas[i];
                    Pelota p2 = pelotas[j];

                    double distancia = Math.Sqrt(Math.Pow(p1.posX - p2.posX, 2) + Math.Pow(p1.posY - p2.posY, 2));
                    if (distancia < p1.radio + p2.radio)
                    {
                        // Colisión detectada
                        double angulo = Math.Atan2(p1.posY - p2.posY, p1.posX - p2.posX);
                        double velocidadX1 = Math.Cos(angulo) * p1.velocidadInicial;
                        double velocidadY1 = Math.Sin(angulo) * p1.velocidadInicial;
                        double velocidadX2 = Math.Cos(angulo + Math.PI) * p2.velocidadInicial;
                        double velocidadY2 = Math.Sin(angulo + Math.PI) * p2.velocidadInicial;

                        p1.velocidadX = Convert.ToInt32(velocidadX1);
                        p1.velocidadY = Convert.ToInt32(velocidadY1);
                        p2.velocidadX = Convert.ToInt32(velocidadX2);
                        p2.velocidadY = Convert.ToInt32(velocidadY2);
                    }
                }
            }

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            // Verificar si se ha hecho clic en la pelota blanca
            foreach (Pelota p in pelotas)
            {
                if (p.color == Brushes.White && Math.Sqrt(Math.Pow(e.X - p.posX, 2) + Math.Pow(e.Y - p.posY, 2)) < p.radio)
                {
                    pelotaBlancaSeleccionada = p;
                    arrastrandoPelotaBlanca = true;
                    posicionAnteriorMouse = e.Location;
                    break;
                }
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            // Verificar si se está arrastrando la pelota blanca
            if (arrastrandoPelotaBlanca)
            {
                // Calcular la dirección y velocidad del movimiento de la pelota blanca
                int direccionX = e.X - posicionAnteriorMouse.X;
                int direccionY = e.Y - posicionAnteriorMouse.Y;
                int velocidad = Math.Min(20, (int)Math.Sqrt(Math.Pow(direccionX, 2) + Math.Pow(direccionY, 2)));

                // Actualizar la posición de la pelota blanca
                pelotaBlancaSeleccionada.velocidadX = direccionX * velocidad / 20;
                pelotaBlancaSeleccionada.velocidadY = direccionY * velocidad / 20;
                pelotaBlancaSeleccionada.Mover(pictureBox1.Width, pictureBox1.Height, pelotas);

                // Redibujar el PictureBox
                pictureBox1.Refresh();

                // Actualizar la posición anterior del mouse
                posicionAnteriorMouse = e.Location;
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            // Indicar que se ha soltado la pelota blanca
            arrastrandoPelotaBlanca = false;
        }
    }
}
