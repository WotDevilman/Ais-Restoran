using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BD.UsersControl;

namespace BD.Forms
{
    public partial class Add_Blydo : Form
    {
        database db = new database();
        public Add_Blydo()
        {
            InitializeComponent();
            string[] kategor = getKategor().Select(n => n.ToString()).ToArray();
            TKategor.Items.AddRange(kategor);
        }
        private List<string> getKategor()
        {
            List<string> opers = new List<string>();
            db.connect.Open();
            OleDbCommand cmd = db.connect.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Категория";
            cmd.ExecuteNonQuery();
            db.connect.Close();
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow item in dt.Rows)
            {
                opers.Add(item[1].ToString());
            }
            return opers;
        }
        private int getKategor(string nameZakazchik)
        {
            db.connect.Open();
            OleDbCommand cmd = db.connect.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT ID_категории FROM Категория where Категория = '" + nameZakazchik + "'";
            cmd.ExecuteNonQuery();
            db.connect.Close();
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            return int.Parse(dt.Rows[0][0].ToString());
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (TName.Text.Length !=0 && TKategor.Text.Length != 0 && TGod.Text.Length != 0 && TCena.Text.Length != 0 && TVes.Text.Length != 0) {
                Form_InformAdd form = new Form_InformAdd();
                form.ShowDialog();
                this.OnLoad(e);

                int prava = form.pravo;
                if (prava == 1)
                {
                    int idkategor = getKategor(TKategor.SelectedItem.ToString());

                    string quest = "INSERT INTO Блюда (Название, Вес_порции, Цена, СрокГодности, ID_категор) VALUES('" + TName.Text + "','" + TVes.Text + "','" + TCena.Text + "','" + TGod.Text + "','" + idkategor + "')";
                    db.connect.Open();
                    OleDbCommand dataAdapter = new OleDbCommand(quest, db.connect);
                    dataAdapter.ExecuteNonQuery();
                    db.connect.Close();

                    Form_Add formadd = new Form_Add();
                    formadd.ShowDialog();
                } 
            }
            else
            {
                using(Form_Add_PoleNo form = new Form_Add_PoleNo())
                {
                    form.ShowDialog();
                    this.OnLoad(e);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char)e.KeyChar == (Char)Keys.Back) return;
            if (char.IsLetter(e.KeyChar)) return;
            e.Handled = true;
            if (e.KeyChar == ' ') e.Handled = true;
        }

        private void TCena_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            e.Handled = !(char.IsDigit(c) || c == '\b');
            if (e.KeyChar == ' ') e.Handled = true;
        }
    }
}
