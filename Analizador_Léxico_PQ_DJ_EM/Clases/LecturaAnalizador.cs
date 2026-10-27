using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Analizador_Léxico_PQ_DJ_EM.Clases
{
    internal class LecturaAnalizador
    {
        public class ConexionBD
        {
            public SqlConnection ObtenerConexion()
            {
                string cadena = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=BDAnalizador; Integrated Security=True";
                return new SqlConnection(cadena);
            }
        }

        int IdLecturaA { get; set; }
        String Resultado { get; set; }
        String Error { get; set; }

        private List<tbResultados> listaTokens;
        private List<tbError> listaErrores;
        private TablaDeSimbolos tablaSimbolos;

        // Modificado a Diccionario para almacenar el Token y su Tipo específico de la BD
        private readonly Dictionary<string, string> palabrasReservadas = new Dictionary<string, string>();

        public void PalabrasReservadas()
        {
            palabrasReservadas.Clear();

            // Catálogo por defecto (respaldo si la base de datos no está disponible)
            string[] porDefecto = {
                "int", "float", "double", "char", "bool", "string", "void",
                "if", "else", "while", "for", "switch", "case", "break", "continue", "return",
                "class", "static", "true", "false", "foreach"
            };
            foreach (var p in porDefecto)
            {
                palabrasReservadas[p] = "TK_PALABRAS_RESERVADAS";
            }

            try
            {
                ConexionBD conexionBD = new ConexionBD();
                using (SqlConnection conexion = conexionBD.ObtenerConexion())
                {
                    conexion.Open();
                    string sql = "SELECT Token, Tipo FROM Token";
                    SqlCommand sqlComando = new SqlCommand(sql, conexion);
                    using (SqlDataReader reader = sqlComando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string token = reader.GetString(0);
                            string tipo = reader.GetString(1);
                            palabrasReservadas[token] = tipo;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Nota: No se pudo conectar a la base de datos local de tokens (" + ex.Message + "). Usando catálogo base.");
            }
        }

        public LecturaAnalizador()
        {
            listaTokens = new List<tbResultados>();
            listaErrores = new List<tbError>();
            tablaSimbolos = new TablaDeSimbolos();
            PalabrasReservadas();
        }

        public List<tbResultados> ObtenerTokens() => listaTokens;
        public List<tbError> ObtenerErrores() => listaErrores;
        public TablaDeSimbolos ObtenerTablaSimbolos() => tablaSimbolos;
        public IEnumerable<string> ObtenerPalabrasReservadas() => palabrasReservadas.Keys;

        public void AnalizarTexto(string contenidoFuente)
        {
            listaTokens.Clear();
            listaErrores.Clear();
            tablaSimbolos.Limpiar();

            int posicion = 0;
            int longitud = contenidoFuente.Length;
            int lineaActual = 1;
            int columnaActual = 1;
            int idResultCounter = 1;
            int idErrorCounter = 1;

            // Pila para verificar delimitadores abiertos: (), {}, []
            Stack<(char caracter, int linea, int columna)> pilaDelimitadores = new Stack<(char, int, int)>();

            // Bandera para control básico de sentencia y punto y coma
            bool sentenciaActiva = false;
            int lineaInicioSentencia = 1;
            int colInicioSentencia = 1;

            while (posicion < longitud)
            {
                char c = contenidoFuente[posicion];

                //Bloque 1: Espacios en blanco y saltos de línea
                if (c == '\n')
                {
                    lineaActual++;
                    columnaActual = 1;
                    posicion++;
                    continue;
                }
                if (c == '\r')
                {
                    posicion++;
                    continue;
                }
                if (char.IsWhiteSpace(c))
                {
                    columnaActual++;
                    posicion++;
                    continue;
                }

                //Bloque 2: Comentarios
                if (c == '/')
                {
                    if (posicion + 1 < longitud && contenidoFuente[posicion + 1] == '/')
                    {
                        while (posicion < longitud && contenidoFuente[posicion] != '\n')
                        {
                            posicion++;
                            columnaActual++;
                        }
                        continue;
                    }
                    else if (posicion + 1 < longitud && contenidoFuente[posicion + 1] == '*')
                    {
                        int colInicio = columnaActual;
                        int linInicio = lineaActual;
                        posicion += 2;
                        columnaActual += 2;
                        bool cerrado = false;

                        while (posicion < longitud)
                        {
                            if (contenidoFuente[posicion] == '\n')
                            {
                                lineaActual++;
                                columnaActual = 1;
                                posicion++;
                            }
                            else if (contenidoFuente[posicion] == '*' && posicion + 1 < longitud && contenidoFuente[posicion + 1] == '/')
                            {
                                posicion += 2;
                                columnaActual += 2;
                                cerrado = true;
                                break;
                            }
                            else
                            {
                                posicion++;
                                columnaActual++;
                            }
                        }

                        if (!cerrado)
                        {
                            listaErrores.Add(new tbError(idErrorCounter++, linInicio, colInicio, "/*", "Comentario de bloque no cerrado"));
                        }
                        continue;
                    }
                }

                //Bloque 3: Identificadores y Palabras Reservadas
                if (char.IsLetter(c) || c == '_')
                {
                    int colInicio = columnaActual;
                    string lexema = "";

                    while (posicion < longitud && (char.IsLetterOrDigit(contenidoFuente[posicion]) || contenidoFuente[posicion] == '_'))
                    {
                        lexema += contenidoFuente[posicion];
                        posicion++;
                        columnaActual++;
                    }

                    // Se valida si pertenece a las palabras reservadas de la BD y se asigna su tipo correspondiente
                    string tipo = palabrasReservadas.ContainsKey(lexema) ? palabrasReservadas[lexema] : "TK_IDENTIFICADOR";
                    listaTokens.Add(new tbResultados(idResultCounter++, 1, lexema, tipo, lineaActual, colInicio));

                    if (tipo == "TK_IDENTIFICADOR")
                    {
                        tablaSimbolos.AgregarIdentificador(lexema, lineaActual);
                    }

                    if (!sentenciaActiva)
                    {
                        sentenciaActiva = true;
                        lineaInicioSentencia = lineaActual;
                        colInicioSentencia = colInicio;
                    }
                    continue;
                }

                // Bloque 4: Números
                if (char.IsDigit(c))
                {
                    int colInicio = columnaActual;
                    string lexema = "";
                    bool tienePunto = false;

                    while (posicion < longitud)
                    {
                        char actual = contenidoFuente[posicion];
                        if (char.IsDigit(actual))
                        {
                            lexema += actual;
                            posicion++;
                            columnaActual++;
                        }
                        else if (actual == '.' && !tienePunto)
                        {
                            if (posicion + 1 < longitud && char.IsDigit(contenidoFuente[posicion + 1]))
                            {
                                tienePunto = true;
                                lexema += actual;
                                posicion++;
                                columnaActual++;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    string tipo = "TK_NUMEROS_REALES";
                    listaTokens.Add(new tbResultados(idResultCounter++, 2, lexema, tipo, lineaActual, colInicio));
                    if (!sentenciaActiva)
                    {
                        sentenciaActiva = true;
                        lineaInicioSentencia = lineaActual;
                        colInicioSentencia = colInicio;
                    }
                    continue;
                }

                //Bloque 5: Cadenas
                if (c == '"')
                {
                    int colInicio = columnaActual;
                    string lexema = "\"";
                    posicion++;
                    columnaActual++;
                    bool cerrada = false;

                    while (posicion < longitud)
                    {
                        char actual = contenidoFuente[posicion];
                        if (actual == '"')
                        {
                            lexema += "\"";
                            posicion++;
                            columnaActual++;
                            cerrada = true;
                            break;
                        }
                        else if (actual == '\n')
                        {
                            break;
                        }
                        else
                        {
                            lexema += actual;
                            posicion++;
                            columnaActual++;
                        }
                    }

                    if (cerrada)
                    {
                        listaTokens.Add(new tbResultados(idResultCounter++, 3, lexema, "TK_CADENA", lineaActual, colInicio));
                    }
                    else
                    {
                        listaErrores.Add(new tbError(idErrorCounter++, lineaActual, colInicio, lexema, "Cadena de caracteres no cerrada correctamente"));
                    }
                    if (!sentenciaActiva)
                    {
                        sentenciaActiva = true;
                        lineaInicioSentencia = lineaActual;
                        colInicioSentencia = colInicio;
                    }
                    continue;
                }

                //Bloque 6: Operadores Relacionales y Lógicos dobles
                if (posicion + 1 < longitud)
                {
                    string subDos = contenidoFuente.Substring(posicion, 2);
                    if (subDos == "==" || subDos == "!=" || subDos == "<=" || subDos == "=>")
                    {
                        listaTokens.Add(new tbResultados(idResultCounter++, 6, subDos, "TK_OP_RELACIONALES", lineaActual, columnaActual));
                        posicion += 2;
                        columnaActual += 2;
                        continue;
                    }
                    if (subDos == "&&" || subDos == "||")
                    {
                        listaTokens.Add(new tbResultados(idResultCounter++, 7, subDos, "TK_OP_LOGICO", lineaActual, columnaActual));
                        posicion += 2;
                        columnaActual += 2;
                        continue;
                    }
                }

                //Bloque 7: Símbolos de Puntuación y Delimitadores
                if (c == '+' || c == '-')
                {
                    listaTokens.Add(new tbResultados(idResultCounter++, 4, c.ToString(), "TK_OP_SUMA", lineaActual, columnaActual));
                    posicion++; columnaActual++;
                    continue;
                }
                if (c == '*' || c == '/')
                {
                    listaTokens.Add(new tbResultados(idResultCounter++, 5, c.ToString(), "TK_OP_MULTIPLICACION", lineaActual, columnaActual));
                    posicion++; columnaActual++;
                    continue;
                }

                // Validación de Puntuación y Delimitadores (Apilado y Desapilado)
                if (c == '(' || c == '{' || c == '[')
                {
                    pilaDelimitadores.Push((c, lineaActual, columnaActual));
                    listaTokens.Add(new tbResultados(idResultCounter++, 6, c.ToString(), "TK_PUNTUACION", lineaActual, columnaActual));
                    posicion++; columnaActual++;
                    continue;
                }
                if (c == ')' || c == '}' || c == ']')
                {
                    char aperturaEsperada = ' ';
                    if (c == ')') aperturaEsperada = '(';
                    if (c == '}') aperturaEsperada = '{';
                    if (c == ']') aperturaEsperada = '[';

                    if (pilaDelimitadores.Count > 0 && pilaDelimitadores.Peek().caracter == aperturaEsperada)
                    {
                        pilaDelimitadores.Pop();
                    }
                    else
                    {
                        listaErrores.Add(new tbError(idErrorCounter++, lineaActual, columnaActual, c.ToString(), $"Delimitador de cierre '{c}' sin apertura correspondiente"));
                    }

                    // SI ES UNA LLAVE DE CIERRE '}', EL BLOQUE/SENTENCIA YA FINALIZÓ
                    if (c == '}')
                    {
                        sentenciaActiva = false;
                    }

                    listaTokens.Add(new tbResultados(idResultCounter++, 6, c.ToString(), "TK_PUNTUACION", lineaActual, columnaActual));
                    posicion++; columnaActual++;
                    continue;
                }

                // Control del punto y coma ";" para cierre de sentencia
                if (c == ';')
                {
                    sentenciaActiva = false; // Se cierra la sentencia actual correctamente
                    listaTokens.Add(new tbResultados(idResultCounter++, 6, c.ToString(), "TK_PUNTUACION", lineaActual, columnaActual));
                    posicion++; columnaActual++;
                    continue;
                }

                if (c == ':' || c == ',' || c == '.')
                {
                    listaTokens.Add(new tbResultados(idResultCounter++, 6, c.ToString(), "TK_PUNTUACION", lineaActual, columnaActual));
                    posicion++; columnaActual++;
                    continue;
                }

                if (c == '<' || c == '>')
                {
                    listaTokens.Add(new tbResultados(idResultCounter++, 8, c.ToString(), "TK_OP_RELACIONALES", lineaActual, columnaActual));
                    posicion++; columnaActual++;
                    continue;
                }
                if (c == '=' || c == '!' || c == 'v' || c == '^' || c == '\\')
                {
                    string tipoOp = (c == '=') ? "TK_OP_RELACIONALES" : "TK_OP_LOGICO";
                    listaTokens.Add(new tbResultados(idResultCounter++, 7, c.ToString(), tipoOp, lineaActual, columnaActual));
                    posicion++; columnaActual++;
                    continue;
                }

                listaErrores.Add(new tbError(idErrorCounter++, lineaActual, columnaActual, c.ToString(), "Carácter no reconocido en el alfabeto del lenguaje"));
                posicion++;
                columnaActual++;
            }

            // Validación al finalizar el archivo: Si quedaron delimitadores abiertos sin cerrar
            while (pilaDelimitadores.Count > 0)
            {
                var abierto = pilaDelimitadores.Pop();
                listaErrores.Add(new tbError(idErrorCounter++, abierto.linea, abierto.columna, abierto.caracter.ToString(), $"Sentencia abierta: El delimitador '{abierto.caracter}' nunca fue cerrado"));
            }

            // Validación de sentencia activa pendiente de punto y coma al final de la entrada
            if (sentenciaActiva)
            {
                listaErrores.Add(new tbError(idErrorCounter++, lineaInicioSentencia, colInicioSentencia, ";", "Error sintáctico: Se esperaba un punto y coma ';' al final de la sentencia"));
            }
        }
    }
}