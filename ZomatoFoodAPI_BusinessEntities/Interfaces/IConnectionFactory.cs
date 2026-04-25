using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;//import this package to use the SqlConnection class for database connections.
namespace ZomatoFoodAPI_BusinessEntities.Interfaces
{
    public interface IConnectionFactory
    {
        //WE NEED TO CREATE A METHOD FOR EACH DATABASE CONNECTION STRING IN THE CONNECTION FACTORY INTERFACE AND IMPLEMENT IT IN THE CONNECTION FACTORY CLASS.
        SqlConnection MidLandSqlConnectionString();
        SqlConnection Northwind_DBSqlConnectionString();
        SqlConnection HotelmanagementsqlConnectionString();
        SqlConnection RestaurantDBSqlConnectionString();
    }
}
