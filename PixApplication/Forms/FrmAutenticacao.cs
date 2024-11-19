using Newtonsoft.Json;
using PixApplication.Business;
using PixApplication.Entity;
using PixApplication.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PixApplication
{
    public partial class FrmAutenticacao : Form
    {
        public FrmAutenticacao()
        {
            InitializeComponent();
        }
        private void btn_Salvar_Click(object sender, EventArgs e)
        {
            try
            {
                var authentication = new Authentication()
                {
                    ClientID = txtClientID.Text,
                    ClientSecret = txtClientSecret.Text,
                    ApplicationKey = txtApplicationKey.Text
                };

                using (var context = new AppDbContext())
                {
                    context.Authentications.Add(authentication);
                    context.SaveChanges();
                }

                MessageBox.Show("Autenticado com sucesso!");
            }

            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar autenticação: " + ex.Message);
            }


        }
    }
}
