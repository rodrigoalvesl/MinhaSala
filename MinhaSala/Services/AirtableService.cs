using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;
using MinhaSala.Models;
using Newtonsoft.Json;

namespace MinhaSala.Services
{
    public class AirtableService
    {
        const string PostUrl = "https://api.airtable.com/v0/appfjNKO39qGavA9Y/Usuario";

        const string API_KEY = "keyPmk2OP18Q9fHWy";

        readonly HttpClient _httpClient;


        public AirtableService()
        {
            _httpClient = new HttpClient();

            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", API_KEY);
        }

        public async Task<List<AirtableData>> GetAllRecords()
        {
            string URL = "https://api.airtable.com/v0/appfjNKO39qGavA9Y/DadosTeste";
            List<AirtableData> list = new List<AirtableData>();
            var initialResponse = await AirtableResponseAsync(URL);
            var initialResult = initialResponse?.Records;
            list.AddRange(initialResult.Select(x => x.Fields));
            var offset = initialResponse.Offset;
            if (offset != null)
            {
                do
                {
                    var response = await AirtableResponseAsync(URL, offset);
                    var result = response?.Records;
                    offset = response?.Offset;
                    list.AddRange(result.Select(x => x.Fields));

                } while (offset != null);
            }
            return list;
        }
        public async Task<AirtableResponse> AirtableResponseAsync(string url, string offset = null)
        {
            try
            {
                if (offset != null)
                    url = $"{url}?offset={offset}";

                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var records = JsonConvert.DeserializeObject<AirtableResponse>(content);

                    return records;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Crashes.TrackError(ex);

            }

            return new AirtableResponse();
        }

        public async Task<IEnumerable<User>> CreateUser(string matricula, string nome, string email)
        {
            try
            {
                var response = new HttpResponseMessage();
                var record = new Record();

                record.Users = new User();
                record.Users.Matricula = matricula;
                record.Users.Nome = nome;
                record.Users.Email = email.ToLower();

                var root = new Root();
                root.Records = new List<Record>();
                root.Records.Add(record);

                var json = JsonConvert.SerializeObject(root);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                response = await _httpClient.PostAsync(PostUrl, stringContent);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var resultDeserialized = JsonConvert.DeserializeObject<Root>(content)?.Records;
                    var result = resultDeserialized.Select(u => u.Users);

                    var listData = result.ToList();

                    return listData;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Crashes.TrackError(ex);

            }
            return new List<User>();
        }

        public async Task<User> CheckUser(string email)
        {
            try
            {
                var response = await _httpClient.GetAsync(PostUrl);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var resultDeserialized = JsonConvert.DeserializeObject<Root>(content)?.Records;
                    var result = resultDeserialized?.Select(u => u.Users)?.ToList();
                    var listEmail = result?.ToList()?.Select(e => e.Email);
                    var resultEmail = result?.Find(e => e.Email == email);

                    var id = resultEmail.Matricula;

                    if (listEmail.Contains(email))
                        return resultEmail;

                    else
                        return new User();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Crashes.TrackError(ex);

            }
            return new User();
        }

        public async Task<bool> CheckRegistration(string registration)
        {
            try
            {
                var response = await _httpClient.GetAsync(PostUrl);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var resultDeserialized = JsonConvert.DeserializeObject<Root>(content)?.Records;
                    var result = resultDeserialized?.Select(u => u.Users)?.ToList();
                    var listId = result?.ToList()?.Select(e => e.Matricula);

                    if (!listId.Contains(registration))
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Crashes.TrackError(ex);
            }
            return false;
        }
    }
}

