using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixApplication.Model.Enum
{
    public enum StatusPagamento
    {
        [Description("Pagamento pendente")]
        Pendente = 1,

        [Description("Pagamento concluído")]
        Concluido = 2,

        [Description("Pagamento cancelado")]
        Cancelado = 3
    }
}
