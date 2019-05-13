using Devart.Data.Oracle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteCiatecnica.Result;

namespace TesteCiatecnica.DBConnection
{
    public class DataBaseConnection
    {

        public static string ConnectionString()
        {

            string protocolBD = Properties.Settings.Default.DB_Protocol.Trim();
            string portBD = Properties.Settings.Default.DB_Port.Trim();
            string serviceNameBD = Properties.Settings.Default.DB_ServiceName.Trim();

            string userIdBD = Properties.Settings.Default.DB_UserID.Trim();
            string passwordBD = Properties.Settings.Default.DB_Password.Trim();
            string hostBD = Properties.Settings.Default.DB_Host.Trim();

            string connectionString = "SERVER=(DESCRIPTION=(ADDRESS=(PROTOCOL=" + protocolBD + ")(HOST=" + hostBD + ")(PORT=" + portBD + "))(CONNECT_DATA=(SERVICE_NAME=" + serviceNameBD + ")));uid=" + userIdBD + ";pwd=" + passwordBD;

            return connectionString;
        }
        public static ExecutionResult ConnectValidation()
        {
            ExecutionResult exeRes = new ExecutionResult();
            try
            {
                OracleConnection connection = new OracleConnection(ConnectionString());
                connection.Open();
                exeRes.Message = "Banco de dados conectado com sucesso!";
                exeRes.Status = true;
                connection.Close();
                return exeRes;

            }
            catch(Exception ex)
            {
                exeRes.Message = $"Falha ao conectar no banco de dados\n\r Erro: {ex.Message}";
                exeRes.Status = false;
                return exeRes;
            }

       
        }
}
}
