using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntegrationService
{
    public partial class FomMenu : Form
    {
        public FomMenu()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.Close(); // Fecha o formulário logo após ele ser mostrado
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.Hide();
            notifyIcon1.Visible = true;
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void linxProdutosInventarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var form in Application.OpenForms.Cast<Form>().ToList())
            {
                if (form != this) // Não fecha o formulário principal
                {
                    form.Close();
                }
            }

            FormProdutosInventario formProdInv = new FormProdutosInventario();  // Cria uma instância do formulário
            formProdInv.Show();                                                 // Abre de forma modal, impedindo interação com o formulário principal até fechar
        }

        private void linxMovimentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var form in Application.OpenForms.Cast<Form>().ToList())
            {
                if (form != this) // Não fecha o formulário principal
                {
                    form.Close();
                }
            }

            FormMovimento formMovimento = new FormMovimento();
            formMovimento.Show();
        }

        private void sairToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void linxProdutosSimplificadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var form in Application.OpenForms.Cast<Form>().ToList())
            {
                if (form != this) // Não fecha o formulário principal
                {
                    form.Close();
                }
            }

            FormProdutosResumido formProdSim = new FormProdutosResumido();
            formProdSim.Show();                                          
        }

        private void linxProdutosDepositoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var form in Application.OpenForms.Cast<Form>().ToList())
            {
                if (form != this) // Não fecha o formulário principal
                {
                    form.Close();
                }
            }

            FormProdutosDeposito formProdDep = new FormProdutosDeposito();
            formProdDep.Show();      
        }

        private void linxCredentialsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var form in Application.OpenForms.Cast<Form>().ToList())
            {
                if (form != this) // Não fecha o formulário principal
                {
                    form.Close();
                }
            }

            FormCredentials formCredential = new FormCredentials();
            formCredential.Show();
        }
    }
}
