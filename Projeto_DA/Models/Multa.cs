using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_DA.Models
{
    [Serializable]
    public class Multa
    {
        [Key]
        public int ID { get; set; }
        public decimal Valor { get; set; }
        public TimeSpan NumHoras { get; set; }

        public Multa()
        {
            // Entity Framework
        }

        public Multa(decimal valor, TimeSpan numHoras)
        {
            Valor = valor;
            NumHoras = numHoras;
        }

        // OVERRIDE PARA DIZER O QUE VAI ESCREVER NA LISTBOX
        public override string ToString()
        {
            return "Valor da Multa: " + Valor + " € " + "       Hora da Multa: " + NumHoras;
        }
    }
}
