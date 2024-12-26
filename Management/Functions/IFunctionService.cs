using Management.Users.Dto;

namespace Management.PgSqlFunction
{
    public interface IFunctionService
    {
        Task<List<UserNameDto>> GetUserNamesByRoleIdAsync(int roleId);
    }
}
