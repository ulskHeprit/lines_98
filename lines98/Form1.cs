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
            size1 = 15;
            size2 = 15;
            InitializeComponent();
        }

        PictureBox[,] arr;
        PictureBox temp;
        List<PictureBox> empty;
        Bitmap[,] bmp;
        Graphics gr;
        Random rnd;
        Pen pen;
        int score;
        int[,] secarr;
        int size1,size2;

        Brush[] br =  { new SolidBrush(Color.Red), new SolidBrush(Color.Blue), new SolidBrush(Color.Yellow)
                        , new SolidBrush(Color.Green), new SolidBrush(Color.Violet), new SolidBrush(Color.Orange), new SolidBrush(Color.SkyBlue) };

        private void Form1_Load(object sender, EventArgs e)
        {
            empty = new List<PictureBox>();
            Brush s = Brushes.Red;
            pen = new Pen(s);
            score = 0;
            //MessageBox.Show(pen.Color.ToString());
            

            try
            {
                arr = new PictureBox[size1, size2];
                secarr = new int[size1+2, size2+2];
                int size = panel1.Height < panel1.Width ? panel1.Height : panel1.Width;
                for (int i = 0; i < size1; i++)
                {
                    for (int j = 0; j < size2; j++)
                    {
                        arr[i, j] = new PictureBox();
                        arr[i, j].Name = "picturebox" + i.ToString() + j.ToString();
                        arr[i, j].Height = size / size1;
                        arr[i, j].Width = size / size2;
                        arr[i, j].BackColor = Color.LightGray;

                        arr[i, j].BorderStyle = BorderStyle.FixedSingle;

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
                    arr[a, b].Tag = false;
                }
                temp = arr[0, 0];
                panel1.SendToBack();
                textBox1.BringToFront();
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
                for (int i = 0; i < size1; i++)
                {
                    for (int j = 0; j < size2; j++)
                    {
                        arr[i, j].Height = size / size1;
                        arr[i, j].Width = size / size2;
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
                        //click big box
                        temp.BackColor = Color.LightGray;
                        (sender as PictureBox).BackColor = Color.Gray;
                        temp = (sender as PictureBox);
                    }
                    else
                    {
                        if (temp.BackColor == Color.Gray)
                        {
                            //if have selected box(big) and click to !bigbox
                            
                            if (checkpath(temp, sender as PictureBox))
                            {
                                (sender as PictureBox).Image = temp.Image;
                                (sender as PictureBox).Image.Tag = temp.Image.Tag;
                                (sender as PictureBox).Tag = true;
                                temp.Image.Tag = null;
                                temp.Image = null;
                                temp.BackColor = Color.LightGray;
                                temp.Tag = false;
                                next();
                            }
                        }
                    }
                }
            }
            //catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void next()
        {
            empty.Clear();
            foreach(PictureBox pb in arr)
            {
                if(Convert.ToBoolean(pb.Tag) == false && Convert.ToString(pb.Image) != "")
                {
                    for(int i=0;i<7;i++)
                    {
                        if ((pb.Image as Bitmap) == bmp[1,i])
                        {
                            pb.Image = bmp[0, i];
                            pb.Tag = true;

                            break;
                        }
                    }
                }
                else
                {
                    if(Convert.ToString(pb.Image) == "")
                    {
                        empty.Add(pb);
                    }
                }
            }
            int a,b;
            for(int i = 0; i < (( empty.Count / 2 ) > 3 ? 3 : (empty.Count / 2)+1 ); i++ )
            {
                if (empty.Count > 1)
                {
                    a = rnd.Next(empty.Count);
                    b = rnd.Next(7);
                    empty[a].Image = bmp[1, b];
                    empty[a].Image.Tag = ((SolidBrush)br[b]).Color;
                    empty[a].Tag = false;
                }
                else
                {
                    a = rnd.Next(empty.Count);
                    b = rnd.Next(7);
                    empty[a].Image = bmp[0, b];
                    empty[a].Image.Tag = ((SolidBrush)br[b]).Color;
                    empty[a].Tag = true;
                    MessageBox.Show("Lose");
                }
            }
            Text = ((empty.Count/2)+1).ToString();
        }

        private void test()
        {/*
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    secarr[i + 1, j + 1] = Convert.ToBoolean(arr[i, j].Tag) ? -1 : 0;
                }
            }*/
            textBox1.Text = "";
            for (int i = 0; i < size1+2; i++)
            {
                for (int j = 0; j < size2+2; j++)
                {
                    if (i == 0) { secarr[i, j] = -1; }
                    if (i == 11) { secarr[i, j] = -1; }
                    if (j == 0) { secarr[i, j] = -1; }
                    if (j == 11) { secarr[i, j] = -1; }

                    textBox1.Text += secarr[i , j] + "  ";
                }
                textBox1.Text += Environment.NewLine;
            }
        }

        private bool checkpath(PictureBox a, PictureBox b)
        {
            int x1 = 0;
            int y1 = 0;
            int x2 = 0;
            int y2 = 0;
            //get indexes of start and end of path
            //с таким массивом удобнее
            for (int i = 0; i < size1; i++)
            {
                for (int j = 0; j < size2; j++)
                {
                    if (a.Name == arr[i, j].Name)
                    {
                        x1 = i + 1;
                        y1 = j + 1;
                    }
                    else
                    {
                        if (b.Name == arr[i, j].Name)
                        {
                            x2 = i + 1;
                            y2 = j + 1;
                        }
                    }
                    secarr[i + 1, j + 1] = Convert.ToBoolean(arr[i, j].Tag) ? -1 : 0;
                }
            }
            for (int i = 0; i < size1+2; i++)
            {
                for (int j = 0; j < size2+2; j++)
                {
                    if (i == 0) { secarr[i, j] = -1; }
                    if (i == size1+1) { secarr[i, j] = -1; }
                    if (j == 0) { secarr[i, j] = -1; }
                    if (j == size2+1) { secarr[i, j] = -1; }
                }
            }

            secarr[x1, y1] = 1;
            int d = 1;
            secarr[x2, y2] = 999;
            if (secarr[x2+1,y2]+ secarr[x2-1, y2] + secarr[x2, y2+1] + secarr[x2, y2-1] == -4) return false;
            //textBox1.Text = "";
            for(int k = 0;k<Math.Pow( Math.Max(size1,size2),2);k++)
            {

                for (int i = 1; i < size1+1; i++)
                {
                    for (int j = 1; j < size2+1; j++)
                    {
                        if(secarr[i,j] == d)
                        {
                                secarr[i + 1, j] = secarr[i + 1, j] == 0 ? d + 1 : secarr[i + 1, j]; 
                                secarr[i - 1, j] = secarr[i - 1, j] == 0 ? d + 1 : secarr[i - 1, j]; 
                                secarr[i, j + 1] = secarr[i, j + 1] == 0 ? d + 1 : secarr[i, j + 1]; 
                                secarr[i, j - 1] = secarr[i, j - 1] == 0 ? d + 1 : secarr[i, j - 1]; 
                        }
                        //textBox1.Text += secarr[j, i].ToString() + " ";
                    }
                    //textBox1.Text += Environment.NewLine;
                }
                if (secarr[x2 + 1, y2] > 0) return true; 
                if (secarr[x2 - 1, y2] > 0) return true; 
                if (secarr[x2, y2 + 1] > 0) return true; 
                if (secarr[x2, y2 - 1] > 0) return true; 
                d++;
            }
            return false;
        }

        private void checkballs()
        {

        }

        private void test(object sender, EventArgs e)
        {
            test();
        }
    }
}
