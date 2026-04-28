using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZomatoFoodAPI_BusinessEntities.Interfaces;
using ZomatoFoodAPI_BusinessEntities.Models;
using Newtonsoft.Json;//import this namespace to use the JsonConvert class which is used to serialize and deserialize the data in json format.
using Microsoft.Extensions.Configuration;//import this namespace to use the IConfiguration interface which is used to read the connection string information from appsettings.json file or from the environment variables or from the user secrets or from any other secure place where you are storing the connection string information.
namespace ZomatoFoodAPI_RepositoryLayer
{
    //IConfiguration interface comming from "Microsoft.Extensions.Configuration" packgae.
    //you need to install this package from  manage nugetpaackage manager.
    public class UserRepository: IUserRepository
    {
        private readonly IConfiguration _config;
        //to read the connections strings or any keys from appsettings.json file we need to use IConfiguration interface.
        //inject the IConfiguration interface in the constructor of the repository class and assign it to the private readonly field of the IConfiguration interface type.
        public UserRepository(IConfiguration config)
        {
            _config = config;
        }
        public async Task<string> InvokeUsersList()
        {//i am reading the 3rd part api url from appsettings.json file using the IConfiguration interface and then i am using that url to call the 3rd part api and get the response from that api and return it to the service layer.
            string Url = Convert.ToString(_config.GetSection("UserApis:GetUsersApiUrl").Value);
            //Don't write the 3rd part api url here.we must read from apssettings.json(realtime companies read from appsettings.json file)
            // string url = "https://fakerestapi.azurewebsites.net/api/v1/Users";
// HttpClient is peredfined class which is used to send http requests and receive http responses from a  3rd part apis.
            HttpClient client = new HttpClient();//which is used to communicate with 3rdpart apis.
            //HttpRequestMessage is predefined class which is used to create http request message to send to the 3rd part api.
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Url);
            //what type of response you are expecting api response.you should specify here.
            // "application/json" means,it will return json formt data from the api response.
            //if you are expecting json format data from the api response,then you should specify "application/json" here.
            //if you are expecting xml format data from the api response,then you should specify "application/xml" here.
            request.Headers.Add("accept", "application/json");
            //HttpResponseMessage is predefined class which is used to receive http response message from the 3rd part api.
            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsStringAsync();//it will read the response data from the api response and return it as a string format.
            return responseData;
        }
        public async Task<string> InvokeUsersById(int id)
        {
            string Apiurl = Convert.ToString(_config.GetSection("UserApis:GetUsersByIdApiUrl").Value);

            string url = string.Format(Apiurl, id);

            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("accept", "application/json");
            HttpResponseMessage response = await client.SendAsync(request);
            // response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsStringAsync();
            return responseData;
        }
        public async Task<string> InsertUserData(User userDetail)
        {
            string url = Convert.ToString(_config.GetSection("UserApis:InsertUserApiUrl").Value);
            //string url = "https://fakerestapi.azurewebsites.net/api/v1/Users";
            //SerializeObject means:Serialize the specified object into json string format.
            var serializedata = JsonConvert.SerializeObject(userDetail);
            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("accept", "application/json");
            StringContent content = new StringContent(serializedata, null, "application/json");
            request.Content = content;
            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsStringAsync();
            return responseData;
        }

        public async Task<string> UpdateUserData(User userDetail, int id)
        {
            string ApiUrl = Convert.ToString(_config.GetSection("UserApis:UpdateUserApiUrl").Value);
            string url = string.Format(ApiUrl, id);
            var serializedata = JsonConvert.SerializeObject(userDetail);
            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Put, url);
            request.Headers.Add("accept", "application/json");
            //we must pass data in the form of json format to these 3rd party api's.
            StringContent content = new StringContent(serializedata, null, "application/json");
            request.Content = content;
            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsStringAsync();
            return responseData;
        }
        public async Task<string> DeleteUserData(int id)
        {
            string ApiUrl = Convert.ToString(_config.GetSection("UserApis:DeleteUserApiUrl").Value);
            string url = string.Format(ApiUrl, id);
            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Delete, url);
            request.Headers.Add("accept", "application/json");
            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadAsStringAsync();
            return responseData;
        }
    }
}
