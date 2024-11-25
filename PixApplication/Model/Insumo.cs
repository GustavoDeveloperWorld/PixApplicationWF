using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixApplication.Model
{
    public class Insumo
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }
    }
}
