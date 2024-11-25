using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixApplication.Model
{

    public class CobrancaPix
    {
        public int Id { get; set; }
        public string IdCobranca { get; set; }
        public string LinkPagamento { get; set; }
        public decimal Valor { get; set; }
        public string Identificacao { get; set; }
        public string NomeDevedor { get; set; }
        public DateTime? DataCriacao { get; set; }
        public virtual Pedido Pedido { get; set; }
    }


}
