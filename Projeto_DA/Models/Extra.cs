using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_DA.Models
{
    [Serializable]
    public class Extra
    {
        [Key]
        public int Id { get; set; }
        public string DescricaoExtra { get; set; }
        public string PrecoExtra { get; set; }
        public string EstadoExtra { get; set; }
        public int QuantidadeExtra { get; set; }

        public Extra(string descricaoExtra, string precoExtra, string estadoExtra, int quantidadeExtra)
        {
            DescricaoExtra = descricaoExtra;
            PrecoExtra = precoExtra;
            EstadoExtra = estadoExtra;
            QuantidadeExtra = quantidadeExtra;
        }

        public Extra()
        {
            // Entity Framework
        }

        public override string ToString()
        {
            return "Extra: " + DescricaoExtra + "    Preço: " + PrecoExtra + "€     Estado: " + EstadoExtra + "     Quantidade: " + QuantidadeExtra;
        }
    }
}
