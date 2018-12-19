using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lines98
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        PictureBox[,] arr;
        PictureBox temp;
        Bitmap[,] bmp;
        Graphics gr;
        Random rnd;
        Pen pen;

        private void Form1_Load(object sender, EventArgs e)
        {

            Brush s = Brushes.Red;
            pen = new Pen(s);
            //MessageBox.Show(pen.Color.ToString());
            

            try
            {
                arr = new PictureBox[10, 10];
                int size = panel1.Height < panel1.Width ? panel1.Height : panel1.Width;
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        arr[i, j] = new PictureBox();
                        arr[i, j].Name = "picturebox" + i.ToString() + j.ToString();
                        arr[i, j].Height = size / 10;
                        arr[i, j].Width = size / 10;
                        arr[i, j].BackColor = Color.LightGray;
                        //arr[i, j].BorderStyle = BorderStyle.FixedSingle;
                        arr[i, j].Left = (panel1.Width - size) / 2 + arr[i, j].Width * j;
                        arr[i, j].Top = (panel1.Height - size) / 2 + arr[i, j].Height * i;
                        arr[i, j].MouseClick += new MouseEventHandler(mouseclick);
                        arr[i, j].SizeMode = PictureBoxSizeMode.Zoom;
                        Controls.Add(arr[i, j]);
                        //arr[i,j] = Controls.OfType<PictureBox>().Where(x => x.Name == "picturebox" + i.ToString() + j.ToString()).FirstOrDefault();
                        arr[i, j] = Controls[Controls.Count - 1] as PictureBox;
                    }
                }
                bmp = new Bitmap[2, 7];
                Brush[] br =  { new SolidBrush(Color.Red), new SolidBrush(Color.Blue), new SolidBrush(Color.Yellow)
                        , new SolidBrush(Color.Green), new SolidBrush(Color.Violet), new SolidBrush(Color.Orange), new SolidBrush(Color.SkyBlue) };
                rnd = new Random();
                for (int i = 0; i < 7; i++)
                {
                    bmp[0, i] = new Bitmap(100, 100);
                    gr = Graphics.FromImage(bmp[0, i]);
                    gr.FillEllipse(br[i], 10, 10, 80, 80);
                    gr.DrawEllipse(Pens.Black, 10, 10, 80, 80);
                    bmp[1, i] = new Bitmap(100, 100);
                    gr = Graphics.FromImage(bmp[1, i]);
                    gr.FillEllipse(br[i], 30, 30, 40, 40);
                    gr.DrawEllipse(Pens.Black, 30, 30, 40, 40);
                    //big
                    int a = rnd.Next(9);
                    int b = rnd.Next(9);
                    arr[a,b].Image = bmp[0, i];
                    arr[a, b].Image.Tag = ((SolidBrush)br[i]).Color;
                    arr[a, b].Tag = true;
                    //small
                    a = rnd.Next(9);
                    b = rnd.Next(9);
                    arr[a, b].Image = bmp[1, i];
                    arr[a, b].Image.Tag = ((SolidBrush)br[i]).Color;
                }
                temp = arr[0, 0];
                panel1.SendToBack();
            }
            catch(Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            resize();
        }

        private void resize()
        {
            try
            {
                int size = panel1.Height < panel1.Width ? panel1.Height : panel1.Width;
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        arr[i, j].Height = size / 10;
                        arr[i, j].Width = size / 10;
                        arr[i, j].Left = (panel1.Width - size) / 2 + arr[i, j].Width * j;
                        arr[i, j].Top = (panel1.Height - size) / 2 + arr[i, j].Height * i;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }
        
        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            //resize();
        }

        private void mouseclick(object sender, MouseEventArgs e)
        {
            //try
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (Convert.ToBoolean((sender as PictureBox).Tag) == true)
                    {
                        temp.BackColor = Color.LightGray;
                        (sender as PictureBox).BackColor = Color.Gray;
                        temp = (sender as PictureBox);
                    }
                    else
                    {
                        if (temp.BackColor == Color.Gray)
                        {
                            if((sender as PictureBox).Image.Tag.ToString()  == "Color [Blue]") { MessageBox.Show("true"); }
                            MessageBox.Show((sender as PictureBox).Image.Tag.ToString());
                            (sender as PictureBox).Image = temp.Image;
                            (sender as PictureBox).Tag = true;
                            temp.Image = null;
                            temp.BackColor = Color.LightGray;
                            temp.Tag = null;
                        }
                    }
                }
            }
            //catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }
    }
}
