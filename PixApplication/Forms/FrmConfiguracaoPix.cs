using Newtonsoft.Json;
using PixApplication.Entity;
using PixApplication.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static QRCoder.PayloadGenerator.WiFi;

namespace PixApplication
{
    public partial class FrmConfiguracaoPix : Form
    {
        public FrmConfiguracaoPix()
        {
            InitializeComponent();
        }

        private void btn_Salvar_Click(object sender, EventArgs e)
        {
            try
            {
                // Cria um objeto com os dados da tela
                var config = new ConfigPix()
                {
                    Name = txtName.Text,
                    Identificacao = txtIdentificacao.Text,
                    ChavePix = txtChavePix.Text,
                    ExpirePix = txtExpirePix.Text,
                };

                using (var context = new AppDbContext())
                {
                    // Verifica se já existe um registro com a mesma Identificação
                    var existingConfig = context.ConfigPixes
                                                .FirstOrDefault(c => c.Identificacao == config.Identificacao);

                    if (existingConfig != null)
                    {
                        // Se o registro já existe, atualiza os valores
                        existingConfig.Name = config.Name;
                        existingConfig.ChavePix = config.ChavePix;
                        existingConfig.ExpirePix = config.ExpirePix;

                        // Marca o registro como modificado
                        context.ConfigPixes.Add(existingConfig);
                        AtualizarChavePixNaAPI(existingConfig);
                    }
                    else
                    {
                        // Caso não exista, adiciona um novo registro
                        context.ConfigPixes.Add(config);
                        AtualizarChavePixNaAPI(config);
                    }

                    // Salva as mudanças no banco de dados
                    context.SaveChanges();
                }

                MessageBox.Show("Configuração salva com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar autenticação: " + ex.Message);
            }
        }

        private async Task AtualizarChavePixNaAPI(ConfigPix config)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = $"https://api.sandbox.bb.com.br/pix/v2/cob/webhook/{config.ChavePix}";

                    // Serializa o objeto config em JSON
                    var jsonContent = new StringContent(JsonConvert.SerializeObject(config.ChavePix), Encoding.UTF8, "application/json");

                    // Envia a requisição PUT
                    var response = await client.PutAsync(url, jsonContent);

                    // Verifica se a requisição foi bem-sucedida
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Chave Pix atualizada com sucesso na API!");
                    }
                    else
                    {
                        string errorResponse = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Erro ao atualizar chave Pix na API: {errorResponse}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao conectar na API: " + ex.Message);
            }
        }
    }
}
