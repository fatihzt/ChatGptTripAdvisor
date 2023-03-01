using ChatGptBot.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatGptBot.DataAccess
{
    public class DataBaseContext:DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<ChatHistory> ChatHistories { get; set; }
        public DataBaseContext() { }
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ChatHistory>(entity =>
            {
                entity.HasOne(c => c.User)
                .WithMany(u => u.ChatHistories)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_ChatHistory_User");
            });
        }
        
    }
}
