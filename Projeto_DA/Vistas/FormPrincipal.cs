using Projeto_DA.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto_DA.Vistas
{
    public partial class FormPrincipal : Form
    {


        public FormPrincipal()
        {
            InitializeComponent();


        }

        private void mudarForm(Form newForm)
        {
            newForm.FormClosed += (s, e) => this.Close();
            newForm.Show();
            this.Hide();
        }

        private void btn_TrocarFunc_Click(object sender, EventArgs e)
        {
            var formFuncionarios = new FormFuncionarios();
            mudarForm(formFuncionarios);
        }

        private void btn_Func_Click(object sender, EventArgs e)
        {
            var formFuncionarios = new FormFuncionarios();
            mudarForm(formFuncionarios);
        }

        private void btn_Menu_Click(object sender, EventArgs e)
        {
            var formMenu = new FormMenu();
            mudarForm(formMenu);
        }

        private void btn_Reserva_Click(object sender, EventArgs e)
        {
            var formReservas = new FormReservas();
            mudarForm(formReservas);
        }

        private void btn_Clientes_Click(object sender, EventArgs e)
        {
            var formClientes = new FormClientes();
            mudarForm(formClientes);
        }

        private void btn_Pratos_Click(object sender, EventArgs e)
        {
            var formPratos = new FormPratos();
            mudarForm(formPratos);
        }

        private void btn_Multa_Click(object sender, EventArgs e)
        {
            var formMulta = new FormMulta();
            mudarForm(formMulta);
        }

        private void btn_Extras_Click(object sender, EventArgs e)
        {
            var formExtras = new FormExtras();
            mudarForm(formExtras);
        }

        private void icon_Sair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Calendar_Principal_DateChanged(object sender, DateRangeEventArgs e)
        {
            //desativar todos os dias anteriores ao atual 
            Calendar_Principal.MinDate = DateTime.Now;
            Calendar_Principal.Show();
        }

        // MÉTODO PARA ATUALIZAR A LABEL COM O NOME DO FUNCIONARIO LOGADO
        public void colocarUsernameFuncionario(int Id)
        {
            var db = new ApplicationContext();
            var funcionario = db.Utilizadores.Find(Id) as Funcionario; // procura o funcionario pelo id recebido
            lab_NomeFunc.Text = funcionario.UsernameFuncionario; // para aparecer o nome na label
        }
    }
}
