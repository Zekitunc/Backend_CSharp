using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.Encryption
{
    public static class SecurityKeyHelper
    {
        public static SecurityKey CreateSecurityKey(string securityKey)  //utf8 yapmış o yanlış 32ye çevirdik
        {
            return new SymmetricSecurityKey(Encoding.UTF32.GetBytes(securityKey));
        }
        //simetrik anahtar oluştur bunlar byte arrayinde olmak zorundadır
    }
}
