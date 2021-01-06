using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using SimpleBoards.Core.Models;

namespace SimpleBoards.Core.Persistence
{
    public class BoardsContext : DbContext
    {
        public BoardsContext([NotNullAttribute] DbContextOptions options) 
            : base(options)
        {
        }

        public virtual DbSet<Board> Boards { get; set; }

        public virtual DbSet<Issue> Issues { get; set; }

        public virtual DbSet<Comment> Comments { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new BoardMapper());
            modelBuilder.ApplyConfiguration(new IssueMapper());
            modelBuilder.ApplyConfiguration(new CommentMapper());
            modelBuilder.ApplyConfiguration(new UserMapper());
        }
    }
}