using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCaseManager.DB.Code_First
{
    public class User
    {
        public User()
        {
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
    }
}
