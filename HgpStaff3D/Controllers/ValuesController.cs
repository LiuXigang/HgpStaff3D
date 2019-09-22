using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HgpStaff3D.Controllers
{
    public class ValuesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ValuesController(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            ViewBag.Msg = await client.GetStringAsync("https://www.microsoft.com/zh-cn/"); ;
            return View();
        }

        public async Task<ActionResult<string>> Get()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "repos/aspnet/docs/pulls");
            var client = _httpClientFactory.CreateClient("github");
            var response = await client.SendAsync(request);
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}