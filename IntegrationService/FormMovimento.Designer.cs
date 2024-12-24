namespace IntegrationService
{
    partial class FormMovimento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMovimento));
            this.button2 = new System.Windows.Forms.Button();
            this.btnImportar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textChaveMov = new System.Windows.Forms.TextBox();
            this.dtFinal = new System.Windows.Forms.MonthCalendar();
            this.label4 = new System.Windows.Forms.Label();
            this.dtInicial = new System.Windows.Forms.MonthCalendar();
            this.cbxCNPJMov = new System.Windows.Forms.ComboBox();
            this.chkCNPJ = new System.Windows.Forms.CheckBox();
            this.txtBoxLog = new System.Windows.Forms.RichTextBox();
            this.lblLog = new System.Windows.Forms.Label();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(800, 277);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "Sair";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnImportar
            // 
            this.btnImportar.Location = new System.Drawing.Point(12, 277);
            this.btnImportar.Name = "btnImportar";
            this.btnImportar.Size = new System.Drawing.Size(109, 23);
            this.btnImportar.TabIndex = 9;
            this.btnImportar.Text = "Importar Dados";
            this.btnImportar.UseVisualStyleBackColor = true;
            this.btnImportar.Click += new System.EventHandler(this.btnImportar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Data de Lançamento Inicial";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "CNPJ Empresa";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Chave";
            // 
            // textChaveMov
            // 
            this.textChaveMov.Location = new System.Drawing.Point(12, 24);
            this.textChaveMov.Name = "textChaveMov";
            this.textChaveMov.Size = new System.Drawing.Size(472, 20);
            this.textChaveMov.TabIndex = 15;
            this.textChaveMov.Text = "e54fa5fd-53aa-42e3-bab5-758d0767c576";
            // 
            // dtFinal
            // 
            this.dtFinal.Location = new System.Drawing.Point(257, 106);
            this.dtFinal.MaxSelectionCount = 1;
            this.dtFinal.Name = "dtFinal";
            this.dtFinal.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(257, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(132, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Data de Lançamento Final";
            // 
            // dtInicial
            // 
            this.dtInicial.Location = new System.Drawing.Point(12, 106);
            this.dtInicial.MaxSelectionCount = 1;
            this.dtInicial.Name = "dtInicial";
            this.dtInicial.TabIndex = 14;
            // 
            // cbxCNPJMov
            // 
            this.cbxCNPJMov.Enabled = false;
            this.cbxCNPJMov.FormattingEnabled = true;
            this.cbxCNPJMov.Location = new System.Drawing.Point(12, 65);
            this.cbxCNPJMov.Name = "cbxCNPJMov";
            this.cbxCNPJMov.Size = new System.Drawing.Size(227, 21);
            this.cbxCNPJMov.TabIndex = 23;
            // 
            // chkCNPJ
            // 
            this.chkCNPJ.AutoSize = true;
            this.chkCNPJ.Checked = true;
            this.chkCNPJ.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCNPJ.Location = new System.Drawing.Point(260, 66);
            this.chkCNPJ.Name = "chkCNPJ";
            this.chkCNPJ.Size = new System.Drawing.Size(119, 17);
            this.chkCNPJ.TabIndex = 24;
            this.chkCNPJ.Text = "Todas as Empresas";
            this.chkCNPJ.UseVisualStyleBackColor = true;
            this.chkCNPJ.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // txtBoxLog
            // 
            this.txtBoxLog.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtBoxLog.Location = new System.Drawing.Point(494, 24);
            this.txtBoxLog.Name = "txtBoxLog";
            this.txtBoxLog.Size = new System.Drawing.Size(381, 244);
            this.txtBoxLog.TabIndex = 26;
            this.txtBoxLog.Text = "";
            // 
            // lblLog
            // 
            this.lblLog.AutoSize = true;
            this.lblLog.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lblLog.Location = new System.Drawing.Point(491, 9);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(25, 13);
            this.lblLog.TabIndex = 27;
            this.lblLog.Text = "Log";
            // 
            // btnLimpar
            // 
            this.btnLimpar.Location = new System.Drawing.Point(494, 278);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(75, 23);
            this.btnLimpar.TabIndex = 28;
            this.btnLimpar.Text = "Limpar";
            this.btnLimpar.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnLimpar.UseVisualStyleBackColor = true;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // FormMovimento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 309);
            this.Controls.Add(this.btnLimpar);
            this.Controls.Add(this.lblLog);
            this.Controls.Add(this.txtBoxLog);
            this.Controls.Add(this.chkCNPJ);
            this.Controls.Add(this.cbxCNPJMov);
            this.Controls.Add(this.dtFinal);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnImportar);
            this.Controls.Add(this.dtInicial);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textChaveMov);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(905, 348);
            this.MinimumSize = new System.Drawing.Size(905, 348);
            this.Name = "FormMovimento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Movimento";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnImportar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textChaveMov;
        private System.Windows.Forms.MonthCalendar dtFinal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MonthCalendar dtInicial;
        private System.Windows.Forms.ComboBox cbxCNPJMov;
        private System.Windows.Forms.CheckBox chkCNPJ;
        private System.Windows.Forms.RichTextBox txtBoxLog;
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.Button btnLimpar;
    }
}