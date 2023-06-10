using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace ProductsCRUDUsingDapper.Models
{
    public static class DapperORM
    {
        private static string connectionString = @"Data Source=SUMIT\SQLExpress;Initial Catalog = DapperDB; Integrated Security=True;";
        
        public static void ExecuteWithoutReturn(string procedureName, DynamicParameters param )
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                sqlCon.Execute(procedureName, param, commandType: CommandType.StoredProcedure);
            }
        }

        public static T ExecuteReturnScalar<T>(string procedureName, DynamicParameters param )
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                return (T)Convert.ChangeType(sqlCon.Execute(procedureName, param, commandType: 
                    CommandType.StoredProcedure),typeof(T));
            }
        }

        public static IEnumerable<T> ReturnList<T>(string procedureName, DynamicParameters param )
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                return sqlCon.Query<T>(procedureName, param, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
