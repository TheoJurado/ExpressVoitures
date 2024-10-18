using ExpressVoitures.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ExpressVoitures.Data
{
    public class P5Referential : DbContext
    {
        private IDbConnection DbConnection { get; }

        public P5Referential(DbContextOptions<P5Referential> options, IConfiguration config)
            : base(options)
        {
            DbConnection = new SqlConnection(config.GetConnectionString("P5Ref"));
        }

        public virtual DbSet<Vehicule> Vehicule { get; set; }
    }
}
