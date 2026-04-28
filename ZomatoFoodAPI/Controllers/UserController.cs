using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZomatoFoodAPI_BusinessEntities.Interfaces;
using ZomatoFoodAPI_BusinessEntities.Models;

namespace ZomatoFoodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        [Route("GetUsers")]
        public async Task<string> GetUsersData()
        {
            return await _userService.InvokeUsersList();
        }

        [HttpGet]
        [Route("GetUserDetailsById/{id}")]
        public async Task<string> GetUsersDataById(int id)
        {
            return await _userService.InvokeUsersById(id);
        }
        [HttpPost]
        [Route("InsertUserDetails")]
        public async Task<string> InsertUserDetails([FromBody] User userDetail)
        {
            return await _userService.InsertUserData(userDetail);
        }
        [HttpPut]
        [Route("UpdateUserDetails/{id}")]
        public async Task<string> UpdateUserDetails([FromBody] User userDetail, [FromRoute] int id)
        {
            return await _userService.UpdateUserData(userDetail, id);
        }
        [HttpDelete]
        [Route("DeleteUserDetailsById/{id}")]
        public async Task<string> DeleteUserDataById(int id)
        {
            return await _userService.DeleteUserData(id);
        }
    }
}
