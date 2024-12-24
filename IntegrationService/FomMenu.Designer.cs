namespace IntegrationService
{
    partial class FomMenu
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FomMenu));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.Menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.linxProdutosInventarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linxProdutosSimplificadoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linxMovimentoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sairToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linxProdutosDepositoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.Menu;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Oxford";
            this.notifyIcon1.Visible = true;
            // 
            // Menu
            // 
            this.Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.linxProdutosInventarioToolStripMenuItem,
            this.linxProdutosDepositoToolStripMenuItem,
            this.linxProdutosSimplificadoToolStripMenuItem,
            this.linxMovimentoToolStripMenuItem,
            this.sairToolStripMenuItem});
            this.Menu.Name = "Menu";
            this.Menu.Size = new System.Drawing.Size(217, 136);
            this.Menu.Text = "Opções";
            // 
            // linxProdutosInventarioToolStripMenuItem
            // 
            this.linxProdutosInventarioToolStripMenuItem.Name = "linxProdutosInventarioToolStripMenuItem";
            this.linxProdutosInventarioToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.linxProdutosInventarioToolStripMenuItem.Text = "Linx Produtos Inventário";
            this.linxProdutosInventarioToolStripMenuItem.Click += new System.EventHandler(this.linxProdutosInventarioToolStripMenuItem_Click);
            // 
            // linxProdutosSimplificadoToolStripMenuItem
            // 
            this.linxProdutosSimplificadoToolStripMenuItem.Name = "linxProdutosSimplificadoToolStripMenuItem";
            this.linxProdutosSimplificadoToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.linxProdutosSimplificadoToolStripMenuItem.Text = "Linx Produtos Simplificado";
            this.linxProdutosSimplificadoToolStripMenuItem.Click += new System.EventHandler(this.linxProdutosSimplificadoToolStripMenuItem_Click);
            // 
            // linxMovimentoToolStripMenuItem
            // 
            this.linxMovimentoToolStripMenuItem.Name = "linxMovimentoToolStripMenuItem";
            this.linxMovimentoToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.linxMovimentoToolStripMenuItem.Text = "Linx Movimento";
            this.linxMovimentoToolStripMenuItem.Click += new System.EventHandler(this.linxMovimentoToolStripMenuItem_Click);
            // 
            // sairToolStripMenuItem
            // 
            this.sairToolStripMenuItem.Name = "sairToolStripMenuItem";
            this.sairToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.sairToolStripMenuItem.Text = "Sair";
            this.sairToolStripMenuItem.Click += new System.EventHandler(this.sairToolStripMenuItem_Click_1);
            // 
            // linxProdutosDepositoToolStripMenuItem
            // 
            this.linxProdutosDepositoToolStripMenuItem.Name = "linxProdutosDepositoToolStripMenuItem";
            this.linxProdutosDepositoToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.linxProdutosDepositoToolStripMenuItem.Text = "Linx Produtos Depósito";
            this.linxProdutosDepositoToolStripMenuItem.Click += new System.EventHandler(this.linxProdutosDepositoToolStripMenuItem_Click);
            // 
            // FomMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(222, 127);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FomMenu";
            this.Opacity = 0D;
            this.Text = "Linx - Importar dados";
            this.Menu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip Menu;
        private System.Windows.Forms.ToolStripMenuItem linxProdutosInventarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem linxMovimentoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sairToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem linxProdutosSimplificadoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem linxProdutosDepositoToolStripMenuItem;
    }
}

