using Microsoft.EntityFrameworkCore;

namespace TestTaking.Shared.Models
{
  public class PracticeTestContext : DbContext
  {
    public PracticeTestContext(DbContextOptions<PracticeTestContext> options)
        : base(options)
    {
    }

    public DbSet<PracticeTest> PracticeTests { get; set; }
    public DbSet<Question> Questions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<PracticeTest>().ToTable("PracticeTest");
      modelBuilder.Entity<Question>().ToTable("Question");
    }
  }
}