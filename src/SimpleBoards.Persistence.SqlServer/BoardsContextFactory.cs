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
            _connectionString = "Server=host.docker.internal;Database=SimpleBoards;User Id=sa;Password=S1mpl3Bo4rds!;MultipleActiveResultSets=true";
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