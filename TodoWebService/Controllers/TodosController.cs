using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using CB.Database.AdoDotNet;
using TodoModels;


namespace TodoWebService.Controllers
{
    public class TodosController: ApiController
    {
        #region Fields
        private const string CONTENT_COL = "Content";
        private const string CONTENT_PAR = "@content";
        private const string CREATEDON_COL = "CreatedOn";
        private const string CREATEDON_PAR = "@createdOn";
        private const string DEADLINE_COL = "Deadline";
        private const string DEADLINE_PAR = "@deadline";
        private const string GET_PROC = "dbo.proc_Todo_Get";
        private const string ID_COL = "Id";
        private const string ID_PAR = "@id";
        private const string ISDONE_COL = "IsDone";
        private const string ISDONE_PAR = "@isDone";
        private const string SAVE_PROC = "dbo.proc_Todo_Save";
        private const string UPDATEDON_COL = "UpdatedOn";
        private const string UPDATEDON_PAR = "@updatedOn";
        private static readonly string _connectionString = GetConnectionString();
        #endregion


        #region Methods
        [HttpGet]
        public async Task<Todo[]> GetAsync( CancellationToken cancellationToken)
            => await UseConnectionAsync(async con =>
            {
                using (var command = new SqlCommand(GET_PROC, con) { CommandType = CommandType.StoredProcedure }
                    )
                {
                    using (var reader = await command.ExecuteReaderAsync( cancellationToken))
                    {
                        return await ReadTodosAsync(reader , cancellationToken);
                    }
                }
            });

        [HttpPut]
        public async Task<Todo[]> PutAsync(Todo[] todos, CancellationToken cancellationToken)
            => await UseConnectionAsync(async con =>
            {
                foreach (var todo in todos)
                {
                    using (
                        var command = new SqlCommand(SAVE_PROC, con)
                        {
                            CommandType = CommandType.StoredProcedure
                        })
                    {
                        var idPar = command.Parameters.Add(ID_PAR, SqlDbType.Int);
                        if (todo.Id.HasValue) idPar.Value = todo.Id.Value;
                        else idPar.Value = DBNull.Value;

                        command.Parameters.AddWithValue(CONTENT_PAR, todo.Content);
                        command.Parameters.AddWithValue(DEADLINE_PAR, todo.Deadline);
                        command.Parameters.AddWithValue(ISDONE_PAR, todo.IsDone);

                        var createdOnPar = command.Parameters.Add(CREATEDON_PAR, SqlDbType.DateTime);
                        if (todo.CreatedOn.HasValue) createdOnPar.Value = todo.CreatedOn.Value;
                        else createdOnPar.Value = DBNull.Value;

                        var updatedOnPar = command.Parameters.Add(UPDATEDON_PAR, SqlDbType.DateTime);
                        if (todo.UpdatedOn.HasValue) updatedOnPar.Value = todo.UpdatedOn.Value;
                        else updatedOnPar.Value = DBNull.Value;

                        var result = await command.ExecuteScalarAsync(cancellationToken);
                        var id = Convert.ToInt32(result);
                        todo.Id = id;
                    }
                }

                return todos;
            });
        #endregion


        #region Implementation
        private static string GetConnectionString()
            => ConfigurationManager.ConnectionStrings["todosConnectionString"].ConnectionString;

        private static async Task<Todo[]> ReadTodosAsync(DbDataReader reader , CancellationToken cancellationToken)
        {
            var todos = new List<Todo>();
            while (await reader.ReadAsync( cancellationToken))
            {
                todos.Add(new Todo
                {
                    Id = reader.ReadInt32(ID_COL),
                    Content = reader.ReadString(CONTENT_COL),
                    Deadline = reader.GetDateTime(DEADLINE_COL),
                    IsDone = reader.GetBoolean(ISDONE_COL),
                    CreatedOn = reader.ReadDateTime(CREATEDON_COL),
                    UpdatedOn = reader.ReadDateTime(UPDATEDON_COL)
                });
            }
            return todos.ToArray();
        }

        private static async Task<T> UseConnectionAsync<T>(Func<SqlConnection, Task<T>> useConnection)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();
                return await useConnection(con);
            }
        }
        #endregion
    }
}