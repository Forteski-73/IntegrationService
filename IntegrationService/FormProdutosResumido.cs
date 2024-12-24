using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace IntegrationService
{
    public partial class FormProdutosResumido : Form
    {
        private RichTextBox txtLog;
        private System.Timers.Timer taskTimer;
        private int contador = 1;

        public FormProdutosResumido()
        {
            // Configuração inicial do Timer
            taskTimer = new System.Timers.Timer();
            taskTimer.Interval = 3600000; //3600000 1 hora em milissegundos | 60000 1 min
            taskTimer.Elapsed += TaskTimer_Elapsed;
            taskTimer.AutoReset = true; // Repetir automaticamente

            InitializeComponent();

            txtLog = txtBoxLogRe;
            LogHelper.ConfigurarCampoLog(txtLog);

            this.MaximizeBox = false; // Desabilita maximizar
            this.SizeGripStyle = SizeGripStyle.Hide; // Desabilita redimensionar

            // Criar lista dinâmica
            string[] listaDeOpcoes = { "86.046.463/0027-49", "86.046.463/0028-20", "86.046.463/0029-00", "86.046.463/0009-67", "86.046.463/0031-25" };
            // Adicionar itens dinamicamente
            cbxCNPJMov.Items.AddRange(listaDeOpcoes);
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                if (!taskTimer.Enabled)
                {
                    ExecuteTask();
                    taskTimer.Start();
                }
                else
                {
                    LogHelper.GravarLog("O Timer já está em execução.");

                }
            });
        }

        private void TaskTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // Código da tarefa que será executada
            ExecuteTask();
        }

        private async void ExecuteTask()
        {
            //txtBoxLog.Text = "";
            this.Cursor = Cursors.WaitCursor;
            var param = new DadosRe
            {
                ChaveRe = textChave.Text,
                CNPJRe = cbxCNPJMov.Text.Replace(".", "").Replace("/", "").Replace("-", ""),
                TodosRe = chkCNPJ.Checked,
                Produto = txtProduto.Text,
                Saldo = ckbSaldo.Checked
            };

            await LogHelper.GravarLog(contador + "° Requisição.");
            contador++;
            // Cria uma instância da classe ProdutosInventario e executa o processamento
            ProdutosInventarioSimplificado processar = new ProdutosInventarioSimplificado();
            await processar.IniciarProcesso(param);
            this.Cursor = Cursors.Default;
        }

        private void chkCNPJ_CheckedChanged(object sender, EventArgs e)
        {
            cbxCNPJMov.Text = "";
            cbxCNPJMov.Enabled = !chkCNPJ.Checked;
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtBoxLogRe.Text = "";
        }

        private void btnLimpar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
