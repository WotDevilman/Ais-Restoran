
namespace BD.Forms
{
    partial class Edit_Blydo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.TGod = new System.Windows.Forms.NumericUpDown();
            this.TVes = new System.Windows.Forms.NumericUpDown();
            this.TKategor = new System.Windows.Forms.ComboBox();
            this.TCena = new System.Windows.Forms.TextBox();
            this.TName = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TGod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TVes)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(250)))));
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.TGod);
            this.panel1.Controls.Add(this.TVes);
            this.panel1.Controls.Add(this.TKategor);
            this.panel1.Controls.Add(this.TCena);
            this.panel1.Controls.Add(this.TName);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(321, 248);
            this.panel1.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(18, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 18);
            this.label4.TabIndex = 22;
            this.label4.Text = "Категория";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(175, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 18);
            this.label3.TabIndex = 21;
            this.label3.Text = "Срок годности";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(175, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 18);
            this.label2.TabIndex = 20;
            this.label2.Text = "Цена";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(18, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 18);
            this.label1.TabIndex = 19;
            this.label1.Text = "Вес порции";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(3, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 18);
            this.label5.TabIndex = 9;
            this.label5.Text = "Название";
            // 
            // TGod
            // 
            this.TGod.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(254)))), ((int)(((byte)(254)))));
            this.TGod.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TGod.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TGod.ForeColor = System.Drawing.Color.Black;
            this.TGod.Location = new System.Drawing.Point(178, 147);
            this.TGod.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.TGod.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.TGod.Name = "TGod";
            this.TGod.Size = new System.Drawing.Size(120, 20);
            this.TGod.TabIndex = 18;
            this.TGod.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // TVes
            // 
            this.TVes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(254)))), ((int)(((byte)(254)))));
            this.TVes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TVes.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TVes.ForeColor = System.Drawing.Color.Black;
            this.TVes.Location = new System.Drawing.Point(21, 94);
            this.TVes.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.TVes.Name = "TVes";
            this.TVes.Size = new System.Drawing.Size(120, 20);
            this.TVes.TabIndex = 17;
            // 
            // TKategor
            // 
            this.TKategor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TKategor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TKategor.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TKategor.ForeColor = System.Drawing.Color.Black;
            this.TKategor.FormattingEnabled = true;
            this.TKategor.Location = new System.Drawing.Point(21, 143);
            this.TKategor.Name = "TKategor";
            this.TKategor.Size = new System.Drawing.Size(130, 26);
            this.TKategor.TabIndex = 16;
            // 
            // TCena
            // 
            this.TCena.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TCena.ForeColor = System.Drawing.Color.Black;
            this.TCena.Location = new System.Drawing.Point(178, 90);
            this.TCena.Name = "TCena";
            this.TCena.Size = new System.Drawing.Size(130, 24);
            this.TCena.TabIndex = 15;
            this.TCena.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TCena_KeyPress);
            // 
            // TName
            // 
            this.TName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TName.ForeColor = System.Drawing.Color.Black;
            this.TName.Location = new System.Drawing.Point(3, 29);
            this.TName.Name = "TName";
            this.TName.Size = new System.Drawing.Size(305, 24);
            this.TName.TabIndex = 14;
            this.TName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TName_KeyPress);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
            this.button1.FlatAppearance.BorderSize = 2;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Lime;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(254)))), ((int)(((byte)(254)))));
            this.button1.Location = new System.Drawing.Point(252, 213);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(66, 32);
            this.button1.TabIndex = 12;
            this.button1.Text = "Сохр";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.Red;
            this.button2.FlatAppearance.BorderSize = 2;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(254)))), ((int)(((byte)(254)))));
            this.button2.Location = new System.Drawing.Point(180, 213);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(66, 32);
            this.button2.TabIndex = 13;
            this.button2.Text = "Закр";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Edit_Blydo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(254)))), ((int)(((byte)(254)))));
            this.ClientSize = new System.Drawing.Size(345, 272);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Edit_Blydo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit_Blydo";
            this.Load += new System.EventHandler(this.Edit_Blydo_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TGod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TVes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown TGod;
        private System.Windows.Forms.NumericUpDown TVes;
        private System.Windows.Forms.ComboBox TKategor;
        private System.Windows.Forms.TextBox TCena;
        private System.Windows.Forms.TextBox TName;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}