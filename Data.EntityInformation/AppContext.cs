using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Data.EntityInformation.Models;

namespace Data.EntityInformation
{
    public class AppContext:DbContext
    {
        //private static string GetConnectionString()
        //{
        //    //var directory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        //    //var fileName = System.IO.Path.Combine(directory ?? "", "ClientData.mdf");
        //    var connection = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='{fileName}';Integrated Security=True;Connect Timeout=30";
        //    var builder = new SqlConnectionStringBuilder(connection);

        //    return builder.ConnectionString;
        //}

        public AppContext() : base("sql:EntityInfo")
        {

        }

        public DbSet<EntityConfiguration> EntityConfigurations { get; set; }
    }
}
