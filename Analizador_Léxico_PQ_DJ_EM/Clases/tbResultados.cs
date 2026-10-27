using System;
using System.Reflection.Emit;

namespace Analizador_Léxico_PQ_DJ_EM.Clases
{
    internal class tbResultados
    {
        public int IdResultado { get; set; }
        public int IdToken { get; set; }
        public string Identificador { get; set; }
        public string TipoToken { get; set; }
        public int Linea { get; set; }
        public int Columna { get; set; }

        public tbResultados() { }

        public tbResultados(int idResultado, int idToken, string identificador, string tipoToken, int linea, int columna)
        {
            IdResultado = idResultado;
            IdToken = idToken;
            Identificador = identificador;
            TipoToken = tipoToken;
            Linea = linea;
            Columna = columna;
        }
        public override string ToString()
        {
            return $"Línea {Linea}, Col {Columna} | Lexema: '{Identificador}' | Token: {TipoToken}";
        }
    }
}
