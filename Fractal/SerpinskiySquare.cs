using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Drawing.Drawing2D;

namespace Fractal
{
    //Фрактал - Ковер Серпинского.
    class SerpinskiySquare : Fractal
    {
        public SerpinskiySquare(int iter, double len, PictureBox p, Color c1, Color c2) : base(iter, len, p, c1, c2)
        {
        }
        //Метод для отрисовки фрактала.
        public override void DrawFrac()
        {
            //Создание битмапа для рисования.
            Bitmap bmp = new Bitmap(pb.Width, pb.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.Black);
            Brush b = new SolidBrush(Color.Black);
            int d;
            //Выбор размера стороны.
            if (pb.Height > pb.Width)
                d = (int)(pb.Width * length);
            else
                d = (int)(pb.Height * length);
            //Создание градиентного цвета.
            LinearGradientBrush l = new LinearGradientBrush(new Point(pb.Width / 2 - d / 2 - 50, pb.Height / 2 - d / 2 - 50), new Point(pb.Width / 2 + d / 2 + 50, pb.Height / 2 + d / 2 + 50), startColor, endColor);
            //Отрисовка начального квадрата.
            g.FillRectangle(l, pb.Width / 2 - d / 2, pb.Height / 2 - d / 2, d, d);
            //Рекурсивный метод для отрисовки элементов фрактала.
            void Draw(int x, int y, int d, int n)
            {
                if (n < iter)
                {
                    g.FillRectangle(b, x + d / 3, y + d / 3, d / 3, d / 3);
                    Draw(x, y, d / 3, n + 1);
                    Draw(x + d / 3, y, d / 3, n + 1);
                    Draw(x, y + d / 3, d / 3, n + 1);
                    Draw(x + d * 2 / 3, y, d / 3, n + 1);
                    Draw(x, y + d * 2 / 3, d / 3, n + 1);
                    Draw(x + d * 2 / 3, y + d / 3, d / 3, n + 1);
                    Draw(x + d / 3, y + d * 2 / 3, d / 3, n + 1);
                    Draw(x + d * 2 / 3, y + d * 2 / 3, d / 3, n + 1);
                }
            }
            Draw(pb.Width / 2 - d / 2, pb.Height / 2 - d / 2, d, 0);
            //Сохранение результата в picture box.
            pb.Image = bmp;
        }
    }
}
