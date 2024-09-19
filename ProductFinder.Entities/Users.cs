using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductFinder.Entities
{
    public class Users
    {
        public static List<User> User = new()
        {
            new User { Id = 1, Username = "Admin", Password = "123456", Role = "Admin"},
            new User { Id = 2, Username = "Furkan", Password = "123456", Role = "DefaultUser" }
        };
    }
}
