using iTextSharp.text.pdf;
using iTextSharp.text;
using Newtonsoft.Json;
using PixApplication.Business;
using PixApplication.Entity;
using PixApplication.Migrations;
using PixApplication.Model;
using QRCoder;
using System;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Media;
using PixApplication.Forms;
using System.Data.Entity.Migrations;

namespace PixApplication
{
    public partial class FrmGerarPix : Form
    {
        private readonly AuthenticationBO _authenticationBO;
        private Authentication authentication = new Authentication();
        private ConfigPix configPix = new ConfigPix();
        private static readonly HttpClient client = new HttpClient();
        private CobrancaPix cobrancapix = new CobrancaPix();
        private int tempoExpiracaoPix;
        private int tempoRestante;
        private string statusPix;
        private string PixCopiaCola;
        private string Location;
        string idCobranca;
        public FrmGerarPix()
        {
            InitializeComponent();

            _authenticationBO = new AuthenticationBO();

            AtualizarPedidos();
        }

        public void AtualizarPedidos()
        {
            using (var context = new AppDbContext())
            {
                var pedidos = context.Pedidos
                                     .Select(p => p.NumeroPedido)
                                     .ToList();
                cmbPedido.DataSource = pedidos;
            }
        }

        private async void btnGerar_Click(object sender, EventArgs e)
        {
            await GerarCobrancaAsync();
        }

        private async Task GerarCobrancaAsync()
        {
            var url = "https://api.sandbox.bb.com.br/pix/v2/cob";
            var client = new HttpClient();

            string accessToken = null;
            Pedido pedidoSelecionado = null;

            using (var context = new AppDbContext())
            {
                accessToken = context.TokenResponses
                                     .Select(x => x.access_token)
                                     .FirstOrDefault();

                configPix = context.ConfigPixes
                                    .FirstOrDefault();

                authentication = context.Authentications
                                    .FirstOrDefault();

                int numeroPedido;
                if (int.TryParse(cmbPedido.Text, out numeroPedido))
                {
                    pedidoSelecionado = context.Pedidos.FirstOrDefault(p => p.NumeroPedido == numeroPedido);
                }

            }

            // Verifique se o access token é válido
            if (string.IsNullOrEmpty(accessToken))
            {
                MessageBox.Show("Token de acesso não encontrado ou está vazio.");
                return;
            }

            // Cabeçalhos de autenticação
            client.DefaultRequestHeaders.Add("gw-dev-app-key", authentication.ApplicationKey.ToString());
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

            // Criação do objeto que será enviado
            var cobranca = new
            {
                calendario = new
                {
                    expiracao = int.Parse(configPix.ExpirePix)
                },
                devedor = new
                {
                    cnpj = configPix.Identificacao.ToString(),
                    nome = configPix.Name.ToString()
                },
                valor = new
                {
                    original = pedidoSelecionado.Valor.ToString("F2").Replace(",", "."),
                },
                chave = "9e881f18-cc66-4fc7-8f2c-a795dbb2bfc1"/*configPix.ChavePix.ToString()*/,
                solicitacaoPagador = "Serviço realizado.",
                infoAdicionais = new[] {
                new { nome = "Campo 1", valor = "Informação Adicional1 do PSP-Recebedor" },
                new { nome = "Campo 2", valor = "Informação Adicional2 do PSP-Recebedor" }
                }
            };

            try
            {
                var json = JsonConvert.SerializeObject(cobranca);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var resultado = JsonConvert.DeserializeObject<dynamic>(responseData);

                    idCobranca = resultado.txid;
                    string location = resultado.location;
                    string pixCopiaECola = resultado.pixCopiaECola;
                    string expire = resultado.calendario.expiracao;
                    string status = resultado.status;

                    statusPix = status;
                    PixCopiaCola = pixCopiaECola;
                    Location = location;
                    tempoExpiracaoPix = Convert.ToInt32(expire);
                    tempoRestante = tempoExpiracaoPix;

                    // Atualiza a barra de progresso
                    progressBar.Maximum = tempoExpiracaoPix;
                    progressBar.Value = 0;

                    // Inicia o Timer
                    timerExpirePix.Start();


                    //int expireInt = Convert.ToInt32(expire);

                    // Criar a cobrança para salvar no banco
                    var novaCobranca = new CobrancaPix
                    {
                        IdCobranca = idCobranca,
                        LinkPagamento = location,
                        Valor = pedidoSelecionado.Valor,
                        Identificacao = configPix.Identificacao.Replace(".", "").Replace("-", ""),
                        NomeDevedor = configPix.Name.ToString(),
                        DataCriacao = DateTime.Now
                    };

                    cobrancapix = novaCobranca;



                    using (var context = new AppDbContext())
                    {
                        // Adiciona a nova cobrança
                        context.Cobrancas.Add(novaCobranca);
                        context.SaveChanges();

                        var numPedido = int.Parse(cmbPedido.Text);
                        var pedido = context.Pedidos.FirstOrDefault(p => p.NumeroPedido == numPedido);

                        if (pedido != null)
                        {
                            pedido.IdCobranca = novaCobranca.IdCobranca; 

                            context.SaveChanges();
                        }
                        else
                        {
                            MessageBox.Show("Pedido não encontrado.");
                        }
                    }




                    // Gerar o QR Code usando o link de pagamento
                    GerarQRCode(pixCopiaECola);
                    pixCopiaCola.Text = $"\nPix Copia e Cola: {pixCopiaECola}";
                }
                else
                {
                    var errorData = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Erro ao gerar cobrança: {errorData}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }
        }      

        private void GerarQRCode(string dados)
        {
            using (var qrGenerator = new QRCodeGenerator())
            {
                var qrCodeData = qrGenerator.CreateQrCode(dados, QRCodeGenerator.ECCLevel.Q);
                using (var qrCode = new QRCode(qrCodeData))
                {
                    var qrCodeImage = qrCode.GetGraphic(20);
                    pictureQRCode.Image = qrCodeImage;
                    pictureQRCode.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
        }

        private async Task<string> ConsultarStatusPixAsync(string txId)
        {
            // Verifique se o txId está definido
            if (string.IsNullOrEmpty(txId))
            {
                throw new ArgumentException("O txId não pode ser nulo ou vazio.", nameof(txId));
            }

            // URL do endpoint com o txId
            var url = $"https://api.sandbox.bb.com.br/pix/v2/cob/{txId}";
            var client = new HttpClient();

            // Recuperar o token de acesso
            string accessToken;
            using (var context = new AppDbContext())
            {
                accessToken = context.TokenResponses
                                     .Select(x => x.access_token)
                                     .FirstOrDefault();
            }

            if (string.IsNullOrEmpty(accessToken))
            {
                throw new InvalidOperationException("Token de acesso não encontrado ou está vazio.");
            }

            // Configurar os cabeçalhos necessários
            client.DefaultRequestHeaders.Add("gw-dev-app-key", authentication.ApplicationKey.ToString());
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

            try
            {
                // Realizar a chamada GET
                var response = await client.GetAsync(url);

                // Verificar a resposta da API
                if (response.IsSuccessStatusCode)
                {
                    // Ler o conteúdo da resposta
                    var responseData = await response.Content.ReadAsStringAsync();
                    var resultado = JsonConvert.DeserializeObject<dynamic>(responseData);

                    // Retornar o status da cobrança
                    return resultado.status;
                }
                else
                {
                    // Lidar com erros de API
                    var errorData = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Erro ao consultar cobrança: {response.StatusCode} - {errorData}");
                }
            }
            catch (Exception ex)
            {
                // Lidar com exceções
                throw new Exception($"Erro ao consultar status do Pix: {ex.Message}", ex);
            }
        }

        private async Task CancelarCobrancaPixAsync()
        {       

        }

        private async void btn_Autenticar_Click(object sender, EventArgs e)
        {
            var auth = await _authenticationBO.ObterAutenticacaoAsync();

            if (auth != null)
            {
                bool autenticado = await _authenticationBO.TentarAutenticacaoAsync(auth);

                if (autenticado)
                {
                    checkAuthentication.CheckState = CheckState.Checked;
                }
            }
            else
            {
                FrmAutenticacao autenticacao = new FrmAutenticacao();
                autenticacao.ShowDialog();
                var resultado = autenticacao.ShowDialog();

                if (resultado == DialogResult.OK)
                {
                    auth = await _authenticationBO.ObterAutenticacaoAsync();
                    bool autenticado = await _authenticationBO.TentarAutenticacaoAsync(auth);

                    if (autenticado)
                    {
                        checkAuthentication.CheckState = CheckState.Checked;
                        MessageBox.Show("Autenticado com sucesso.");
                    }
                    else
                    {
                        MessageBox.Show("Falha na autenticação após configurar.");
                    }
                }

            }

        }

        private void btnConfiguracao_Click(object sender, EventArgs e)
        {
            FrmConfiguracaoPix configPix = new FrmConfiguracaoPix();

            configPix.ShowDialog();
        }

        private async void timerExpirePix_Tick(object sender, EventArgs e)
        {
            if (tempoRestante > 0)
            {
                tempoRestante--;
                progressBar.Value = tempoExpiracaoPix - tempoRestante;

                try
                {
                    // Consulta o status da cobrança
                    var novoStatus = await ConsultarStatusPixAsync(idCobranca);

                    // Atualiza o status na label
                    status.Text = novoStatus;

                    // Verifica se o status mudou para "Concluído"
                    if (novoStatus == "CONCLUIDO")
                    {
                        timerExpirePix.Stop();
                        MessageBox.Show("Pagamento concluído com sucesso!");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    timerExpirePix.Stop();
                    MessageBox.Show($"Erro ao consultar status: {ex.Message}");
                    return;
                }
            }
            else
            {
                // Tempo expirado, cancela o pagamento
                timerExpirePix.Stop();
                status.Text = "CANCELADO";
                MessageBox.Show("Tempo de expiração do Pix atingido. Cobrança cancelada.");
                await CancelarCobrancaPixAsync(); // Opcional: Cancela no sistema ou no banco
                ResetarTela();
            }
        }

        private void ResetarTela()
        {
            progressBar.Value = 0;

            pixCopiaCola.Text = string.Empty;

            pictureQRCode.Image = null;

            status.Text = "---";
            txtValor.Clear();
            txtValor.Enabled = true;
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            CobrancaPixBO.GerarPDF(cobrancapix.IdCobranca, configPix.ChavePix, statusPix, cobrancapix.Valor.ToString(), PixCopiaCola, Location);
        }

        private void btnPedido_Click(object sender, EventArgs e)
        {
            FrmPedido frm = new FrmPedido(this);
            frm.ShowDialog();
        }
    }
}
