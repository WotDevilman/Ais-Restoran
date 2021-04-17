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

using Word = Microsoft.Office.Interop.Word;

using BD.Forms;
using System.Reflection;

namespace BD.UsersControl
{
    public partial class UC_Sclad : UserControl
    {
        public UC_Sclad()
        {
            InitializeComponent();
        }
        database db = new database();
        private void UC_Sclad_Load(object sender, EventArgs e)
        {
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(db.selectSclad, db.connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            DGVIEWBlydo.DataSource = ds.Tables[0].DefaultView;

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

        private void button2_Click(object sender, EventArgs e)
        {
            using(Add_Sclad add_Sclad = new Add_Sclad())
            {
                add_Sclad.ShowDialog();
                this.OnLoad(e);
            }
        }
        public List<string> EditItems = new List<string>(); 
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

        private void button5_Click(object sender, EventArgs e)
        {
            //Form_InformEdit form = new Form_InformEdit();
            //form.ShowDialog();
            //this.OnLoad(e);
            //int pravo = form.pravo;
            //if (pravo != 0)
            //{
                using (Edit_Sclad edit_Sclad = new Edit_Sclad())
                {
                    if (EditItems.Count > 0)
                    {
                        edit_Sclad.id = Convert.ToInt32(EditItems[0]);
                        edit_Sclad.tprodyc = EditItems[1];
                        edit_Sclad.tizmeren = EditItems[2];
                        edit_Sclad.tkol = EditItems[3];
                        edit_Sclad.ShowDialog();
                        this.OnLoad(e);
                    }
                //}
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (DGVIEWBlydo.RowCount != 0)
            {
                Form_InformDelete form = new Form_InformDelete();
                form.ShowDialog();
                this.OnLoad(e);

                int prava = form.pravo;
                if (prava == 1)
                {
                    int iddel = Convert.ToInt32(DGVIEWBlydo[0, DGVIEWBlydo.CurrentCell.RowIndex].Value);

                    string quest = $"DELETE  FROM Склад WHERE ID_записи = {iddel}";
                    db.connect.Open();
                    OleDbCommand dataCommander = new OleDbCommand(quest, db.connect);
                    dataCommander.ExecuteNonQuery();

                    OleDbDataAdapter dataAdapter = new OleDbDataAdapter(db.selectSclad, db.connect);
                    DataSet ds = new DataSet();
                    dataAdapter.Fill(ds);
                    db.connect.Close();

                    DGVIEWBlydo.DataSource = ds.Tables[0].DefaultView;

                    DGVIEWBlydo.Columns[0].Visible = false;
                }
            }
        }
        private void ReplateWordDocument(string stupToReplate, string text, Word.Document worddoc)
        {
            var range = worddoc.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: stupToReplate, ReplaceWith: text);
        }
        private readonly string TemplateFileName = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Documents\\otchet_po_skladu.docx");
        List<string> sclad = new List<string>();
        List<string> gram = new List<string>();
        List<string> izmeren = new List<string>();
        private void getblsos()
        {
            db.connect.Open();
            OleDbCommand cmd = db.connect.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"SELECT Продукты.Название, Склад.Количество, ЕдИзмер.ЕдИзмерения FROM ЕдИзмер INNER JOIN(Продукты INNER JOIN Склад ON Продукты.ID_продукта = Склад.ID_продукта) ON ЕдИзмер.ID_ед = Склад.ID_ЕдИзмерний";
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
            }
        }
        private void button1_Click(object sender, EventArgs e)
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
                for (int i = 2; i <= sclad.Count+1; i++)
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
    }
}
