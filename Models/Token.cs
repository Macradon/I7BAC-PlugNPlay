using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlugNPlayBackend.Models
{
    public class Token
    {
        public Token(string username)
        {
            JsonWebToken = "Token response not implemented";
        }

        public string JsonWebToken { get; set; }
    }
}
