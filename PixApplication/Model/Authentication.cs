using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixApplication.Model
{
    public class Authentication
    {
        public int Id { get; set; }
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }
        public string ApplicationKey { get; set; }

    }
}
