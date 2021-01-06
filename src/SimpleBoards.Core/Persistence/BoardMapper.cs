using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleBoards.Core.Models;

namespace SimpleBoards.Core.Persistence
{
    public class BoardMapper : IEntityTypeConfiguration<Board>
    {
        public void Configure(EntityTypeBuilder<Board> builder)
        {
            builder
                .Property(b => b.Name)
                .HasMaxLength(255)
                .IsRequired();
        }
    }
}