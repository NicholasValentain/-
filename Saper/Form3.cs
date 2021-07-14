using System;
using System.IO;
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
    public partial class Form3 : Form
    {
        public Form1 frm1;
        public Form3()
        {
            this.Text = "Help";
            this.Width = 900;
            this.Height = 400;
            this.BackColor = Color.LightGray;
            this.Icon = new Icon("bb.ico");
            InitializeComponent();
            Init();
        }
        public void Init()
        {
            CreatemyExit();
            Helps();
        }

        private void Help_Load(object sender, EventArgs e)
        {

        }
        private void Exit_Click(object sender, EventArgs e)
        {
            this.Hide();
            frm1.Show();
        }

        public void Helps()
        {
            Label HelpL = new Label();
            HelpL.Size = new Size(850, 270);
            HelpL.Font = new Font("Arial", 15, FontStyle.Regular);
            HelpL.Location = new Point(10, 10);
            this.Controls.Add(HelpL);
            StreamReader read = new StreamReader("help.txt");
            HelpL.Text = read.ReadToEnd();

        }

        public void CreatemyExit()
        {
            Button buttonE = new Button();
            buttonE.Location = new Point(20, 280);
            buttonE.Size = new Size(150, 60);
            buttonE.Font = new Font("Arial", 20, FontStyle.Regular);
            buttonE.Text = "Exit";
            buttonE.Click += Exit_Click;
            buttonE.BackColor = Color.White;
            this.Controls.Add(buttonE);
        }
    }
}
