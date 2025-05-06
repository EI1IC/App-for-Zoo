using RazorPagesZoo.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Npgsql;

namespace RazorPagesZoo
{
    public class EmployeeService
    {
        private readonly ZoodbContext _context;

        public EmployeeService(ZoodbContext context)
        {
            _context = context;
        }

        public async Task<long> GetEmployeeIdByUserIdAsync(string userId)
        {
            return await _context.Employees.Where(c => c.IdentityUserId == userId).Select(c => c.IdEmployee).FirstOrDefaultAsync();
        }

        public async Task<bool> IsEmployeeIdExistsAsync(string userId)
        {
            return await _context.Employees.AnyAsync(p => p.IdentityUserId == userId);
        }
        public async Task<float> CalculateWorkEfficiencyAsync(long employeeId)
        {
            var query = @"
            SELECT zoo_keepers.worker_efficiency(@employeeId);";

            await using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.Parameters.Add(new NpgsqlParameter("employeeId", employeeId));

                await _context.Database.OpenConnectionAsync();

                var result = await command.ExecuteScalarAsync();
                return Convert.ToSingle(result);
            }
        }
    }
}