﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SosyalMedya_Web.Models;
using System.Text;

namespace SosyalMedya_Web.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet("giris-yap")]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost("LoginPost")]
        public async Task<IActionResult> Login(UserForLogin userForLogin)
        {
            var httpClient = new HttpClient();
            var jsonLogin = JsonConvert.SerializeObject(userForLogin);
            StringContent conten=new StringContent(jsonLogin,Encoding.UTF8,"application/json");
            var responseMessage=await httpClient.PostAsync("https://localhost:5190/api/auth/login", conten);

            if(responseMessage.IsSuccessStatusCode)
            {
                string responseContent= await responseMessage.Content.ReadAsStringAsync();
                var responseLogin=JsonConvert.DeserializeObject<ApiAuthDataResponse<UserForLogin>>(responseContent);

                return RedirectToAction("Index","Home");
            }
            return View("Login","Auth");
        }
    }
}
