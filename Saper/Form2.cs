using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saper
{
    public partial class Form2 : Form
    {
        public Form1 frm1;
        public int bSize = 30;
        public static int mapSize = 30;
        public static int mapSize2 = 60;
        public static int[,] myMap = new int[30, 60];
        public static Button[,] myButton = new Button[30, 60];
        bool ferst = true;
        public static Point first;
        // 375 бомб
        public Form2()
        {
            InitializeComponent();
            this.Text = "Saper";
            this.Width = 1920;
            this.Height =1080;
            this.AutoScroll = true;
            this.BackColor = Color.LightGray;
            this.Icon = new Icon("bb.ico");
            this.WindowState = FormWindowState.Maximized;
            Init();
        }
        public void Init()
        {
            CreateMap();
        }
        private void ExetClick(object sender, EventArgs e)
        {
            this.Hide();
            frm1.Show();
        }
        public void CreateMap()
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize2; j++)
                {
                    myMap[i, j] = 0;
                    Button button = new Button();
                    button.Location = new Point(j * bSize,i * bSize);
                    button.Size = new Size(bSize, bSize);
                    //button.Click += bt_Click;
                    button.MouseUp += m_Click;
                    button.BackColor = Color.DarkGray;
                    this.Controls.Add(button);
                    myButton[i, j] = button;
                }
            }

            Button Exet = new Button();
            Exet.Location = new Point(50, 980);
            Exet.Text = "Exit";
            Exet.BackColor = Color.White;
            Exet.Click += ExetClick;
            this.Controls.Add(Exet);
        }
        public void CreateBoomb()
        {
            Random rnd = new Random();
            int boomb = 375;
            while(boomb > 0)
            {
                int x = rnd.Next(0, 30);
                int y = rnd.Next(0, 60);
                while (myMap[x,y] == -1 || (Math.Abs(x - first.Y) <= 1 && Math.Abs(y - first.X) <= 1))
                {
                    x = rnd.Next(0, 30);
                    y = rnd.Next(0, 60);
                }
                myMap[x, y] = -1;
                //myButton[x, y].BackColor = Color.Black;
                boomb--;
            }
        }
        public void CreateNums()
        {
            for (int k = 0; k < mapSize; k++)
            {
                for (int y = 0; y < mapSize2; y++)
                {
                    if (myMap[k, y] != -1)
                    {
                        if (k - 1 >= 0 && y - 1 >= 0 && k - 1 <= 29 && y - 1 <= 59)
                        {
                            if (myMap[k - 1, y - 1] == -1)
                            {
                                myMap[k, y] = myMap[k, y] + 1;
                            }
                        }
                        if (k >= 0 && y - 1 >= 0 && k <= 29 && y - 1 <= 59)
                        {
                            if (myMap[k, y - 1] == -1)
                            {
                                myMap[k, y] = myMap[k, y] + 1;
                            }
                        }
                        if (k + 1 >= 0 && y - 1 >= 0 && k + 1 <= 29 && y - 1 <= 59)
                        {
                            if (myMap[k + 1, y - 1] == -1)
                            {
                                myMap[k, y] = myMap[k, y] + 1;
                            }
                        }
                        if (k - 1 >= 0 && y >= 0 && k - 1 <= 29 && y <= 59)
                        {
                            if (myMap[k - 1, y] == -1)
                            {
                                myMap[k, y] = myMap[k, y] + 1;
                            }
                        }
                        if (k + 1 >= 0 && y >= 0 && k + 1 <= 29 && y <= 59)
                        {
                            if (myMap[k + 1, y] == -1)
                            {
                                myMap[k, y] = myMap[k, y] + 1;
                            }
                        }
                        if (k - 1 >= 0 && y + 1 >= 0 && k - 1 <= 29 && y + 1 <= 59)
                        {
                            if (myMap[k - 1, y + 1] == -1)
                            {
                                myMap[k, y] = myMap[k, y] + 1;
                            }
                        }
                        if (k >= 0 && y + 1 >= 0 && k <= 29 && y + 1 <= 59)
                        {
                            if (myMap[k, y + 1] == -1)
                            {
                                myMap[k, y] = myMap[k, y] + 1;
                            }
                        }
                        if (k + 1 >= 0 && y + 1 >= 0 && k + 1 <= 29 && y + 1 <= 59)
                        {
                            if (myMap[k + 1, y + 1] == -1)
                            {
                                myMap[k, y] = myMap[k, y] + 1;
                            }
                        }
                        //myButton[k, y].Text = $"{myMap[k,y]}";
                    }
                }
            }
        }
        public void m_Click(object sender, MouseEventArgs e)
        {
            Button Tb = sender as Button;
            switch (e.Button.ToString())
            {
                case "Right":
                    btl_Click(Tb);
                    break;
                case "Left":
                    bt_Click(Tb);
                    break;
            }
        }
        public void btl_Click(Button Tb)
        {
            Tb.BackColor = Color.Red;
        }
        public void bt_Click(Button Tb)
        {
            Tb.BackColor = Color.Blue;

            int xx = Tb.Location.Y / bSize;
            int yy = Tb.Location.X / bSize;

            if (ferst)
            {
                first = new Point(yy ,xx);
                ferst = false;
                CreateBoomb();
                CreateNums();
                Tb.BackColor = Color.Blue;
            }

            OpenCells(xx, yy);

            if (myMap[xx, yy] == -1)
            {
                MessageBox.Show("Поражение!!!");
            }
        }
        
        private static void OpenCell(int x, int y)
        {
            myButton[x, y].Enabled = false;
            myButton[x, y].Text = $"{myMap[x, y]}";
            myButton[x, y].BackColor = Color.White;
        }
        private static void OpenCells(int i, int j)
        {
            OpenCell(i, j);

            if (myMap[i, j] > 0)
                return;

            for (int k = i - 1; k < i + 2; k++)
            {
                for (int l = j - 1; l < j + 2; l++)
                {
                    if (!VneMap(k, l))
                        continue;
                    if (!myButton[k, l].Enabled)
                        continue;
                    if (myMap[k, l] == 0)
                        OpenCells(k, l);
                    else if (myMap[k, l] > 0)
                        OpenCell(k, l);
                }
            }
        }
        private static bool VneMap(int x, int y)
        {
            if(x<0 || y<0 || x> mapSize || y> mapSize2)
            {
                return false;
            }
            return true;
        }
    }
}
