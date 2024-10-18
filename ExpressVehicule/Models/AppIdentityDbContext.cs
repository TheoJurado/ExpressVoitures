using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ExpressVoitures.Models
{
    public class AppIdentityDbContext : IdentityDbContext<IdentityUser>
    {
        private IDbConnection DbConnection { get; }

        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options, IConfiguration config)
        : base(options)
        {
            DbConnection = new SqlConnection(config.GetConnectionString("P5Id"));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DbConnection.ConnectionString, providerOptions => providerOptions.EnableRetryOnFailure());
            }
        }
    }
}
