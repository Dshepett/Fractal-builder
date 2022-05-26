using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Drawing.Drawing2D;

namespace Fractal
{
    //Фрактал - кривая Коха.
    class Kokh : Fractal
    {
        public Kokh(int iter, double len, PictureBox p, Color c1, Color c2) : base(iter, len, p, c1, c2)
        {
        }
        //Метод для отрисовки фрактала.
        public override void DrawFrac()
        {
            //Cоздание градиентного цвета.
            LinearGradientBrush l = new LinearGradientBrush(new Point((int)(pb.Width / 2 * (1 - length)), pb.Height * 9 / 10), new Point((int)(pb.Width - pb.Width / 2 * (1 - length)), pb.Height * 9 / 10), startColor, endColor);
            //Создание битмапа для рисвания в нем.
            Bitmap bmp = new Bitmap(pb.Width, pb.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.Black);
            Pen a = new Pen(l);
            Pen b = new Pen(Color.Black);
            //Отрисовываем начальный отрезок.
            g.DrawLine(new Pen(l), new Point((int)(pb.Width / 2 * (1 - length)), pb.Height * 9 / 10), new Point((int)(pb.Width - pb.Width / 2 * (1 - length)), pb.Height * 9 / 10));
            //Рекурсивный метод лля отрисовки следующих элментов фратала.
            void Draw(int x01, int y01, int x02, int y02, int n, double alfa)
            {
                if (n < iter)
                {
                    int x1 = (int)((x01 + x02 * (1.0 / 3.0)) / (4.0 / 3.0));
                    int x2 = (int)((x02 + x01 * (1.0 / 3.0)) / (4.0 / 3.0));
                    int y1 = (int)((y01 + y02 * (1.0 / 3.0)) / (4.0 / 3.0));
                    int y2 = (int)((y02 + y01 * (1.0 / 3.0)) / (4.0 / 3.0));
                    g.DrawLine(b, new Point(x01, y01), new Point(x02, y02));
                    g.DrawLine(b, new Point(x01, y01 + 1), new Point(x02, y02 + 1));
                    g.DrawLine(b, new Point(x01, y01 - 1), new Point(x02, y02 - 1));
                    g.DrawLine(a, new Point(x01, y01), new Point(x1, y1));
                    g.DrawLine(a, new Point(x2, y2), new Point(x02, y02));
                    int xm = (x1 + x2) / 2;
                    int ym = (y1 + y2) / 2;
                    int len = (int)(Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2)) * Math.Sqrt(3) / 2);
                    int x3 = xm + (int)(len * Math.Sin(alfa));
                    int y3 = ym + (int)(len * Math.Cos(alfa));
                    g.DrawLine(a, new Point(x1, y1), new Point(x3, y3));
                    g.DrawLine(a, new Point(x2, y2), new Point(x3, y3));
                    Draw(x01, y01, x1, y1, n + 1, alfa);
                    Draw(x2, y2, x02, y02, n + 1, alfa);
                    Draw(x1, y1, x3, y3, n + 1, alfa + Math.PI / 3);
                    Draw(x3, y3, x2, y2, n + 1, alfa - Math.PI / 3);
                }
            }
            Draw((int)(pb.Width / 2 * (1 - length)), pb.Height * 9 / 10, (int)(pb.Width - pb.Width / 2 * (1 - length)), pb.Height * 9 / 10, 0, Math.PI);
            //Сохранение результата в picture box.
            pb.Image = bmp;
        }
    }
}
