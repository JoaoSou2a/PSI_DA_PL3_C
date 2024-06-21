using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Projeto_DA.Models
{
    [Serializable]
    public class Menu
    {
        [Key]
        public int Id { get; set; }
        public string DescricaoMenu { get; set; }
        public int IdPrato { get; set; }
        public int IdExtra { get; set; }
        public decimal PrecoEstudante { get; set; }
        public decimal PrecoProfessor { get; set; }
        public int Quantidade { get; set; }

        public Menu(string descricaoMenu, int idPrato, int idExtra, decimal precoEstudante, decimal precoProfessor, int quantidade)
        {
            DescricaoMenu = descricaoMenu;
            IdPrato = idPrato;
            IdExtra = idExtra;
            PrecoEstudante = precoEstudante;
            PrecoProfessor = precoProfessor;
            Quantidade = quantidade;
        }

        public Menu()
        {
            // Entity Framework
        }

        public override string ToString()
        {
            var db = new ApplicationContext();
            var nomePrato = db.Pratos.Find(IdPrato);
            var nomeExtra = db.Extras.Find(IdExtra);
            return "Menu: " + DescricaoMenu + "    Prato: " + nomePrato.nomePrato + "     Extra: " + nomeExtra.DescricaoExtra + "     Preço Estudante: " + PrecoEstudante + "€    Preço Professor: " + PrecoProfessor + "€       Quantidade: " + Quantidade;
        }
    }
}
