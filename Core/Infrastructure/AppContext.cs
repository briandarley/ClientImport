using System.Data.Entity;
using System.Data.SqlClient;
using System.Reflection;
using Core.OrgMapping;

namespace Core.Infrastructure
{
    public class AppContext : DbContext
    {
        private static string GetConnectionString()
        {
            var directory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var fileName = System.IO.Path.Combine(directory??"", "ClientData.mdf");
            var connection = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='{fileName}';Integrated Security=True;Connect Timeout=30";
            var builder = new SqlConnectionStringBuilder(connection);

            return builder.ConnectionString;
        }
        public DbSet<OrgLevel> OrgLevels { get; set; }
        public AppContext() : base(GetConnectionString())
        {
        
        }
        //public AppContext():base("sql:default")
        //{

        //}
        //public AppContext(string connectionString):base(connectionString)
        //{

        //}
    }
}
