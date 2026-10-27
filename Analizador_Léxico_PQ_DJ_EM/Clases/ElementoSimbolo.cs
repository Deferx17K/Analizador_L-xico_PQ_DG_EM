using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analizador_Léxico_PQ_DJ_EM.Clases
{
    public class ElementoSimbolo
    {
        public string Nombre { get; set; }
        public string CategoriaToken { get; set; }
        public string TipoDato { get; set; }
        public int Linea { get; set; }
        public string Ambito { get; set; }

        public ElementoSimbolo(string nombre, string categoriaToken, string tipoDato, int linea, string ambito)
        {
            Nombre = nombre;
            CategoriaToken = categoriaToken;
            TipoDato = tipoDato;
            Linea = linea;
            Ambito = ambito;
        }
    }

    public class TablaDeSimbolos
    {
        private Dictionary<string, ElementoSimbolo> simbolos;

        public TablaDeSimbolos()
        {
            simbolos = new Dictionary<string, ElementoSimbolo>();
        }

        // Agrega un identificador si no ha sido registrado previamente (evita duplicados)
        public bool AgregarIdentificador(string nombre, int linea, string tipoDato = "Desconocido", string ambito = "Global")
        {
            if (!simbolos.ContainsKey(nombre))
            {
                simbolos.Add(nombre, new ElementoSimbolo(nombre, "TK_IDENTIFICADOR", tipoDato, linea, ambito));
                return true;
            }
            return false;
        }

        // Método agregado para resolver el error CS1061
        public bool Eliminar(string nombre)
        {
            if (simbolos.ContainsKey(nombre))
            {
                return simbolos.Remove(nombre);
            }
            return false;
        }

        public IEnumerable<ElementoSimbolo> ObtenerSimbolos()
        {
            return simbolos.Values;
        }

        public void Limpiar()
        {
            simbolos.Clear();
        }
    }
}
