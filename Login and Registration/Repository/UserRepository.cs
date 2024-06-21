using Dapper;
using Login_and_Registration.Models;
using Microsoft.Data.SqlClient;

namespace Login_and_Registration.Repository
{
    public class UserRepository:IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<int> AddUser(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "INSERT INTO Users (Username, PasswordHash, Email) VALUES (@Username, @PasswordHash, @Email); SELECT CAST(SCOPE_IDENTITY() as int);";
                return await connection.QuerySingleAsync<int>(sql, user);
            }
        }

        public async Task<User> GetUserByUsername(string username)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM Users WHERE Username = @Username";
                return (await connection.QueryAsync<User>(sql, new { Username = username })).FirstOrDefault();
            }
        }
    }
}
