using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PixApplication.Entity;
using PixApplication.Model;
using PixApplication.Model.Enum;

namespace PixApplication.Forms
{
    public partial class FrmPedido : Form
    {
        private BindingSource _pedidoBindingSource;
        private List<Pedido> _pedidos;
        private FrmGerarPix _frmGerarPix;

        public FrmPedido(FrmGerarPix frmGerarPix)
        {
            InitializeComponent();
            _frmGerarPix = frmGerarPix;
            // Inicializa a lista e o BindingSource
            _pedidos = new List<Pedido>();
            _pedidoBindingSource = new BindingSource { DataSource = _pedidos };

            grdPedido.DataSource = _pedidoBindingSource;

            // Configura o ComboBox
            var descricaoList = Helper.Helper.GetEnumDescriptionList<FormaPagamento>();
            cmbFormaPagamento.DataSource = descricaoList;
            cmbFormaPagamento.DisplayMember = "Value";
            cmbFormaPagamento.ValueMember = "Key";
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validações
                if (string.IsNullOrWhiteSpace(txtInsumo.Text))
                {
                    MessageBox.Show("Por favor, insira o nome do insumo.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtNumeroPedido.Text) ||
                    string.IsNullOrWhiteSpace(txtValor.Text) ||
                    cmbFormaPagamento.SelectedIndex == -1)
                {
                    MessageBox.Show("Preencha todos os campos.");
                    return;
                }

                // Cria um novo pedido
                var insumos = txtInsumo.Text
                              .Split(',')
                              .Select(i => new Insumo { Nome = i.Trim() })
                              .ToList();

                var pedido = new Pedido
                {
                    NumeroPedido = int.Parse(txtNumeroPedido.Text),
                    Insumos = insumos,
                    Valor = decimal.Parse(txtValor.Text),
                    FormaPagamento = (FormaPagamento)Enum.Parse(typeof(FormaPagamento), cmbFormaPagamento.SelectedValue.ToString())
                };

                // Adiciona o pedido na lista e atualiza o BindingSource
                _pedidos.Add(pedido);
                _pedidoBindingSource.ResetBindings(false);

                // Limpa os campos de entrada
                txtInsumo.Clear();
                txtNumeroPedido.Clear();
                txtValor.Clear();
                cmbFormaPagamento.SelectedIndex = -1;

                txtInsumo.Focus();

                MessageBox.Show("Pedido adicionado à lista!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao adicionar pedido: " + ex.Message);
            }
        }


        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    // Salva os pedidos no banco de dados
                    context.Pedidos.AddRange(_pedidos);
                    context.SaveChanges();
                }

                MessageBox.Show("Pedidos salvos com sucesso!");

                // Atualiza os pedidos no formulário de geração de Pix
                _frmGerarPix?.AtualizarPedidos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar pedidos: " + ex.Message);
            }
        }
    }
}
