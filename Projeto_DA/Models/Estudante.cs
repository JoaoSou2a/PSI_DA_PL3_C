using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_DA.Models
{
    [Serializable]
    public class Estudante : Cliente
    {
        [Key]

        public int NumEstudante { get; set; }

        public Estudante(string nomeutilizador, int nifutilizador, decimal saldo, int numEstudante) : base(nomeutilizador, nifutilizador, saldo)
        {
            NumEstudante = numEstudante;
        }

        public Estudante()
        {
            // Entity Framework
        }

        public override string ToString()
        {
            return "Nome: " + UtilizadorNome + "       NIF: " + UtilizadorNIF + "      Saldo: " + Saldo + "€      Número de Estudante: " + NumEstudante;
        }

    }
}
