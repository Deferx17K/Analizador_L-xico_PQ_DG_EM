using Analizador_Léxico_PQ_DJ_EM.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using System.Data;

namespace Analizador_Léxico_PQ_DJ_EM
{
    public partial class FrmTokens : Form
    {
        public FrmTokens()
        {
            InitializeComponent();
        }

        int idTokenSeleccionado = 0;

        private void FrmTokens_Load(object sender, EventArgs e)
        {
            PalabrasReservadas();
        }

        public class ConexionBD
        {
            public SqlConnection ObtenerConexion()
            {
                string cadena = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=BDAnalizador; Integrated Security=True";
                return new SqlConnection(cadena);
            }

        }

        public void PalabrasReservadas()
        {
            ConexionBD conexionBD = new ConexionBD();
            try
            {
                using (SqlConnection conexion = conexionBD.ObtenerConexion())
                {
                    conexion.Open();
                    string sql = "SELECT IdToken, Token, Tipo FROM Token";
                    SqlDataAdapter sqlComando = new SqlDataAdapter(sql, conexion);
                    DataTable dt = new DataTable();
                    sqlComando.Fill(dt);
                    dgvTablaSimbolos.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener las palabras reservadas: " + ex.Message);
            }
        }

        private void btnAnalizar_Click(object sender, System.EventArgs e)
        {
            if (txtToken.Text != "" && txtSimbolo.Text != "")
            {
                ConexionBD conexionBD = new ConexionBD();
                try
                {
                    using (SqlConnection conexion = conexionBD.ObtenerConexion())
                    {
                        conexion.Open();
                        string sql = "INSERT INTO Token (Token, Tipo) VALUES (@Token, @Tipo)";
                        SqlCommand cmd = new SqlCommand(sql, conexion);
                        cmd.Parameters.AddWithValue("@Token", txtToken.Text);
                        cmd.Parameters.AddWithValue("@Tipo", txtSimbolo.Text);
                        cmd.ExecuteNonQuery();
                        PalabrasReservadas();
                        DatosCompartidos.Motor.PalabrasReservadas();
                        txtToken.Clear();
                        txtSimbolo.Clear();
                        MessageBox.Show("Palabra reservada Agregada","Exito", MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al agregar el token: " + ex.Message);
                    MessageBox.Show("No se pudo agregar el token: " + ex.Message, "Error BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else { 
                MessageBox.Show("Por favor, complete todos los campos antes de agregar un token.", "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEliminar_Click(object sender, System.EventArgs e)
        {
            if (idTokenSeleccionado > 0)
            {
                ConexionBD conexionBD = new ConexionBD();
                try
                {
                    using (SqlConnection conexion = conexionBD.ObtenerConexion())
                    {
                        conexion.Open();
                        string sql = "DELETE FROM Token WHERE IdToken = @IdToken";
                        SqlCommand cmd = new SqlCommand(sql, conexion);
                        cmd.Parameters.AddWithValue("@IdToken", idTokenSeleccionado);
                        cmd.ExecuteNonQuery();
                        PalabrasReservadas();
                        DatosCompartidos.Motor.PalabrasReservadas();
                        idTokenSeleccionado = 0;
                        MessageBox.Show("Palabra reservada Eliminada", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    idTokenSeleccionado = 0;
                    Console.WriteLine("Error al eliminar el token: " + ex.Message);
                    MessageBox.Show("No se pudo eliminar el token: " + ex.Message, "Error BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void lblAnalizador_Click(object sender, EventArgs e)
        {
            FrmAnalizador FrmAnalizador = new FrmAnalizador();
            this.Hide();
            FrmAnalizador.Show();
        }

        private void dgvTablaSimbolos_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Aqui quiero llenar mis txt con la celda seleccionada
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvTablaSimbolos.Rows[e.RowIndex];
                idTokenSeleccionado = Convert.ToInt32(row.Cells["IdToken"].Value);
                txtToken.Text = row.Cells["Token"].Value.ToString();
                txtSimbolo.Text = row.Cells["Tipo"].Value.ToString();
            }
        }
    }
}
