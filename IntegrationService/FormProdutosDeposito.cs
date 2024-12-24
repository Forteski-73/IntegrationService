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
    public partial class FormProdutosDeposito : Form
    {
        private RichTextBox txtLog;
        private System.Timers.Timer taskTimer;
        private int contador = 1;

        public FormProdutosDeposito()
        {
            // Configuração inicial do Timer
            taskTimer = new System.Timers.Timer();
            taskTimer.Interval = 3600000; //3600000 1 hora em milissegundos | 60000 1 min
            taskTimer.Elapsed += TaskTimer_Elapsed;
            taskTimer.AutoReset = true; // Repetir automaticamente

            InitializeComponent();

            txtLog = txtBoxLogDep;
            LogHelper.ConfigurarCampoLog(txtLog);

            this.MaximizeBox = false; // Desabilita maximizar
            this.SizeGripStyle = SizeGripStyle.Hide; // Desabilita redimensionar

            // Criar lista dinâmica
            string[] listaDeOpcoes = { "86.046.463/0027-49", "86.046.463/0028-20", "86.046.463/0029-00", "86.046.463/0009-67", "86.046.463/0031-25" };
            // Adicionar itens dinamicamente
            cbxCNPJDep.Items.AddRange(listaDeOpcoes);
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
            var param = new DadosDep
            {
                ChaveDep = textChaveDep.Text,
                CNPJDep = cbxCNPJDep.Text.Replace(".", "").Replace("/", "").Replace("-", ""),
                TodosDep = chkCNPJDep.Checked,
                Depositos = txtDeposito.Text
            };

            await LogHelper.GravarLog(contador + "° Requisição.");
            contador++;
            // Cria uma instância da classe ProdutosInventario e executa o processamento
            ProdutosDeposito processar = new ProdutosDeposito();
            await processar.IniciarProcesso(param);
            this.Cursor = Cursors.Default;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkCNPJDep_CheckedChanged(object sender, EventArgs e)
        {
            cbxCNPJDep.Text = "";
            cbxCNPJDep.Enabled = !chkCNPJDep.Checked;
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtBoxLogDep.Text = "";
        }
    }

    // Object filtros da tela
    public class DadosDep
    {
        public string ChaveDep { get; set; }
        public string CNPJDep { get; set; }
        public Boolean TodosDep { get; set; }
        public string Depositos { get; set; }
    }
}
