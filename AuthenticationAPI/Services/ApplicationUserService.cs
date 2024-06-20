﻿using AuthenticationAPI.Data;
using AuthenticationAPI.DTO;
using AuthenticationAPI.Models;
using Newtonsoft.Json;
using System.Text;

namespace AuthenticationAPI.Services
{

    public class ApplicationUserService
    {
        private readonly HttpClient _httpClient;
        private readonly ApplicationDbContext _context;

        public ApplicationUserService(HttpClient httpClient, ApplicationDbContext context)
        {
            _httpClient = httpClient;
            _context = context;
        }

        public async Task<bool> ApplicationUserEntityAsync(string ApplicationUserId)
        {
            /*    // Validate IdentityUserId
                var response = await _httpClient.GetAsync($"http://localhost:5094/api/user/{ApplicationUserId}");
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
                return true;*/

            // Prepare the request payload
            var payload = new { UserId = ApplicationUserId };
            var jsonPayload = JsonConvert.SerializeObject(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            // Validate IdentityUserId
            var response = await _httpClient.PostAsync("http://localhost:5094/api/user/createuserprofileasync", content);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            return true;
        }

        public async Task<string> GetUserIdByUserName(string userName)
        {
            var userFromDb = _context.ApplicationUsers.FirstOrDefault
               (u => u.UserName.ToLower() == userName.ToLower());
            return userFromDb.Id.ToString();
        }


    }
}
