using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleBoards.Core.Models;

namespace SimpleBoards.Core.Persistence
{
    public class IssueMapper : IEntityTypeConfiguration<Issue>
    {
        public void Configure(EntityTypeBuilder<Issue> builder)
        {
            builder
                .Property(i => i.Title)
                .HasMaxLength(255)
                .IsRequired();

            builder
                .HasOne(i => i.Board)
                .WithMany(b => b.Issues);

            builder
                .HasMany(i => i.Comments)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(i => i.Reporter)
                .WithMany();

            builder
                .HasOne(i => i.Assignee)
                .WithMany();

            builder
                .HasOne(i => i.Tester)
                .WithMany();
        }
    }
}