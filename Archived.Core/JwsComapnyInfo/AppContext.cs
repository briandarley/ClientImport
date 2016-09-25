using System.Data.Entity;

namespace Archived.Core.JwsComapnyInfo
{
    public class AppContext:DbContext
    {
        public DbSet<ComapnyInfo> CompanInfos { get; set; }
        public AppContext():base("database:jws")
        {
            
        }
    }
}
