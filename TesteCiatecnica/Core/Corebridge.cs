using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteCiatecnica.DAO;
using TesteCiatecnica.Pessoa;
using TesteCiatecnica.Result;

namespace TesteCiatecnica.Core
{
    
    public class Corebridge
    {
        DAOPessoa daoPessoa;
        ExecutionResult execRes;
        public Corebridge()
        {
            daoPessoa = new DAOPessoa();
            execRes = new ExecutionResult();
        }

        public ExecutionResult InsertPessoaJuridica(PessoaJuridica pessoaJuridica)
        {
            
            execRes = CheckNullValuesPessoaJuridica(pessoaJuridica);
            if(execRes.Status)
            {
                execRes = daoPessoa.InsertPessoaJuridica(pessoaJuridica);
                if(execRes.Status)
                {
                    execRes = daoPessoa.GetAllPessoaJuridica();
                    if (execRes.Status)
                    {
                        execRes.Message = "Cliente inserido com sucesso!";
                    }
                }
            }
            return execRes;
        }
        public ExecutionResult InsertPessoaFisica(PessoaFisica pessoaFisica)
        {
            execRes = CheckIdadePessoaFisica(pessoaFisica);
            if(!execRes.Status)
            {
                return execRes;
            }
            execRes = CheckNullValuesPessoaFisica(pessoaFisica);
            if (execRes.Status)
            {
                execRes = daoPessoa.InsertPessoaFisica(pessoaFisica);
                if (execRes.Status)
                {
                    execRes = daoPessoa.GetAllPessoaFisica();
                    if (execRes.Status)
                    {
                        execRes.Message = "Cliente inserido com sucesso!";
                    }
                }
            }
            return execRes;
        }        
        public ExecutionResult FilterPessoaFisica(string cpf)
        {
            execRes = daoPessoa.FilterPessoaFisica(cpf);
            return execRes;
        }
        public ExecutionResult FilterPessoaJuridica(string cnpj)
        {
            execRes = daoPessoa.FilterPessoaJuridica(cnpj);
            return execRes;
        }
        public ExecutionResult DeletePessoaFisica(int idPessoa)
        {
            execRes = daoPessoa.DeletePessoaFisica(idPessoa);
            if (execRes.Status)
            {
                execRes = daoPessoa.GetAllPessoaFisica();
                if (execRes.Status)
                {
                    execRes.Message = "Cliente deletado com sucesso!";
                }
            }
            return execRes;
        }
        public ExecutionResult DeletePessoaJuridicaData(int idPessoa)
        {

            execRes = daoPessoa.DeletePessoaJuridica(idPessoa);
            if (execRes.Status)
            {
                execRes = daoPessoa.GetAllPessoaFisica();
                if (execRes.Status)
                {
                    execRes.Message = "Cliente deletado com sucesso!";
                }
            }
            return execRes;
        }
        public ExecutionResult UpdatePessoaFisica(PessoaFisica pessoaFisica,int idPessoa)
        {
            execRes = daoPessoa.UpdatePessoaFisica(pessoaFisica, idPessoa);
            if (execRes.Status)
            {
                execRes = daoPessoa.GetAllPessoaFisica();
                if (execRes.Status)
                {
                    execRes.Message = "Cliente atualizado com sucesso!";
                }
            }
            return execRes;
        }
        public ExecutionResult UpdatePessoaJuridicaData(PessoaJuridica pessoaJuridica,int idPessoa)
        {

            execRes = daoPessoa.UpdatePessoaJuridica(pessoaJuridica,idPessoa);
            if (execRes.Status)
            {
                execRes = daoPessoa.GetAllPessoaFisica();
                if (execRes.Status)
                {
                    execRes.Message = "Cliente atualizado com sucesso!";
                }
            }
            return execRes;
        }
        private ExecutionResult CheckNullValuesPessoaFisica(PessoaFisica pessoaFisica)
        {
            if (String.IsNullOrEmpty(pessoaFisica.Complemento))
                pessoaFisica.Complemento = "N/A";
            execRes.Status = pessoaFisica.GetType().GetProperties()
                            .All(p => p.GetValue(pessoaFisica) != null);
            if(!execRes.Status)
            {
                execRes.Message = "Obrigatório preencher todos os campos!";
            }

            return execRes;
        }
        private ExecutionResult CheckNullValuesPessoaJuridica(PessoaJuridica pessoaJuridica)
        {
            if (String.IsNullOrEmpty(pessoaJuridica.Complemento))
                pessoaJuridica.Complemento = "N/A";
            execRes.Status = pessoaJuridica.GetType().GetProperties()
                           .All(p => p.GetValue(pessoaJuridica) != null);
            if (!execRes.Status)
            {
                execRes.Message = "Obrigatório preencher todos os campos!";
            }
            return execRes;
        }
        public ExecutionResult CheckIdadePessoaFisica(PessoaFisica pessoaFisica)
        {
            var birthdate = new DateTime(pessoaFisica.Data_de_nascimento.Year,
                pessoaFisica.Data_de_nascimento.Month, 
                pessoaFisica.Data_de_nascimento.Day);

            var today = new DateTime(DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.Day);
            var age = today.Year - birthdate.Year;
            if (birthdate > today.AddYears(-age)) age--;

            if(age<19)
            {
                execRes.Status = false;
                execRes.Message = "O cliente deve ter mais que 19 anos para ser cadastrado!";
            }
            else
            {
                execRes.Status = true;
            }

            return execRes;
        }

        

        
    }
}
