using Analizador_Léxico_PQ_DJ_EM.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Analizador_Léxico_PQ_DJ_EM
{
    public partial class FrmAnalizador : Form
    {
        private LecturaAnalizador motor;
        private double ultimoTiempoMs = 0;
        private bool aplicandoResaltado = false;

        // Para bloquear redibujado durante el resaltado y evitar parpadeos
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);
        private const int WM_SETREDRAW = 0x000B;

        public FrmAnalizador()
        {
            InitializeComponent();
            motor = new LecturaAnalizador();
        }

        public class CategoriaEstadistica
        {
            public string Categoria { get; set; }
            public int Cantidad { get; set; }
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Archivos de código/texto (*.txt;*.cs)|*.txt;*.cs|Todos los archivos (*.*)|*.*",
                Title = "Seleccionar archivo de código fuente"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    rtbTexto.Text = File.ReadAllText(openFileDialog.FileName, Encoding.UTF8);
                    AplicarResaltadoSintaxis();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al leer el archivo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAnalizar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(rtbTexto.Text))
            {
                MessageBox.Show("Ingrese o cargue código fuente para analizar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Medición del tiempo de análisis
            Stopwatch cronometro = Stopwatch.StartNew();
            DatosCompartidos.Motor.AnalizarTexto(rtbTexto.Text);
            cronometro.Stop();
            ultimoTiempoMs = cronometro.Elapsed.TotalMilliseconds;

            var tokens = DatosCompartidos.Motor.ObtenerTokens();
            var errores = DatosCompartidos.Motor.ObtenerErrores();
            var simbolos = DatosCompartidos.Motor.ObtenerTablaSimbolos().ObtenerSimbolos().ToList();

            // Asignación de resultados a las tablas
            dgvResultado.DataSource = null;
            dgvResultado.DataSource = new BindingList<tbResultados>(tokens);

            dgvError.DataSource = null;
            dgvError.DataSource = new BindingList<tbError>(errores);

            dgvSimbolos.DataSource = null;
            dgvSimbolos.DataSource = new BindingList<ElementoSimbolo>(simbolos);

            // Actualización de títulos en pestañas con contadores
            tabPaginaTokens.Text = $"Tokens Encontrados ({tokens.Count})";
            tabPaginaErrores.Text = $"Errores Léxicos ({errores.Count})";
            tabPaginaSimbolos.Text = $"Tabla de Símbolos ({simbolos.Count})";

            // Selección inteligente de pestaña: si hay errores se resalta la pestaña de errores
            if (errores.Count > 0)
            {
                tabResultados.SelectedTab = tabPaginaErrores;
            }
            else
            {
                tabResultados.SelectedTab = tabPaginaTokens;
            }

            // Punto 1: Panel de Estadísticas del Análisis
            ActualizarPanelEstadisticas(tokens, errores, ultimoTiempoMs);

            // Punto 3: Resaltado de sintaxis básico
            AplicarResaltadoSintaxis();
        }

        private void ActualizarPanelEstadisticas(List<tbResultados> tokens, List<tbError> errores, double tiempoMs)
        {
            int totalLineas = string.IsNullOrEmpty(rtbTexto.Text) ? 0 : rtbTexto.Lines.Length;
            if (totalLineas == 0 && !string.IsNullOrEmpty(rtbTexto.Text)) totalLineas = 1;

            lblStatLineas.Text = $"Total de líneas: {totalLineas}";
            lblStatTokens.Text = $"Total de tokens: {tokens.Count}";
            lblStatErrores.Text = $"Total de errores: {errores.Count}";
            lblStatTiempo.Text = $"Tiempo aprox: {tiempoMs:F2} ms";

            var desgloseCategorias = tokens
                .GroupBy(t => string.IsNullOrWhiteSpace(t.TipoToken) ? "SIN_TIPO" : t.TipoToken)
                .Select(g => new CategoriaEstadistica
                {
                    Categoria = g.Key,
                    Cantidad = g.Count()
                })
                .OrderByDescending(c => c.Cantidad)
                .ToList();

            dgvEstadisticas.DataSource = null;
            dgvEstadisticas.DataSource = desgloseCategorias;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            rtbTexto.Clear();
            dgvResultado.DataSource = null;
            dgvError.DataSource = null;
            dgvSimbolos.DataSource = null;
            dgvEstadisticas.DataSource = null;

            lblStatLineas.Text = "Total de líneas: 0";
            lblStatTokens.Text = "Total de tokens: 0";
            lblStatErrores.Text = "Total de errores: 0";
            lblStatTiempo.Text = "Tiempo aprox: 0.00 ms";

            tabPaginaTokens.Text = "Tokens Encontrados";
            tabPaginaErrores.Text = "Errores Léxicos";
            tabPaginaSimbolos.Text = "Tabla de Símbolos";
            ultimoTiempoMs = 0;
        }

        
        private void btnExportar_Click(object sender, EventArgs e)
        {
            
            EjecutarExportacion("TODO");
        }

        private void menuExportarTodo_Click(object sender, EventArgs e) => EjecutarExportacion("TODO");
        private void menuExportarTokens_Click(object sender, EventArgs e) => EjecutarExportacion("TOKENS");
        private void menuExportarErrores_Click(object sender, EventArgs e) => EjecutarExportacion("ERRORES");
        private void menuExportarSimbolos_Click(object sender, EventArgs e) => EjecutarExportacion("SIMBOLOS");
        private void menuExportarEstadisticas_Click(object sender, EventArgs e) => EjecutarExportacion("ESTADISTICAS");

        private void EjecutarExportacion(string tipo)
        {
            var tokens = DatosCompartidos.Motor.ObtenerTokens();
            var errores = DatosCompartidos.Motor.ObtenerErrores();

            if ((tokens == null || tokens.Count == 0) && (errores == null || errores.Count == 0))
            {
                MessageBox.Show("No hay resultados de análisis disponibles para exportar. Por favor analice código primero.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                string etiqueta = tipo == "TODO" ? "Completo" : tipo;
                sfd.Title = $"Exportar Resultados del Análisis ({etiqueta})";
                sfd.Filter = "Archivo CSV (*.csv)|*.csv|Archivo de Texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
                sfd.FileName = $"Analisis_Lexico_{etiqueta}_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string ext = Path.GetExtension(sfd.FileName).ToLower();
                        if (ext == ".csv")
                        {
                            GenerarCSV(sfd.FileName, tipo);
                        }
                        else
                        {
                            GenerarTXT(sfd.FileName, tipo);
                        }

                        MessageBox.Show($"Archivo exportado exitosamente:\n{sfd.FileName}", "Exportación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al exportar los resultados: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private string EscapeCsv(string valor)
        {
            if (string.IsNullOrEmpty(valor)) return "";
            return valor.Replace("\"", "\"\"");
        }

        private void GenerarCSV(string ruta, string tipo)
        {
            StringBuilder sb = new StringBuilder();
            var tokens = DatosCompartidos.Motor.ObtenerTokens();
            var errores = DatosCompartidos.Motor.ObtenerErrores();
            var simbolos = DatosCompartidos.Motor.ObtenerTablaSimbolos().ObtenerSimbolos().ToList();
            int totalLineas = string.IsNullOrEmpty(rtbTexto.Text) ? 0 : rtbTexto.Lines.Length;

            if (tipo == "TODO" || tipo == "ESTADISTICAS")
            {
                sb.AppendLine("=== ESTADISTICAS DEL ANALISIS ===");
                sb.AppendLine("Metrica,Valor");
                sb.AppendLine($"Total de lineas,{totalLineas}");
                sb.AppendLine($"Total de tokens,{tokens.Count}");
                sb.AppendLine($"Total de errores,{errores.Count}");
                sb.AppendLine($"Tiempo de analisis aproximado,{ultimoTiempoMs:F2} ms");
                sb.AppendLine();
                sb.AppendLine("Categoria de Token,Cantidad");
                foreach (var cat in tokens.GroupBy(t => t.TipoToken).Select(g => new { Categoria = g.Key, Total = g.Count() }).OrderByDescending(x => x.Total))
                {
                    sb.AppendLine($"\"{EscapeCsv(cat.Categoria)}\",{cat.Total}");
                }
                sb.AppendLine();
            }

            if (tipo == "TODO" || tipo == "TOKENS")
            {
                sb.AppendLine("=== LISTADO DE TOKENS ===");
                sb.AppendLine("#,Lexema,Tipo de Token,Linea,Columna");
                foreach (var t in tokens)
                {
                    sb.AppendLine($"{t.IdResultado},\"{EscapeCsv(t.Identificador)}\",\"{EscapeCsv(t.TipoToken)}\",{t.Linea},{t.Columna}");
                }
                sb.AppendLine();
            }

            if (tipo == "TODO" || tipo == "ERRORES")
            {
                sb.AppendLine("=== LISTADO DE ERRORES LEXICOS ===");
                sb.AppendLine("#,Linea,Columna,Caracter/Lexema,Descripcion del Error");
                foreach (var err in errores)
                {
                    sb.AppendLine($"{err.IdError},{err.LineaError},{err.ColumnaError},\"{EscapeCsv(err.Error)}\",\"{EscapeCsv(err.Observacion)}\"");
                }
                sb.AppendLine();
            }

            if (tipo == "TODO" || tipo == "SIMBOLOS")
            {
                sb.AppendLine("=== TABLA DE SIMBOLOS ===");
                sb.AppendLine("Nombre,Categoria,Tipo de Dato,Linea,Ambito");
                foreach (var s in simbolos)
                {
                    sb.AppendLine($"\"{EscapeCsv(s.Nombre)}\",\"{EscapeCsv(s.CategoriaToken)}\",\"{EscapeCsv(s.TipoDato)}\",{s.Linea},\"{EscapeCsv(s.Ambito)}\"");
                }
                sb.AppendLine();
            }

            File.WriteAllText(ruta, sb.ToString(), Encoding.UTF8);
        }

        private void GenerarTXT(string ruta, string tipo)
        {
            StringBuilder sb = new StringBuilder();
            var tokens = DatosCompartidos.Motor.ObtenerTokens();
            var errores = DatosCompartidos.Motor.ObtenerErrores();
            var simbolos = DatosCompartidos.Motor.ObtenerTablaSimbolos().ObtenerSimbolos().ToList();
            int totalLineas = string.IsNullOrEmpty(rtbTexto.Text) ? 0 : rtbTexto.Lines.Length;

            sb.AppendLine("================================================================================");
            sb.AppendLine("                      REPORTE DE ANALISIS LEXICO");
            sb.AppendLine("================================================================================");
            sb.AppendLine($"Fecha y Hora de Generación: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            sb.AppendLine($"Archivo de Salida          : {Path.GetFileName(ruta)}");
            sb.AppendLine("================================================================================");
            sb.AppendLine();

            if (tipo == "TODO" || tipo == "ESTADISTICAS")
            {
                sb.AppendLine("--------------------------------------------------------------------------------");
                sb.AppendLine("1. PANEL DE ESTADÍSTICAS");
                sb.AppendLine("--------------------------------------------------------------------------------");
                sb.AppendLine($"Total de líneas analizadas : {totalLineas}");
                sb.AppendLine($"Total de tokens reconocidos: {tokens.Count}");
                sb.AppendLine($"Total de errores léxicos   : {errores.Count}");
                sb.AppendLine($"Tiempo de análisis aprox.  : {ultimoTiempoMs:F2} ms");
                sb.AppendLine();
                sb.AppendLine("Desglose por Categoría:");
                sb.AppendLine(string.Format("{0,-35} | {1,10}", "Categoría de Token", "Cantidad"));
                sb.AppendLine(new string('-', 50));
                foreach (var cat in tokens.GroupBy(t => t.TipoToken).Select(g => new { Categoria = g.Key, Total = g.Count() }).OrderByDescending(x => x.Total))
                {
                    sb.AppendLine(string.Format("{0,-35} | {1,10}", cat.Categoria, cat.Total));
                }
                sb.AppendLine();
            }

            if (tipo == "TODO" || tipo == "TOKENS")
            {
                sb.AppendLine("--------------------------------------------------------------------------------");
                sb.AppendLine("2. LISTADO DE TOKENS");
                sb.AppendLine("--------------------------------------------------------------------------------");
                sb.AppendLine(string.Format("{0,-6} | {1,-20} | {2,-30} | {3,-6} | {4,-6}", "#", "Lexema", "Tipo de Token", "Línea", "Col"));
                sb.AppendLine(new string('-', 78));
                foreach (var t in tokens)
                {
                    sb.AppendLine(string.Format("{0,-6} | {1,-20} | {2,-30} | {3,-6} | {4,-6}", t.IdResultado, t.Identificador, t.TipoToken, t.Linea, t.Columna));
                }
                sb.AppendLine();
            }

            if (tipo == "TODO" || tipo == "ERRORES")
            {
                sb.AppendLine("--------------------------------------------------------------------------------");
                sb.AppendLine("3. LISTADO DE ERRORES LÉXICOS");
                sb.AppendLine("--------------------------------------------------------------------------------");
                if (errores.Count == 0)
                {
                    sb.AppendLine("No se detectaron errores léxicos en el código analizado.");
                }
                else
                {
                    sb.AppendLine(string.Format("{0,-6} | {1,-6} | {2,-6} | {3,-15} | {4,-35}", "#", "Línea", "Col", "Carácter", "Observación"));
                    sb.AppendLine(new string('-', 78));
                    foreach (var err in errores)
                    {
                        sb.AppendLine(string.Format("{0,-6} | {1,-6} | {2,-6} | {3,-15} | {4,-35}", err.IdError, err.LineaError, err.ColumnaError, err.Error, err.Observacion));
                    }
                }
                sb.AppendLine();
            }

            if (tipo == "TODO" || tipo == "SIMBOLOS")
            {
                sb.AppendLine("--------------------------------------------------------------------------------");
                sb.AppendLine("4. TABLA DE SÍMBOLOS");
                sb.AppendLine("--------------------------------------------------------------------------------");
                if (simbolos.Count == 0)
                {
                    sb.AppendLine("No se registraron identificadores en la tabla de símbolos.");
                }
                else
                {
                    sb.AppendLine(string.Format("{0,-25} | {1,-25} | {2,-15} | {3,-6} | {4,-12}", "Nombre", "Categoría", "Tipo Dato", "Línea", "Ámbito"));
                    sb.AppendLine(new string('-', 88));
                    foreach (var s in simbolos)
                    {
                        sb.AppendLine(string.Format("{0,-25} | {1,-25} | {2,-15} | {3,-6} | {4,-12}", s.Nombre, s.CategoriaToken, s.TipoDato, s.Linea, s.Ambito));
                    }
                }
                sb.AppendLine();
            }

            File.WriteAllText(ruta, sb.ToString(), Encoding.UTF8);
        }

    
        private void rtbTexto_TextChanged(object sender, EventArgs e)
        {
            if (chkResaltado.Checked)
            {
                timerResaltado.Stop();
                timerResaltado.Start();
            }
        }

        private void timerResaltado_Tick(object sender, EventArgs e)
        {
            timerResaltado.Stop();
            AplicarResaltadoSintaxis();
        }

        private void chkResaltado_CheckedChanged(object sender, EventArgs e)
        {
            if (chkResaltado.Checked)
            {
                AplicarResaltadoSintaxis();
            }
            else
            {
                // Restaurar texto a formato plano
                int selStart = rtbTexto.SelectionStart;
                int selLength = rtbTexto.SelectionLength;
                rtbTexto.SelectAll();
                rtbTexto.SelectionColor = Color.Black;
                rtbTexto.SelectionFont = new Font(rtbTexto.Font.FontFamily, rtbTexto.Font.Size, FontStyle.Regular);
                rtbTexto.Select(selStart, selLength);
            }
        }

        private void AplicarResaltadoSintaxis()
        {
            if (!chkResaltado.Checked || aplicandoResaltado) return;
            if (string.IsNullOrEmpty(rtbTexto.Text)) return;

            aplicandoResaltado = true;
            int selStart = rtbTexto.SelectionStart;
            int selLength = rtbTexto.SelectionLength;

            // Bloquear actualización visual del control para evitar parpadeo
            SendMessage(rtbTexto.Handle, WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero);

            try
            {
                // 1. Restaurar base neutra
                rtbTexto.SelectAll();
                rtbTexto.SelectionColor = Color.Black;
                rtbTexto.SelectionFont = new Font(rtbTexto.Font.FontFamily, rtbTexto.Font.Size, FontStyle.Regular);

                string texto = rtbTexto.Text;

                // 2. Colorear Números (morado/púrpura)
                foreach (Match m in Regex.Matches(texto, @"\b\d+(\.\d+)?\b"))
                {
                    rtbTexto.Select(m.Index, m.Length);
                    rtbTexto.SelectionColor = Color.DarkMagenta;
                }

                // 3. Colorear Operadores (gris oscuro)
                foreach (Match m in Regex.Matches(texto, @"(==|!=|<=|>=|&&|\|\||[+\-*/=<>!^v\\])"))
                {
                    rtbTexto.Select(m.Index, m.Length);
                    rtbTexto.SelectionColor = Color.DarkSlateGray;
                }

                // 4. Colorear Palabras Reservadas (azul y negrita)
                var listaKeywords = DatosCompartidos.Motor.ObtenerPalabrasReservadas();
                if (listaKeywords != null && listaKeywords.Any())
                {
                    string patronKeywords = @"\b(" + string.Join("|", listaKeywords.Select(Regex.Escape)) + @")\b";
                    foreach (Match m in Regex.Matches(texto, patronKeywords, RegexOptions.IgnoreCase))
                    {
                        rtbTexto.Select(m.Index, m.Length);
                        rtbTexto.SelectionColor = Color.Blue;
                        rtbTexto.SelectionFont = new Font(rtbTexto.Font.FontFamily, rtbTexto.Font.Size, FontStyle.Bold);
                    }
                }

                // 5. Colorear Cadenas de Texto (rojo oscuro / marrón)
                foreach (Match m in Regex.Matches(texto, @"""[^""\r\n]*""?"))
                {
                    rtbTexto.Select(m.Index, m.Length);
                    rtbTexto.SelectionColor = Color.FromArgb(163, 21, 21);
                    rtbTexto.SelectionFont = new Font(rtbTexto.Font.FontFamily, rtbTexto.Font.Size, FontStyle.Regular);
                }

                // 6. Colorear Comentarios de una línea (// ...) (verde e itálica)
                foreach (Match m in Regex.Matches(texto, @"//.*?$", RegexOptions.Multiline))
                {
                    rtbTexto.Select(m.Index, m.Length);
                    rtbTexto.SelectionColor = Color.ForestGreen;
                    rtbTexto.SelectionFont = new Font(rtbTexto.Font.FontFamily, rtbTexto.Font.Size, FontStyle.Italic);
                }

                // 7. Colorear Comentarios multilínea (/* ... */) (verde e itálica)
                foreach (Match m in Regex.Matches(texto, @"/\*[\s\S]*?(\*/|$)"))
                {
                    rtbTexto.Select(m.Index, m.Length);
                    rtbTexto.SelectionColor = Color.ForestGreen;
                    rtbTexto.SelectionFont = new Font(rtbTexto.Font.FontFamily, rtbTexto.Font.Size, FontStyle.Italic);
                }
            }
            catch
            {
                // Manejo silencioso ante texto anómalo
            }
            finally
            {
                rtbTexto.Select(selStart, selLength);
                SendMessage(rtbTexto.Handle, WM_SETREDRAW, new IntPtr(1), IntPtr.Zero);
                rtbTexto.Invalidate();
                aplicandoResaltado = false;
            }
        }

        public TablaDeSimbolos ObtenerTablaSimbolos()
        {
            return motor.ObtenerTablaSimbolos();
        }

        private void lbltokens_Click(object sender, EventArgs e)
        {
            FrmTokens FrmTokens = new FrmTokens();
            this.Hide();
            FrmTokens.Show();
        }
    }
}
