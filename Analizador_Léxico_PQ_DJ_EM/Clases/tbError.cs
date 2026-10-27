using System;


namespace Analizador_Léxico_PQ_DJ_EM.Clases
{
    internal class tbError
    {
        public int IdError { get; set; }
        public int LineaError { get; set; }
        public int ColumnaError { get; set; }
        public string Error { get; set; }
        public string Observacion { get; set; }

        public tbError() { }

        public tbError(int idError, int lineaError, int columnaError, string error, string observacion)
        {
            IdError = idError;
            LineaError = lineaError;
            ColumnaError = columnaError;
            Error = error;
            Observacion = observacion;
        }

        public override string ToString()
        {
            return $"[ERROR LÉXICO] Línea {LineaError}, Col {ColumnaError} | Carácter/Cadena: '{Error}' | {Observacion}";
        }
    }
}
