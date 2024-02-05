using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api_opendata.Data
{
    public class DatabaseContext : IdentityDbContext<AspNetUsers, AspNetRoles, string>
    {
        private readonly IConfiguration configuration;
        public DatabaseContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }
        //
        #region DbSet

        //For Authoright -- assign permission folow dashboard
        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<Dashboards> Dashboards { get; set; }
        public DbSet<UserDashboards> UserDashboards { get; set; }
        public DbSet<RoleDashboards> RoleDashboards { get; set; }
        public DbSet<Functions> Functions { get; set; }

        //Other database

        public DbSet<Department> Department { get; set; }
        public DbSet<Repository> Repository { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Document> Document { get; set; }
        public DbSet<Staff_Document> Staff_Document { get; set; }

        #endregion
    }
}
