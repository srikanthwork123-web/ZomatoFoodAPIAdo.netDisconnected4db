using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZomatoFoodAPI_BusinessEntities.Interfaces;
using ZomatoFoodAPI_BusinessEntities.Models;

namespace ZomatoFoodAPI_ServiceLayer
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<string> InvokeUsersList()
        {
            return await this._userRepository.InvokeUsersList();
        }
        public async Task<string> InvokeUsersById(int id)
        {
            return await this._userRepository.InvokeUsersById(id);
        }
        public async Task<string> InsertUserData(User userDetail)
        {
            return await this._userRepository.InsertUserData(userDetail);
        }
        public async Task<string> UpdateUserData(User userDetail, int id)
        {
            return await this._userRepository.UpdateUserData(userDetail, id);
        }

        public async Task<string> DeleteUserData(int id)
        {
            return await this._userRepository.DeleteUserData(id);
        }
    }
}
