using iTextSharp.text.pdf.fonts.cmaps;
using PixApplication.Entity;
using PixApplication.Model;
using PixApplication.Model.Enum;
using System;
using System.Linq;
using System.Windows.Forms;

namespace PixApplication.Forms
{
    public partial class FrmPedido : Form
    {
        public FrmPedido()
        {
            InitializeComponent();

            var descricaoList = Helper.Helper.GetEnumDescriptionList<FormaPagamento>();

            // Define como DataSource
            cmbFormaPagamento.DataSource = descricaoList;
            cmbFormaPagamento.DisplayMember = "Value";
            cmbFormaPagamento.ValueMember = "Key";
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            try
            {
                var pedido = new Pedido()
                {
                    NumeroPedido = int.Parse(txtNumeroPedido.Text),
                    Insumo = (System.Collections.Generic.ICollection<Insumo>)txtInsumo.Text.ToList(),
                    Valor = decimal.Parse(txtValor.Text),
                    FormaPagamento = (FormaPagamento)Enum.Parse(typeof(FormaPagamento), cmbFormaPagamento.SelectedValue.ToString())

                };

                grdPedido.Rows.Add(
               pedido.NumeroPedido,
               pedido.Insumo,
               pedido.Valor.ToString("C2"),
               pedido.FormaPagamento.ToString()
       );
            }

            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar pedido: " + ex.Message);
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    foreach (DataGridViewRow row in grdPedido.Rows)
                    {
                        // Ignorar linhas não preenchidas
                        if (row.IsNewRow) continue;

                        // Criar um objeto Pedido com os valores da linhas
                        var pedido = new Pedido
                        {
                            NumeroPedido = Convert.ToInt32(row.Cells["NumeroPedido"].Value),
                            Insumo = (System.Collections.Generic.ICollection<Insumo>)row.Cells["Insumo"].Value.ToString()
                                      .Split(',').Select(i => i.Trim()).ToList(), // Converter string de insumos para lista
                            Valor = Convert.ToDecimal(row.Cells["Valor"].Value),
                            FormaPagamento = (FormaPagamento)Enum.Parse(typeof(FormaPagamento), row.Cells["FormaPagamento"].Value.ToString())
                        };

                        // Adicionar ao contexto
                        context.Pedidos.Add(pedido);
                    }

                    // Salvar mudanças no banco
                    context.SaveChanges();
                }

                MessageBox.Show("Pedidos salvos com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar pedidos: " + ex.Message);
            }
        }

    }
}
