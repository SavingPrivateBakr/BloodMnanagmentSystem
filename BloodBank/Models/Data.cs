using BloodBank.Mapping;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BloodBank.Models
{
    public class Data : IdentityDbContext<ApplicationUser>
    {

        public Data(DbContextOptions<Data> bb ): base( bb ) { 
        
        }
       /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=LAPTOP-HD9LIS85;Initial Catalog=BloodManagment;Integrated Security=True;Pooling=False;Encrypt=False;Trust Server Certificate=False");
        }*/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Nurse>().HasMany(w => w.Donors).WithOne(w=>w.Nurses);
            modelBuilder.Entity<Nurse>().HasMany(w => w.Bloods).WithOne(w => w.Nurses).OnDelete(DeleteBehavior.Restrict) ;
            modelBuilder.Entity<Donor>().HasMany(w => w.Bloods).WithOne(w => w.Donors);
            modelBuilder.Entity<Blood>().HasOne(w=>w.Hospital).WithOne(w=>w.Blood).HasForeignKey<Hospital>();
            modelBuilder.Entity<Donor>().Property(w => w.Id).ValueGeneratedNever();
            
            base.OnModelCreating(modelBuilder);
            
        }

        public DbSet<Nurse> Nurses { get; set; }

        public DbSet<Hospital> Hospitals { get; set; }

        public DbSet<Donor> Donors { get; set; }

        public DbSet<Blood> Bloods { get; set; }
       
      


    }
}
