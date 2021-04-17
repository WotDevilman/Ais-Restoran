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

namespace BD.Forms
{
    public partial class Edit_Menu : Form
    {
        public Edit_Menu()
        {
            InitializeComponent();
            string[] kategor = getProdict().Select(n => n.ToString()).ToArray();
            TProdyct.Items.AddRange(kategor);
        }
        database db = new database();
        private List<string> getProdict()
        {
            List<string> opers = new List<string>();
            db.connect.Open();
            OleDbCommand cmd = db.connect.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Блюда";
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
        private int getProdict(string nameZakazchik)
        {
            db.connect.Open();
            OleDbCommand cmd = db.connect.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT ID_блюда FROM Блюда where Название = '" + nameZakazchik + "'";
            cmd.ExecuteNonQuery();
            db.connect.Close();
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            return int.Parse(dt.Rows[0][0].ToString());
        }
        public int id { get; set; }
        public string blydo { get; set; }
        public string data { get; set; }
        public string kolvo { get; set; }
        private void button1_Click(object sender, EventArgs e)
        {
            if (TKol.Text.Length != 0 && TProdyct.Text.Length != 0)
            {
                    int idprodict = getProdict(TProdyct.SelectedItem.ToString());

                    string quest = $"UPDATE Меню SET ID_блюд = '{idprodict}', Дата = '{dateTimePicker1.Value.ToShortDateString()}', Количество = '{TKol.Text}'  WHERE ID_запМеню = {id}";
                    db.connect.Open();
                    OleDbCommand dataAdapter = new OleDbCommand(quest, db.connect);
                    dataAdapter.ExecuteNonQuery();
                    db.connect.Close();

                Form_Add w = new Form_Add();
                w.Show();
            }
            else
            {
                using (Form_Add_PoleNo form = new Form_Add_PoleNo())
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

        private void Edit_Menu_Load(object sender, EventArgs e)
        {
            dateTimePicker1.MaxDate = DateTime.Now;

            TProdyct.Text = blydo;
            dateTimePicker1.Value = Convert.ToDateTime(data);
            TKol.Text = kolvo;
        }

        private void TKol_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            e.Handled = !(char.IsDigit(c) || c == '\b');
            if (e.KeyChar == ' ') e.Handled = true;
        }
    }
}
