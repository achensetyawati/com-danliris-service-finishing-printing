﻿using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Services.HttpClientService
{
    public class HttpClientService : IHttpClientService
    {
        private HttpClient _client = new HttpClient();

        public HttpClientService(IIdentityService identityService)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, identityService.Token);
            _client.DefaultRequestHeaders.Add("x-timezone-offset", identityService.TimezoneOffset.ToString());
        }

        public async Task<HttpResponseMessage> PutAsync(string url, HttpContent content)
        {
            return await _client.PutAsync(url, content);
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await _client.GetAsync(url);
        }

        public async Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
        {
            return await _client.PostAsync(url, content);
        }

        public async Task<HttpResponseMessage> SendAsync(HttpMethod method, string url, string token, HttpContent content)
        {
            var request = new HttpRequestMessage(method, url)
            {
                Content = content
            };
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await _client.SendAsync(request);

        }
    }
}
