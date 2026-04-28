using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZomatoFoodAPI_BusinessEntities.Dtos
{
    public class UserDTO
    {
        public int id { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
    }
}
