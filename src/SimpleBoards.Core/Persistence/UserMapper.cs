using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleBoards.Core.Models;

namespace SimpleBoards.Core.Persistence
{
    public class UserMapper : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .Property(u => u.Id)
                .ValueGeneratedNever();

            builder
                .Property(u => u.UserName)
                .IsRequired();
                
            builder
                .HasIndex(u => u.UserName)
                .IsUnique();
        }
    }
}