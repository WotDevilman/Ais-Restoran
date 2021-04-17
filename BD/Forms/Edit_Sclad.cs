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
    public partial class Edit_Sclad : Form
    {
        public Edit_Sclad()
        {
            InitializeComponent();
            string[] kategor = getProdict().Select(n => n.ToString()).ToArray();
            TProdyct.Items.AddRange(kategor);
            string[] pro = getizmer().Select(n => n.ToString()).ToArray();
            TIzmeren.Items.AddRange(pro);
        }
        database db = new database();
        private List<string> getizmer()
        {
            List<string> opers = new List<string>();
            db.connect.Open();
            OleDbCommand cmd = db.connect.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM ЕдИзмер";
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
        private List<string> getProdict()
        {
            List<string> opers = new List<string>();
            db.connect.Open();
            OleDbCommand cmd = db.connect.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Продукты";
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
        private int getizmer(string nameZakazchik)
        {
            db.connect.Open();
            OleDbCommand cmd = db.connect.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT ID_ед FROM ЕдИзмер where ЕдИзмерения = '" + nameZakazchik + "'";
            cmd.ExecuteNonQuery();
            db.connect.Close();
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            return int.Parse(dt.Rows[0][0].ToString());
        }
        private int getProdict(string nameZakazchik)
        {
            db.connect.Open();
            OleDbCommand cmd = db.connect.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT ID_продукта FROM Продукты where Название = '" + nameZakazchik + "'";
            cmd.ExecuteNonQuery();
            db.connect.Close();
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            return int.Parse(dt.Rows[0][0].ToString());
        }

        public int id { get; set; }
        public string tprodyc { get; set; }
        public string tizmeren { get; set; }
        public string tkol { get; set; }
            
        private void button1_Click(object sender, EventArgs e)
        {
            if (TProdyct.Text.Length != 0 && TKol.Text.Length != 0 && TIzmeren.Text.Length != 0)
            {
                    int idprod = getProdict(TProdyct.SelectedItem.ToString());
                    int idediz = getizmer(TIzmeren.SelectedItem.ToString());

                    string quest = $"UPDATE Склад SET ID_продукта = '{idprod}', ID_ЕдИзмерний = '{idediz}', Количество = '{TKol.Text}'  WHERE ID_записи = {id}";
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
                    form.Show();    
                }
            }
        }

        private void Edit_Sclad_Load(object sender, EventArgs e)
        {
            TProdyct.Text = tprodyc;
            TKol.Text = tkol;
            TIzmeren.Text = tizmeren;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TKol_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            e.Handled = !(char.IsDigit(c) || c == '\b');
            if (e.KeyChar == ' ') e.Handled = true;
        }
    }
}
