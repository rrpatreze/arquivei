using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

using Dapper.Contrib.Extensions;
using Dapper;

namespace DAO
{
    public class DAOBase<T> : DAOBase where T : class
    {

        public T GetById(long id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Get<T>(id);
            }
        }


        public List<T> Query(string str, dynamic param = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Query<T>(str, param: (object)param).ToList();
            }
        }

        public T Get(Func<T, bool> filter)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.GetAll<T>().Where(filter).ToList().FirstOrDefault();
            }
        }

        public List<T> GetAll()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.GetAll<T>().ToList();
            }
        }

        public List<T> Find(Func<T, bool> filter)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.GetAll<T>().Where(filter).ToList();
            }
        }

        public long Insert(T entity)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Insert(entity);
            }
        }

        public long Insert(T entity, SqlConnection connection, SqlTransaction transaction)
        {
            return connection.Insert(entity, transaction);
        }

        public long Insert(T entity, SqlTransaction transaction)
        {
            return transaction.Connection.Insert(entity, transaction);
        }

        public long Insert(List<T> entities)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Insert(entities);
            }
        }

        public long Insert(List<T> entities, SqlConnection connection, SqlTransaction transaction)
        {
            return connection.Insert(entities, transaction);
        }

        public bool Update(T entity)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Update(entity);
            }
        }

        public bool Update(T entity, SqlConnection connection, SqlTransaction transaction)
        {
            return connection.Update(entity, transaction);
        }

        public bool Update(List<T> entities)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Update(entities);
            }
        }

        public bool Update(List<T> entities, SqlConnection connection, SqlTransaction transaction)
        {
            return connection.Update(entities, transaction);
        }

        public bool Delete(T entity)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Delete(entity);
            }
        }

        public bool Delete(T entity, SqlConnection connection, SqlTransaction transaction)
        {
            return connection.Delete(entity, transaction);
        }

        public bool Delete(List<T> entities)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Delete(entities);
            }
        }

        public bool Delete(List<T> entities, SqlConnection connection, SqlTransaction transaction)
        {
            return connection.Delete(entities, transaction);
        }

    }

    public class DAOBase
    {
        protected string ConnectionString = @"Data Source=sql5042.site4now.net;Initial Catalog=DB_9B0417_arquivei;User ID=DB_9B0417_arquivei_admin;Password=D@aniel01;Connection Timeout=1000";

        public SqlConnection AbrirConexao()
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            return conn;
        }
        public void FecharConexao(SqlConnection connection)
        {
            connection.Close();
        }

        public SqlTransaction AbrirTransacao(SqlConnection connection)
        {
            return connection.BeginTransaction();
        }
        public void EfetivarTransacao(SqlTransaction transaction)
        {
            transaction.Commit();
        }
        public void CancelarTransacao(SqlTransaction transaction)
        {
            transaction.Rollback();
        }

        public IEnumerable<dynamic> ExecuteReader(string sql, dynamic param = null, CommandType commandType = CommandType.Text)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Query(sql, param: (object)param, commandType: commandType);
            }
        }

        public dynamic ExecuteScalar(string sql, dynamic param = null, CommandType commandType = CommandType.Text)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.ExecuteScalar(sql, param: (object)param, commandType: commandType);
            }
        }

        public int ExecuteNonQuery(string sql, dynamic param = null, CommandType commandType = CommandType.Text)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                return connection.Execute(sql, param: (object)param,
                            commandTimeout: 120, commandType: commandType);
            }
        }

    }
}