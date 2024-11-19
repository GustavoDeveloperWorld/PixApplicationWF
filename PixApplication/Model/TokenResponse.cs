using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixApplication.Model
{
    public class TokenResponse
    {
        public int Id { get; set; }
        public string access_token { get; set; }
        public string token_type { get; set; }
        public double expires_in { get; set; }
    }
}
