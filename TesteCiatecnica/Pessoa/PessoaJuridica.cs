using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteCiatecnica.Pessoa
{
    public class PessoaJuridica:Pessoa
    {
       
        public string CNPJ { get; set; }
        public string Razao_social { get; set; }
        public string Nome_fantasia { get; set; }
    }
}
