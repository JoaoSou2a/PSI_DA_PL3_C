using Projeto_DA.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Projeto_DA
{
    public partial class FormClientes : Form
    {

        public FormClientes()
        {
            InitializeComponent();
        }


        public bool VerificarInfoCliente()
        {
            // Validar se as variáveis das TextBox não estão vazias
            // Retorna false se qualquer condição não for acumprida
            string nomeCliente = txt_NomeCliente.Text;
            if (nomeCliente.Length < 3)
            {
                MessageBox.Show("O campo 'nome' não pode ter menos de 2 caracteres!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            int nifCliente;
            if (txt_NIF_Cliente.Text.Length != 9 || !int.TryParse(txt_NIF_Cliente.Text, out nifCliente))
            {
                MessageBox.Show("O campo 'NIF' tem de ter 9 digitos!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            decimal saldoCliente;
            if (!decimal.TryParse(txt_Saldo.Text, out saldoCliente))
            {
                MessageBox.Show("O campo 'saldo' tem de ser um número positivo!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            string emailCliente;
            if (CheckBox_Professor.Checked)
            {
                emailCliente = txt_Email.Text;
                if (emailCliente.Length < 3)
                {
                    MessageBox.Show("O campo 'email' não pode ter menos de 5 caracteres!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                // Validação do formato do email
                var emailRegex = new System.Text.RegularExpressions.Regex(@"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$");
                if (!emailRegex.IsMatch(emailCliente))
                {
                    MessageBox.Show("O campo 'email' não está num formato válido!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }


            int numEstudante;
            if (CheckBox_Estudante.Checked)
            {
                if (!int.TryParse(txt_NumEstudante.Text, out numEstudante))
                {
                    MessageBox.Show("O campo 'número de estudante' tem de ser um número positivo!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }

        private void btn_Adicionar_Click_2(object sender, EventArgs e)
        {
            if (!VerificarInfoCliente())
            {
                return;
            }

            // Guarda os valores colocados nas variaveis
            string nomeCliente = txt_NomeCliente.Text;
            int nifCliente = int.Parse(txt_NIF_Cliente.Text); // Converte o texto para Int
            decimal saldoCliente = decimal.Parse(txt_Saldo.Text);
            string emailCliente = txt_Email.Text;
            int numEstudante = 0;
            if (CheckBox_Estudante.Checked && !string.IsNullOrEmpty(txt_NumEstudante.Text))
            {
                int.TryParse(txt_NumEstudante.Text, out numEstudante);
            }

            try
            {
                if (listB_Clientes.SelectedIndex != -1) // se tiver um cliente selecionado, altera os dados
                {
                    Cliente clienteSelecionado = (Cliente)listB_Clientes.SelectedItem;
                    // Altera os dados do Cliente selecionado
                    clienteSelecionado.UtilizadorNome = nomeCliente;
                    clienteSelecionado.UtilizadorNIF = nifCliente;
                    clienteSelecionado.Saldo = saldoCliente;

                    // Adiciona as informações do email ou do número do estudante
                    if (CheckBox_Professor.Checked && clienteSelecionado is Professor professor)
                    {
                        professor.Email = emailCliente;
                    }
                    else if (CheckBox_Estudante.Checked && clienteSelecionado is Estudante estudante)
                    {
                        estudante.NumEstudante = numEstudante;
                    }

                    // Atualizar a exibição do cliente na ListBox
                    int editarCliente = listB_Clientes.SelectedIndex;
                    listB_Clientes.Items[editarCliente] = clienteSelecionado;

                    using (var db = new ApplicationContext())
                    {
                        db.Utilizadores.AddOrUpdate(clienteSelecionado);
                        db.SaveChanges();
                    }
                }
                else // Caso não tenha, cria um novo
                {
                    Cliente novoCliente;

                    if (CheckBox_Professor.Checked)
                    {
                        novoCliente = new Professor(nomeCliente, nifCliente, saldoCliente, emailCliente);
                    }
                    else
                    {
                        novoCliente = new Estudante(nomeCliente, nifCliente, saldoCliente, numEstudante);
                    }

                    listB_Clientes.Items.Add(novoCliente); // Antes de atualizar a base de dados, mostra na Listbox
                    using (var db = new ApplicationContext())
                    {
                        db.Utilizadores.Add(novoCliente);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                //Se houver algum erro, mostra uma mensagem de erro
                MessageBox.Show($"Erro ao guardar o cliente: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Limpa as textbox
            txt_NomeCliente.Clear();
            txt_NIF_Cliente.Clear();
            txt_Saldo.Clear();
            txt_Email.Clear();
            txt_NumEstudante.Clear();
        }

        private void btn_Apagar_Click(object sender, EventArgs e)
        {
            int apagarCliente = listB_Clientes.SelectedIndex;
            if (apagarCliente == -1)
            {
                // Caso n tenha um cliente selecionado aparece uma mensagem de erro
                MessageBox.Show("Selecione um cliente para apagar!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (listB_Clientes.Items[apagarCliente] is Cliente cliente)
            {
                // Caso tenha um cliente selecionado apaga da ListBox
                listB_Clientes.Items.RemoveAt(apagarCliente);
                // Apaga da base de dados
                var db = new ApplicationContext();
                var apagarClienteDb = db.Utilizadores.Find(cliente.Id);
                if (apagarClienteDb != null)
                {
                    db.Utilizadores.Remove(apagarClienteDb);
                    db.SaveChanges();
                }
            }
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

        private void lab_MenuDiario_Click(object sender, EventArgs e)
        {
            var formMenu = new FormMenu();
            mudarForm(formMenu);
        }

        private void lab_Reserva_Click(object sender, EventArgs e)
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


        //LIMITAÇÕES DAS CHECKBOX NOS CLIENTE
        private void CheckBox_Estudante_CheckedChanged_1(object sender, EventArgs e)
        {
            if (CheckBox_Estudante.Checked)
            {
                txt_Email.Enabled = false;
                CheckBox_Professor.Enabled = false;
            }
            else
            {
                txt_Email.Enabled = true;
                CheckBox_Professor.Enabled = true;
            }
        }

        private void CheckBox_Professor_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox_Professor.Checked)
            {
                txt_NumEstudante.Enabled = false;
                CheckBox_Estudante.Enabled = false;
            }
            else
            {
                txt_NumEstudante.Enabled = true;
                CheckBox_Estudante.Enabled = true;
            }
        }

        private void listB_Clientes_DoubleClick(object sender, MouseEventArgs e)
        {
            // Executar esta codigo apenas quando o cliente for selecionado com um duplo clique
            int clienteSelecionado = listB_Clientes.SelectedIndex;
            if (clienteSelecionado != -1)
            {
                Cliente cliente = (Cliente)listB_Clientes.SelectedItem;
                txt_NomeCliente.Text = cliente.UtilizadorNome;
                txt_NIF_Cliente.Text = cliente.UtilizadorNIF.ToString();
                txt_Saldo.Text = cliente.Saldo.ToString();
            }
            else
            {
                // Mostrar mensagem de erro se nenhum funcionário estiver selecionado
                MessageBox.Show("Selecione um Funcionário!", "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void limparDadosInseridos()
        {
            listB_Clientes.ClearSelected();
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            listB_Clientes.ClearSelected();
            limparDadosInseridos();
        }
    }
}
