using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework.Context
{
    public class PatronContextDb : DbContext
    {
        //192.168.101.3LAPTOP-SN3MKSFL //Trusted_Connection=True;MultipleActiveResultSets=true User Id=sa; Password=300881_?;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=192.168.101.3; Database=patronDB; User Id=sa; Password=300881_?;");
            optionsBuilder.UseSqlServer(@"Server=192.168.101.3;Database=patronDB;User Id=sa; Password=300881_?;");
        }

        public DbSet<CarTbl> CarTbl { get; set; }
        public DbSet<CityTbl> CityTbl { get; set; }
        public DbSet<PermitImageTbl> PermitImageTbl { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<TransactionsTbl> TransactionsTbl { get; set; }
        public DbSet<NDataTbl> NDataTbl { get; set; }
        public DbSet<LDataTbl> LDataTbl { get; set; }
        public DbSet<FinishLocation> FinishLocation { get; set; }
        public DbSet<StartLocation> StartLocation { get; set; }
        public DbSet<Recipe> Recipe { get; set; }
        public DbSet<UserCredit> UserCredits { get; set; }

    }
}
