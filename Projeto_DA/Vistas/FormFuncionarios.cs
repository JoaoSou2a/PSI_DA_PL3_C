
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Diagnostics.Eventing.Reader;
using System.Data.Entity.Migrations;
using Projeto_DA.Models;
using Projeto_DA.Vistas;

namespace Projeto_DA
{
    public partial class FormFuncionarios : Form
    {

        public FormFuncionarios()
        {
            InitializeComponent();
            atualizarDadosAoEntrar();
        }

        private void atualizarDadosAoEntrar()
        {
            using (var db = new ApplicationContext())
            {
                var funcionarios = db.Utilizadores.OfType<Funcionario>();
                foreach (var funcionario in funcionarios) //correr os funcionarios para os adicionar à listBox 
                {
                    lb_Func.Items.Add(funcionario);
                }
            }
        }

        public bool verificarInfoFunc()
        {
            string UserNameFunc = txt_UserNameFunc.Text;
            if (UserNameFunc.Length < 2)
            {
                MessageBox.Show("O campo 'User Name' tem de ter mais de 2 caracteres!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            string NomeCompleto = txt_NomeCpltFunc.Text;
            if (NomeCompleto.Length < 5)
            {
                MessageBox.Show("O campo 'Nome Completo' tem de ter mais de 5 caracteres!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            int NIF;
            if (txt_NIF_Func.Text.Length != 9 || !int.TryParse(txt_NIF_Func.Text, out NIF))
            {
                MessageBox.Show("O valor no campo 'NIF' não é válido!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            else
            {
                return true;
            }
        }

        private void btn_Adicionar_Click(object sender, EventArgs e)
        {
            // Vê se cumpre todas as condições, caso não cumpra qualquer condição vai saltar fora
            if (!verificarInfoFunc())
            {// Caso seja verificado corretamente vai continuar
                return;
            }

            // Guarda os valores inseridos nas variáveis
            string usernameFuncionario = txt_UserNameFunc.Text;
            int utilizadorNIF = int.Parse(txt_NIF_Func.Text); // Converte de texto para int
            string utilizadorNome = txt_NomeCpltFunc.Text;

            try
            {
                Funcionario funcionario = new Funcionario(usernameFuncionario, utilizadorNIF, utilizadorNome.ToString());
                txt_NomeCpltFunc.Text = funcionario.UtilizadorNome;
                txt_UserNameFunc.Text = funcionario.UsernameFuncionario;
                txt_NIF_Func.Text = funcionario.UtilizadorNIF.ToString();
            }
            catch (Exception)
            {
                // Caso exista algum erro
                MessageBox.Show("Erro ao Criar um novo Funcionário!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (lb_Func.SelectedIndex != -1) // Caso esteja um funcionario selecionado, edita os dados
            {
                Funcionario funcionarioSelecionado = (Funcionario)lb_Func.SelectedItem;
                // Altera os dados do funcionario selecionado
                funcionarioSelecionado.UsernameFuncionario = txt_NomeCpltFunc.Text;
                funcionarioSelecionado.UtilizadorNIF = int.Parse(txt_NIF_Func.Text);
                funcionarioSelecionado.UtilizadorNome = txt_NomeCpltFunc.Text;

                int editarfuncionario = lb_Func.SelectedIndex;
                lb_Func.Items[editarfuncionario] = funcionarioSelecionado;

                using (var db = new ApplicationContext())
                {
                    db.Utilizadores.AddOrUpdate(funcionarioSelecionado);
                    db.SaveChanges();
                }
            }
            else // Se não tiver um Funcionário, cria um novo
            {
                Funcionario novofuncionario = new Funcionario(txt_UserNameFunc.Text, int.Parse(txt_NIF_Func.Text), txt_NomeCpltFunc.Text);

                lb_Func.Items.Add(novofuncionario); // mostra na listbox antes de atualizar a db
                using (var db = new ApplicationContext())
                {
                    // Novo funcionario
                    db.Utilizadores.Add(novofuncionario);
                    db.SaveChanges();
                }
            }
        }

        private void btn_Apagar_Click(object sender, EventArgs e)
        {
            int apagarFunc = lb_Func.SelectedIndex;
            if (apagarFunc == -1)
            {
                // Caso n tenha um Funcionario selecionado aparece uma mensagem de erro
                MessageBox.Show("Selecione um Funcionário!", "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (lb_Func.Items[apagarFunc] is Funcionario funcionario)
            {
                //se tiver funcionario selecionado
                // apaga da listbox
                lb_Func.Items.Remove(funcionario);
                //apaga da base de dados
                var db = new ApplicationContext();
                var apagarfuncionario = db.Utilizadores.Find(funcionario.Id); // buscar o id do funcionario q queremos apagar
                if (apagarfuncionario != null) // so faz isso se tiver um funcionario
                {
                    db.Utilizadores.Remove(apagarfuncionario); // remove funcionario pelo id
                    db.SaveChanges(); // guarda as alterações na base de dados
                }
            }
        }

        private void mudarForm(Form newForm)
        {
            newForm.FormClosed += (s, e) => this.Close();
            newForm.Show();
            this.Hide();
        }

        private void lab_Clientes_Click(object sender, EventArgs e)
        {
            var formClientes = new FormClientes();
            mudarForm(formClientes);
        }

        private void lab_Menu_Click(object sender, EventArgs e)
        {
            var formMenu = new FormMenu();
            mudarForm(formMenu);
        }

        private void lab_Reservas_Click(object sender, EventArgs e)
        {
            var formReservas = new FormReservas();
            mudarForm(formReservas);
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

        private void lb_Func_SelectedIndexChanged(object sender, EventArgs e)
        {
            int escolherFunc = lb_Func.SelectedIndex;
            if (escolherFunc != -1)
            {
                using (var db = new ApplicationContext())
                {
                    Funcionario funcionarioSelecionado = (Funcionario)lb_Func.SelectedItem; // descobrir o que será indicado nas textbox ao selecionar na listBox
                    // mostrar na textBox os dados do funcionario selecionado                                                                                   
                    txt_UserNameFunc.Text = funcionarioSelecionado.UsernameFuncionario;
                    txt_NIF_Func.Text = funcionarioSelecionado.UtilizadorNIF.ToString();
                    txt_NomeCpltFunc.Text = funcionarioSelecionado.UtilizadorNome;

                }
            }
        }

        public void limparDadosInseridos()
        {
            txt_UserNameFunc.Clear();
            txt_NIF_Func.Clear();
            txt_NomeCpltFunc.Clear();
        }

        private void FormFuncionarios_Load(object sender, EventArgs e)
        {
            lb_Func.ClearSelected();
            limparDadosInseridos();
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            lb_Func.ClearSelected();
            limparDadosInseridos();
        }
    }
}
