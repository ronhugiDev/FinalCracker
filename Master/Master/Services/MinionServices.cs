using Master.Dto;
using Master.Entities;
using Master.Reposetories;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Master.Services
{
    public class MinionServices : IMinionServices
    {
        private readonly HttpClient _httpClient;
        private readonly ServiceSettings _serviceSettings;

        public MinionServices(HttpClient httpClient
                , IOptions<ServiceSettings> options)
        {
            _httpClient = httpClient;
            _serviceSettings = options.Value;
        }


        public async Task<string> Post_GetNumberFromHash(string hashNumber)
        {
            var r = PasswordReposetory.Instance;
            if (PasswordReposetory.Passwords.ContainsKey(hashNumber))
            {
                return PasswordReposetory.Passwords[hashNumber];
            }
            List<string> urls = new();
            var minions = InitMinions(hashNumber);
            foreach (var minion in minions)
            {
                urls.Add(minion.Url);
            }
            try
            {
                var requests = urls.Select(url => _httpClient.GetFromJsonAsync<PhoneNumberDto>(url)).ToList();
                await Task.WhenAll(requests);
                var responses = requests.Select(task => task.Result);
                foreach (var response in responses)
                {
                    if (response != null)
                    {
                        PasswordReposetory.Passwords.Add(response.HashedNumber, response.FullNumber);
                    }
                }
            }
            catch (Exception)
            {
                throw new HttpRequestException("no minions is active");
            }
            return PasswordReposetory.Passwords[hashNumber];
        }

        private MinionData[] InitMinions(string hashPass)
        {
            var minions = new MinionData[_serviceSettings.MinionsSetting.Length];

            int divition = (int)Math.Pow(10, 8) / _serviceSettings.MinionsSetting.Length;
            int startRange = 0;
            for (int i = 0; i < minions.Length; i++)
            {
                minions[i] = new MinionData();

                minions[i].StartRange = startRange;
                minions[i].EndRange = divition + startRange;
                var getFunction = string.Format(_serviceSettings.MinionsSetting[i].CrackerFunction,
                        hashPass, minions[i].StartRange, minions[i].EndRange);
                minions[i].Url = _serviceSettings.MinionsSetting[i].MinionCrackerUrl + getFunction;
            }
            startRange = divition + startRange;

            return minions;
        }

        public async Task<List<string>> MinionsAliveCheck()
        {
            var minionUrl = string.Empty;
            List<string> deadMinions = new();
            foreach (var minion in _serviceSettings.MinionsSetting)
            {
                minionUrl = minion.MinionCrackerUrl + minion.IsAlive;
                var response = await _httpClient.GetAsync(minionUrl);
                if (!response.IsSuccessStatusCode)
                {
                    deadMinions.Add("dead Minion url: " + minion.MinionCrackerUrl);
                }
            }
            return deadMinions;
        }
    }
}
