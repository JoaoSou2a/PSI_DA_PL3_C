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
    public partial class FormExtras : Form
    {
        

        public FormExtras()
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

        private void lab_Menu_Click(object sender, EventArgs e)
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
        public void limparDadosInseridos()
        {
            lb_Extras.ClearSelected();
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            lb_Extras.ClearSelected();
            limparDadosInseridos();
        }

        //---------------------------------------------------------verificarInfoExtras------------------------------------------------------------//
        public bool verificarInfoExtras()
        {
            string descricaoExtra = txt_Extra.Text;
            if (descricaoExtra.Length == 0)
            {
                MessageBox.Show("Insira o nome do Extra!", "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            decimal precoExtra = decimal.Parse(txt_PrecoExtra.Text);
            if (precoExtra == 0)
            {
                MessageBox.Show("Insira o preço do Extra!", "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            int quantidadeExtra = int.Parse(txt_Quantidade.Text);
            if (quantidadeExtra == 0)
            {
                MessageBox.Show("Insira a quantidade do Extra!", "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            string EstadoExtra = ComboBox_EstadoExtras.Text;
            if (EstadoExtra.Length == 0)
            {
                MessageBox.Show("Escolha o estado do Extra!", "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (ComboBox_EstadoExtras.SelectedIndex < 0)
            {
                MessageBox.Show("Escolha um estado da lista!", "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        //----------------------------------------------------------btn_Adicionar_Click-----------------------------------------------------------//

        private void btn_Adicionar_Click(object sender, EventArgs e)
        {
            string nomeExtra = txt_Extra.Text;
            int quantidadeExtra = int.Parse(txt_Quantidade.Text);
            string precoExtra = txt_PrecoExtra.Text;
            string estadoExtra = ComboBox_EstadoExtras.SelectedItem?.ToString();

            bool quantidadeValida = int.TryParse(txt_Quantidade.Text, out int quantidade);

            if (nomeExtra == "" || precoExtra == "" || !quantidadeValida || estadoExtra == null)
            {
                MessageBox.Show("Preencha todos os campos!");
                return;
            }

            if (!verificarInfoExtras())
            {
                return;
            }

            try
            {
                if (lb_Extras.SelectedIndex != -1)
                {
                    Extra extraSelecionado = (Extra)lb_Extras.SelectedItem;
                    //Altera o Extra Selecionado
                    extraSelecionado.DescricaoExtra = nomeExtra;
                    extraSelecionado.PrecoExtra = precoExtra;
                    extraSelecionado.QuantidadeExtra = quantidadeExtra;
                    extraSelecionado.EstadoExtra = estadoExtra;


                    int editarExtra = lb_Extras.SelectedIndex;
                    lb_Extras.Items[editarExtra] = extraSelecionado;

                    using (var db = new ApplicationContext())
                    {
                        db.Extras.AddOrUpdate(extraSelecionado);
                        db.SaveChanges();
                    }
                }
                else
                {
                    Extra extra;
                    extra = new Extra(nomeExtra, precoExtra, estadoExtra, quantidadeExtra);

                    lb_Extras.Items.Add(extra);

                    using (var db = new ApplicationContext())
                    {
                        db.Extras.Add(extra);
                        db.SaveChanges();
                    }
                }

            }
            catch (Exception ex)
            {
                //Se houver algum erro, mostra uma mensagem de erro
                MessageBox.Show($"Erro ao guardar o cliente: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            txt_Extra.Text = "";
            txt_Quantidade.Text = "";
            txt_PrecoExtra.Text = "";
            ComboBox_EstadoExtras.SelectedIndex = -1;
        }

        //----------------------------------------------------------btn_Apagar_Click--------------------------------------------------------------//

        private void btn_Apagar_Click(object sender, EventArgs e)
        {
            int apagarExtra = lb_Extras.SelectedIndex;
            if (apagarExtra == -1)
            {
                // Se não tiver um extra selecionado aparece uma mensagem de Aviso
                MessageBox.Show("Selecione um Extra para eliminar!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (lb_Extras.Items[apagarExtra] is Extra extra)
            {
                // Caso tenha um extra selecionado apaga da ListBox
                lb_Extras.Items.RemoveAt(apagarExtra);
                // Elimmina da base de dados
                var db = new ApplicationContext();
                var apagarextra = db.Utilizadores.Find(extra.Id);
                if (apagarextra != null)
                {
                    db.Utilizadores.Remove(apagarextra);
                    db.SaveChanges();
                }
            }
        }

        //------------------------------------------------lb_Extras_SelectedIndexChanged----------------------------------------------------------------//

        private void lb_Extras_SelectedIndexChanged(object sender, EventArgs e)
        {
            int escolherExtra = lb_Extras.SelectedIndex;
            if (escolherExtra != -1)
            {
                Extra extraSelecionado = (Extra)lb_Extras.SelectedItem;
                txt_Extra.Text = extraSelecionado.DescricaoExtra;
                txt_PrecoExtra.Text = extraSelecionado.PrecoExtra.ToString();
                txt_Quantidade.Text = extraSelecionado.QuantidadeExtra.ToString();
                ComboBox_EstadoExtras.Text = extraSelecionado.EstadoExtra;
            }
        }

        //----------------------------------------------------AtualizarListBoxExtras-----------------------------------------------------------------//

        public void AtualizarListBoxExtras()
        {
            lb_Extras.Items.Clear();
            using (var db = new ApplicationContext())
            {
                foreach (var extra in db.Extras) //correr os extras para os adicionar à listBox
                {
                    lb_Extras.Items.Add(extra);
                }
            }
        }
    }
}
