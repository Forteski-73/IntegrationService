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
    public class Movimento
    {
        public async Task IniProcesso(DadosMov _param)
        {
            string[] listaDeOpcoes = { "86046463002749", "86046463002820", "86046463002900", "86046463000967", "86046463003125" };

            if (_param.Todos)
            {
                foreach (string cnpj in listaDeOpcoes)
                {
                    _param.CNPJMov = cnpj;
                    await LogHelper.GravarLog("-------------------------------------------------------------------------------");
                    await LogHelper.GravarLog("Iniciando empresa: " +
                        string.Format("{0}.{1}.{2}/{3}-{4}", _param.CNPJMov.Substring(0, 2), _param.CNPJMov.Substring(2, 3), _param.CNPJMov.Substring(5, 3), _param.CNPJMov.Substring(8, 4), _param.CNPJMov.Substring(12, 2)));
                    await LogHelper.GravarLog("Período.: " + DateTime.ParseExact(_param.DataI, "yyyy-MM-dd", null).ToString("dd/MM/yyyy") + " até " +
                        DateTime.ParseExact(_param.DataF, "yyyy-MM-dd", null).ToString("dd/MM/yyyy"));
                    // Chama o método assíncrono
                    await GetProdutos(_param);
                }

            }
            else
            {
                await LogHelper.GravarLog("-------------------------------------------------------------------------------");
                await LogHelper.GravarLog("Iniciando empresa: " +
                        string.Format("{0}.{1}.{2}/{3}-{4}", _param.CNPJMov.Substring(0, 2), _param.CNPJMov.Substring(2, 3), _param.CNPJMov.Substring(5, 3), _param.CNPJMov.Substring(8, 4), _param.CNPJMov.Substring(12, 2)));
                await LogHelper.GravarLog("Período.: " + DateTime.ParseExact(_param.DataI, "yyyy-MM-dd", null).ToString("dd/MM/yyyy") + " até " +
                    DateTime.ParseExact(_param.DataF, "yyyy-MM-dd", null).ToString("dd/MM/yyyy"));
                // Chama o método assíncrono
                await GetProdutos(_param);
            }
            await LogHelper.GravarLog("-------------------------------------------------------------------------------");
            await LogHelper.GravarLog("Processamentos concluídos.");
            await LogHelper.GravarLog("Próxima integração as: " + DateTime.Now.AddHours(1) + "\n");
        }

        public static async Task GetProdutos(DadosMov _param)
        {
            string apiUrl = "https://webapi.microvix.com.br/1.0/api/integracao", st = "";

            // Construir o XML como string
            string xmlData = @"<?xml version='1.0' encoding='utf-8' ?>
            <LinxMicrovix>
             <Authentication user='linx_export' password='linx_export'/>
             <ResponseFormat>xml</ResponseFormat>
             <Command>
	            <Name>LinxMovimento</Name>
	            <Parameters>
		            <Parameter id='chave'>"+_param.ChaveMov+"</Parameter>"+
		            "<Parameter id='cnpjEmp'>"+_param.CNPJMov+"</Parameter>"+
		            "<Parameter id='timestamp'>0</Parameter>"+
		            "<Parameter id='data_inicial'>"+_param.DataI+"</Parameter>"+
		            "<Parameter id='data_fim'>"+_param.DataF+"</Parameter>"+
	            "</Parameters>"+
             "</Command>"+
            "</LinxMicrovix>";
            try
            {
                // Criando uma instância do HttpClient
                using (HttpClient client = new HttpClient())
                {
                    // Criando o conteúdo da requisição com o tipo de mídia 'application/xml'
                    StringContent content = new StringContent(xmlData, Encoding.UTF8, "application/xml");


                    //client.Timeout = TimeSpan.FromSeconds(30);
                    client.Timeout = TimeSpan.FromMinutes(59);

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
            catch (Exception ex)
            {
                // Tratamento genérico para outras exceções
                Console.WriteLine("Ocorreu um erro: " + ex.Message);
            }
            finally
            {

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
                connection.Dispose();
            }   // A conexão será fechada automaticamente ao sair do bloco using.
            return st;
        }

        static string Gravar(string _texto, SqlConnection _connection)
        {
            string status = "";
            int qtd = 0;
            Boolean gravar = true;
            try
            {
                // Query SQL de inserção
                string query = "INSERT INTO MovimentoLoja (Portal, CNPJEmp, Transacao, Usuario, Documento, ChaveNF, ECF, NumeroSerieECF," +
                        "ModeloNF, DataDocumento, DataLancamento, CodigoCliente, Serie, DescCFOP, IDCFOP, CodVendedor, Quantidade,"+
                        "PrecoCusto, ValorLiquido, Desconto, CSTICMS, CSTPIS, CSTCOFINS, CSTIPI, ValorICMS, AliquotaICMS, BaseICMS,"+
                        "ValorPIS, AliquotaPIS, BasePIS, ValorCOFINS, AliquotaCOFINS, BaseCOFINS, ValorICMSST, AliquotaICMSST,"+
                        "BaseICMSST, ValorIPI, AliquotaIPI, BaseIPI, ValorTotal, FormaDinheiro, TotalDinheiro, FormaCheque,"+
                        "TotalCheque, FormaCartao, TotalCartao, FormaCrediario, TotalCrediario, FormaConvenio, TotalConvenio,"+
                        "Frete, Operacao, TipoTransacao, CodProduto, CodBarra, Cancelado, Excluido, SomaRelatorio, Identificador,"+
                        "Deposito, Obs, PrecoUnitario, HoraLancamento, NaturezaOperacao, TabelaPreco, NomeTabelaPreco, CodSefazSituacao,"+
                        "DescSefazSituacao, ProtocoloAutNFE, DTUpdate, FormaChequePrazo, TotalChequePrazo, CodNaturezaOperacao,"+
                        "PrecoTabelaEpoca, DescontoTotalItem, Conferido, TransacaoPedidoVenda, CodigoModeloNF, Acrescimo, MobCheckOut," +
                        "AliquotaISS, BaseISS, Ordem, CodRotinaRrigem, Timestamp, Troco, Transportador, ICMSAliquotaDesonerado,"+
                        "ICMSValorDesoneradoItem, Empresa, DescontoItem, AliqISS, ISSBaseItem, Despesas, SeguroTotalItem,"+
                        "AcrescimoTotalItem, DespesasTotalItem, FormaPix, Totalpix, FormaDepositoDancario, TotalDepositoBancario,"+
                        "IDVendaProdutoB2C, ItemPromocional, AcrescimoItem, ICMSSTAntecipadoAliquota, ICMSSTAntecipadoMargem,"+
                        "ICMSSTAntecipadoPercReducao, ICMSSTAntecipadoValorItem, ICMSBaseDesoneradoItem, CodigoStatusNFE, PrecoProdutoEpoca)" +
                        
                        "VALUES (@Portal, @CNPJEmp, @Transacao, @Usuario, @Documento, @ChaveNF, @ECF, @NumeroSerieECF,"+
                        "@ModeloNF, @DataDocumento, @DataLancamento, @CodigoCliente, @Serie, @DescCFOP, @IDCFOP, @CodVendedor, @Quantidade,"+
                        "@PrecoCusto, @ValorLiquido, @Desconto, @CSTICMS, @CSTPIS, @CSTCOFINS, @CSTIPI, @ValorICMS, @AliquotaICMS, @BaseICMS,"+
                        "@ValorPIS, @AliquotaPIS, @BasePIS, @ValorCOFINS, @AliquotaCOFINS, @BaseCOFINS, @ValorICMSST, @AliquotaICMSST,"+
                        "@BaseICMSST, @ValorIPI, @AliquotaIPI, @BaseIPI, @ValorTotal, @FormaDinheiro, @TotalDinheiro, @FormaCheque,"+
                        "@TotalCheque, @FormaCartao, @TotalCartao, @FormaCrediario, @TotalCrediario, @FormaConvenio, @TotalConvenio,"+
                        "@Frete, @Operacao, @TipoTransacao, @CodProduto, @CodBarra, @Cancelado, @Excluido, @SomaRelatorio, @Identificador,"+
                        "@Deposito, @Obs, @PrecoUnitario, @HoraLancamento, @NaturezaOperacao, @TabelaPreco, @NomeTabelaPreco, @CodSefazSituacao,"+
                        "@DescSefazSituacao, @ProtocoloAutNFE, @DTUpdate, @FormaChequePrazo, @TotalChequePrazo, @CodNaturezaOperacao,"+
                        "@PrecoTabelaEpoca, @DescontoTotalItem, @Conferido, @TransacaoPedidoVenda, @CodigoModeloNF, @Acrescimo, @MobCheckOut," +
                        "@AliquotaISS, @BaseISS, @Ordem, @CodRotinaRrigem, @Timestamp, @Troco, @Transportador, @ICMSAliquotaDesonerado,"+
                        "@ICMSValorDesoneradoItem, @Empresa, @DescontoItem, @AliqISS, @ISSBaseItem, @Despesas, @SeguroTotalItem,"+
                        "@AcrescimoTotalItem, @DespesasTotalItem, @FormaPix, @Totalpix, @FormaDepositoDancario, @TotalDepositoBancario,"+
                        "@IDVendaProdutoB2C, @ItemPromocional, @AcrescimoItem, @ICMSSTAntecipadoAliquota, @ICMSSTAntecipadoMargem," +
                        "@ICMSSTAntecipadoPercReducao, @ICMSSTAntecipadoValorItem, @ICMSBaseDesoneradoItem, @CodigoStatusNFE, @PrecoProdutoEpoca)";

                string[] campos = { "Portal", "CNPJEmp", "Transacao", "Usuario", "Documento", "ChaveNF", "ECF", "NumeroSerieECF", 
                            "ModeloNF", "DataDocumento", "DataLancamento", "CodigoCliente", "Serie", "DescCFOP", "IDCFOP", "CodVendedor", "Quantidade",
                            "PrecoCusto", "ValorLiquido", "Desconto", "CSTICMS", "CSTPIS", "CSTCOFINS", "CSTIPI", "ValorICMS", "AliquotaICMS", "BaseICMS",
                            "ValorPIS", "AliquotaPIS", "BasePIS", "ValorCOFINS", "AliquotaCOFINS", "BaseCOFINS", "ValorICMSST", "AliquotaICMSST",
                            "BaseICMSST", "ValorIPI", "AliquotaIPI", "BaseIPI", "ValorTotal", "FormaDinheiro", "TotalDinheiro", "FormaCheque",
                            "TotalCheque", "FormaCartao", "TotalCartao", "FormaCrediario", "TotalCrediario", "FormaConvenio", "TotalConvenio",
                            "Frete", "Operacao", "TipoTransacao", "CodProduto", "CodBarra", "Cancelado", "Excluido", "SomaRelatorio", "Identificador",
                            "Deposito", "Obs", "PrecoUnitario", "HoraLancamento", "NaturezaOperacao", "TabelaPreco", "NomeTabelaPreco", "CodSefazSituacao",
                            "DescSefazSituacao", "ProtocoloAutNFE", "DTUpdate", "FormaChequePrazo", "TotalChequePrazo", "CodNaturezaOperacao",
                            "PrecoTabelaEpoca", "DescontoTotalItem", "Conferido", "TransacaoPedidoVenda", "CodigoModeloNF", "Acrescimo", "MobCheckOut",
                            "AliquotaISS", "BaseISS", "Ordem", "CodRotinaRrigem", "Timestamp", "Troco", "Transportador", "ICMSAliquotaDesonerado",
                            "ICMSValorDesoneradoItem", "Empresa", "DescontoItem", "AliqISS", "ISSBaseItem", "Despesas", "SeguroTotalItem",
                            "AcrescimoTotalItem", "DespesasTotalItem", "FormaPix", "Totalpix", "FormaDepositoDancario", "TotalDepositoBancario",
                            "IDVendaProdutoB2C", "ItemPromocional", "AcrescimoItem", "ICMSSTAntecipadoAliquota", "ICMSSTAntecipadoMargem",
                            "ICMSSTAntecipadoPercReducao", "ICMSSTAntecipadoValorItem", "ICMSBaseDesoneradoItem", "CodigoStatusNFE", "PrecoProdutoEpoca" };

                // Define a expressão regular para capturar o conteúdo entre as tags <D> e </D>
                string patternR = @"<R>(.*?)</R>";
                string patternD = @"<D>(.*?)</D>";
                _texto = _texto.Replace("<D />", "<D></D>");

                int cont = 0;
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
                        string[] valores = new string[111];
                        foreach (Match matchD in matchesD)
                        {
                            // Obtém o conteúdo entre as tags <D> e </D> e adiciona na lista
                            string valor = matchD.Groups[1].Value;

                            if (cont == 79 || cont == 99 || cont == 102 || cont == 103)
                            {
                                if (valor == "False" || valor == "0.0000")
                                    valor = "0";
                                else if(valor == "True")
                                    valor = "1";
                            }

                            command.Parameters.AddWithValue("@" + campos[cont], valor);

                            valores[cont] = valor;
                            cont++;
                        }

                        if (gravar)
                        {
                            //Valida se já existe no BD
                            string queryExists = "SELECT COUNT(*) FROM MovimentoLoja " +
                                "WHERE ChaveNF = @ChaveNF AND CNPJEmp = @CNPJEmp AND Operacao = @Operacao AND TipoTransacao = @TipoTransacao AND " +
                                        "Transacao = @Transacao AND Identificador = @Identificador AND CodigoCliente = @CodigoCliente AND " +
                                        "CodProduto = @CodProduto AND Documento = @Documento";
                            using (SqlCommand command1 = new SqlCommand(queryExists, _connection))
                            {
                                // Parametros da consulta
                                command1.Parameters.AddWithValue("@ChaveNF", valores[5]);
                                command1.Parameters.AddWithValue("@CNPJEmp", valores[1]);
                                command1.Parameters.AddWithValue("@Operacao", valores[51]);
                                command1.Parameters.AddWithValue("@TipoTransacao", valores[52]);
                                command1.Parameters.AddWithValue("@Transacao", valores[2]);
                                command1.Parameters.AddWithValue("@Identificador", valores[58]);
                                command1.Parameters.AddWithValue("@CodigoCliente", valores[11]);
                                command1.Parameters.AddWithValue("@CodProduto", valores[53]);
                                command1.Parameters.AddWithValue("@Documento", valores[4]);

                                // Executa a consulta e obtém o resultado
                                int count = (int)command1.ExecuteScalar();

                                //Verifica se a informação existe no banco de dados
                                if (count > 0)
                                {
                                    // Atualizar o registro existente
                                    string queryUpdate = "UPDATE MovimentoLoja SET Portal = @Portal,"+
                                                            "Usuario = @Usuario,"+
                                                            "ECF = @ECF,"+
                                                            "NumeroSerieECF = @NumeroSerieECF,"+
                                                            "ModeloNF = @ModeloNF,"+
                                                            "DataDocumento = @DataDocumento,"+
                                                            "DataLancamento = @DataLancamento,"+
                                                            "Serie = @Serie,"+
                                                            "DescCFOP = @DescCFOP,"+
                                                            "IDCFOP = @IDCFOP,"+
                                                            "CodVendedor = @CodVendedor,"+
                                                            "Quantidade = @Quantidade,"+
                                                            "PrecoCusto = @PrecoCusto,"+
                                                            "ValorLiquido = @ValorLiquido,"+
                                                            "Desconto = @Desconto,"+
                                                            "CSTICMS = @CSTICMS,"+
                                                            "CSTPIS = @CSTPIS,"+
                                                            "CSTCOFINS = @CSTCOFINS,"+
                                                            "CSTIPI = @CSTIPI,"+
                                                            "ValorICMS = @ValorICMS,"+
                                                            "AliquotaICMS = @AliquotaICMS,"+
                                                            "BaseICMS = @BaseICMS,"+
                                                            "ValorPIS = @ValorPIS,"+
                                                            "AliquotaPIS = @AliquotaPIS,"+
                                                            "BasePIS = @BasePIS,"+
                                                            "ValorCOFINS = @ValorCOFINS,"+
                                                            "AliquotaCOFINS = @AliquotaCOFINS,"+
                                                            "BaseCOFINS = @BaseCOFINS,"+
                                                            "ValorICMSST = @ValorICMSST,"+
                                                            "AliquotaICMSST = @AliquotaICMSST,"+
                                                            "BaseICMSST = @BaseICMSST,"+
                                                            "ValorIPI = @ValorIPI,"+
                                                            "AliquotaIPI = @AliquotaIPI,"+
                                                            "BaseIPI = @BaseIPI,"+
                                                            "ValorTotal = @ValorTotal,"+
                                                            "FormaDinheiro = @FormaDinheiro,"+
                                                            "TotalDinheiro = @TotalDinheiro,"+
                                                            "FormaCheque = @FormaCheque,"+
                                                            "TotalCheque = @TotalCheque,"+
                                                            "FormaCartao = @FormaCartao,"+
                                                            "TotalCartao = @TotalCartao,"+
                                                            "FormaCrediario = @FormaCrediario,"+
                                                            "TotalCrediario = @TotalCrediario,"+
                                                            "FormaConvenio = @FormaConvenio,"+
                                                            "TotalConvenio = @TotalConvenio,"+
                                                            "Frete = @Frete,"+
                                                            "CodBarra = @CodBarra,"+
                                                            "Cancelado = @Cancelado,"+
                                                            "Excluido = @Excluido,"+
                                                            "SomaRelatorio = @SomaRelatorio,"+
                                                            "Deposito = @Deposito,"+
                                                            "Obs = @Obs,"+
                                                            "PrecoUnitario = @PrecoUnitario,"+
                                                            "HoraLancamento = @HoraLancamento,"+
                                                            "NaturezaOperacao = @NaturezaOperacao,"+
                                                            "TabelaPreco = @TabelaPreco,"+
                                                            "NomeTabelaPreco = @NomeTabelaPreco,"+
                                                            "CodSefazSituacao = @CodSefazSituacao,"+
                                                            "DescSefazSituacao = @DescSefazSituacao,"+
                                                            "ProtocoloAutNFE = @ProtocoloAutNFE,"+
                                                            "DTUpdate = @DTUpdate,"+
                                                            "FormaChequePrazo = @FormaChequePrazo,"+
                                                            "TotalChequePrazo = @TotalChequePrazo,"+
                                                            "CodNaturezaOperacao = @CodNaturezaOperacao,"+
                                                            "PrecoTabelaEpoca = @PrecoTabelaEpoca,"+
                                                            "DescontoTotalItem = @DescontoTotalItem,"+
                                                            "Conferido = @Conferido,"+
                                                            "TransacaoPedidoVenda = @TransacaoPedidoVenda,"+
                                                            "CodigoModeloNF = @CodigoModeloNF,"+
                                                            "Acrescimo = @Acrescimo,"+
                                                            "MobCheckOut = @MobCheckOut,"+
                                                            "AliquotaISS = @AliquotaISS,"+
                                                            "BaseISS = @BaseISS,"+
                                                            "Ordem = @Ordem,"+
                                                            "CodRotinaRrigem = @CodRotinaRrigem,"+
                                                            "Timestamp = @Timestamp,"+
                                                            "Troco = @Troco,"+
                                                            "Transportador = @Transportador,"+
                                                            "ICMSAliquotaDesonerado = @ICMSAliquotaDesonerado,"+
                                                            "ICMSValorDesoneradoItem = @ICMSValorDesoneradoItem,"+
                                                            "Empresa = @Empresa,"+
                                                            "DescontoItem = @DescontoItem,"+
                                                            "AliqISS = @AliqISS,"+
                                                            "ISSBaseItem = @ISSBaseItem,"+
                                                            "Despesas = @Despesas,"+
                                                            "SeguroTotalItem = @SeguroTotalItem,"+
                                                            "AcrescimoTotalItem = @AcrescimoTotalItem,"+
                                                            "DespesasTotalItem = @DespesasTotalItem,"+
                                                            "FormaPix = @FormaPix,"+
                                                            "Totalpix = @Totalpix,"+
                                                            "FormaDepositoDancario = @FormaDepositoDancario,"+
                                                            "TotalDepositoBancario = @TotalDepositoBancario,"+
                                                            "IDVendaProdutoB2C = @IDVendaProdutoB2C,"+
                                                            "ItemPromocional = @ItemPromocional,"+
                                                            "AcrescimoItem = @AcrescimoItem,"+
                                                            "ICMSSTAntecipadoAliquota = @ICMSSTAntecipadoAliquota,"+
                                                            "ICMSSTAntecipadoMargem = @ICMSSTAntecipadoMargem,"+
                                                            "ICMSSTAntecipadoPercReducao = @ICMSSTAntecipadoPercReducao,"+
                                                            "ICMSSTAntecipadoValorItem = @ICMSSTAntecipadoValorItem,"+
                                                            "ICMSBaseDesoneradoItem = @ICMSBaseDesoneradoItem,"+
                                                            "CodigoStatusNFE = @CodigoStatusNFE,"+
                                                            "PrecoProdutoEpoca = @PrecoProdutoEpoca "+
                                        "WHERE ChaveNF = @ChaveNF AND CNPJEmp = @CNPJEmp AND Operacao = @Operacao AND TipoTransacao = @TipoTransacao AND " +
                                        "Transacao = @Transacao AND Identificador = @Identificador AND CodigoCliente = @CodigoCliente AND CodProduto = @CodProduto AND Documento = @Documento";
                                    using (SqlCommand updateCommand = new SqlCommand(queryUpdate, _connection))
                                    {

                                        updateCommand.Parameters.AddWithValue("@Portal",                valores[0]);
                                        //updateCommand.Parameters.AddWithValue("@CNPJEmp",               valores[1]);
                                        //updateCommand.Parameters.AddWithValue("@Transacao",             valores[2]);
                                        updateCommand.Parameters.AddWithValue("@Usuario",               valores[3]);
                                        //updateCommand.Parameters.AddWithValue("@Documento",             valores[4]);
                                        //updateCommand.Parameters.AddWithValue("@ChaveNF",               valores[5]);
                                        updateCommand.Parameters.AddWithValue("@ECF",                   valores[6]);
                                        updateCommand.Parameters.AddWithValue("@NumeroSerieECF",        valores[7]);
                                        updateCommand.Parameters.AddWithValue("@ModeloNF",              valores[8]);
                                        updateCommand.Parameters.AddWithValue("@DataDocumento",         valores[9]);
                                        updateCommand.Parameters.AddWithValue("@DataLancamento",        valores[10]);
                                        //updateCommand.Parameters.AddWithValue("@CodigoCliente",         valores[11]);
                                        updateCommand.Parameters.AddWithValue("@Serie",                 valores[12]);
                                        updateCommand.Parameters.AddWithValue("@DescCFOP",              valores[13]);
                                        updateCommand.Parameters.AddWithValue("@IDCFOP",                valores[14]);
                                        updateCommand.Parameters.AddWithValue("@CodVendedor",           valores[15]);
                                        updateCommand.Parameters.AddWithValue("@Quantidade",           valores[16]);
                                        updateCommand.Parameters.AddWithValue("@PrecoCusto",            valores[17]);
                                        updateCommand.Parameters.AddWithValue("@ValorLiquido",          valores[18]);
                                        updateCommand.Parameters.AddWithValue("@Desconto",              valores[19]);
                                        updateCommand.Parameters.AddWithValue("@CSTICMS",               valores[20]);
                                        updateCommand.Parameters.AddWithValue("@CSTPIS",                valores[21]);
                                        updateCommand.Parameters.AddWithValue("@CSTCOFINS",             valores[22]);
                                        updateCommand.Parameters.AddWithValue("@CSTIPI",                valores[23]);
                                        updateCommand.Parameters.AddWithValue("@ValorICMS",             valores[24]);
                                        updateCommand.Parameters.AddWithValue("@AliquotaICMS",          valores[25]);
                                        updateCommand.Parameters.AddWithValue("@BaseICMS",              valores[26]);
                                        updateCommand.Parameters.AddWithValue("@ValorPIS",              valores[27]);
                                        updateCommand.Parameters.AddWithValue("@AliquotaPIS",           valores[28]);
                                        updateCommand.Parameters.AddWithValue("@BasePIS",               valores[29]);
                                        updateCommand.Parameters.AddWithValue("@ValorCOFINS",           valores[30]);
                                        updateCommand.Parameters.AddWithValue("@AliquotaCOFINS",        valores[31]);
                                        updateCommand.Parameters.AddWithValue("@BaseCOFINS",            valores[32]);
                                        updateCommand.Parameters.AddWithValue("@ValorICMSST",           valores[33]);
                                        updateCommand.Parameters.AddWithValue("@AliquotaICMSST",        valores[34]);
                                        updateCommand.Parameters.AddWithValue("@BaseICMSST",            valores[35]);
                                        updateCommand.Parameters.AddWithValue("@ValorIPI",              valores[36]);
                                        updateCommand.Parameters.AddWithValue("@AliquotaIPI",           valores[37]);
                                        updateCommand.Parameters.AddWithValue("@BaseIPI",               valores[38]);
                                        updateCommand.Parameters.AddWithValue("@ValorTotal",            valores[39]);
                                        updateCommand.Parameters.AddWithValue("@FormaDinheiro",         valores[40]);
                                        updateCommand.Parameters.AddWithValue("@TotalDinheiro",         valores[41]);
                                        updateCommand.Parameters.AddWithValue("@FormaCheque",           valores[42]);
                                        updateCommand.Parameters.AddWithValue("@TotalCheque",           valores[43]);
                                        updateCommand.Parameters.AddWithValue("@FormaCartao",           valores[44]);
                                        updateCommand.Parameters.AddWithValue("@TotalCartao",           valores[45]);
                                        updateCommand.Parameters.AddWithValue("@FormaCrediario",        valores[46]);
                                        updateCommand.Parameters.AddWithValue("@TotalCrediario",        valores[47]);
                                        updateCommand.Parameters.AddWithValue("@FormaConvenio",         valores[48]);
                                        updateCommand.Parameters.AddWithValue("@TotalConvenio",         valores[49]);
                                        updateCommand.Parameters.AddWithValue("@Frete",                 valores[50]);
                                        //updateCommand.Parameters.AddWithValue("@Operacao",              valores[51]);
                                        //updateCommand.Parameters.AddWithValue("@TipoTransacao",         valores[52]);
                                        //updateCommand.Parameters.AddWithValue("@CodProduto",            valores[53]);
                                        updateCommand.Parameters.AddWithValue("@CodBarra",              valores[54]);
                                        updateCommand.Parameters.AddWithValue("@Cancelado",             valores[55]);
                                        updateCommand.Parameters.AddWithValue("@Excluido",              valores[56]);
                                        updateCommand.Parameters.AddWithValue("@SomaRelatorio",         valores[57]);
                                        //updateCommand.Parameters.AddWithValue("@Identificador",         valores[58]);
                                        updateCommand.Parameters.AddWithValue("@Deposito",              valores[59]);
                                        updateCommand.Parameters.AddWithValue("@Obs",                   valores[60]);
                                        updateCommand.Parameters.AddWithValue("@PrecoUnitario",         valores[61]);
                                        updateCommand.Parameters.AddWithValue("@HoraLancamento",        valores[62]);
                                        updateCommand.Parameters.AddWithValue("@NaturezaOperacao",      valores[63]);
                                        updateCommand.Parameters.AddWithValue("@TabelaPreco",           valores[64]);
                                        updateCommand.Parameters.AddWithValue("@NomeTabelaPreco",       valores[65]);
                                        updateCommand.Parameters.AddWithValue("@CodSefazSituacao",      valores[66]);
                                        updateCommand.Parameters.AddWithValue("@DescSefazSituacao",     valores[67]);
                                        updateCommand.Parameters.AddWithValue("@ProtocoloAutNFE",       valores[68]);
                                        updateCommand.Parameters.AddWithValue("@DTUpdate",              valores[69]);
                                        updateCommand.Parameters.AddWithValue("@FormaChequePrazo",      valores[70]);
                                        updateCommand.Parameters.AddWithValue("@TotalChequePrazo",      valores[71]);
                                        updateCommand.Parameters.AddWithValue("@CodNaturezaOperacao",   valores[72]);
                                        updateCommand.Parameters.AddWithValue("@PrecoTabelaEpoca",      valores[73]);
                                        updateCommand.Parameters.AddWithValue("@DescontoTotalItem",     valores[74]);
                                        updateCommand.Parameters.AddWithValue("@Conferido",             valores[75]);
                                        updateCommand.Parameters.AddWithValue("@TransacaoPedidoVenda",  valores[76]);
                                        updateCommand.Parameters.AddWithValue("@CodigoModeloNF",        valores[77]);
                                        updateCommand.Parameters.AddWithValue("@Acrescimo",             valores[78]);
                                        updateCommand.Parameters.AddWithValue("@MobCheckOut",           valores[79]);
                                        updateCommand.Parameters.AddWithValue("@AliquotaISS",           valores[80]);
                                        updateCommand.Parameters.AddWithValue("@BaseISS",               valores[81]);
                                        updateCommand.Parameters.AddWithValue("@Ordem",                 valores[82]);
                                        updateCommand.Parameters.AddWithValue("@CodRotinaRrigem",       valores[83]);
                                        updateCommand.Parameters.AddWithValue("@Timestamp",             valores[84]);
                                        updateCommand.Parameters.AddWithValue("@Troco",                 valores[85]);
                                        updateCommand.Parameters.AddWithValue("@Transportador",         valores[86]);
                                        updateCommand.Parameters.AddWithValue("@ICMSAliquotaDesonerado", valores[87]);
                                        updateCommand.Parameters.AddWithValue("@ICMSValorDesoneradoItem", valores[88]);
                                        updateCommand.Parameters.AddWithValue("@Empresa",               valores[89]);
                                        updateCommand.Parameters.AddWithValue("@DescontoItem",          valores[90]);
                                        updateCommand.Parameters.AddWithValue("@AliqISS",               valores[91]);
                                        updateCommand.Parameters.AddWithValue("@ISSBaseItem",           valores[92]);
                                        updateCommand.Parameters.AddWithValue("@Despesas",              valores[93]);
                                        updateCommand.Parameters.AddWithValue("@SeguroTotalItem",       valores[94]);
                                        updateCommand.Parameters.AddWithValue("@AcrescimoTotalItem",    valores[95]);
                                        updateCommand.Parameters.AddWithValue("@DespesasTotalItem",     valores[96]);
                                        updateCommand.Parameters.AddWithValue("@FormaPix",              valores[97]);
                                        updateCommand.Parameters.AddWithValue("@Totalpix",              valores[98]);
                                        updateCommand.Parameters.AddWithValue("@FormaDepositoDancario", valores[99]);
                                        updateCommand.Parameters.AddWithValue("@TotalDepositoBancario", valores[100]);
                                        updateCommand.Parameters.AddWithValue("@IDVendaProdutoB2C",     valores[101]);
                                        updateCommand.Parameters.AddWithValue("@ItemPromocional",       valores[102]);
                                        updateCommand.Parameters.AddWithValue("@AcrescimoItem",         valores[103]);
                                        updateCommand.Parameters.AddWithValue("@ICMSSTAntecipadoAliquota",  valores[104]);
                                        updateCommand.Parameters.AddWithValue("@ICMSSTAntecipadoMargem",    valores[105]);
                                        updateCommand.Parameters.AddWithValue("@ICMSSTAntecipadoPercReducao", valores[106]);
                                        updateCommand.Parameters.AddWithValue("@ICMSSTAntecipadoValorItem", valores[107]);
                                        updateCommand.Parameters.AddWithValue("@ICMSBaseDesoneradoItem",    valores[108]);
                                        updateCommand.Parameters.AddWithValue("@CodigoStatusNFE",       valores[109]);
                                        updateCommand.Parameters.AddWithValue("@PrecoProdutoEpoca",     valores[110]);

                                        updateCommand.Parameters.AddWithValue("@ChaveNF",       valores[5]);
                                        updateCommand.Parameters.AddWithValue("@CNPJEmp",       valores[1]);
                                        updateCommand.Parameters.AddWithValue("@Operacao",      valores[51]);
                                        updateCommand.Parameters.AddWithValue("@TipoTransacao", valores[52]);
                                        updateCommand.Parameters.AddWithValue("@Transacao",     valores[2]);
                                        updateCommand.Parameters.AddWithValue("@Identificador", valores[58]);
                                        updateCommand.Parameters.AddWithValue("@CodigoCliente", valores[11]);
                                        updateCommand.Parameters.AddWithValue("@CodProduto",    valores[53]);
                                        updateCommand.Parameters.AddWithValue("@Documento",     valores[4]);

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
                }
                if (cont == 0) status = "Não existem registros para integrar.";
                else if (qtd == 0) status = "Registros já existentes na base de dados.";
            }
            catch (SqlException ex)
            {
                status = "Erro ao gravar no banco de dados: " + ex.Message;
                //Console.WriteLine("Ocorreu um erro: " + ex.Message);
            }
            return status;
        }
    }

    // Object filtros da tela
    public class DadosMov
    {
        public string ChaveMov { get; set; }
        public string CNPJMov { get; set; }
        public Boolean Todos { get; set; }
        public string DataI { get; set; }
        public string DataF { get; set; }
    }

}
