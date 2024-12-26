using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Management.PgSqlFunction;
using StudentWebApi;
using Management.Users.Dto;

namespace Management.PgSqlFunction
{
    public class FunctionService:IFunctionService
    {
        private readonly AppDbContext _context;

        public FunctionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserNameDto>> GetUserNamesByRoleIdAsync(int roleId)
        {
            var userNames = new List<UserNameDto>();

            var sql = "SELECT u\"UserName\" FROM public.getuserbyid(@role_id) AS u";
            var roleIdParameter = new Npgsql.NpgsqlParameter("@role_id", roleId);

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = sql;
                command.Parameters.Add(roleIdParameter);
                _context.Database.OpenConnection();

                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        userNames.Add(new UserNameDto
                        {
                            UserName = result.GetString(0) 
                        });
                    }
                }
            }

            return userNames;
        }
    }
}
