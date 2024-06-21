using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_DA.Models
{
    [Serializable]
    public class Prato
    {
        [Key]
        public int Id { get; set; }
        public string nomePrato { get; set; }
        public string tipoPrato { get; set; }
        public string estadoPrato { get; set; }

        public Prato()
        {
            // Entity Framework
        }

        public Prato(string nomePrato, string tipoPrato, string estadoPrato)
        {
            this.nomePrato = nomePrato;
            this.tipoPrato = tipoPrato;
            this.estadoPrato = estadoPrato;
        }

        // Diz o que vai escrever na listbox
        public override string ToString()
        {
            return "Prato: " + nomePrato + "       Tipo: " + tipoPrato + "     Estado: " + estadoPrato;
        }
    }
}
