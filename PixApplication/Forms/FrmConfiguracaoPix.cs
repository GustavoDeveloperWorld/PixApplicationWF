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
        private Model.Authentication authentication = new Model.Authentication();
        private ConfigPix configPix = new ConfigPix();
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

                        context.ConfigPixes.Add(existingConfig);
                    }
                    else
                    {
                        context.ConfigPixes.Add(config);
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
    }
}
