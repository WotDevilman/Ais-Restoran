using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BD.Forms
{
    public partial class Form_InformEdit : Form
    {
        public Form_InformEdit()
        {
            InitializeComponent();
        }
        public int pravo = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            pravo = 0;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
