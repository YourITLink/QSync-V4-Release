using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QSync.Models;
using System.Data.Entity;

namespace QSync.Utils
{
    class UserContext : DbContext
    {
        static UserContext()
        {
            //Database.SetInitializer(new UserContextInitializer());

            //// kinda bicycle, cause my localdb is not set up properly
            //var type = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
        }

        public UserContext() : base("name=UsersDb")
        {
            Database.SetInitializer(new UserContextInitializer());

            // kinda bicycle, cause my localdb is not set up properly
            var type = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
        }

        public DbSet<User> Users { get; set; }
    }

    class UserContextInitializer : CreateDatabaseIfNotExists<UserContext>
    {
        protected override void Seed(UserContext context)
        {
        /*    var user = new User("admin", "1234", true);

            foreach (var dir in user.AvailableDirectoriesList)
            {
                FileSystemManager.Instance.CreateDirectory(dir);
                FileSystemManager.Instance.AddUserPermissionToDirectory(dir, user.Username);
            }

            FileSystemManager.Instance.Save();

            context.Users.Add(user);
            context.SaveChanges(); */
        }
    }
}
