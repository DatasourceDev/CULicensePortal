using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CULCS.Models;
using Microsoft.EntityFrameworkCore;

namespace CULCS.DAL
{
    public class CuContext : DbContext
    {
        public CuContext(DbContextOptions options) : base(options) { }

        public CuContext()
        {

        }
        public DbSet<Aumphur> Aumphurs { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Tumbon> Tumbons { get; set; }
        public DbSet<User> Users { get; set; }
        //public DbSet<Guest> Guests { get; set; }
        //public DbSet<GuestImport> GuestImports { get; set; }
        //public DbSet<Admin> Admins { get; set; }
        public DbSet<Setup> Setups { get; set; }
        //public DbSet<AuditLog> AuditLogs { get; set; }
        //public DbSet<OU> OUs { get; set; }
        //public DbSet<AdminRole> AdminRoles { get; set; }
        //public DbSet<Role> Roles { get; set; }
        public DbSet<Borrow> Borrows { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<ProgramLicense> ProgramLicenses { get; set; }
        public DbSet<AzureGroup> AzureGroups { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}
