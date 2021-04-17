using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Word = Microsoft.Office.Interop.Word;

using BD.Forms;

namespace BD.UsersControl
{
    public partial class UC_Blydo : UserControl
    {
        public UC_Blydo()
        {
            InitializeComponent();
            string[] prodyct = getProdyct().Select(n => n.ToString()).ToArray();
            TProdyct.Items.AddRange(prodyct);
            string[] edizmerenia = getEDIzmerenia().Select(n => n.ToString()).ToArray();
            TEDIzmerenia.Items.AddRange(edizmerenia);
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
        private List<string> getProdyct()
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
        private List<string> getEDIzmerenia()
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

        database db = new database();
        int idblydo;
        private void UC_Blydo_Load(object sender, EventArgs e)
        {
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(db.selectBlydo, db.connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            DGVIEWBlydo.DataSource = ds.Tables[0].DefaultView;

            //TRecept.Enabled = false;

            DGVIEWBlydo.Columns[0].Visible = false;
            if (DGVIEWBlydo.RowCount > 0)
            {
                EditItems.Clear();
                for (int i = 0; i < DGVIEWBlydo.ColumnCount; i++)
                {
                    EditItems.Add(DGVIEWBlydo.Rows[0].Cells[i].Value.ToString());
                }
            }
            DGSostav(Convert.ToInt32(EditItems[0]));

            getRecept(Convert.ToInt32(EditItems[0]));
        }
        public List<string> EditItems = new List<string>();
        private void DGVIEWBlydo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGVIEWBlydo.RowCount != 0 && e.RowIndex != -1)
            {
                //TRecept.Enabled = false;
                nameblydo = DGVIEWBlydo.Rows[e.RowIndex].Cells[1].Value.ToString();
                idblydo = Convert.ToInt32(DGVIEWBlydo.Rows[e.RowIndex].Cells[0].Value);
                DGSostav(idblydo);

                getRecept(idblydo);

                int mSelectedRowIndex = DGVIEWBlydo.SelectedCells[0].RowIndex;
                DataGridViewRow mSelectedRow = DGVIEWBlydo.Rows[mSelectedRowIndex];

                EditItems.Clear();

                for (int i = 0; i < DGVIEWBlydo.ColumnCount; i++)
                {
                    EditItems.Add(Convert.ToString(mSelectedRow.Cells[i].Value));
                }
            }
        }

        void DGSostav(int id)
        {
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(db.selectSostavBlydo + id, db.connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            int a = ds.Tables[0].Rows.Count;
            
            if (a != 0)
            {
                DGVIEWSostav.DataSource = ds.Tables[0].DefaultView;

                DGVIEWSostav.Columns[0].Visible = false;
            }
            else
            {
                while (DGVIEWSostav.Rows.Count > 1)
                    for (int i = 0; i < DGVIEWSostav.Rows.Count -1; i++)
                        DGVIEWSostav.Rows.Remove(DGVIEWSostav.Rows[i]);
                DGVIEWSostav.Rows.Remove(DGVIEWSostav.Rows[0]);
            }
        }

        private void getRecept(int id)
        {
            List<string> receptall = new List<string>();
            string recept = "";
            db.connect.Open();
            OleDbCommand cmd = db.connect.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = db.selectRecept + id;
            cmd.ExecuteNonQuery(); 
            OleDbDataReader myOleDbDataReader1 = cmd.ExecuteReader();
            int i = 0;
            while(myOleDbDataReader1.Read()){
                i++;
            }
            myOleDbDataReader1.Close();
            db.connect.Close();
            if (i != 0)
            {
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow item in dt.Rows)
                {
                    receptall.Add(item[0].ToString());
                }
                TRecept.Text = receptall[0];
            }
            else
            {
                TRecept.Clear();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            TRecept.ReadOnly = !TRecept.ReadOnly;
        }

        private void TRecept_TextChanged(object sender, EventArgs e)
        {
            if(TRecept.Enabled == true)
            {
                string quest = $"UPDATE СоставБлюд SET Рецепт = '{TRecept.Text}' WHERE ID_блюд = {idblydo}";
                db.connect.Open();
                OleDbCommand dataAdapter = new OleDbCommand(quest, db.connect);
                dataAdapter.ExecuteNonQuery();
                db.connect.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {if (TProdyct.Text.Length != 0 && TEDIzmerenia.Text.Length != 0) {


                    int idprodyct = getProdyct(TProdyct.SelectedItem.ToString());
                    int idediz = getEdizmeren(TEDIzmerenia.SelectedItem.ToString());

                    string quest = "INSERT INTO СоставБлюд (ID_блюд, ID_продукт, Количество, Eд, Рецепт) VALUES('" + idblydo + "','" + idprodyct + "','" + TKol.Value + "','" + idediz + "','" + TRecept.Text + "')";
                    db.connect.Open();
                    OleDbCommand dataAdapter = new OleDbCommand(quest, db.connect);
                    dataAdapter.ExecuteNonQuery();
                    db.connect.Close();

                    DGSostav(idblydo); 
                    
                    Form_Add formadd = new Form_Add();
                    formadd.ShowDialog();
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
        private int getProdyct(string nameZakazchik)
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
        private int getEdizmeren(string nameZakazchik)
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (DGVIEWBlydo.RowCount != 0)
            {
                Form_InformDelete form = new Form_InformDelete();
                form.ShowDialog();
                this.OnLoad(e);

                int prava = form.pravo;
                if (prava == 1)
                {
                    int iddel = Convert.ToInt32(DGVIEWSostav[0, DGVIEWSostav.CurrentCell.RowIndex].Value);

                    string quest = $"DELETE  FROM СоставБлюд WHERE ID_зап = {iddel}";
                    db.connect.Open();
                    OleDbCommand dataCommander = new OleDbCommand(quest, db.connect);
                    dataCommander.ExecuteNonQuery();

                    DGSostav(idblydo);

                    DGVIEWSostav.Columns[0].Visible = false;
                }
            }

        }
        int idsostav;
        private void button4_Click(object sender, EventArgs e)
        {
            if (TProdyct.Text.Length != 0 && TEDIzmerenia.Text.Length != 0)
            {
                int idprodyct = getProdyct(TProdyct.SelectedItem.ToString());
                int idediz = getEdizmeren(TEDIzmerenia.SelectedItem.ToString());

                string quest = $"UPDATE СоставБлюд SET ID_продукт = '{idprodyct}', Количество = '{TKol.Value}', Eд = '{idediz}'  WHERE ID_зап = {idsostav}";
                db.connect.Open();
                OleDbCommand dataAdapter = new OleDbCommand(quest, db.connect);
                dataAdapter.ExecuteNonQuery();
                db.connect.Close();

                DGSostav(idblydo);

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

        private void DGVIEWSostav_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGVIEWSostav.RowCount != 0 && e.RowIndex != -1)
            {
                idsostav = Convert.ToInt32(DGVIEWSostav.Rows[e.RowIndex].Cells[0].Value.ToString());
                TProdyct.Text = DGVIEWSostav.Rows[e.RowIndex].Cells[1].Value.ToString();
                TKol.Text = DGVIEWSostav.Rows[e.RowIndex].Cells[2].Value.ToString();
                TEDIzmerenia.Text = DGVIEWSostav.Rows[e.RowIndex].Cells[3].Value.ToString();
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
                    

                    string quest = $"DELETE  FROM Блюда WHERE ID_блюда = {iddel}";
                    db.connect.Open();
                    OleDbCommand dataCommander = new OleDbCommand(quest, db.connect);
                    dataCommander.ExecuteNonQuery();

                    OleDbDataAdapter dataAdapter = new OleDbDataAdapter(db.selectBlydo, db.connect);
                    DataSet ds = new DataSet();
                    dataAdapter.Fill(ds);
                    db.connect.Close();

                    DGVIEWBlydo.DataSource = ds.Tables[0].DefaultView;

                    DGVIEWBlydo.Columns[0].Visible = false;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Add_Blydo add_Blydo = new Add_Blydo();

            add_Blydo.ShowDialog();
            this.OnLoad(e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (Edit_Blydo edit_Blydo = new Edit_Blydo())
            {
                if (EditItems.Count > 0)
                {
                    edit_Blydo.id = Convert.ToInt32(EditItems[0]);
                    edit_Blydo.tname = EditItems[1];
                    edit_Blydo.tves = EditItems[2];
                    edit_Blydo.tcena = EditItems[3];
                    edit_Blydo.tsroc = EditItems[4];
                    edit_Blydo.tkateg = EditItems[5];
                    edit_Blydo.ShowDialog();
                    this.OnLoad(e);
                }
            }
        }
        string nameblydo = "";
        List<string> sostav = new List<string>();
        List<string> gram = new List<string>();
        List<string> izmeren = new List<string>();
        private void getblsos(int name)
        {
            db.connect.Open();
            OleDbCommand cmd = db.connect.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"SELECT Продукты.Название, СоставБлюд.Количество, ЕдИзмер.ЕдИзмерения FROM Продукты INNER JOIN (ЕдИзмер INNER JOIN (Блюда INNER JOIN СоставБлюд ON Блюда.ID_блюда = СоставБлюд.ID_блюд) ON ЕдИзмер.ID_ед = СоставБлюд.Eд) ON Продукты.ID_продукта = СоставБлюд.ID_продукт WHERE Блюда.ID_блюда = {name}";
            cmd.ExecuteNonQuery();
            db.connect.Close();
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow item in dt.Rows)
            {
                sostav.Add(item[0].ToString());
                gram.Add(item[1].ToString());
                izmeren.Add(item[2].ToString());
            }
        }
        private void ReplateWordDocument(string stupToReplate, string text, Word.Document worddoc)
        {
            var range = worddoc.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: stupToReplate, ReplaceWith: text);
        }
        private readonly string TemplateFileName = Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Documents\\Blyudo.docx");
        private void button7_Click(object sender, EventArgs e)
        {
            string recept = TRecept.Text;
            getblsos(idblydo);
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Document (*.docx) | *.docx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                var newpathdoc = sfd.FileName;

                //TODO
                var wordAPP = new Word.Application();
                wordAPP.Visible = false;

                var worddocument = wordAPP.Documents.Open(TemplateFileName);

                ReplateWordDocument("{Название блюда}", nameblydo, worddocument);

                //Таблица 1
                var n = 0;
                Word.Table tab = worddocument.Tables[1];
                for (int i = 3; i <= sostav.Count; i++)
                {
                    tab.Rows.Add(Missing.Value);
                    tab.Cell(i, 1).Range.Text = sostav[n];
                    tab.Cell(i, 2).Range.Text = gram[n];
                    tab.Cell(i, 3).Range.Text = izmeren[n];
                    n++;
                }
                tab = worddocument.Tables[2];
                    tab.Rows.Add(Missing.Value);
                    tab.Cell(2, 1).Range.Text = recept;

                worddocument.SaveAs(newpathdoc);
                wordAPP.Visible = true; 
            }
            MessageBox.Show("Сохраненно");
        }

        private void TVes_ValueChanged(object sender, EventArgs e)
        {

        }
        string ves = "";
        string cena = "";
        string kategor = "";
        string god = "";
        private void TVes_TextChanged(object sender, EventArgs e)
        {
            if (TVes.Text != " ")
            {
                ves = $"Блюда.Вес_порции = {TVes.Text}";
            }
            else
            {
                ves = "";
            }
        }

        private void TCena_TextChanged(object sender, EventArgs e)
        {
            if (TCena.Text != " ")
            {
                cena = $"Блюда.Цена = {TCena.Text}";
            }
            else
            {
                cena = "";
            }
        }

        private void TGod_TextChanged(object sender, EventArgs e)
        {
            if (TGod.Text != " ")
            {
                god = $"Блюда.СрокГодности = {TGod.Text}";
            }
            else
            {
                god = "";
            }
        }

        private void TKategor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TKategor.Text != " ")
            {
                kategor = $"Категория.Категория = '{TKategor.Text}'";
            }
            else
            {
                kategor = "";
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            List<string> tex = new List<string>();
            List<string> te = new List<string>();
            string quest = " WHERE ";

            tex.Clear();
            te.Clear();

            if (cena.Length != 0) tex.Add(cena);
            if (god.Length != 0) tex.Add(god);
            if (kategor.Length != 0) tex.Add(kategor);
            if (ves.Length != 0) tex.Add(ves);
            if (tex.Count() != 0)
            {
                if (tex.Count == 1)
                {
                    te.Add(tex[0]);
                }
                if (tex.Count == 2)
                {
                    te.Add(tex[0]);
                    te.Add(" and ");
                    te.Add(tex[1]);
                }
                else if (tex.Count == 3)
                {
                    te.Add(tex[0]);
                    te.Add(" and ");
                    te.Add(tex[1]);
                    te.Add(" and ");
                    te.Add(tex[2]);
                }
                else if (tex.Count == 4)
                {
                    te.Add(tex[0]);
                    te.Add(" and ");
                    te.Add(tex[1]);
                    te.Add(" and ");
                    te.Add(tex[2]);
                    te.Add(" and ");
                    te.Add(tex[3]);
                }
                for (int i = 0; i < te.Count; i++)
                {
                    quest += te[i];
                }
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(db.selectBlydo + quest, db.connect);
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds);
                DGVIEWBlydo.DataSource = ds.Tables[0].DefaultView;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(db.selectBlydo, db.connect);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds);
            DGVIEWBlydo.DataSource = ds.Tables[0].DefaultView;

            //TRecept.Enabled = false;

            DGVIEWBlydo.Columns[0].Visible = false;
            if (DGVIEWBlydo.RowCount > 0)
            {
                EditItems.Clear();
                for (int i = 0; i < DGVIEWBlydo.ColumnCount; i++)
                {
                    EditItems.Add(DGVIEWBlydo.Rows[0].Cells[i].Value.ToString());
                }
            }
            DGSostav(Convert.ToInt32(EditItems[0]));

            TRecept.Clear();
            getRecept(Convert.ToInt32(EditItems[0]));
        }

        private void TRecept_TextChanged_1(object sender, EventArgs e)
        {
            if (TRecept.Enabled == true)
            {
                string quest = $"UPDATE СоставБлюд SET Рецепт = '{TRecept.Text}' WHERE ID_блюд = {idblydo}";
                db.connect.Open();
                OleDbCommand dataAdapter = new OleDbCommand(quest, db.connect);
                dataAdapter.ExecuteNonQuery();
                db.connect.Close();
            }
        }
    }
}
