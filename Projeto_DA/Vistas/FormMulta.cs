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
    public partial class FormMulta : Form
    {

        public FormMulta()
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

        }

        private void icon_Sair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void limprarDadosInseridos()
        {
            txt_ValorMulta.Text = 0.ToString();
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            lb_Multa.ClearSelected();
            limprarDadosInseridos();
        }

        public bool validarDadosInseridos()
        {   // RECEBE VALORES DAS TEXTSBOX E VALIDA
            string valorMulta = txt_ValorMulta.Text;
            if (valorMulta == null)
            {
                MessageBox.Show("Tem que inserir um valor para a multa!", "Aviso!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void btn_Criar_Click_1(object sender, EventArgs e)
        {
            //Criar uma multa
            TimeSpan numHoras = TimeSpan.Parse(dtaPicker_Multa.Text);
            decimal Valor = decimal.Parse(txt_ValorMulta.Text);

            if (!validarDadosInseridos())
            {
                return;
            }
            try
            {
                // manda para o construtor faz a instancia
                Multa multa = new Multa(Valor, numHoras);
            }
            catch
            {
                // caso haja algum erro
                MessageBox.Show("Erro ao criar Multa", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (lb_Multa.SelectedIndex != -1) // se tiver uma multa selecionada, editar os dados
            {
                Multa multaSelecionada = (Multa)lb_Multa.SelectedItem;
                // altera dos dados da multa selecionada
                multaSelecionada.Valor = decimal.Parse(txt_ValorMulta.Text);
                multaSelecionada.NumHoras = TimeSpan.Parse(dtaPicker_Multa.Text);

                // Atualizar a exibição das Multas na ListBox
                int editarMulta = lb_Multa.SelectedIndex;
                lb_Multa.Items[editarMulta] = multaSelecionada;

                using (var db = new ApplicationContext())
                {   //faz update da Multa
                    db.Multas.AddOrUpdate(multaSelecionada);
                    db.SaveChanges();
                }
            }
            else // se não , cria um novo
            {
                Multa novaMulta = new Multa(decimal.Parse(txt_ValorMulta.Text), TimeSpan.Parse(dtaPicker_Multa.Text));

                lb_Multa.Items.Add(novaMulta); // mostra na listbox antes de atualizar a db
                using (var db = new ApplicationContext())
                {   // cria nova multa
                    db.Multas.Add(novaMulta);
                    db.SaveChanges();
                }
            }
        }

        private void lb_Multa_SelectedIndexChanged(object sender, EventArgs e)
        {
            int escmulta = lb_Multa.SelectedIndex;
            if (escmulta != -1)
            {
                using (var db = new ApplicationContext())
                {
                    Multa multaselecionada = (Multa)lb_Multa.SelectedItem; // descobrir o que será indicado nas textbox ao selecionar na listBox
                                                                           // mostra dados da multa                                                                                   
                    txt_ValorMulta.Text = multaselecionada.Valor.ToString();
                    dtaPicker_Multa.Value.Add(multaselecionada.NumHoras);


                }
            }
        }

        
    }
}
