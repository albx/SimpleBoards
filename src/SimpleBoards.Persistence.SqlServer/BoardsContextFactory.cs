using SimpleBoards.Core.Persistence;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace SimpleBoards.Persistence.SqlServer
{
    public class BoardsContextFactory : IDesignTimeDbContextFactory<BoardsContext>
    {
        private readonly string _connectionString;

        private readonly Assembly _migrationAssembly;

        public BoardsContextFactory()
        {
            _connectionString = "Server=.\\SQLExpress;Database=SimpleBoards;Trusted_Connection=True;MultipleActiveResultSets=true";
            _migrationAssembly = this.GetType().Assembly;
        }

        public BoardsContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<BoardsContext>()
                .UseSqlServer(_connectionString, b => b.MigrationsAssembly(_migrationAssembly.GetName().Name))
                .Options;

            return new BoardsContext(options);
        }
    }
}