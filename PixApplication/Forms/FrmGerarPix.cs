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
        public FrmGerarPix()
        {
            InitializeComponent();

            _authenticationBO = new AuthenticationBO();

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

            using (var context = new AppDbContext())
            {
                accessToken = context.TokenResponses
                                     .Select(x => x.access_token)
                                     .FirstOrDefault();

                configPix = context.ConfigPixes
                                    .FirstOrDefault();

                authentication = context.Authentications
                                    .FirstOrDefault();


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
                    original = txtValor.Text.ToString(),
                },
                chave = /*"9e881f18-cc66-4fc7-8f2c-a795dbb2bfc1"*/configPix.ChavePix.ToString(),
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

                    string idCobranca = resultado.txid;
                    string location = resultado.location;
                    string pixCopiaECola = resultado.pixCopiaECola;
                    string expire = resultado.calendario.expiracao;
                    string status = resultado.status;

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
                        Valor = decimal.Parse(txtValor.Text.Replace(",", ".")),
                        Identificacao = configPix.Identificacao.Replace(".", "").Replace("-", ""),
                        NomeDevedor = configPix.Name.ToString(),
                        DataCriacao = DateTime.Now
                    };

                    cobrancapix = novaCobranca;



                    using (var context = new AppDbContext())
                    {
                        context.Cobrancas.Add(novaCobranca);
                        context.SaveChanges();
                    }

                    // Gerar o QR Code usando o link de pagamento
                    GerarQRCode(pixCopiaECola);
                    pixCopiaCola.Text = $"\nPix Copia e Cola: {pixCopiaECola}";
                    label3.Text = status;
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

        private void GerarPDF(string cobrancapix, string status, string valor, string pixCopiaECola, string location)
        {
            try
            {
                Document doc = new Document(PageSize.A4);
                string pdfFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Cobranca_Pix_" + cobrancapix + ".pdf");

                PdfWriter.GetInstance(doc, new FileStream(pdfFilePath, FileMode.Create));

                doc.Open();

                doc.Add(new Paragraph("Cobrança PIX"));
                doc.Add(new Paragraph("-------------------------------------------------"));

                doc.Add(new Paragraph($"ID da cobrança: {cobrancapix}"));
                doc.Add(new Paragraph($"Status: {status}"));
                doc.Add(new Paragraph($"Valor: {valor}"));
                doc.Add(new Paragraph($"Chave Pix: {configPix.ChavePix}"));
                doc.Add(new Paragraph($"Solicitação do pagador: Serviço realizado"));
                doc.Add(new Paragraph($"Link de pagamento: {location}"));

                doc.Add(new Paragraph("QR Code:"));

                var qrGenerator = new QRCodeGenerator();
                var qrCodeData = qrGenerator.CreateQrCode(pixCopiaECola, QRCodeGenerator.ECCLevel.Q);
                var qrCode = new QRCode(qrCodeData);
                using (Bitmap qrBitmap = qrCode.GetGraphic(20))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        qrBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(ms.ToArray());
                        img.ScaleToFit(100f, 100f);
                        img.Alignment = Element.ALIGN_CENTER;
                        doc.Add(img);
                    }
                }

                doc.Close();
                MessageBox.Show($"PDF gerado com sucesso: {pdfFilePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao gerar PDF: {ex.Message}");
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

        private void timerExpirePix_Tick(object sender, EventArgs e)
        {
            if (tempoRestante > 0)
            {
                tempoRestante--;
                progressBar.Value = tempoExpiracaoPix - tempoRestante;
            }
            else
            {
                timerExpirePix.Stop();
                MessageBox.Show("Tempo de expiração do Pix foi atingido.");
                ResetarTela();
            }
        }
        private void ResetarTela()
        {
            progressBar.Value = 0;

            pixCopiaCola.Text = string.Empty;

            pictureQRCode.Image = null;

            label3.Text = "---";
            txtValor.Clear();
            txtValor.Enabled = true;
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            //GerarPDF(cobrancapix.IdCobranca, string status, cobrancapix.Valor, string pixCopiaECola, string location); ;
        }
    }
}
