using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IntegrationService
{
    public static class LogHelper
    {
        private static RichTextBox _campoLog;

        /// Configura o campo de texto onde os logs serão exibidos.
        public static void ConfigurarCampoLog(RichTextBox campoLog)
        {
            _campoLog = campoLog;
        }

        /// Método estático e assíncrono que grava logs no campo de texto.
        public static async Task GravarLog(string mensagem)
        {
            //string logCompleto = "";
            if (_campoLog == null)
                throw new InvalidOperationException("Campo de log não configurado.");

            // Log formatado
            string logCompleto = $"{DateTime.Now}: {mensagem}\n";

            // Atualiza o campo de log na UI thread
            if (_campoLog.InvokeRequired)
            {
                _campoLog.Invoke((Action)(() =>
                {
                    _campoLog.AppendText(logCompleto);
                }));
            }
            else
            {
                _campoLog.AppendText(logCompleto);
            }
        }
    }
}
