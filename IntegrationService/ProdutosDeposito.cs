using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Diagnostics;

namespace IntegrationService
{
    class ProdutosDeposito
    {
        public async Task IniciarProcesso(DadosDep _param)
        {
            string[] listaDeOpcoes = { "86046463002749", "86046463002820", "86046463002900", "86046463000967", "86046463003125" };

            if (_param.TodosDep)
            {
                foreach (string cnpj in listaDeOpcoes)
                {
                    _param.CNPJDep = cnpj;
                    await LogHelper.GravarLog("-------------------------------------------------------------------------------");
                    await LogHelper.GravarLog("Iniciando empresa: " +
                    string.Format("{0}.{1}.{2}/{3}-{4}", _param.CNPJDep.Substring(0, 2), _param.CNPJDep.Substring(2, 3), _param.CNPJDep.Substring(5, 3), _param.CNPJDep.Substring(8, 4), _param.CNPJDep.Substring(12, 2)));
                    await GetDeposito(_param);
                }
            }
            else
            {
                await LogHelper.GravarLog("------------------------------------------------------------------------------");
                await LogHelper.GravarLog("Iniciando empresa: " +
                string.Format("{0}.{1}.{2}/{3}-{4}", _param.CNPJDep.Substring(0, 2), _param.CNPJDep.Substring(2, 3), _param.CNPJDep.Substring(5, 3), _param.CNPJDep.Substring(8, 4), _param.CNPJDep.Substring(12, 2)));
                // Chama o método assíncrono
                await GetDeposito(_param);
            }
            await LogHelper.GravarLog("-------------------------------------------------------------------------------");
            await LogHelper.GravarLog("Processamentos concluídos.");
            await LogHelper.GravarLog("Próxima integração as: " + DateTime.Now.AddHours(1) + "\n");
        }


        public static async Task GetDeposito(DadosDep _param)
        {
            string apiUrl = "https://webapi.microvix.com.br/1.0/api/integracao", st = "";

            // Construir o XML como string
            string xmlData = @"<?xml version='1.0' encoding='utf-8' ?>
                    <LinxMicrovix>
                     <Authentication user='linx_export' password='linx_export' />
                     <ResponseFormat>xml</ResponseFormat>
                     <Command>
	                    <Name>LinxProdutosDetalhesDepositos</Name>
	                    <Parameters>
                            <Parameter id='cnpjEmp'>" + _param.CNPJDep + "</Parameter>" +
                            "<Parameter id='chave'>" + _param.ChaveDep + "</Parameter>" +
                            "<Parameter id='cod_deposito'>" + _param.Depositos + "</Parameter>" +
                            "<Parameter id='timestamp'>0</Parameter>" +
                        "</Parameters>" +
                     "</Command>" +
                    "</LinxMicrovix>";

            // Criando uma instância do HttpClient
            using (HttpClient client = new HttpClient())
            {
                // Criando o conteúdo da requisição com o tipo de mídia 'application/xml'
                StringContent content = new StringContent(xmlData, Encoding.UTF8, "application/xml");

                // Enviando a requisição POST
                await LogHelper.GravarLog("Enviando a requisição POST..");
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    // Lendo a resposta da API
                    await LogHelper.GravarLog("Lendo a resposta da API..");
                    string result = await response.Content.ReadAsStringAsync();
                    await LogHelper.GravarLog("Processando..");

                    st = ConnectBD(result);
                    await LogHelper.GravarLog(st);
                }
                else
                {
                    await LogHelper.GravarLog("Erro ao conectar ana API: " + response.StatusCode);
                }
            }
        }

        static string ConnectBD(string _result)
        {
            Credential credentials = Credentials.LoadCredentials();
            if (credentials == null)
            {
                return "As credenciais não foram encontradas.";
            }
            // Defina a string de conexão
            string connectionString = $"Server=VMSRVSQL01;Database=LINX;User Id={credentials.User};Password={credentials.Password};";
            string st = "";
            // Cria uma instância de SqlConnection com a string de conexão.
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Abrir a conexão com o banco de dados.
                    connection.Open();

                    // Chama a estrutura de gravação
                    st = Gravar(_result, connection);

                }
                catch (SqlException ex)
                {
                    connection.Dispose();
                    st = "Erro ao conectar ao banco de dados: " + ex.Message;
                }
                // A conexão será fechada automaticamente ao sair do bloco using.
                connection.Dispose();
            }
            return st;
        }

        static string Gravar(string _texto, SqlConnection _connection)
        {
            string status = "";
            int qtd = 0;
            try
            {
                // Query SQL de inserção
                string query = "INSERT INTO ProdutosDetalhesDeposito (Portal, CNPJEmp, CodProduto, Empresa, CodDeposito, Saldo, Timestamp) " +
                                "VALUES (@Portal, @CNPJEmp, @CodProduto, @Empresa, @CodDeposito, @Saldo, @Timestamp)";

                string[] campos = { "Portal", "CNPJEmp", "CodProduto", "Empresa", "CodDeposito", "Saldo", "Timestamp" };

                // Define a expressão regular para capturar o conteúdo entre as tags <D> e </D>
                string patternR = @"<R>(.*?)</R>";
                string patternD = @"<D>(.*?)</D>";
                _texto = _texto.Replace("<D />", "<D></D>");

                int cont = 0;
                // Cria uma lista para armazenar os conteúdos
                List<string> listValores = new List<string>();
                _texto = _texto.Replace("<D />", "<D></D>");

                MatchCollection matchesR = Regex.Matches(_texto, patternR);
                foreach (Match matchR in matchesR)
                {
                    string valorR = matchR.Groups[1].Value;

                    // Usa o Regex para encontrar as correspondências
                    MatchCollection matchesD = Regex.Matches(valorR, patternD);

                    cont = 0;
                    // Cria o comando SQL e faz a gravação no banco
                    using (SqlCommand command = new SqlCommand(query, _connection))
                    {
                        string[] valores = new string[7];
                        foreach (Match matchD in matchesD)
                        {
                            // Obtém o conteúdo entre as tags <D> e </D> e adiciona na lista
                            string valor = matchD.Groups[1].Value;

                            listValores.Add(valor);

                            command.Parameters.AddWithValue("@" + campos[cont], valor);
                            valores[cont] = valor;
                            cont++;
                        }

                        //Valida se já existe no BD
                        string queryExists = "SELECT COUNT(*) FROM ProdutosDetalhesDeposito WHERE CNPJEmp = @CNPJEmp AND CodProduto = @CodProduto AND CodDeposito = @CodDeposito";
                        using (SqlCommand command1 = new SqlCommand(queryExists, _connection))
                        {
                            // Parametros da consulta
                            command1.Parameters.AddWithValue("@CNPJEmp",     valores[1]);
                            command1.Parameters.AddWithValue("@CodProduto",  valores[2]);
                            command1.Parameters.AddWithValue("@CodDeposito", valores[4]);

                            // Executa a consulta e obtém o resultado
                            int count = (int)command1.ExecuteScalar();

                            //Verifica se a informação existe no banco de dados
                            if (count > 0)
                            {
                                // Atualizar o registro existente
                                string queryUpdate = "UPDATE ProdutosDetalhesDeposito SET Saldo = @Saldo WHERE CNPJEmp = @CNPJEmp AND CodProduto = @CodProduto AND CodDeposito = @CodDeposito";
                                using (SqlCommand updateCommand = new SqlCommand(queryUpdate, _connection))
                                {
                                    updateCommand.Parameters.AddWithValue("@Saldo",         valores[5]);
                                    updateCommand.Parameters.AddWithValue("@CNPJEmp",       valores[1]);
                                    updateCommand.Parameters.AddWithValue("@CodProduto",    valores[2]);
                                    updateCommand.Parameters.AddWithValue("@CodDeposito",   valores[4]);

                                    int rowsAffected = updateCommand.ExecuteNonQuery();
                                    qtd++;
                                    if (rowsAffected > 0)
                                    {
                                        status = qtd + " registros salvos com sucesso.";
                                    }
                                    else
                                    {
                                        status = "Erro ao executar o comando SQL: " + queryUpdate;
                                        //throw new Exception("Erro ao executar o comando SQL: " + query);
                                    }
                                }
                            }
                            else
                            {

                                int rowsAffected = command.ExecuteNonQuery();
                                qtd++;
                                if (rowsAffected > 0)
                                {
                                    status = qtd + " registros salvos com sucesso!";
                                }
                                else
                                {
                                    status = "Erro ao executar o comando SQL: " + query;
                                    //throw new Exception("Erro ao executar o comando SQL: " + query);
                                }
                            }
                        }
                    }
                }
                if (cont == 0) status = "Não existem registros para integrar.";
                else if (qtd == 0) status = "Registros já existentes na base de dados.";
            }
            catch (SqlException ex)
            {
                status = "Erro ao gravar no banco de dados: " + ex.Message;
            }
            return status;
        }
    }
}
