using Newtonsoft.Json;
using PixApplication.Business;
using PixApplication.Entity;
using PixApplication.Model;
using QRCoder;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PixApplication
{
    public partial class FrmGerarPix : Form
    {
        private readonly AuthenticationBO _authenticationBO;
        private static readonly HttpClient client = new HttpClient();
        public FrmGerarPix()
        {
            InitializeComponent();

            _authenticationBO= new AuthenticationBO();

        }

        private void btnGerar_Click(object sender, EventArgs e)
        {
            // Chamar o método assíncrono
            _ = GerarCobrancaAsync();
        }

        private async Task GerarCobrancaAsync()
        {
            var url = "https://api.sandbox.bb.com.br/pix/v2/cob"; // URL do endpoint da API
            var client = new HttpClient();

            // Configurar a chave de app key
            client.DefaultRequestHeaders.Add("gw-dev-app-key", "6548cb25d4dbb1da7c92c6e3211533c8");

            // Criação do objeto que será enviado
            var cobranca = new
            {
                calendario = new
                {
                    expiracao = 3600 // Expiração em segundos
                },
                devedor = new
                {
                    cnpj = "12345678000195",
                    nome = "Neocart Tecnologia SA"
                },
                valor = new
                {
                    original = txtValor.Text,
                },
                chave = "9e881f18-cc66-4fc7-8f2c-a795dbb2bfc1", // Sua chave PIX
                solcnpjitacaoPagador = "Serviço realizado.",
                infoAdicionais = new[]
                {
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

                    string idCobranca = resultado.id; // Ajuste conforme a estrutura real do retorno
                    string linkPagamento = resultado.link; // Ajuste conforme a estrutura real do retorno

                    // Criar a cobrança para salvar no banco
                    var novaCobranca = new CobrancaPix
                    {
                        IdCobranca = idCobranca,
                        LinkPagamento = linkPagamento,
                        Valor = decimal.Parse(txtValor.Text),
                        Identificacao = "12345678000195", // Use o valor real
                        NomeDevedor = "Neocart Tecnologia SA", // Use o valor real
                        DataCriacao = DateTime.Now
                    };

                    using (var context = new AppDbContext())
                    {
                        context.Cobrancas.Add(novaCobranca);
                        context.SaveChanges();
                    }

                    // Gerar o QR Code usando o link de pagamento
                    GerarQRCode(linkPagamento);

                    MessageBox.Show($"Cobrança gerada e salva com sucesso!\nID: {idCobranca}\nLink de Pagamento: {linkPagamento}");
                }
                else
                {
                    var errorData = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Erro ao gerar cobrança: " + errorData);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
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
                    pictureQRCode.Image = qrCodeImage; // Exibe a imagem no PictureBox
                }
            }
        }

        private async void btn_Autenticar_Click(object sender, EventArgs e)
        {
            var auth = await _authenticationBO.ObterAutenticacaoAsync();

            if (auth != null)
            {
                bool autenticado = await _authenticationBO.TentarAutenticacaoAsync(auth);

                if (autenticado)
                {
                    MessageBox.Show("Autenticação realizada com sucesso!");
                }
                else if(!autenticado)
                {
                    MessageBox.Show("Problemas com a sua autenticação!");

                    FrmAutenticacao autenticacao = new FrmAutenticacao();

                    autenticacao.ShowDialog();
                }
            }

        }
        private void btnConfiguracao_Click(object sender, EventArgs e)
        {
            FrmConfiguracaoPix configPix = new FrmConfiguracaoPix();

            configPix.ShowDialog();
        }
    }
}
