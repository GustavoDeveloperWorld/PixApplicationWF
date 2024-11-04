using PixApplication.Entity;
using PixApplication.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
                var Config = new ConfigPix()
                {
                    Identificacao = txtIdentificacao.Text,
                    ChavePix = txtChavePix.Text,
                    Name = txtName.Text,
                };
                using (var context = new AppDbContext())
                {
                    context.ConfigPixes.Add(Config);
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
