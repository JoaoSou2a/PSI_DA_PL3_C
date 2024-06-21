using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_DA.Models
{
    [Serializable]
    public class Cliente : Utilizador
    {
        [Key]
        public decimal Saldo { get; set; }

        public Cliente()
        {
            // Entity Framework
        }

        public Cliente(string nomeutilizador, int nifutilizador, decimal saldo) : base(nomeutilizador, nifutilizador)
        {
            Saldo = saldo;
        }

        public override string ToString()
        {
            return "Nome: " + UtilizadorNome + "       NIF: " + UtilizadorNIF + "      Saldo: " + Saldo + "€";
        }
    }
}
