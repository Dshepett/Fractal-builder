using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace Fractal
{
    //Фрактал - Множество Кантора.
    class Kantor : Fractal
    {
        double distance;
        public Kantor(int iter, double len, PictureBox p, Color c1, Color c2, double dist) : base(iter, len, p, c1, c2)
        {
            distance = dist;
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
            //Отрисовка начального треугольника.
            g.FillRectangle(new SolidBrush(colorList[0]), (int)(pb.Width / 2 * (1 - length)), 10,
                (int)(pb.Width * length), 30);
            //Рекурсивный метод для отрисовки элементов фрактала.
            void Draw(int x1, int x2, int y, int n)
            {
                if (n < iter)
                {
                    int len = (int)((x2 - x1) / 3);
                    int x12 = len + x1;
                    int x22 = x2 - len;
                    g.FillRectangle(new SolidBrush(colorList[n]), x1, y, len, 30);
                    g.FillRectangle(new SolidBrush(colorList[n]), x22, y, len, 30);
                    Draw(x1, x12, (int)(y + 30 * (1 + distance)), n + 1);
                    Draw(x22, x2, (int)(y + 30 * (1 + distance)), n + 1);
                }
            }
            Draw((int)(pb.Width / 2 * (1 - length)), (int)(pb.Width - pb.Width / 2 * (1 - length)),
                (int)(10 + 30 * (1 + distance)), 0);
            //Сохранение результата в picture box.
            pb.Image = bmp;
        }
    }
}
