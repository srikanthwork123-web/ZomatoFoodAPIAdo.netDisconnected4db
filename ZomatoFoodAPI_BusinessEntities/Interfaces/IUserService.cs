using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZomatoFoodAPI_BusinessEntities.Models;

namespace ZomatoFoodAPI_BusinessEntities.Interfaces
{
    public interface IUserService
    {
        //Abstarct Methods.it having only method heding
        Task<string> InvokeUsersList();
        Task<string> InvokeUsersById(int id);
        Task<string> InsertUserData(User userDetail);
        Task<string> UpdateUserData(User userDetail, int id);
        Task<string> DeleteUserData(int id);
    }
}
