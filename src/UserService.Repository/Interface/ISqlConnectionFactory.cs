
using System.Data;


namespace UserService.Repository.Interface
{
    public interface ISqlConnectionFactory
    {
        IDbConnection GetConnection { get; }
        void OpenConnection(IDbConnection connection);
        IDbTransaction BeginTransaction(IDbConnection connection);
        void ChangeDatabase(IDbConnection connection, string databasename);
    }
}
