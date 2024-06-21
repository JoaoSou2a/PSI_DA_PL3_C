using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto_DA
{
    public partial class FormMenu : Form
    {
        public FormMenu()
        {
            InitializeComponent();
        }



        private void mudarForm(Form newForm)
        {
            newForm.FormClosed += (s, e) => this.Close();
            newForm.Show();
            this.Hide();
        }

        private void lab_Func_Click(object sender, EventArgs e)
        {
            var formFuncionarios = new FormFuncionarios();
            mudarForm(formFuncionarios);
        }

        private void lab_Clientes_Click(object sender, EventArgs e)
        {
            var formClientes = new FormClientes();
            mudarForm(formClientes);
        }

        private void lab_MenuDiario_Click(object sender, EventArgs e)
        {
            var formMenu = new FormMenu();
            mudarForm(formMenu);
        }

        private void lab_Pratos_Click(object sender, EventArgs e)
        {
            var formPratos = new FormPratos();
            mudarForm(formPratos);
        }

        private void lab_Extras_Click(object sender, EventArgs e)
        {
            var formExtras = new FormExtras();
            mudarForm(formExtras);
        }

        private void lab_Multa_Click(object sender, EventArgs e)
        {
            var formMulta = new FormMulta();
            mudarForm(formMulta);
        }

        private void icon_Sair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lab_Reserva_Click(object sender, EventArgs e)
        {
            var formReserva = new FormReservas();
            mudarForm(formReserva);
        }
    }
}
