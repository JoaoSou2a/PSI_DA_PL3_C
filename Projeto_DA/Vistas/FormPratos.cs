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

namespace Projeto_DA
{
    public partial class FormPratos : Form
    {

        public FormPratos()
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

        private void lab_Reserva_Click(object sender, EventArgs e)
        {
            var formReservas = new FormReservas();
            mudarForm(formReservas);
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

        public bool verificarInfoPratos()
        {   // RECEBE VALORES DAS TEXTSBOX E VALIDA
            string nomePrato = txt_NomePrato.Text;
            if (nomePrato.Length == 0)
            {
                MessageBox.Show("Introduza o nome do Prato!", "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            string tipoPrato = ComboBox_Tipo.Text;
            if (tipoPrato.Length == 0)
            {
                MessageBox.Show("Escolha um tipo de Prato!", "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            string estadoPrato = ComboBox_Estado.Text;
            if (estadoPrato.Length == 0)
            {
                MessageBox.Show("Escolha o estado do Prato!", "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (ComboBox_Tipo.SelectedIndex < 0)
            {
                MessageBox.Show("Escolha um tipo da lista!", "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (ComboBox_Estado.SelectedIndex < 0)
            {
                MessageBox.Show("Escolha o estado do Prato!", "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btn_Adicionar_Click(object sender, EventArgs e)
        {
            string nomePrato = txt_NomePrato.Text;
            string tipoPrato = ComboBox_Tipo.Text;
            string estadoPrato = ComboBox_Estado.Text;
            if (!verificarInfoPratos())
            {
                return;
            }
            try
            {
                Prato prato = new Prato(nomePrato, tipoPrato, estadoPrato);
            }
            catch (Exception)
            {   // Se existir algum erro, aparece uma mensagem de erro
                MessageBox.Show("Erro ao criar o prato!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (lb_Pratos.SelectedIndex != -1) // se tiver um Prato selecionado, altera os dados
            {
                Prato pratoSelecionado = (Prato)lb_Pratos.SelectedItem;
                // altera dos dados do prato selecionado
                pratoSelecionado.nomePrato = txt_NomePrato.Text;
                pratoSelecionado.tipoPrato = ComboBox_Tipo.Text;
                pratoSelecionado.estadoPrato = ComboBox_Estado.Text;

                // Atualizar a exibição dos pratos na ListBox
                int editarPrato = lb_Pratos.SelectedIndex;
                lb_Pratos.Items[editarPrato] = pratoSelecionado;

                using (var db = new ApplicationContext())
                {   //faz update do Prato
                    db.Pratos.AddOrUpdate(pratoSelecionado);
                    db.SaveChanges();
                }
            }
            else // se não tiver, cria um novo
            {
                Prato novoprato = new Prato(txt_NomePrato.Text, ComboBox_Tipo.Text, ComboBox_Estado.Text);

                lb_Pratos.Items.Add(novoprato); // mostra na listbox antes de atualizar a db
                using (var db = new ApplicationContext())
                {   // cria novo prato
                    db.Pratos.Add(novoprato);
                    db.SaveChanges();
                }
            }
        }

        private void btn_Apagar_Click(object sender, EventArgs e)
        {
            int apagarPrato = lb_Pratos.SelectedIndex;
            if (apagarPrato == -1)
            {
                // se n tiver Prato selecionado mensagem de erro
                MessageBox.Show("Selecione um Prato!", "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (lb_Pratos.Items[apagarPrato] is Prato prato)
            {
                lb_Pratos.Items.Remove(prato);
                var db = new ApplicationContext();
                var apagarprato = db.Pratos.Find(prato.Id); // buscar o id do prato q queremos apagar
                if (apagarprato != null) // so faz isso se tiver um filme
                {
                    db.Pratos.Remove(apagarprato); // remove prato pelo id
                    db.SaveChanges(); // guarda as alterações na base de dados
                }
            }
        }

        private void lb_Pratos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int escolherPrato = lb_Pratos.SelectedIndex;
            if (escolherPrato != -1)
            {
                using (var db = new ApplicationContext())
                {
                    Prato pratoEscolhido = (Prato)lb_Pratos.SelectedItem;
                    txt_NomePrato.Text = pratoEscolhido.nomePrato;
                    ComboBox_Tipo.Text = pratoEscolhido.tipoPrato;
                    ComboBox_Estado.Text = pratoEscolhido.estadoPrato;
                }
            }
        }

        public void limparDadosInseridos()
        {
            txt_NomePrato.Clear();
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            lb_Pratos.ClearSelected();
            limparDadosInseridos();
        }
    }
}
