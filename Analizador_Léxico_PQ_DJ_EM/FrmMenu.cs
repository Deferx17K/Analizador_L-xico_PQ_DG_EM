using System;
using System.Windows.Forms;

namespace Analizador_Léxico_PQ_DJ_EM
{
    public partial class FrmMenu : Form
    {
        public FrmMenu()
        {
            InitializeComponent();
        }

        private void btnAnalizador_Click(object sender, EventArgs e)
        {
            FrmAnalizador FrmMenu = new FrmAnalizador();
            this.Hide();
            FrmMenu.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmTokens FrmTokens = new FrmTokens();
            this.Hide();
            FrmTokens.Show();
        }
    }
}
