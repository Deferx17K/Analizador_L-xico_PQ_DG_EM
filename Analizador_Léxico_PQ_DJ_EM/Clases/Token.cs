using System;

namespace Analizador_Léxico_PQ_DJ_EM.Clases
{
    internal class Token
    {
        int IdToken { get; set; }

        String token { get; set; }

        String Tipo { get; set; }

        public Token() { }

        public Token(int idToken, string tokenStr, string tipo)
        {
            IdToken = idToken;
            token = tokenStr;
            Tipo = tipo;
        }
    }
}
