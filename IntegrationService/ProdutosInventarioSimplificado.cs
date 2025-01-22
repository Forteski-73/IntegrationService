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
    class ProdutosInventarioSimplificado
    {
        public async Task IniciarProcesso(DadosRe _param)
        {
            string[] listaDeOpcoes = { "86046463002749", "86046463002820", "86046463002900", "86046463000967", "86046463003125" };

            if (_param.TodosRe)
            {
                foreach (string cnpj in listaDeOpcoes)
                {
                    _param.CNPJRe = cnpj;
                    await LogHelper.GravarLog("-------------------------------------------------------------------------------");
                    await LogHelper.GravarLog("Iniciando empresa: " +
                    string.Format("{0}.{1}.{2}/{3}-{4}", _param.CNPJRe.Substring(0, 2), _param.CNPJRe.Substring(2, 3), _param.CNPJRe.Substring(5, 3), _param.CNPJRe.Substring(8, 4), _param.CNPJRe.Substring(12, 2)));
                    await GetProdutos(_param);
                }
            }
            else
            {
                await LogHelper.GravarLog("------------------------------------------------------------------------------");
                await LogHelper.GravarLog("Iniciando empresa: " +
                string.Format("{0}.{1}.{2}/{3}-{4}", _param.CNPJRe.Substring(0, 2), _param.CNPJRe.Substring(2, 3), _param.CNPJRe.Substring(5, 3), _param.CNPJRe.Substring(8, 4), _param.CNPJRe.Substring(12, 2)));
                // Chama o método assíncrono
                await GetProdutos(_param);
            }
            await LogHelper.GravarLog("-------------------------------------------------------------------------------");
            await LogHelper.GravarLog("Processamentos concluídos.");
            await LogHelper.GravarLog("Próxima integração as: " + DateTime.Now.AddHours(1) + "\n");
        }

        public static async Task GetProdutos(DadosRe _param)
        {
            string apiUrl = "https://webapi.microvix.com.br/1.0/api/integracao", st = "";

            // Construir o XML como string
            string xmlData = @"<?xml version='1.0' encoding='utf-8' ?>
                    <LinxMicrovix>
                     <Authentication user='linx_export' password='linx_export' />
                     <ResponseFormat>xml</ResponseFormat>
                     <Command>
	                    <Name>LinxProdutosDetalhesSimplificado</Name>
	                    <Parameters>
                            <Parameter id='cnpjEmp'>"+_param.CNPJRe+"</Parameter>"+
		                    "<Parameter id='chave'>"+_param.ChaveRe+"</Parameter>"+
		                    "<Parameter id='timestamp'>0</Parameter>"+
		                    "<Parameter id='cod_produto'>"+_param.Produto+"</Parameter>"+
                            "<Parameter id='somente_com_saldo'>" + Convert.ToInt32(_param.Saldo)+ "</Parameter>" +
	                    "</Parameters>"+
                     "</Command>"+
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

                    st = ConnectBD(result, _param.Saldo);
                    await LogHelper.GravarLog(st);
                }
                else
                {
                    await LogHelper.GravarLog("Erro ao conectar ana API: " + response.StatusCode);
                }
            }
        }

        static string ConnectBD(string _result, Boolean _saldo)
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
                    st = Gravar(_result, connection, _saldo);

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

        static string Gravar(string _texto, SqlConnection _connection, Boolean _saldo)
        {
            string status = "";
            int qtd = 0;
            Boolean gravar = true;
            Boolean thereNewFields = false;
            try
            {
                // Query SQL de inserção
                string query = "INSERT INTO ProdutosSimplificado (Portal, Empresa, CNPJEmp, CodProduto, Quantidade, IdConfigTributaria, Timestamp) " +
                                "VALUES (@Portal, @Empresa, @CNPJEmp, @CodProduto, @Quantidade, @IdConfigTributaria, @Timestamp)";

                string[] campos = { "Portal", "Empresa", "CNPJEmp", "CodProduto", "Quantidade", "IdConfigTributaria", "Timestamp" };

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
                        gravar = true;
                        string[] valores = new string[7];
                        foreach (Match matchD in matchesD)
                        {
                            if (cont > 6)
                            {
                                // Interrompe o loopingo por que se passar de 110 posições é por que o retorno da API
                                // está trazendo campos a mais que precisam ser atualizados nesse escopo
                                thereNewFields = true;
                                cont = 6;
                                break;
                            }
                            // Obtém o conteúdo entre as tags <D> e </D> e adiciona na lista
                            string valor = matchD.Groups[1].Value;

                            if (_saldo && (campos[cont] == "Quantidade") && decimal.Parse(valor) == 0)
                            {
                                gravar = false;
                                break;
                            }

                            listValores.Add(valor);

                            command.Parameters.AddWithValue("@" + campos[cont], valor);
                            valores[cont] = valor;
                            cont++;
                        }
                        if (gravar)
                        {
                            //Valida se já existe no BD
                            string queryExists = "SELECT COUNT(*) FROM ProdutosSimplificado WHERE CNPJEmp = @CNPJEmp AND CodProduto = @CodProduto";
                            using (SqlCommand command1 = new SqlCommand(queryExists, _connection))
                            {
                                // Parametros da consulta
                                command1.Parameters.AddWithValue("@CNPJEmp", valores[2]);
                                command1.Parameters.AddWithValue("@CodProduto", valores[3]);

                                // Executa a consulta e obtém o resultado
                                int count = (int)command1.ExecuteScalar();

                                //Verifica se a informação existe no banco de dados
                                if (count > 0)
                                {
                                    // Atualizar o registro existente
                                    string queryUpdate = "UPDATE ProdutosSimplificado SET Quantidade = @Quantidade WHERE CNPJEmp = @CNPJEmp AND CodProduto = @CodProduto";
                                    using (SqlCommand updateCommand = new SqlCommand(queryUpdate, _connection))
                                    {
                                        updateCommand.Parameters.AddWithValue("@Quantidade", valores[4]);
                                        updateCommand.Parameters.AddWithValue("@CNPJEmp", valores[2]);
                                        updateCommand.Parameters.AddWithValue("@CodProduto", valores[3]);

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
                            if (thereNewFields)
                            {
                                status = status + "\n\n*Existem campos novos na API que não foram integrados*\n";
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

    // Object filtros da tela
    public class DadosRe
    {
        public string ChaveRe { get; set; }
        public string CNPJRe { get; set; }
        public Boolean TodosRe { get; set; }
        public string Produto { get; set; }
        public Boolean Saldo { get; set; }
    }
}
