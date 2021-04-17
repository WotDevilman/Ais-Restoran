using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BD.UsersControl;

namespace BD
{
    public partial class Main : Form
    {
        private void addControll(UserControl uc)
        {
            panelUC.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panelUC.Controls.Add(uc);
            uc.BringToFront();
        }
        public Main()
        {
            InitializeComponent();
        }

        private void Loading_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            UC_Grafic uC_Grafic = new UC_Grafic();
            addControll(uC_Grafic);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            UC_Blydo uC_Blydo = new UC_Blydo();
            addControll(uC_Blydo);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UC_Sclad uC_Sclad = new UC_Sclad();
            addControll(uC_Sclad);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UC_Menu uC_Menu = new UC_Menu();
            addControll(uC_Menu);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            UC_Grafic uC_Grafic = new UC_Grafic();
            addControll(uC_Grafic);
        }
    }
}
