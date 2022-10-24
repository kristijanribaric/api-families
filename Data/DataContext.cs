using Microsoft.EntityFrameworkCore;
using FamiliesApi.Models;

namespace FamiliesApi.Data {
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

        // override SaveChangesAsync so last name and first name are always capitalizedd
        public override async Task<int> SaveChangesAsync( CancellationToken cancellationToken = new CancellationToken() ) {
            var changedFamilyMembers = ChangeTracker.Entries().Where(e => e.Entity is FamilyMember && (e.State == EntityState.Added || e.State == EntityState.Modified));
            // set first name and last name first letter of Family Member to uppercase
            if ( changedFamilyMembers != null ) {
                foreach ( var changedMember in changedFamilyMembers ) {
                    var entity = ( FamilyMember )changedMember.Entity;
                    entity.FirstName = entity.FirstName.First().ToString().ToUpper() + entity.FirstName.Substring(1);
                    entity.LastName = entity.LastName.First().ToString().ToUpper() + entity.LastName.Substring(1);
                }
            }

            var changedFamilies = ChangeTracker.Entries().Where(e => e.Entity is Family && (e.State == EntityState.Added || e.State == EntityState.Modified));
            // set last name first letter of Family to uppercase
            if ( changedFamilies != null) {
                foreach ( var changedFamily in changedFamilies ) {
                    var entity = ( Family )changedFamily.Entity;
                    entity.LastName = entity.LastName.First().ToString().ToUpper() + entity.LastName.Substring(1);
                }
            }


            return await base.SaveChangesAsync(cancellationToken);
        }
    }


}

