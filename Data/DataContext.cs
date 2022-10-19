using Microsoft.EntityFrameworkCore;
using TestApi.Models;

namespace TestApi.Data {
    public class DataContext : DbContext {
        public DataContext( DbContextOptions<DataContext> options): base (options) {}

        

        public DbSet<Family> Families => Set<Family>();
        public DbSet<FamilyMember> FamilyMembers => Set<FamilyMember>();
        public DbSet<Father> Fathers => Set<Father>();
        public DbSet<Mother> Mothers => Set<Mother>();
        public DbSet<Child> Children => Set<Child>();

        protected override void OnModelCreating( ModelBuilder modelBuilder ) {
            modelBuilder.Entity<Family>().Navigation(f => f.Members).AutoInclude();
        }

        //public override Task<int> SaveChangesAsync( CancellationToken cancellationToken = new CancellationToken() ) {
        //    // idk
        //    return base.SaveChangesAsync(cancellationToken);
        //}
    }


}

