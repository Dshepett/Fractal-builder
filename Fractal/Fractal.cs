using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace Fractal
{
    abstract class Fractal
    {
        //Объект, где будет выполняться рисование. 
        public PictureBox pb;
        public Fractal(int iter, double len, PictureBox p, Color c1, Color c2)
        {
            pb = p;
            this.iter = iter;
            length = len;
            startColor = c1;
            endColor = c2;
        }
        //Количество итераций.
        public int iter;
        //Длина начального отрезка.
        public double length;
        //Начальный цвет градиента.
        public Color startColor;
        //Конечный цвет градиента.
        public Color endColor;
        //Метод для отрисовки фрактала.
        public abstract void DrawFrac();
    }
}