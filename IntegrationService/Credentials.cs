using System;
using System.IO;
using System.Windows.Forms;

namespace IntegrationService
{
    public class Credential
    {
        public string User { get; set; }
        public string Password { get; set; }

        public Credential(string user, string password)
        {
            User = user;
            Password = password;
        }
    }

    public static class Credentials
    {
        // Salva as credenciais em texto puro no arquivo
        public static bool SaveCredentials(string user, string password)
        {
            try
            {
                string tempPath = Path.GetTempPath();
                string filePath = Path.Combine(tempPath, "credentials.txt");

                // Salva as credenciais no arquivo como texto simples
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine($"Usuário: {user}");
                    writer.WriteLine($"Senha: {password}");
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar os dados: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Carrega as credenciais do arquivo
        public static Credential LoadCredentials()
        {
            try
            {
                string tempPath = Path.GetTempPath();
                string filePath = Path.Combine(tempPath, "credentials.txt");

                if (!File.Exists(filePath))
                {
                    MessageBox.Show("O arquivo de credenciais não foi encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
                }

                string user = null;
                string password = null;

                // Lê as credenciais do arquivo
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.StartsWith("Usuário: "))
                            user = line.Replace("Usuário: ", "").Trim();
                        else if (line.StartsWith("Senha: "))
                            password = line.Replace("Senha: ", "").Trim();
                    }
                }

                if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("As credenciais no arquivo estão incompletas.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
                }

                return new Credential(user, password);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar as credenciais: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
