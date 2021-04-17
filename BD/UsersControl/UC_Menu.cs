using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Word = Microsoft.Office.Interop.Word;

using BD.Forms;

namespace BD.UsersControl
{
    public partial class UC_Menu : UserControl
    {
        public UC_Menu()
        {
            InitializeComponent();
        }
        database db = new database();
        private void UC_Menu_Load(object sender, EventArgs e)
        {
            TBlydo.Items.Clear();
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(db.selectMenu, db.connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            DGVIEWBlydo.DataSource = ds.Tables[0].DefaultView;

            string[] kategor = getProdict().Select(n => n.ToString()).ToArray();
            TBlydo.Items.AddRange(kategor);

            DGVIEWBlydo.Columns[0].Visible = false;

            if (DGVIEWBlydo.RowCount > 0)
            {
                EditItems.Clear();
                for (int i = 0; i < DGVIEWBlydo.ColumnCount; i++)
                {
                    EditItems.Add(DGVIEWBlydo.Rows[0].Cells[i].Value.ToString());
                }
            }
        }
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
        private void button2_Click(object sender, EventArgs e)
        {
            using(Add_Menu add = new Add_Menu())
            {
                add.ShowDialog();
                this.OnLoad(e);
            }
        }
        public List<string> EditItems = new List<string>();
        private void button5_Click(object sender, EventArgs e)
        {
                using (Edit_Menu edit_Menu = new Edit_Menu())
                {
                    if (EditItems.Count > 0)
                    {
                        edit_Menu.id = Convert.ToInt32(EditItems[0]);
                        edit_Menu.blydo = EditItems[1];
                        edit_Menu.data = EditItems[2];
                        edit_Menu.kolvo = EditItems[3];
                        edit_Menu.ShowDialog();
                        this.OnLoad(e);
                    }
            }
        }
        int idblydo;
        private void DGVIEWBlydo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGVIEWBlydo.RowCount != 0 && e.RowIndex != -1)
            {
                //TRecept.Enabled = false;
                idblydo = Convert.ToInt32(DGVIEWBlydo.Rows[e.RowIndex].Cells[0].Value);

                int mSelectedRowIndex = DGVIEWBlydo.SelectedCells[0].RowIndex;
                DataGridViewRow mSelectedRow = DGVIEWBlydo.Rows[mSelectedRowIndex];

                EditItems.Clear();

                for (int i = 0; i < DGVIEWBlydo.ColumnCount; i++)
                {
                    EditItems.Add(Convert.ToString(mSelectedRow.Cells[i].Value));
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (DGVIEWBlydo.RowCount != 0)
            {
                int iddel = Convert.ToInt32(DGVIEWBlydo[0, DGVIEWBlydo.CurrentCell.RowIndex].Value);
                Form_InformDelete form = new Form_InformDelete();
                form.ShowDialog();
                this.OnLoad(e);

                int prava = form.pravo;
                if (prava == 1)
                {
                    
                    string quest = $"DELETE  FROM Меню WHERE ID_запМеню = {iddel}";
                    db.connect.Open();
                    OleDbCommand dataCommander = new OleDbCommand(quest, db.connect);
                    dataCommander.ExecuteNonQuery();

                    OleDbDataAdapter dataAdapter = new OleDbDataAdapter(db.selectMenu, db.connect);
                    DataSet ds = new DataSet();
                    dataAdapter.Fill(ds);
                    db.connect.Close();

                    DGVIEWBlydo.DataSource = ds.Tables[0].DefaultView;

                    DGVIEWBlydo.Columns[0].Visible = false;
                }
            }
        }
        int idbl;

        private void button1_Click(object sender, EventArgs e)
        {
            using (Add_Menu add = new Add_Menu())
            {
                add.idprodict = idbl;
                add.ShowDialog();
                this.OnLoad(e);
            }
        }
        
        private void TBlydo_SelectedIndexChanged(object sender, EventArgs e)
        {
            idbl = getProdict(TBlydo.SelectedItem.ToString());
        }
        private void ReplateWordDocument(string stupToReplate, string text, Word.Document worddoc)
        {
            var range = worddoc.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: stupToReplate, ReplaceWith: text);
        }
        private readonly string TemplateFileName = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Documents\\potreblenie_produktov.docx");
        List<string> sclad = new List<string>();
        List<string> gram = new List<string>();
        List<string> izmeren = new List<string>();
        private void getblsos()
        {
            sclad.Clear();
            gram.Clear();
            izmeren.Clear();
            db.connect.Open();
            OleDbCommand cmd = db.connect.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"SELECT Продукты.Название, Sum(СоставБлюд.Количество) AS [Sum-Количество], ЕдИзмер.ЕдИзмерения, Sum(Меню.Количество) AS [Sum-Количество1] FROM ЕдИзмер INNER JOIN ((Блюда INNER JOIN Меню ON Блюда.ID_блюда = Меню.ID_блюд) INNER JOIN (Продукты INNER JOIN СоставБлюд ON Продукты.ID_продукта = СоставБлюд.ID_продукт) ON Блюда.ID_блюда = СоставБлюд.ID_блюд) ON ЕдИзмер.ID_ед = СоставБлюд.Eд GROUP BY Меню.Дата, Продукты.Название, ЕдИзмер.ЕдИзмерения HAVING Меню.Дата = #{DateTime.Now.ToString().Split(' ')[0].Split('.')[0] + "/" + DateTime.Now.ToString().Split(' ')[0].Split('.')[1] + "/" + DateTime.Now.ToString().Split(' ')[0].Split('.')[2]}#";
            cmd.ExecuteNonQuery();
            db.connect.Close();
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow item in dt.Rows)
            {
                sclad.Add(item[0].ToString());
                gram.Add((Convert.ToInt32(item[1]) * Convert.ToInt32(item[3])).ToString());
                izmeren.Add(item[2].ToString());
            }
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            getblsos();
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Document (*.docx) | *.docx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                var newpathdoc = sfd.FileName;

                //TODO
                var wordAPP = new Word.Application();
                wordAPP.Visible = false;

                var worddocument = wordAPP.Documents.Open(TemplateFileName);

                ReplateWordDocument("{datatime}", DateTime.Now.ToString(), worddocument);

                //Таблица 1
                var n = 0;
                Word.Table tab = worddocument.Tables[1];
                for (int i = 2; i <= sclad.Count + 1; i++)
                {
                    tab.Rows.Add(Missing.Value);
                    tab.Cell(i, 1).Range.Text = sclad[n];
                    tab.Cell(i, 2).Range.Text = gram[n];
                    tab.Cell(i, 3).Range.Text = izmeren[n];
                    n++;
                }

                worddocument.SaveAs(newpathdoc);
                wordAPP.Visible = true;
            }
            MessageBox.Show("Сохраненно");
        }
        private readonly string TemplateFileName1 = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Documents\\menyu.docx");
        List<string> cena = new List<string>();
        private void getMenu()
        {
            sclad.Clear();
            gram.Clear();
            izmeren.Clear();
            cena.Clear();
            db.connect.Open();
            OleDbCommand cmd = db.connect.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"SELECT Категория.Категория, Блюда.Название, Блюда.Вес_порции, Блюда.Цена FROM Категория INNER JOIN Блюда ON Категория.ID_категории = Блюда.ID_категор GROUP BY Категория.Категория, Блюда.Название, Блюда.Вес_порции, Блюда.Цена;";
            cmd.ExecuteNonQuery();
            db.connect.Close();
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow item in dt.Rows)
            {
                sclad.Add(item[0].ToString());
                gram.Add(item[1].ToString());
                izmeren.Add(item[2].ToString());
                cena.Add(item[3].ToString());
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            getMenu();
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Document (*.docx) | *.docx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                var newpathdoc = sfd.FileName;

                //TODO
                var wordAPP = new Word.Application();
                wordAPP.Visible = false;

                var worddocument = wordAPP.Documents.Open(TemplateFileName1);

                //Таблица 1
                var n = 0;
                Word.Table tab = worddocument.Tables[1];
                for (int i = 2; i <= sclad.Count + 1; i++)
                {
                    tab.Rows.Add(Missing.Value);
                    tab.Cell(i, 1).Range.Text = sclad[n];
                    tab.Cell(i, 2).Range.Text = gram[n];
                    tab.Cell(i, 3).Range.Text = izmeren[n];
                    tab.Cell(i, 4).Range.Text = cena[n];
                    n++;
                }

                worddocument.SaveAs(newpathdoc);
                wordAPP.Visible = true;
            }
            MessageBox.Show("Сохраненно");
        }
    }
}
