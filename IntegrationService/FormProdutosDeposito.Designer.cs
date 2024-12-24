namespace IntegrationService
{
    partial class FormProdutosDeposito
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProdutosDeposito));
            this.txtDeposito = new System.Windows.Forms.TextBox();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.lblLog = new System.Windows.Forms.Label();
            this.txtBoxLogDep = new System.Windows.Forms.RichTextBox();
            this.cbxCNPJDep = new System.Windows.Forms.ComboBox();
            this.chkCNPJDep = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.btnImportar = new System.Windows.Forms.Button();
            this.lblDeposito = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textChaveDep = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtDeposito
            // 
            this.txtDeposito.Location = new System.Drawing.Point(10, 100);
            this.txtDeposito.Name = "txtDeposito";
            this.txtDeposito.Size = new System.Drawing.Size(368, 20);
            this.txtDeposito.TabIndex = 56;
            this.txtDeposito.Text = "1,2,3,4";
            // 
            // btnLimpar
            // 
            this.btnLimpar.Location = new System.Drawing.Point(387, 266);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(75, 23);
            this.btnLimpar.TabIndex = 54;
            this.btnLimpar.Text = "Limpar";
            this.btnLimpar.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnLimpar.UseVisualStyleBackColor = true;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // lblLog
            // 
            this.lblLog.AutoSize = true;
            this.lblLog.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lblLog.Location = new System.Drawing.Point(384, 5);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(25, 13);
            this.lblLog.TabIndex = 53;
            this.lblLog.Text = "Log";
            // 
            // txtBoxLogDep
            // 
            this.txtBoxLogDep.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtBoxLogDep.Location = new System.Drawing.Point(387, 20);
            this.txtBoxLogDep.Name = "txtBoxLogDep";
            this.txtBoxLogDep.Size = new System.Drawing.Size(381, 240);
            this.txtBoxLogDep.TabIndex = 52;
            this.txtBoxLogDep.Text = "";
            // 
            // cbxCNPJDep
            // 
            this.cbxCNPJDep.Enabled = false;
            this.cbxCNPJDep.FormattingEnabled = true;
            this.cbxCNPJDep.Location = new System.Drawing.Point(10, 60);
            this.cbxCNPJDep.Name = "cbxCNPJDep";
            this.cbxCNPJDep.Size = new System.Drawing.Size(238, 21);
            this.cbxCNPJDep.TabIndex = 51;
            // 
            // chkCNPJDep
            // 
            this.chkCNPJDep.AutoSize = true;
            this.chkCNPJDep.Checked = true;
            this.chkCNPJDep.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCNPJDep.Location = new System.Drawing.Point(259, 64);
            this.chkCNPJDep.Name = "chkCNPJDep";
            this.chkCNPJDep.Size = new System.Drawing.Size(119, 17);
            this.chkCNPJDep.TabIndex = 50;
            this.chkCNPJDep.Text = "Todas as Empresas";
            this.chkCNPJDep.CheckedChanged += new System.EventHandler(this.chkCNPJDep_CheckedChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(693, 266);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 45;
            this.button2.Text = "Sair";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnImportar
            // 
            this.btnImportar.Location = new System.Drawing.Point(10, 265);
            this.btnImportar.Name = "btnImportar";
            this.btnImportar.Size = new System.Drawing.Size(109, 23);
            this.btnImportar.TabIndex = 44;
            this.btnImportar.Text = "Importar Dados";
            this.btnImportar.UseVisualStyleBackColor = true;
            this.btnImportar.Click += new System.EventHandler(this.btnImportar_Click);
            // 
            // lblDeposito
            // 
            this.lblDeposito.AutoSize = true;
            this.lblDeposito.Location = new System.Drawing.Point(11, 86);
            this.lblDeposito.Name = "lblDeposito";
            this.lblDeposito.Size = new System.Drawing.Size(60, 13);
            this.lblDeposito.TabIndex = 48;
            this.lblDeposito.Text = "Deposito(s)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 47;
            this.label2.Text = "CNPJ Empresa";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 46;
            this.label1.Text = "Chave";
            // 
            // textChaveDep
            // 
            this.textChaveDep.Location = new System.Drawing.Point(10, 20);
            this.textChaveDep.Name = "textChaveDep";
            this.textChaveDep.Size = new System.Drawing.Size(368, 20);
            this.textChaveDep.TabIndex = 49;
            this.textChaveDep.Text = "e54fa5fd-53aa-42e3-bab5-758d0767c576";
            // 
            // FormProdutosDeposito
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 296);
            this.Controls.Add(this.txtDeposito);
            this.Controls.Add(this.btnLimpar);
            this.Controls.Add(this.lblLog);
            this.Controls.Add(this.txtBoxLogDep);
            this.Controls.Add(this.cbxCNPJDep);
            this.Controls.Add(this.chkCNPJDep);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnImportar);
            this.Controls.Add(this.lblDeposito);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textChaveDep);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(795, 335);
            this.MinimumSize = new System.Drawing.Size(795, 335);
            this.Name = "FormProdutosDeposito";
            this.Text = "Produtos Deposito";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDeposito;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.RichTextBox txtBoxLogDep;
        private System.Windows.Forms.ComboBox cbxCNPJDep;
        private System.Windows.Forms.CheckBox chkCNPJDep;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnImportar;
        private System.Windows.Forms.Label lblDeposito;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textChaveDep;
    }
}