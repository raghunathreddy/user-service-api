using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Repository.Interface;
using Microsoft.Extensions.Configuration;


namespace UserService.Repository.Implementation
{
    public class SqlConnectionFactory : ISqlConnectionFactory
    {
        protected readonly IConfiguration _configuration;
        public SqlConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public virtual IDbConnection GetConnection => new SqlConnection(_configuration.GetConnectionString("BookliberaryDB"));
        public IDbTransaction BeginTransaction(IDbConnection connection)
        {
            return connection.BeginTransaction();
        }

        public void ChangeDatabase(IDbConnection connection, string databasename)
        {
            connection.ChangeDatabase(databasename);
        }

        public void OpenConnection(IDbConnection connection)
        {
            connection.Open();
        }
    }
}
