using Devart.Data.Oracle;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TesteCiatecnica.DBConnection;
using TesteCiatecnica.Pessoa;
using TesteCiatecnica.Result;

namespace TesteCiatecnica.DAO
{
    public class DAOPessoa
    {
        private string connectionString = string.Empty;
        public DAOPessoa()
        {
            connectionString = DataBaseConnection.ConnectionString();
        }
        public ExecutionResult InsertPessoaFisica(PessoaFisica pessoaFisica)
        {
            ExecutionResult exeRes = new ExecutionResult();
            int ret = 0;

            try
            {
                string sql1 = $@"INSERT INTO PESSOA(CEP, LOGRADOURO, NUMERO, COMPLEMENTO, BAIRRO, CIDADE, UF)
                                                                    VALUES('{pessoaFisica.CEP}',
                                                                            '{pessoaFisica.Logradouro}',
                                                                            '{pessoaFisica.Numero}',
                                                                            '{pessoaFisica.Complemento}',
                                                                            '{pessoaFisica.Bairro}',
                                                                              '{pessoaFisica.Cidade}',
                                                                            '{pessoaFisica.UF}')";
                string sql2 = "select max(id) as id_pessoa from pessoa";
                ret = ExecuteInsertOrUpdate(sql1);
            
                if (ret == 1)
                {
                    DataTable dt = ExecuteQuery(sql2);
                    int id_pessoa = Convert.ToInt32(dt.Rows[0][0].ToString());

                    string sql3 = $@"INSERT INTO PESSOA_FISICA(CPF,DATA_DE_NASCIMENTO,NOME,SOBRENOME,ID_PESSOA)
                                                           VALUES( '{pessoaFisica.CPF}',
                                                                   TO_DATE('{pessoaFisica.Data_de_nascimento}', 'dd/mm/yyyy hh24:mi:ss'),
                                                                   '{pessoaFisica.Nome}',
                                                                   '{pessoaFisica.Sobrenome}',
                                                                   { id_pessoa })";
                    ret = ExecuteInsertOrUpdate(sql3);
                    if(ret==1)
                    {
                        exeRes.Status = true;
                    }
                }
            }
            catch (Exception ex)
            {
                exeRes.Status = false;
                exeRes.Message = ex.Message;

            }

            return exeRes;
        }
        public ExecutionResult InsertPessoaJuridica(PessoaJuridica pessoaJuridica)
        {
            ExecutionResult exeRes = new ExecutionResult();
            int ret = 0;

            try
            {
                string sql1 = $@"INSERT INTO PESSOA(CEP, LOGRADOURO, NUMERO, COMPLEMENTO, BAIRRO, CIDADE, UF)
                                                                    VALUES('{pessoaJuridica.CEP}',
                                                                            '{pessoaJuridica.Logradouro}',
                                                                            '{pessoaJuridica.Numero}',
                                                                            '{pessoaJuridica.Complemento}',
                                                                            '{pessoaJuridica.Bairro}',
                                                                            '{pessoaJuridica.Cidade}',
                                                                            '{pessoaJuridica.UF}')";

                string sql2 = "select max(id) as id_pessoa from pessoa";

      
                ret = ExecuteInsertOrUpdate(sql1);
                if (ret == 1)
                {
                    DataTable dt = ExecuteQuery(sql2);
                    int id_pessoa = Convert.ToInt32(dt.Rows[0][0].ToString());



                    string sql3 = $@"INSERT INTO PESSOA_JURIDICA(CNPJ,RAZAO_SOCIAL,NOME_FANTASIA,ID_PESSOA)
                                                           VALUES('{pessoaJuridica.CNPJ}',
                                                                   '{pessoaJuridica.Razao_social}',
                                                                   '{pessoaJuridica.Nome_fantasia}',
                                                                   { id_pessoa})";

                    ret = ExecuteInsertOrUpdate(sql3);
                    if (ret == 1)
                    {
                        exeRes.Status = true;
                    }
                }
            }
            catch (Exception ex)
            {
                exeRes.Status = false;
                exeRes.Message = ex.Message;

            }

            return exeRes;
        }

        
        public ExecutionResult DeletePessoaJuridica(int idPessoa)
        {
            ExecutionResult execRes = new ExecutionResult();
            try
            {
                string sql1=$@"DELETE PESSOA_JURIDICA WHERE ID_PESSOA={idPessoa}";
                string sql2 = $@"DELETE PESSOA WHERE ID={idPessoa}";
                ExecuteDelete(sql1);
                ExecuteDelete(sql2);
                execRes.Status = true;
                return execRes;
            }
            catch(Exception ex)
            {
                execRes.Status = false;
                execRes.Message = ex.Message;
                return execRes;
            }
        }
        public ExecutionResult DeletePessoaFisica(int idPessoa)
        {
            ExecutionResult execRes = new ExecutionResult();
            try
            {
                string sql1 = $@"DELETE PESSOA_FISICA WHERE ID_PESSOA={idPessoa}";
                string sql2 = $@"DELETE PESSOA WHERE ID={idPessoa}";
                ExecuteDelete(sql1);
                ExecuteDelete(sql2);
                execRes.Status = true;
                return execRes;
            }
            catch(Exception ex)
            {
                execRes.Status = false;
                execRes.Message = ex.Message;
                return execRes;
            }
        }
        public ExecutionResult UpdatePessoaFisica(PessoaFisica pessoaFisica,int idPessoa)
        {
            ExecutionResult exeRes = new ExecutionResult();
            int ret = 0;

            try
            {
                string sql1 = $@"UPDATE PESSOA
                                 SET CEP = '{pessoaFisica.CEP}',
                                     LOGRADOURO = '{pessoaFisica.Logradouro}',
                                     NUMERO =  '{pessoaFisica.Numero}',
                                     COMPLEMENTO = '{pessoaFisica.Complemento}',
                                     BAIRRO = '{pessoaFisica.Bairro}',
                                     CIDADE =    '{pessoaFisica.Cidade}',
                                     UF =   '{pessoaFisica.UF}'
                                  WHERE ID = {idPessoa}"; 

                string sql2 = $@"UPDATE PESSOA_FISICA
                                       SET CPF = '{pessoaFisica.CPF}',
                                           DATA_DE_NASCIMENTO = TO_DATE('{pessoaFisica.Data_de_nascimento}', 'dd/mm/yyyy hh24:mi:ss'),
                                           NOME =  '{pessoaFisica.Nome}',
                                           SOBRENOME = '{pessoaFisica.Sobrenome}'
                                       WHERE ID_PESSOA = {idPessoa}";

                ret = ExecuteInsertOrUpdate(sql1);
                ret = ExecuteInsertOrUpdate(sql2);
                if (ret == 1)
                {
                    exeRes.Status = true;                   
                }
            }
            catch (Exception ex)
            {
                exeRes.Status = false;
                exeRes.Message = ex.Message;

            }

            return exeRes;
        }
        public ExecutionResult UpdatePessoaJuridica(PessoaJuridica pessoaJuridica,int idPessoa)
        {
            ExecutionResult exeRes = new ExecutionResult();
            int ret = 0;

            try
            {
                string sql1 = $@"UPDATE PESSOA
                                 SET CEP = '{pessoaJuridica.CEP}',
                                     LOGRADOURO = '{pessoaJuridica.Logradouro}',
                                     NUMERO =  '{pessoaJuridica.Numero}',
                                     COMPLEMENTO = '{pessoaJuridica.Complemento}',
                                     BAIRRO = '{pessoaJuridica.Bairro}',
                                     CIDADE =    '{pessoaJuridica.Cidade}',
                                     UF =   '{pessoaJuridica.UF}'
                                 WHERE ID = {idPessoa}";


                string sql2 = $@"UPDATE PESSOA_JURIDICA
                                        SET CNPJ = '{pessoaJuridica.CNPJ}',
                                            RAZAO_SOCIAL =    '{pessoaJuridica.Razao_social}',
                                            NOME_FANTASIA =   '{pessoaJuridica.Nome_fantasia}'
                                        WHERE ID_PESSOA = { idPessoa})";

                ret = ExecuteInsertOrUpdate(sql1);
                ret = ExecuteInsertOrUpdate(sql2);
                if (ret == 1)
                {
                  exeRes.Status = true;   
                }
            }
            catch (Exception ex)
            {
                exeRes.Status = false;
                exeRes.Message = ex.Message;

            }

            return exeRes;
        }
        public ExecutionResult GetAllPessoaFisica()
        {
            ExecutionResult execRes = new ExecutionResult();
            try
            {
                string sql = @"select  
                           p.Id,
                           pf.cpf,
                           pf.data_de_nascimento,
                           pf.nome,
                           pf.sobrenome,
                           p.cep,
                           p.logradouro,
                           p.numero,
                           p.complemento,
                           p.bairro,
                           p.cidade,
                           p.uf
                           from pessoa_fisica pf, pessoa p where p.id=pf.id_pessoa";
                DataTable dt = ExecuteQuery(sql);
                execRes.Anything = dt;
                execRes.Status = true;
            }
            catch (Exception ex)
            {
                execRes.Status = false;
                execRes.Message = ex.Message;
            }
            return execRes;
        }
        public ExecutionResult GetAllPessoaJuridica()
        {
            ExecutionResult execRes = new ExecutionResult();
            try
            {
                string sql = @"select
                           p.Id,
                           pj.cnpj,
                           pj.razao_social,
                           pj.nome_fantasia,
                           p.cep,
                           p.logradouro,
                           p.numero,
                           p.complemento,
                           p.bairro,
                           p.cidade,
                           p.uf
                           from pessoa_juridica pj, pessoa p where p.id=pj.id_pessoa";
                DataTable dt = ExecuteQuery(sql);
                execRes.Anything = dt;
                execRes.Status = true;
            }
            catch (Exception ex)
            {
                execRes.Status = false;
                execRes.Message = ex.Message;
            }

            return execRes;
        }
        public ExecutionResult FilterPessoaFisica(string cpf)
        {
            ExecutionResult execRes = new ExecutionResult();
            try
            {
                string sql = $@"select  
                           p.Id,
                           pf.cpf,
                           pf.data_de_nascimento,
                           pf.nome,
                           pf.sobrenome,
                           p.cep,
                           p.logradouro,
                           p.numero,
                           p.complemento,
                           p.bairro,
                           p.cidade,
                           p.uf
                           from pessoa_fisica pf, pessoa p where p.id=pf.id_pessoa 
                           and cpf like '{cpf}%'";
                DataTable dt = ExecuteQuery(sql);
                execRes.Anything = dt;
                execRes.Status = true;
            }
            catch (Exception ex)
            {
                execRes.Status = false;
                execRes.Message = ex.Message;
            }
            return execRes;
        }
        public ExecutionResult FilterPessoaJuridica(string cnpj)
        {
            ExecutionResult execRes = new ExecutionResult();
            try
            {
                string sql = $@"select
                           p.Id,
                           pj.cnpj,
                           pj.razao_social,
                           pj.nome_fantasia,
                           p.cep,
                           p.logradouro,
                           p.numero,
                           p.complemento,
                           p.bairro,
                           p.cidade,
                           p.uf
                           from pessoa_juridica pj, pessoa p where p.id=pj.id_pessoa 
                           and pj.cnpj like '{cnpj}%'";
                DataTable dt = ExecuteQuery(sql);
                execRes.Anything = dt;
                execRes.Status = true;
            }
            catch (Exception ex)
            {
                execRes.Status = false;
                execRes.Message = ex.Message;
            }

            return execRes;
        }
        private int ExecuteInsertOrUpdate(string sql)
        {
            OracleConnection connection = new OracleConnection(connectionString);
            OracleCommand command = new OracleCommand(sql);

            command.CommandType = CommandType.Text;
            command.Connection = connection;
            //Abre a conexão e executa o comando
            connection.ConnectionTimeout.Equals(15);
            connection.Open();
            connection.Clone();
            return command.ExecuteNonQuery();
        }
        private int ExecuteDelete(string sql)
        {
            OracleConnection connection = new OracleConnection(connectionString);
            OracleCommand command = new OracleCommand(sql);

            command.CommandType = CommandType.Text;
            command.Connection = connection;
            //Abre a conexão e executa o comando
            connection.ConnectionTimeout.Equals(15);
            connection.Open();
            connection.Clone();
            return command.ExecuteNonQuery();
        }
        private DataTable ExecuteQuery(string sql)
        {
            DataTable dt = new DataTable();
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                OracleCommand command = new OracleCommand(sql);

                command.CommandType = CommandType.Text;
                command.Connection = connection;
                connection.ConnectionTimeout.Equals(15);

                connection.Open();
                OracleDataReader reader = command.ExecuteReader();
                {
                    dt.Load(reader);
                }
             return dt;
        }

       }
    }
}
