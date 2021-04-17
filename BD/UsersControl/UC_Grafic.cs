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

namespace BD.UsersControl
{
    public partial class UC_Grafic : UserControl
    {
        public UC_Grafic()
        {
            InitializeComponent();
        }
        List<string> name = new List<string>();
        List<string> colvo = new List<string>();
        database db = new database();
        private void getGrafic()
        {
            string[] data = DateTime.Now.ToString("dd/MM/yyyy").Split('.');
            db.connect.Open();
            OleDbCommand cmd = db.connect.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"SELECT Блюда.Название, Sum(Меню.Количество) AS [Sum-Количество] FROM Блюда INNER JOIN Меню ON Блюда.ID_блюда = Меню.ID_блюд GROUP BY Блюда.Название, Меню.Дата HAVING Меню.Дата = #{data[0] + "/"+ data[1] + "/" + data[2]}#";
            cmd.ExecuteNonQuery();
            db.connect.Close();
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow item in dt.Rows)
            {
                name.Add(item[0].ToString());
                colvo.Add(item[1].ToString());
            }
        }
        private void UC_Grafic_Load(object sender, EventArgs e)
        {
            getGrafic();
            chart1.Series[0]["PieLabelStyle"] = "Disabled";
            for (int i = 0; i < name.Count; i++)
            {
                chart1.Series[0].Points.AddXY(name[i], colvo[i]);
            }
        }
    }
}
