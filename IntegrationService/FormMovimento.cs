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
    public partial class FormMovimento : Form
    {
        private RichTextBox txtLog;
        private System.Timers.Timer taskTimer;
        private int contador = 1;

        public FormMovimento()
        {

            // Configuração inicial do Timer
            taskTimer = new System.Timers.Timer();
            taskTimer.Interval = 3600000; //3600000 1 hora em milissegundos | 60000 1 min
            taskTimer.Elapsed += TaskTimer_Elapsed;
            taskTimer.AutoReset = true; // Repetir automaticamente

            InitializeComponent();

            txtLog = txtBoxLog;
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
                    taskTimer.Start(); //await
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
            // Execução do processo
            //txtBoxLog.Text = "";
            if (dtInicial.SelectionStart <= dtFinal.SelectionStart)
            {
                Cursor.Current = Cursors.WaitCursor;
                var param = new DadosMov
                {
                    ChaveMov = textChaveMov.Text,
                    //CNPJMov = cbxCNPJMov.Text.Replace(".", "").Replace("/", "").Replace("-", ""),
                    CNPJMov = ObterTextoCbxCNPJMov().Replace(".", "").Replace("/", "").Replace("-", ""),
                    Todos = chkCNPJ.Checked,
                    DataI = dtInicial.SelectionStart.ToString("yyyy-MM-dd"),
                    DataF = dtFinal.SelectionStart.ToString("yyyy-MM-dd")
                };
                await LogHelper.GravarLog(contador + "° Requisição.");
                contador++;
                // Cria uma instância da classe ProdutosInventario e executa o processamento
                Movimento proc = new Movimento();
                await proc.IniProcesso(param);
                Cursor.Current = Cursors.Default;

            }
            else
            {
                MessageBox.Show("Período inválido!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private string ObterTextoCbxCNPJMov()
        {
            if (cbxCNPJMov.InvokeRequired)
            {
                // Executa a ação no thread da UI
                return (string)cbxCNPJMov.Invoke(new Func<string>(() => cbxCNPJMov.Text));
            }
            else
            {
                return cbxCNPJMov.Text;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            cbxCNPJMov.Text = "";
            cbxCNPJMov.Enabled = !chkCNPJ.Checked;
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtBoxLog.Text = "";
        }
    }
}
