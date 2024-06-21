using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_DA.Models
{
    [Serializable]
    public class Funcionario : Utilizador
    {
        [Key]
        public string UsernameFuncionario { get; set; }

        public Funcionario()
        {
            // Entity Framework
        }

        public Funcionario(string nomeutilizador, int nifutilizador, string usernameFuncionario) : base(nomeutilizador, nifutilizador)
        {
            UsernameFuncionario = usernameFuncionario;
        }

        public override string ToString()
        {
            return "Username: " + UsernameFuncionario + "       NIF: " + UtilizadorNIF + "      Nome: " + UtilizadorNome;
        }


    }
}
