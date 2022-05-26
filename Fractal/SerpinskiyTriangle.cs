using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace Fractal
{
    //Фрактал - Треугольник Серпинского.
    class SerpinskiyTriangle : Fractal
    {
        public SerpinskiyTriangle(int iter, double len, PictureBox p, Color c1, Color c2) : base(iter, len, p, c1, c2)
        {
        }
        //Метод для отрисовки фрактала.
        public override void DrawFrac()
        {
            //Создание градиентного цвета.
            int rMin = startColor.R;
            int rMax = endColor.R;
            int bMin = startColor.B;
            int bMax = endColor.B;
            int gMin = startColor.G;
            int gMax = endColor.G;
            var colorList = new List<Color>();
            for (int i = 0; i < iter; i++)
            {
                var rAverage = rMin + (int)((rMax - rMin) * i / iter);
                var gAverage = gMin + (int)((gMax - gMin) * i / iter);
                var bAverage = bMin + (int)((bMax - bMin) * i / iter);
                colorList.Add(Color.FromArgb(rAverage, gAverage, bAverage));
            }
            //Создание битмапа для рисования в нем.
            Bitmap bmp = new Bitmap(pb.Width, pb.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.Black);
            int d;
            //Выбор длины стороны начального треугольника.
            if (pb.Height > pb.Width)
                d = (int)(pb.Width * length);
            else
                d = (int)(pb.Height * length);
            int x1 = pb.Width / 2 - d / 2, y1 = pb.Height / 2 + d / 2;
            int x2 = x1 + d;
            int y2 = y1;
            int x3 = (x1 + x2) / 2;
            int y3 = (int)(y1 - d * Math.Sqrt(3) / 2);
            //Отрисовка начального треугольника.
            g.DrawPolygon(new Pen(colorList[0]), new Point[] { new Point(x1, y1), new Point(x2, y2), new Point(x3, y3) });
            //Рекурсивный метод для отрисовки элементов фрактала.
            void Draw(int x01, int y01, int x02, int y02, int x03, int y03, int n)
            {
                if (n < iter)
                {
                    int x1 = (x01 + x02) / 2;
                    int x2 = (x02 + x03) / 2;
                    int x3 = (x01 + x03) / 2;
                    int y1 = (y01 + y02) / 2;
                    int y2 = (y02 + y03) / 2;
                    int y3 = (y01 + y03) / 2;
                    g.DrawPolygon(new Pen(colorList[n]), new Point[] { new Point(x1, y1), new Point(x2, y2), new Point(x3, y3) });
                    Draw(x01, y01, x1, y1, x3, y3, n + 1);
                    Draw(x02, y02, x1, y1, x2, y2, n + 1);
                    Draw(x03, y03, x2, y2, x3, y3, n + 1);
                }

            }
            Draw(x1, y1, x2, y2, x3, y3, 0);
            //Сохранение резкльтата в picture box.
            pb.Image = bmp;
        }
    }
}

