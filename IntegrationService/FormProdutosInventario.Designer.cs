namespace IntegrationService
{
    partial class FormProdutosInventario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProdutosInventario));
            this.textChave = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtInventario = new System.Windows.Forms.MonthCalendar();
            this.btnImportar = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.chkCNPJ = new System.Windows.Forms.CheckBox();
            this.cbxCNPJ = new System.Windows.Forms.ComboBox();
            this.lblLog = new System.Windows.Forms.Label();
            this.txtBoxLogInv = new System.Windows.Forms.RichTextBox();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textChave
            // 
            this.textChave.Location = new System.Drawing.Point(12, 22);
            this.textChave.Name = "textChave";
            this.textChave.Size = new System.Drawing.Size(363, 20);
            this.textChave.TabIndex = 7;
            this.textChave.Text = "e54fa5fd-53aa-42e3-bab5-758d0767c576";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Chave";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "CNPJ Empresa";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Data Inventário";
            // 
            // dtInventario
            // 
            this.dtInventario.BackColor = System.Drawing.SystemColors.Window;
            this.dtInventario.Location = new System.Drawing.Point(12, 100);
            this.dtInventario.MaxSelectionCount = 1;
            this.dtInventario.Name = "dtInventario";
            this.dtInventario.TabIndex = 6;
            // 
            // btnImportar
            // 
            this.btnImportar.Location = new System.Drawing.Point(12, 271);
            this.btnImportar.Name = "btnImportar";
            this.btnImportar.Size = new System.Drawing.Size(109, 23);
            this.btnImportar.TabIndex = 0;
            this.btnImportar.Text = "Importar Dados";
            this.btnImportar.UseVisualStyleBackColor = true;
            this.btnImportar.Click += new System.EventHandler(this.btnImportar_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(694, 271);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Sair";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // chkCNPJ
            // 
            this.chkCNPJ.AutoSize = true;
            this.chkCNPJ.Checked = true;
            this.chkCNPJ.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCNPJ.Location = new System.Drawing.Point(256, 66);
            this.chkCNPJ.Name = "chkCNPJ";
            this.chkCNPJ.Size = new System.Drawing.Size(119, 17);
            this.chkCNPJ.TabIndex = 25;
            this.chkCNPJ.Text = "Todas as Empresas";
            this.chkCNPJ.UseVisualStyleBackColor = true;
            this.chkCNPJ.CheckedChanged += new System.EventHandler(this.chkCNPJ_CheckedChanged);
            // 
            // cbxCNPJ
            // 
            this.cbxCNPJ.Enabled = false;
            this.cbxCNPJ.FormattingEnabled = true;
            this.cbxCNPJ.Location = new System.Drawing.Point(12, 62);
            this.cbxCNPJ.Name = "cbxCNPJ";
            this.cbxCNPJ.Size = new System.Drawing.Size(227, 21);
            this.cbxCNPJ.TabIndex = 26;
            // 
            // lblLog
            // 
            this.lblLog.AutoSize = true;
            this.lblLog.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lblLog.Location = new System.Drawing.Point(385, 7);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(25, 13);
            this.lblLog.TabIndex = 29;
            this.lblLog.Text = "Log";
            // 
            // txtBoxLogInv
            // 
            this.txtBoxLogInv.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtBoxLogInv.Location = new System.Drawing.Point(388, 22);
            this.txtBoxLogInv.Name = "txtBoxLogInv";
            this.txtBoxLogInv.Size = new System.Drawing.Size(381, 240);
            this.txtBoxLogInv.TabIndex = 28;
            this.txtBoxLogInv.Text = "";
            // 
            // btnLimpar
            // 
            this.btnLimpar.Location = new System.Drawing.Point(388, 271);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(75, 23);
            this.btnLimpar.TabIndex = 30;
            this.btnLimpar.Text = "Limpar";
            this.btnLimpar.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnLimpar.UseVisualStyleBackColor = true;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // FormProdutosInventario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 304);
            this.Controls.Add(this.btnLimpar);
            this.Controls.Add(this.lblLog);
            this.Controls.Add(this.txtBoxLogInv);
            this.Controls.Add(this.cbxCNPJ);
            this.Controls.Add(this.chkCNPJ);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnImportar);
            this.Controls.Add(this.dtInventario);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textChave);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(800, 343);
            this.MinimumSize = new System.Drawing.Size(800, 343);
            this.Name = "FormProdutosInventario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Produtos Inventário";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textChave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MonthCalendar dtInventario;
        private System.Windows.Forms.Button btnImportar;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox chkCNPJ;
        private System.Windows.Forms.ComboBox cbxCNPJ;
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.RichTextBox txtBoxLogInv;
        private System.Windows.Forms.Button btnLimpar;
    }
}