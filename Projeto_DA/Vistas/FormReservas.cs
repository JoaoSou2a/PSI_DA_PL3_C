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

namespace Projeto_DA
{
    public partial class FormReservas : Form
    {
        public FormReservas()
        {
            InitializeComponent();
        }

        //---------------------------------------------------------Mudança de Página------------------------------------------------------------//

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

        //------------------------------------------------------------------btn_Pesquisar_Click-------------------------------------------//

        private void btn_Pesquisar_Click(object sender, EventArgs e)
        {
            string valorPesquisa = txt_NIF_Cliente.Text;

            using (var db = new ApplicationContext())
            {
                List<Cliente> clientes = new List<Cliente>();

                // Apaga a verificação que busca todos os clientes, mas só se a pesquisa estiver vazia
                if (!string.IsNullOrEmpty(valorPesquisa))
                {
                    // Se a pesquisa não estiver vazia, obtêm os clientes que correspondem à pesquisa
                    clientes = db.Utilizadores.OfType<Cliente>().Where(c => c.UtilizadorNIF.ToString().Contains(valorPesquisa)).ToList();
                }

                if (clientes.Any()) // Caso tenha algum cliente q corresponda à pesquisa
                {
                    txt_Cliente.Text = ""; // Limpa o texto antes de adicionar novos itens
                    foreach (var cliente in clientes)
                    {
                        txt_Cliente.Text += cliente.UtilizadorNome;
                        lab_Saldo.Text = cliente.Saldo.ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Cliente não encontrado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
