using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_DA.Models
{
    [Serializable]
    public class Utilizador
    {
        [Key]
        public int Id { get; set; }
        public string UtilizadorNome { get; set; }
        public int UtilizadorNIF { get; set; }

        public Utilizador(string utilizadorNome, int utilizadorNIF)
        {
            UtilizadorNome = utilizadorNome;
            UtilizadorNIF = utilizadorNIF;
        }

        public Utilizador()
        {
            // Entity Framework
        }

    }
}
