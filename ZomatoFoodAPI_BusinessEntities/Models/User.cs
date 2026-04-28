using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZomatoFoodAPI_BusinessEntities.Models
{
    public  class User
    {
        public int id { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
    }
}
/*response
 * ===========
  {
        "id": 1,
        "userName": "User 1",
        "password": "Password1"
    }
*/