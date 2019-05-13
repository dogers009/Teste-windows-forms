using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteCiatecnica.Pessoa
{
    public class PessoaFisica:Pessoa
    {
        

        public string CPF { get; set; }
        public DateTime Data_de_nascimento { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
      
    }
}
