using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fractal
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox2.SelectedItem = "Белый";
            comboBox3.SelectedItem = "Белый";
        }

        //Строка, в которой будет сохраняться название текущего вида фрактала.
        string s;

        //Переменная фрактала.
        Fractal frac;

        //Условие для возможности отрисовки фрактала.
        bool usl = false;

        //Количество итераций.
        int iteraion;

        //Значение угла для левой ветки дерева.
        int a1;

        //Значение угла для правой ветки дерева.
        int a2;

        //Длина начального отрезка фрактала.
        double len;

        //Коэффицент уменьшения ветки дерева.
        double koef;

        //Начальный цвет градиента.
        Color color1;

        //Конечный цвет градиента.
        Color color2;

        //Текущий уровень масштабирования.
        int zoom = 1;

        //Расстояние между итерациями в канторе.
        double dist;

        //Условие для показателя третьего трекбара.
        bool usl1 = false;

        //Метод, при котором становятся определенные элементы для изменения характеристик фрактала.
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            usl1 = false;
            trackBar1.Visible = true;
            trackBar1.Minimum = 1;
            trackBar1.Maximum = 5;
            label2.Visible = true;
            label2.Text = "Выберите число итераций";
            label3.Visible = true;
            label3.Text = trackBar1.Value.ToString();
            trackBar2.Visible = true;
            trackBar2.Minimum = 1;
            trackBar2.Maximum = 10;
            label4.Visible = true;
            label4.Text = "Выберите отнош. нач. отрезка к размеру окна";
            label5.Visible = true;
            label5.Text = $"{trackBar2.Value / 10.0}";
            trackBar3.Visible = false;
            trackBar4.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            trackBar5.Visible = false;
            comboBox2.Visible = true;
            comboBox3.Visible = true;
            label13.Visible = true;
            label14.Visible = true;
            if (comboBox1.Text == "Фрактальное дерево")
            {
                SpecialSettings();
            }
            if (comboBox1.Text == "Множество Кантора")
            {
                Specialsettings1();
            }
            button1.Visible = true;
        }

        // Метод для вызова специфических условий.        
        public void SpecialSettings()
        {
            trackBar1.Maximum = 10;
            trackBar2.Maximum = 4;
            trackBar3.Visible = true;
            trackBar3.Minimum = 3;
            trackBar3.Maximum = 10;
            label6.Visible = true;
            label6.Text = "Выберите величину левого угла";
            label7.Visible = true;
            label7.Text = $"PI/{trackBar3.Value}";
            trackBar4.Visible = true;
            trackBar4.Minimum = 3;
            trackBar4.Maximum = 10;
            label8.Visible = true;
            label8.Text = "Выберите величину правого угла";
            label9.Visible = true;
            label9.Text = $"PI/{trackBar4.Value}";
            trackBar5.Visible = true;
            trackBar5.Minimum = 1;
            trackBar5.Maximum = 7;
            label11.Visible = true;
            label10.Text = "Выберите коэффицент уменьшения";
            label10.Visible = true;
            label11.Text = $"{trackBar5.Value / 10.0}";
        }

        // Метод для вызова специфических условий.
        public void Specialsettings1()
        {
            usl1 = true;
            trackBar3.Visible = true;
            trackBar3.Minimum = 1;
            trackBar3.Maximum = 30;
            label6.Visible = true;
            label6.Text = "Выберите расст. между итерац.";
            label7.Visible = true;
            label7.Text = $"{trackBar3.Value / 10.0}\nразмера \nблока";
        }

        //Метод для сохранения в фрактал текущих значений и отрисовки самого фрактала.
        private void button1_Click(object sender, EventArgs e)
        {
            color1 = ChooseColor(comboBox2);
            color2 = ChooseColor(comboBox3);
            iteraion = trackBar1.Value;
            len = trackBar2.Value / 10.0;
            usl = true;
            button2.Visible = true;
            label12.Visible = true;
            zoom = 1;
            label12.Text = $"x{zoom}";
            s = comboBox1.SelectedItem.ToString();
            switch (s)
            {
                case "Фрактальное дерево":
                    a1 = trackBar3.Value;
                    a2 = trackBar4.Value;
                    koef = trackBar5.Value / 10.0;
                    frac = new Tree(a1, a2, koef, iteraion, len, pictureBox1, color1, color2);
                    break;
                case "Кривая Коха":
                    frac = new Kokh(iteraion, len, pictureBox1, color1, color2);
                    break;
                case "Ковер Серпинского":
                    frac = new SerpinskiySquare(iteraion, len, pictureBox1, color1, color2);
                    break;
                case "Треугольник Серпинского":
                    frac = new SerpinskiyTriangle(iteraion, len, pictureBox1, color1, color2);
                    break;
                case "Множество Кантора":
                    dist = trackBar3.Value / 10.0;
                    frac = new Kantor(iteraion, len, pictureBox1, color1, color2, dist);
                    break;
            }
            frac.DrawFrac();
            button3.Visible = true;
        }

        //Метод для отображения текущего значения ползунка.
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label3.Text = trackBar1.Value.ToString();
        }

        //Метод для отображения текущего значения ползунка.
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            label5.Text = $"{trackBar2.Value / 10.0}";
        }

        //Метод для отображения текущего значения ползунка.
        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            if (usl1)
                label7.Text = $"{trackBar3.Value / 10.0}\nразмера \nблока";
            else
                label7.Text = $"PI/{trackBar3.Value}";
        }

        //Метод для отображения текущего значения ползунка.
        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            label9.Text = $"PI/{trackBar4.Value}";
        }

        //Метод для отображения текущего значения ползунка.
        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            label11.Text = $"{trackBar5.Value / 10.0}";
        }


        //Далее идут методы ДОП. ФУНКЦИОНАЛА.


        //Метод для масштаба.
        private void button2_Click(object sender, EventArgs e)
        {
            switch (zoom)
            {
                case 1:
                    frac.length *= 2;
                    zoom = 2;
                    frac.DrawFrac();
                    break;
                case 2:
                    frac.length *= 1.5;
                    zoom = 3;
                    frac.DrawFrac();
                    break;
                case 3:
                    frac.length *= (5.0 / 3.0);
                    zoom = 5;
                    frac.DrawFrac();
                    break;
                case 5:
                    frac.length /= 5;
                    frac.DrawFrac();
                    zoom = 1;
                    break;
            }
            label12.Text = $"x{zoom}";
        }

        //Метод для выбора цвета.
        public Color ChooseColor(ComboBox cb)
        {
            switch (cb.SelectedItem)
            {
                case "Красный":
                    return Color.Red;
                case "Оранжевый":
                    return Color.Orange;
                case "Желтый":
                    return Color.Yellow;
                case "Зеленый":
                    return Color.Green;
                case "Голубой":
                    return Color.Aqua;
                case "Синий":
                    return Color.Blue;
                case "Фиолетовый":
                    return Color.Purple;
                case "Белый":
                    return Color.White;
                case "Коричневый":
                    return Color.Brown;
                case "Серый":
                    return Color.Gray;
                default:
                    return Color.White;
            }
        }

        //Метод для сохранения текущего изображения.
        private void button3_Click(object sender, EventArgs e)
        {
            Bitmap bm = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.DrawToBitmap(bm, new Rectangle(0, 0, bm.Width, bm.Height));
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PNG-изображение(*.png)|*.png";
            sfd.FileName = "image.png";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                bm.Save(sfd.FileName, ImageFormat.Png);
            }
        }

        //Метод для перерисовки фрактала при изменении размера окна.
        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            if (usl)
                frac.DrawFrac();
        }

        //Метод для перерисовки фрактала при изменении размера окна.
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (usl)
                frac.DrawFrac();
        }

        //Метод для перерисовки фрактала при изменении размера окна.
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (usl)
                frac.DrawFrac();
        }
    }
}