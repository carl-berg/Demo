using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlServerCe;
using System.Linq.Expressions;

namespace Demo.AnonymousTypes.Domain.Persistence
{
    public abstract class CommandQueryBase
    {
        private readonly string _connectionString;

        protected CommandQueryBase(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<TType> Query<TType>(string query, Func<IDataRecord,TType> parser, object parameters = null)
        {
			using (var sqlConnection = new SqlCeConnection(_connectionString))
            {
                sqlConnection.Open();
                var command = BuildCommand(sqlConnection, query, parameters);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    yield return parser(reader);
                }
            }
        }

        public void Execute(string sql, object parameters = null)
        {
			using (var sqlConnection = new SqlCeConnection(_connectionString))
            {
                sqlConnection.Open();
                var command = BuildCommand(sqlConnection, sql, parameters);
                command.ExecuteNonQuery();
            }
        }

		protected virtual SqlCeCommand BuildCommand(SqlCeConnection connection, string sql, object parameters)
        {
            var command = connection.CreateCommand();
            command.CommandText = sql;
        	foreach (var parameter in BuildParameters(parameters))
        	{
        		command.Parameters.Add(parameter);
        	}
            return command;
        }

		protected virtual IEnumerable<SqlCeParameter> BuildParameters(object parameters)
		{
			foreach (PropertyDescriptor propertyDecriptor in TypeDescriptor.GetProperties(parameters))
			{
				var name = propertyDecriptor.Name.StartsWith("@")
					? propertyDecriptor.Name
					: "@" + propertyDecriptor.Name;
				var value = propertyDecriptor.GetValue(parameters);
				yield return new SqlCeParameter(name, value ?? DBNull.Value);
			}
		}

		protected static TOut Parse<T, TOut>(IDataRecord record, Expression<Func<T, TOut>> expression)
		{
			var memberExpression = (MemberExpression)expression.Body;
			var name = memberExpression.Member.Name;
			return (TOut) record[name];
		}
    }

}
