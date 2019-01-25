using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace NotVision
{
    public partial class Edit : Form
    {
        public Map map;

        Graphics graph;
        Image image;
        int size = 0;

        public Edit()
        {
            InitializeComponent();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Edit_Load(object sender, EventArgs e)
        {
            image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graph = Graphics.FromImage(image);
            pictureBox1.Image = image;

            EditNewForm form = new EditNewForm();
            form.Owner = this;
            form.ShowDialog();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditNewForm form = new EditNewForm();
            form.Owner = this;
            form.ShowDialog();
        }

        private void Draw()
        {
            if (map.Wid > map.Hei) size = (int)pictureBox1.Width / (map.Wid - 2);
            else size = (int)pictureBox1.Height / (map.Hei - 2);

            graph.Clear(Color.White);

            graph.DrawRectangle(Pens.Black,0,0,(map.Wid-2)*size-1,(map.Hei-2)*size-1);

            for (int i = 1; i < map.Wid - 2; i++)
                graph.DrawLine(Pens.Black, i * size, 0, i * size, (map.Hei - 2) * size);
            for (int i = 1; i < map.Hei - 2; i++)
                graph.DrawLine(Pens.Black, 0, i*size, (map.Wid - 2) * size,i*size);

            for (int i = 1; i < map.Wid - 1; i++)
                for (int j = 1; j < map.Hei - 1; j++)
                {
                    if (map.GetBlock(i,j) == 1) graph.FillRectangle(Brushes.Black,(i-1)*size+1,(j-1)*size+1,size-1,size-1);
                    if (map.GetBlock(i, j) == 2) graph.FillRectangle(Brushes.Yellow, (i - 1) * size + 1, (j - 1) * size + 1, size - 1, size - 1);
                    if (map.GetBlock(i, j) == 3) graph.FillRectangle(Brushes.Green, (i - 1) * size + 1, (j - 1) * size + 1, size - 1, size - 1);
                }  

            pictureBox1.Refresh();
        }

        public void CreateMaps(int w,int h)
        {
            map = new Map(w,h);
            Draw();
        }

        private void DrawBlock(bool left,int X, int Y)
        {
            if (left)
            {
                int x = X / size + 1;
                int y = Y / size + 1;
                if (x >= map.Wid - 1 || y >= map.Hei - 1) return;

                if (radioButton1.Checked)
                {
                    map.SetBlock(x,y,1);
                    graph.FillRectangle(Brushes.Black, (x - 1) * size + 1, (y - 1) * size + 1, size - 1, size - 1);
                }
                if (radioButton2.Checked)
                {
                    map.SetBlock(x, y, 2);
                    graph.FillRectangle(Brushes.Yellow, (x - 1) * size + 1, (y - 1) * size + 1, size - 1, size - 1);
                }
                if (radioButton3.Checked)
                {
                    if(map.xe>-1 && map.ye>-1)
                    {
                        graph.FillRectangle(Brushes.White, (map.xe - 1) * size + 1, (map.ye - 1) * size + 1, size - 1, size - 1);
                    }
                    map.SetBlock(x, y, 3);
                    graph.FillRectangle(Brushes.Green, (x - 1) * size + 1, (y - 1) * size + 1, size - 1, size - 1);
                }
            }
            else
            {
                int x = X / size + 1;
                int y = Y / size + 1;
                if (x >= map.Wid - 1 || y >= map.Hei - 1) return;
                map.SetBlock(x, y, 0);
                graph.FillRectangle(Brushes.White, (x - 1) * size + 1, (y - 1) * size + 1, size - 1, size - 1);
            }
            pictureBox1.Refresh();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) DrawBlock(true, e.X, e.Y);
            if (e.Button == MouseButtons.Right) DrawBlock(false, e.X, e.Y);
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            DrawBlock(e.Button == MouseButtons.Left, e.X, e.Y);
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditSaveForm form = new EditSaveForm();
            form.Owner = this;
            form.ShowDialog();
        }

        public void SaveMaps(string fname)
        {
            BinaryFormatter bin = new BinaryFormatter();
            FileStream file = new FileStream(Environment.CurrentDirectory+@"\Source\maps\"+fname + @".map", FileMode.Create, FileAccess.Write);
            bin.Serialize(file, map);
            file.Close();
        }

        private void открытьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            EditOpenForm form = new EditOpenForm();
            form.Owner = this;
            form.ShowDialog();
        }

        public void OpenMap(string fname)
        {
            BinaryFormatter bin = new BinaryFormatter();
            FileStream file = new FileStream(Environment.CurrentDirectory+@"\Source\maps\"+fname, FileMode.Open, FileAccess.Read);
            map = (Map)bin.Deserialize(file);
            file.Close();
            Draw();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Draw();
        }
    }

}
