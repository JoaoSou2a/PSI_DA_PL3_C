using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_DA.Models
{
    [Serializable]
    public class Professor : Cliente
    {
        [Key]

        public string Email { get; set; }

        public Professor(string nomeUtilizador, int nifUtilizador, decimal saldo, string email) : base(nomeUtilizador, nifUtilizador, saldo)
        {
            Email = email;
        }

        public Professor()
        {
            // Construtor vazio necessário para o Entity Framework
        }

        public override string ToString()
        {
            return "Nome: " + UtilizadorNome + "       NIF: " + UtilizadorNIF + "      Saldo: " + Saldo + "€      Email: " + Email;
        }

    }
}
