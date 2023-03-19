using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pool_Club
{
    internal class Pelota
    {

        public int posX;
        public int posY;
        public int radio;
        public int velocidadX;
        public int velocidadY;
        public int velocidadInicial;
        public Brush bcolor;
        public Image Image { get; private set; }
        public String name = "pelotaSinNombre";

        public int PosX
        {
            get { return posX; }
            set { posX = value; }
        }

        public int PosY
        {
            get { return posY; }
            set { posY = value; }
        }

        public Pelota(int x, int y, int r, int vx, int vy, String color)
        {
            posX = x;
            posY = y;
            radio = r;
            velocidadX = vx;
            velocidadY = vy;
            velocidadInicial = (int)Math.Sqrt(Math.Pow(velocidadX, 2) + Math.Pow(velocidadY, 2));
            switch(color)
            {
                case "1":
                    Image = Resources.pelota1;
                    color = "1";
                    name = "pelota 1";
                    break;
                case "2":
                    Image = Resources.pelota2;
                    color = "2";
                    break;
                case "3":
                    Image = Resources.pelota3;
                    color = "3";
                    break;
                case "blanca":
                    Brush c = Brushes.White;
                    bcolor = c;
                    Image = Resources.pelotaBlanca;
                    break;
                default:
                    Image = Resources.pelota2; break;
            }
        }

        public void Dibujar(Graphics g)
        {
            //g.FillEllipse(color, posX - radio, posY - radio, radio * 2, radio * 2);
            g.DrawImage(Image, new Rectangle(posX - radio, posY - radio, radio * 2, radio * 2));
        }

        public void Mover(int ancho, int alto, List<Pelota> pelotas)
        {
            posX += velocidadX;
            posY += velocidadY;

            // Comprobar si la pelota ha llegado a los bordes de la mesa
            // izquierda - derecha
            if (posX - radio < 55 || posX + radio > ancho-55)
            {
                velocidadX = -velocidadX;
            }
            // arriba - abajo
            if (posY - radio < 55 || posY + radio > alto-55)
            {
                velocidadY = -velocidadY;
            }

            // Comprobar si la pelota ha colisionado con otra pelota
            foreach (Pelota otraPelota in pelotas)
            {
                if (otraPelota != this) // Evita comprobar la colisión consigo misma
                {
                    double distancia = Math.Sqrt(Math.Pow(posX - otraPelota.posX, 2) + Math.Pow(posY - otraPelota.posY, 2));
                    if (distancia < radio + otraPelota.radio)
                    {
                        // Colisión detectada
                        double angulo = Math.Atan2(posY - otraPelota.posY, posX - otraPelota.posX);
                        double velocidadX1 = Math.Cos(angulo) * velocidadInicial;
                        double velocidadY1 = Math.Sin(angulo) * velocidadInicial;
                        double velocidadX2 = Math.Cos(angulo + Math.PI) * otraPelota.velocidadInicial;
                        double velocidadY2 = Math.Sin(angulo + Math.PI) * otraPelota.velocidadInicial;

                        velocidadX = Convert.ToInt32(velocidadX1);
                        velocidadY = Convert.ToInt32(velocidadY1);
                        otraPelota.velocidadX = Convert.ToInt32(velocidadX2);
                        otraPelota.velocidadY = Convert.ToInt32(velocidadY2);
                    }
                }

            }
        }

    }
}
