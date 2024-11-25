using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixApplication.Model.Enum
{
    public enum FormaPagamento
    {
        [Description("Pix")]
        Boleto = 1,

        [Description("Cartão de debito")]
        CartaoDebito = 2,

       [Description("Cartão de crédito")]
         CartaoCredito = 3,
    }
}
