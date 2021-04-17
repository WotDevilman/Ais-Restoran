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
    public partial class Add_Menu : Form
    {
        public Add_Menu()
        {
            InitializeComponent();
        }
        database db = new database();
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public int idprodict { get; set; }
        List<string> kolvo = new List<string>();
        List<string> name = new List<string>();
        private void getProdict(int id)
        {
            db.connect.Open();
            OleDbCommand cmd = db.connect.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT Продукты.Название, СоставБлюд.Количество FROM Блюда INNER JOIN(Продукты INNER JOIN СоставБлюд ON Продукты.ID_продукта = СоставБлюд.ID_продукт) ON Блюда.ID_блюда = СоставБлюд.ID_блюд WHERE Блюда.ID_блюда = "+id+"";
            cmd.ExecuteNonQuery();
            db.connect.Close();
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow item in dt.Rows)
            {
                name.Add(item[0].ToString());
                kolvo.Add(item[1].ToString());
            }
        }
        int blydocount;
        int idmenu;
        int summenu;
        private void getBlydocount(int id)
        {
            db.connect.Open();
            OleDbCommand cmd = db.connect.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"SELECT Меню.ID_запМеню,Блюда.ID_блюда, Меню.Дата, Sum(Меню.Количество) AS [Sum-Количество] FROM Блюда INNER JOIN Меню ON Блюда.ID_блюда = Меню.ID_блюд GROUP BY Меню.ID_запМеню,Блюда.ID_блюда, Меню.Дата HAVING Блюда.ID_блюда = {id} AND Меню.Дата = #{DateTime.Now.ToString().Split(' ')[0].Split('.')[0] + " / " + DateTime.Now.ToString().Split(' ')[0].Split('.')[1] + " / " + DateTime.Now.ToString().Split(' ')[0].Split('.')[2]}#";
            cmd.ExecuteNonQuery();
            db.connect.Close();
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow item in dt.Rows)
            {
                blydocount = Convert.ToInt32(item[1]);
                idmenu = Convert.ToInt32(item[0]);
                summenu = Convert.ToInt32(item[3]);
            }
        }
        private void getSclad(EventArgs e)
        {
            List<int> c = new List<int>();
            int cena = 0;
            List<int> idcklad = new List<int>();
            List<int> idprody = new List<int>();
            for (int i = 0; i < name.Count; i++)
            {
                db.connect.Open();
                OleDbCommand cmd = db.connect.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Продукты.Название, Склад.Количество, Склад.ID_записи, Продукты.ID_продукта FROM Продукты INNER JOIN Склад ON Продукты.ID_продукта = Склад.ID_продукта WHERE Продукты.Название = '" + name[i]+"'";
                cmd.ExecuteNonQuery();
                db.connect.Close();
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    cena = Convert.ToInt32(item[1].ToString());
                    idcklad.Add(Convert.ToInt32(item[2].ToString()));
                    idprody.Add(Convert.ToInt32(item[3].ToString()));
                }
                if (Convert.ToInt32(kolvo[i])*Convert.ToInt32(TKol.Text) <= cena)
                {
                    c.Add(cena - (Convert.ToInt32(kolvo[i]) * Convert.ToInt32(TKol.Text)));
                }
                
            }
            if (c.Count == name.Count)
            {
                if (blydocount != idprodict)
                {
                    for (int j = 0; j < c.Count; j++)
                    {
                        string quest = $"UPDATE Склад SET ID_продукта = '{idprody[j]}', Количество = '{c[j]}'  WHERE ID_записи = {idcklad[j]}";
                        db.connect.Open();
                        OleDbCommand dataAdapter = new OleDbCommand(quest, db.connect);
                        dataAdapter.ExecuteNonQuery();
                        db.connect.Close();
                    }
                    getAdd();
                }
                else
                {
                    for (int j = 0; j < c.Count; j++)
                    {
                        string quest = $"UPDATE Склад SET ID_продукта = '{idprody[j]}', Количество = '{c[j]}'  WHERE ID_записи = {idcklad[j]}";
                        db.connect.Open();
                        OleDbCommand dataAdapter = new OleDbCommand(quest, db.connect);
                        dataAdapter.ExecuteNonQuery();
                        db.connect.Close();
                    }
                    getEdit();
                }

            }
            else
            {
                using (Form_InformProdyct form = new Form_InformProdyct())
                {
                    form.ShowDialog();
                    this.OnLoad(e);
                }
            }
        }
        public void getAdd()
        {
            string quest1 = "INSERT INTO Меню (ID_блюд, Дата, Количество) VALUES('" + idprodict + "','" + DateTime.Now.ToString().Split(' ')[0] + "','" + TKol.Text + "')";
            db.connect.Open();
            OleDbCommand dataAdapter = new OleDbCommand(quest1, db.connect);
            dataAdapter.ExecuteNonQuery();
            db.connect.Close();

            Form_Add form = new Form_Add();
            form.ShowDialog();
        }
        public void getEdit()
        {
            string quest1 = $"UPDATE Меню SET ID_блюд = '{idprodict}', Количество = '{summenu + Convert.ToInt32(TKol.Text)}' where ID_запМеню = {idmenu}";
            db.connect.Open();
            OleDbCommand dataAdapter = new OleDbCommand(quest1, db.connect);
            dataAdapter.ExecuteNonQuery();
            db.connect.Close();

            Form_Add form = new Form_Add();
            form.ShowDialog();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (TKol.Text.Length != 0)
            {
                    getProdict(idprodict);
                    getBlydocount(idprodict);
                    getSclad(e);
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

        private void Add_Menu_Load(object sender, EventArgs e)
        {

        }

        private void TKol_TextChanged(object sender, EventArgs e)
        {

        }

        private void TKol_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            e.Handled = !(char.IsDigit(c) || c == '\b');
            if (e.KeyChar == ' ') e.Handled = true;
        }
    }
}
