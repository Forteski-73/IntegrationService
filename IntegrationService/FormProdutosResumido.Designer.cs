namespace IntegrationService
{
    partial class FormProdutosResumido
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProdutosResumido));
            this.btnLimpar = new System.Windows.Forms.Button();
            this.lblLog = new System.Windows.Forms.Label();
            this.txtBoxLogRe = new System.Windows.Forms.RichTextBox();
            this.cbxCNPJMov = new System.Windows.Forms.ComboBox();
            this.chkCNPJ = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.btnImportar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textChave = new System.Windows.Forms.TextBox();
            this.ckbSaldo = new System.Windows.Forms.CheckBox();
            this.txtProduto = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnLimpar
            // 
            this.btnLimpar.Location = new System.Drawing.Point(388, 266);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(75, 23);
            this.btnLimpar.TabIndex = 41;
            this.btnLimpar.Text = "Limpar";
            this.btnLimpar.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnLimpar.UseVisualStyleBackColor = true;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click_1);
            // 
            // lblLog
            // 
            this.lblLog.AutoSize = true;
            this.lblLog.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lblLog.Location = new System.Drawing.Point(385, 5);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(25, 13);
            this.lblLog.TabIndex = 40;
            this.lblLog.Text = "Log";
            // 
            // txtBoxLogRe
            // 
            this.txtBoxLogRe.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtBoxLogRe.Location = new System.Drawing.Point(388, 20);
            this.txtBoxLogRe.Name = "txtBoxLogRe";
            this.txtBoxLogRe.Size = new System.Drawing.Size(381, 240);
            this.txtBoxLogRe.TabIndex = 39;
            this.txtBoxLogRe.Text = "";
            // 
            // cbxCNPJMov
            // 
            this.cbxCNPJMov.Enabled = false;
            this.cbxCNPJMov.FormattingEnabled = true;
            this.cbxCNPJMov.Location = new System.Drawing.Point(11, 60);
            this.cbxCNPJMov.Name = "cbxCNPJMov";
            this.cbxCNPJMov.Size = new System.Drawing.Size(238, 21);
            this.cbxCNPJMov.TabIndex = 38;
            // 
            // chkCNPJ
            // 
            this.chkCNPJ.AutoSize = true;
            this.chkCNPJ.Checked = true;
            this.chkCNPJ.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCNPJ.Location = new System.Drawing.Point(260, 64);
            this.chkCNPJ.Name = "chkCNPJ";
            this.chkCNPJ.Size = new System.Drawing.Size(119, 17);
            this.chkCNPJ.TabIndex = 37;
            this.chkCNPJ.Text = "Todas as Empresas";
            this.chkCNPJ.UseVisualStyleBackColor = true;
            this.chkCNPJ.CheckedChanged += new System.EventHandler(this.chkCNPJ_CheckedChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(694, 266);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 32;
            this.button2.Text = "Sair";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnImportar
            // 
            this.btnImportar.Location = new System.Drawing.Point(11, 265);
            this.btnImportar.Name = "btnImportar";
            this.btnImportar.Size = new System.Drawing.Size(109, 23);
            this.btnImportar.TabIndex = 31;
            this.btnImportar.Text = "Importar Dados";
            this.btnImportar.UseVisualStyleBackColor = true;
            this.btnImportar.Click += new System.EventHandler(this.btnImportar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 35;
            this.label3.Text = "Produto";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 34;
            this.label2.Text = "CNPJ Empresa";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 33;
            this.label1.Text = "Chave";
            // 
            // textChave
            // 
            this.textChave.Location = new System.Drawing.Point(11, 20);
            this.textChave.Name = "textChave";
            this.textChave.Size = new System.Drawing.Size(368, 20);
            this.textChave.TabIndex = 36;
            this.textChave.Text = "e54fa5fd-53aa-42e3-bab5-758d0767c576";
            // 
            // ckbSaldo
            // 
            this.ckbSaldo.AutoSize = true;
            this.ckbSaldo.Checked = true;
            this.ckbSaldo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbSaldo.Location = new System.Drawing.Point(131, 103);
            this.ckbSaldo.Name = "ckbSaldo";
            this.ckbSaldo.Size = new System.Drawing.Size(115, 17);
            this.ckbSaldo.TabIndex = 42;
            this.ckbSaldo.Text = "Apenas com Saldo";
            this.ckbSaldo.UseVisualStyleBackColor = true;
            // 
            // txtProduto
            // 
            this.txtProduto.Location = new System.Drawing.Point(11, 100);
            this.txtProduto.Name = "txtProduto";
            this.txtProduto.Size = new System.Drawing.Size(110, 20);
            this.txtProduto.TabIndex = 43;
            // 
            // FormProdutosResumido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 296);
            this.Controls.Add(this.txtProduto);
            this.Controls.Add(this.ckbSaldo);
            this.Controls.Add(this.btnLimpar);
            this.Controls.Add(this.lblLog);
            this.Controls.Add(this.txtBoxLogRe);
            this.Controls.Add(this.cbxCNPJMov);
            this.Controls.Add(this.chkCNPJ);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnImportar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textChave);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(797, 335);
            this.MinimumSize = new System.Drawing.Size(797, 335);
            this.Name = "FormProdutosResumido";
            this.Text = "Produto Simplificado";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.RichTextBox txtBoxLogRe;
        private System.Windows.Forms.ComboBox cbxCNPJMov;
        private System.Windows.Forms.CheckBox chkCNPJ;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnImportar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textChave;
        private System.Windows.Forms.CheckBox ckbSaldo;
        private System.Windows.Forms.TextBox txtProduto;
    }
}