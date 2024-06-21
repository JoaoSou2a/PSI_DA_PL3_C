using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_DA.Models
{
    [Serializable]
    public class ItemFatura
    {
        [Key]
        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public Fatura Fatura { get; set; }

        public ItemFatura(int id, string descricao, decimal preco, Fatura fatura)
        {
            Id = id;
            Descricao = descricao;
            Preco = preco;
            Fatura = fatura;
        }

        public ItemFatura()
        {
            // Entity Framework
        }


    }
}
