

using Microsoft.EntityFrameworkCore;

namespace api_viddils2.Models
{
    public class Viddil_context : DbContext
    {
        public Viddil_context(DbContextOptions<Viddil_context> options)
            : base(options)
        {
        }

        public virtual DbSet<Viddil_item> ViddilItems { get; set; }
        public virtual DbSet<User_items> UserItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Viddil_item>().ToTable("viddils");
            modelBuilder.Entity<User_items>().ToTable("users");
            modelBuilder.UseSerialColumns();

        }
       


    }
}

