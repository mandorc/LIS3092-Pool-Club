﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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

        private static String[] ordenPelotas = { "pelota 1", "pelota 2", "pelota 3", "pelota 4", "pelota 5", "pelota 6", "pelota 7", "pelota 8", "pelota 9", "pelota 10", "pelota 11", "pelota 12", "pelota 13", "pelota 14", "pelota 15" };
    
        private static int ordenContador = 0;
        private static int limiteContador;

        private Configuration settings;

        private DateTime ultimoTiempoActualizacion = DateTime.Now;

        public GameScene()
        {
            InitializeComponent();

            // Dibujar la mesa de billar
            DrawPoolTable(pictureBox1.CreateGraphics());

            settings = Configuration.Default;

            pelotas = new List<Pelota>();

            // Crear una pelota verde en el centro de la mesa
            int ballRadius = 10;
            int ballDiameter = ballRadius * 2;
            int ballPosX = pictureBox1.Width / 2;
            int ballPosY = pictureBox1.Height / 2;

            Random rnd = new Random();

            // Obtener el nivel que ha seleccionado el usuario
            if (settings.Easy)
            {
                // Primera columna
                Pelota pelota1 = new Pelota(ballPosX, ballPosY, ballRadius, rnd.Next(-10, 10), rnd.Next(-10, 10), "1");

                // Segunda columna
                Pelota pelota2 = new Pelota(ballPosX + ballDiameter, ballPosY - ballRadius, ballRadius, rnd.Next(-10, 10), rnd.Next(-10, 10), "2");
                Pelota pelota3 = new Pelota(ballPosX + ballDiameter, ballPosY + ballRadius, ballRadius, rnd.Next(-10, 10), rnd.Next(-10, 10), "3");

                pelotas.Add(pelota1);
                pelotas.Add(pelota2);
                pelotas.Add(pelota3);

                limiteContador = 3;
            } else if (settings.Normal)
            {
                // Primera columna
                Pelota pelota1 = new Pelota(ballPosX, ballPosY, ballRadius, rnd.Next(-10, 10), rnd.Next(-10, 10), "1");

                // Segunda columna
                Pelota pelota2 = new Pelota(ballPosX + ballDiameter, ballPosY - ballRadius, ballRadius, rnd.Next(-10, 10), rnd.Next(-10, 10), "2");
                Pelota pelota3 = new Pelota(ballPosX + ballDiameter, ballPosY + ballRadius, ballRadius, rnd.Next(-10, 10), rnd.Next(-10, 10), "3");
                // Tercera
                Pelota pelota4 = new Pelota(ballPosX + (2 * ballDiameter), ballPosY - (2 * ballRadius), ballRadius, rnd.Next(-10, 10), rnd.Next(-10, 10), "4");
                Pelota pelota5 = new Pelota(ballPosX + (2 * ballDiameter), ballPosY, ballRadius, rnd.Next(-10, 10), rnd.Next(-10, 10), "5");
                Pelota pelota6 = new Pelota(ballPosX + (2 * ballDiameter), ballPosY + (2 * ballRadius), ballRadius, rnd.Next(-10, 10), rnd.Next(-10, 10), "6");

                pelotas.Add(pelota1);
                pelotas.Add(pelota2);
                pelotas.Add(pelota3);
                pelotas.Add(pelota4);
                pelotas.Add(pelota5);
                pelotas.Add(pelota6);

                limiteContador = 6;
            } else
            {
                // Primera columna
                Pelota pelota1 = new Pelota(ballPosX, ballPosY, ballRadius, rnd.Next(-10, 10), rnd.Next(-10, 10), "1");

                // Segunda columna
                Pelota pelota2 = new Pelota(ballPosX + ballDiameter, ballPosY - ballRadius, ballRadius, rnd.Next(-10, 10), rnd.Next(-10, 10), "2");
                Pelota pelota3 = new Pelota(ballPosX + ballDiameter, ballPosY + ballRadius, ballRadius, rnd.Next(-10, 10), rnd.Next(-10, 10), "3");
                // Tercera
                Pelota pelota4 = new Pelota(ballPosX + (2 * ballDiameter), ballPosY - (2 * ballRadius), ballRadius, rnd.Next(-10, 10), rnd.Next(-10, 10), "4");
                Pelota pelota5 = new Pelota(ballPosX + (2 * ballDiameter), ballPosY, ballRadius, rnd.Next(-10, 10), rnd.Next(-10, 10), "5");
                Pelota pelota6 = new Pelota(ballPosX + (2 * ballDiameter), ballPosY + (2 * ballRadius), ballRadius, rnd.Next(-10, 10), rnd.Next(-10, 10), "6");
                // Cuarta
                Pelota pelota7 = new Pelota(ballPosX + (3 * ballDiameter), ballPosY - (3 * ballRadius), ballRadius, rnd.Next(-10, 10), rnd.Next(-10, 10), "7");
                Pelota pelota8 = new Pelota(ballPosX + (3 * ballDiameter), ballPosY - (ballRadius), ballRadius, rnd.Next(-10, 10), rnd.Next(-10, 10), "8");
                Pelota pelota9 = new Pelota(ballPosX + (3 * ballDiameter), ballPosY - (ballRadius - ballDiameter), ballRadius, rnd.Next(-10, 10), rnd.Next(-10, 10), "9");
                Pelota pelota10 = new Pelota(ballPosX + (3 * ballDiameter), ballPosY + (ballRadius + ballDiameter), ballRadius, rnd.Next(-10, 10), rnd.Next(-10, 10), "10");
                // Quinta
                Pelota pelota11 = new Pelota(ballPosX + (4 * ballDiameter), ballPosY - (4 * ballRadius), ballRadius, rnd.Next(-10, 10), rnd.Next(-10, 10), "11");
                Pelota pelota12 = new Pelota(ballPosX + (4 * ballDiameter), ballPosY - (2 * ballRadius), ballRadius, rnd.Next(-10, 10), rnd.Next(-10, 10), "12");
                Pelota pelota13 = new Pelota(ballPosX + (4 * ballDiameter), ballPosY, ballRadius, rnd.Next(-10, 10), rnd.Next(-10, 10), "13");
                Pelota pelota14 = new Pelota(ballPosX + (4 * ballDiameter), ballPosY + ballDiameter, ballRadius, rnd.Next(-10, 10), rnd.Next(-10, 10), "14");
                Pelota pelota15 = new Pelota(ballPosX + (4 * ballDiameter), ballPosY + 2 * ballDiameter, ballRadius, rnd.Next(-10, 10), rnd.Next(-10, 10), "15");

                pelotas.Add(pelota1);
                pelotas.Add(pelota2);
                pelotas.Add(pelota3);
                pelotas.Add(pelota4);
                pelotas.Add(pelota5);
                pelotas.Add(pelota6);
                pelotas.Add(pelota7);
                pelotas.Add(pelota8);
                pelotas.Add(pelota9);
                pelotas.Add(pelota10);
                pelotas.Add(pelota11);
                pelotas.Add(pelota12);
                pelotas.Add(pelota13);
                pelotas.Add(pelota14);
                pelotas.Add(pelota15);

                limiteContador = 15;
            }



            Pelota pelotaBlanca = new Pelota(ballPosX-200, ballPosY, ballRadius, 0, 0, "blanca");

            

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

                        // Comprobamos si va en el orden que debe ir
                        if (ordenPelotas[ordenContador] == p.name) {
                            ordenContador++;
                            if (ordenContador == limiteContador)
                            {
                                Win winWindow = new Win();
                                winWindow.Show();
                                this.Close();
                            }
                        } else
                        {
                            LoseWindow loseWindow = new LoseWindow();
                            loseWindow.Show();
                            this.Close();
                        }

                        //pelotaDesaparecida[i] = true;
                        p.PosX = -100; // establecer la posición de la pelota fuera del área visible del PictureBox
                        p.PosY = -100;
                        //pelotaDesaparecida[i] = false;
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
            // Draw the pool table
            DrawPoolTable(e.Graphics);

            // Draw the balls
            foreach (Pelota p in pelotas)
            {
                p.Dibujar(e.Graphics);
            }

            // Detect and resolve collisions between balls
            if (!arrastrandoPelotaBlanca)
            {
                // Create a Path object for each ball that represents its collision circle
                GraphicsPath[] ballPaths = new GraphicsPath[pelotas.Count];
                for (int i = 0; i < pelotas.Count; i++)
                {
                    GraphicsPath path = new GraphicsPath();
                    path.AddEllipse(pelotas[i].posX - pelotas[i].radio, pelotas[i].posY - pelotas[i].radio, pelotas[i].radio * 2, pelotas[i].radio * 2);
                    ballPaths[i] = path;
                }

                // Detect collisions between the collision circles of the balls
                for (int i = 0; i < pelotas.Count; i++)
                {
                    for (int j = i + 1; j < pelotas.Count; j++)
                    {
                        // Check if the collision circles of the balls intersect
                        if (ballPaths[i].IsVisible(pelotas[j].posX, pelotas[j].posY))
                        {
                            // Collision detected
                            double angulo = Math.Atan2(pelotas[i].posY - pelotas[j].posY, pelotas[i].posX - pelotas[j].posX);
                            double velocidadX1 = Math.Cos(angulo) * pelotas[i].velocidadInicial;
                            double velocidadY1 = Math.Sin(angulo) * pelotas[i].velocidadInicial;
                            double velocidadX2 = Math.Cos(angulo + Math.PI) * pelotas[j].velocidadInicial;
                            double velocidadY2 = Math.Sin(angulo + Math.PI) * pelotas[j].velocidadInicial;

                            pelotas[i].velocidadX = Convert.ToInt32(velocidadX1);
                            pelotas[i].velocidadY = Convert.ToInt32(velocidadY1);
                            pelotas[j].velocidadX = Convert.ToInt32(velocidadX2);
                            pelotas[j].velocidadY = Convert.ToInt32(velocidadY2);

                            // Move the balls after the collision
                            pelotas[i].Mover(pictureBox1.Width, pictureBox1.Height, pelotas);
                            pelotas[j].Mover(pictureBox1.Width, pictureBox1.Height, pelotas);
                        }
                    }
                }
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            timer1.Enabled = true;

            ultimoTiempoActualizacion = DateTime.Now;

            // Verificar si el clic del mouse está en la pelota blanca
            foreach (Pelota p in pelotas)
            {
                if (p.bcolor == Brushes.White && Math.Sqrt(Math.Pow(e.X - p.posX, 2) + Math.Pow(e.Y - p.posY, 2)) < p.radio)
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
                // Verificar si ha pasado suficiente tiempo desde la última actualización
                DateTime ahora = DateTime.Now;
                TimeSpan tiempoDesdeUltimaActualizacion = ahora - ultimoTiempoActualizacion;
                if (tiempoDesdeUltimaActualizacion.TotalMilliseconds >= temporizador.Interval)
                {
                    // Actualizar la posición de la pelota blanca
                    int deltaX = e.Location.X - posicionAnteriorMouse.X;
                    int deltaY = e.Location.Y - posicionAnteriorMouse.Y;
                    pelotaBlancaSeleccionada.PosX += deltaX;
                    pelotaBlancaSeleccionada.PosY += deltaY;
                    posicionAnteriorMouse = e.Location;
                    ultimoTiempoActualizacion = ahora;

                    // Volver a dibujar el PictureBox
                    pictureBox1.Refresh();
                }
            }

            // Cambiar el cursor del mouse a la forma correcta
            Cursor = Cursors.Cross; // O la forma que desees para el cursor personalizado

            // Actualizar la posición del cursor personalizado
            int x = e.X - (mousePic.Width / 2);
            int y = e.Y - (mousePic.Height / 2);
            mousePic.Location = new Point(x, y);
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            // Indicar que se ha soltado la pelota blanca
            arrastrandoPelotaBlanca = false;
            temporizador.Stop();

        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            Cursor.Hide(); // Oculta el cursor del mouse
            mousePic.Visible = true; // Muestra el PictureBox del cursor personalizado
            Cursor = Cursors.Cross; // Cambia el cursor del mouse a una cruz
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Show(); // Muestra el cursor del mouse
            mousePic.Visible = false; // Oculta el PictureBox del cursor personalizado
            Cursor = Cursors.Default; // Restablece el cursor del mouse al predeterminado
        }
    }
}
