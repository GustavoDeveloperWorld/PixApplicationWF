using PixApplication.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixApplication.Model
{
    public class Pedido
    {
        public int Id { get; set; }
        public int NumeroPedido { get; set; }
        public ICollection<Insumo> Insumo { get; set; }
        public decimal Valor { get; set; }
        public StatusPagamento StatusPagamento { get;}
        public FormaPagamento FormaPagamento { get; set; }
        public ICollection<Insumo> Insumos { get; set; }
        public string IdCobranca { get; set; }
        public virtual CobrancaPix Cobranca { get; set; }

    }
}
