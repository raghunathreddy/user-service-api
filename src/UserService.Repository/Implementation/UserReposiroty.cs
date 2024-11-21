using UserService.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using UserService.Model;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Transactions;


namespace UserService.Repository.Implementation
{
    public class UserReposiroty : IUserRepository
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        public UserReposiroty(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public User GetAllUser(string email, string pwd)
        {
            using (IDbConnection connection = _sqlConnectionFactory.GetConnection)
            {
                string selectuser = @"SELECT * FROM Users WHERE email = @email and pwd = @pwd ";
                _sqlConnectionFactory.OpenConnection(connection);
                using (var transaction = _sqlConnectionFactory.BeginTransaction(connection))
                {
                    try
                    {
                        return connection.QueryAsync<User>(selectuser, new { email = email, pwd = pwd }, transaction: transaction).Result.FirstOrDefault();
                    }
                    catch (Exception ex)
                    {

                        throw new InvalidOperationException(ex.Message);
                    }
                }
            }
        }

        public User GetUser(int user_id)
        {
            using (IDbConnection connection = _sqlConnectionFactory.GetConnection)
            {
                string selectuser = @"SELECT * FROM Users WHERE user_id = @user_id ";
                _sqlConnectionFactory.OpenConnection(connection);
                using (var transaction = _sqlConnectionFactory.BeginTransaction(connection))
                {
                    try
                    {
                        return connection.QueryAsync<User>(selectuser, new { user_id = user_id }, transaction: transaction).Result.FirstOrDefault();
                    }
                    catch (Exception ex)
                    {

                        throw new InvalidOperationException(ex.Message);
                    }
                }
            }
        }

        public async Task<List<User>> GetAllUsers()
        {
            using (IDbConnection connection = _sqlConnectionFactory.GetConnection)
            {
                _sqlConnectionFactory.OpenConnection(connection);
                using (var transaction = _sqlConnectionFactory.BeginTransaction(connection))
                {
                    try
                    {
                        var results = await connection.QueryAsync<User>(SqlQueries.UserQueries.GetAllUsers, transaction: transaction);
                        return results?.AsList();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new InvalidOperationException(ex.Message);
                    }
                }
            }
        }

        //public async Task<List<User>> GetUsersByEmail(string email)
        //{
        //    using (IDbConnection connection = _sqlConnectionFactory.GetConnection)
        //    {
        //        _sqlConnectionFactory.OpenConnection(connection);
        //        using (var transaction = _sqlConnectionFactory.BeginTransaction(connection))
        //        {
        //            try
        //            {
        //                var results = await connection.QueryAsync<User>(SqlQueries.UserQueries.GetByEmail, new { email = email }, transaction: transaction);
        //                return results?.AsList();
        //            }
        //            catch (Exception ex)
        //            {
        //                transaction.Rollback();
        //                throw new InvalidOperationException(ex.Message);
        //            }
        //        }
        //    }
        //}

        public async void AddUserdetails(User usersdetails)
        {
            using (IDbConnection connection = _sqlConnectionFactory.GetConnection)
            {
                string insertQuery = @"INSERT INTO users ([email],[pwd],[username],[user_address],[favorite_genres],[reading_preferences]) VALUES (@email, @pwd, @username, @user_address, @favorite_genres, @reading_preferences)";
                _sqlConnectionFactory.OpenConnection(connection);
                try
                {
                    var results = await connection.ExecuteAsync(insertQuery, new
                    {
                        email = usersdetails.email,
                        pwd = usersdetails.pwd,
                        username = usersdetails.username,
                        user_address = usersdetails.user_address,
                        favorite_genres = usersdetails.favorite_genres,
                        reading_preferences = usersdetails.reading_preferences
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString() + "\n" + ex.InnerException.ToString());
                    throw new InvalidOperationException(ex.Message);
                }
            }
        }

        public async void Updatepwd(User userdata)
        {
            using (IDbConnection connection = _sqlConnectionFactory.GetConnection)
            {
                string insertQuery = @"UPDATE users SET [pwd] = @pwd where user_id=@userdata.user_id";
                _sqlConnectionFactory.OpenConnection(connection);
                try
                {
                    var results = await connection.ExecuteAsync(insertQuery, new
                    {
                        pwd = userdata.pwd,
                       
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString() + "\n" + ex.InnerException.ToString());
                    throw new InvalidOperationException(ex.Message);
                }
            }
        }

        //public int ActivateUser(User users)
        //{
        //    using (IDbConnection connection = _sqlConnectionFactory.GetConnection)
        //    {
        //        string updateQuery = @"UPDATE tbluser SET IsActive=1 WHERE UserKey=@UserKey AND Email=@Email";
        //        _sqlConnectionFactory.OpenConnection(connection);
        //        try
        //        {
        //            return connection.Execute(updateQuery, new
        //            {
        //                users.UserKey,
        //                users.Email
        //            });
        //        }
        //        catch (Exception ex)
        //        {

        //            throw new InvalidOperationException(ex.Message);
        //        }

        //    }
        //}
    }
}
