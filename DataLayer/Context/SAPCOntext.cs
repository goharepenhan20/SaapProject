using DataLayer.Entity.Person;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Context
{
    public class SAPCOntext:DbContext
    {
        public SAPCOntext(DbContextOptions<SAPCOntext> options):base(options)
        {
            
        }

        public DbSet<PersonEntity> PersonEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PersonEntity>().HasIndex(p => p.CodeMelli).IsUnique();
        }
    }
}