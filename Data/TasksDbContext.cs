using Microsoft.EntityFrameworkCore;

namespace TasksManager.Data
{
    public class TasksDbContext : DbContext
    {
        public TasksDbContext(DbContextOptions<TasksDbContext> options) : base(options)
        {
        }

        public DbSet<TasksManager.Models.TaskMaster> TaskMaster { get; set; }
        public DbSet<TasksManager.Models.TaskTransaction> TaskTransaction { get; set; }
        public DbSet<TasksManager.Models.TaskLog> TaskLog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TasksManager.Models.TaskTransaction>()
                .Property(t => t.TransactionId)
                .HasColumnName("TaskTransactionId");

            modelBuilder.Entity<TasksManager.Models.TaskMaster>()
                .ToTable("TaskMaster")
                .Property(t => t.Functions)
                .HasColumnName("Functions");

            modelBuilder.Entity<TasksManager.Models.TaskLog>()
                .ToTable("TaskLogs")
                .HasKey(t => new { t.TaskId, t.StatusUpdateDate });
        }
    }
}
