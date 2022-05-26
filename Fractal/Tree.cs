using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Drawing.Drawing2D;

namespace Fractal
{
    //Фрактал - Фрактальное дерево.
    class Tree : Fractal
    {
        //Угол левой ветки дерева.
        int alfa1;
        //угол правой ветки дерева.
        int alfa2;
        //коэффицент уменьшения.
        double koef;
        public Tree(int a1, int a2, double k, int iter, double len, PictureBox p, Color c1, Color c2) : base(iter, len, p, c1, c2)
        {
            alfa1 = a1;
            alfa2 = a2;
            koef = k;
        }
        //Метод для отрислвки фрактала.
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
            //Рекурсивный метод для отрисовки веток дерева.
            void Draw(int x, int y, int n, int ll, double l)
            {
                if ((n < iter) & (x > 0) & (y > 0) & (ll > 0))
                {

                    int x1 = x + (int)((ll * koef) * Math.Sin(l + Math.PI / alfa1));
                    int x2 = x + (int)((ll * koef) * Math.Sin(l - Math.PI / alfa2));
                    int y1 = y + (int)((ll * koef) * Math.Cos(l + Math.PI / alfa1));
                    int y2 = y + (int)((ll * koef) * Math.Cos(l - Math.PI / alfa2));
                    g.DrawLine(new Pen(colorList[n]), new Point(x, y), new Point(x1, y1));
                    g.DrawLine(new Pen(colorList[n]), new Point(x, y), new Point(x2, y2));
                    Draw(x1, y1, n + 1, (int)(ll * koef), l + Math.PI / alfa1);
                    Draw(x2, y2, n + 1, (int)(ll * koef), l - Math.PI / alfa2);
                }

            }
            //Отрисовка начального отрезка.
            g.DrawLine(new Pen(colorList[0]), new Point(pb.Width / 2, pb.Height), new Point(pb.Width / 2, (int)(pb.Height * (1 - length))));
            Draw(pb.Width / 2, (int)(pb.Height * (1 - length)), 1, (int)(pb.Height * length), Math.PI);
            //Сохранение результата в picture box.
            pb.Image = bmp;
        }
    }
}
