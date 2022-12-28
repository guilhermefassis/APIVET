using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using VetApi.Models;
using VetApi.Services.Interfaces;

namespace VetApi.Services
{
    public class TheDogAPI : ITheDogAPI
    {
        private readonly IConfiguration _configuration;
        private HttpClient client;
        
        public TheDogAPI(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<Breed>> GetAll(int page, int limit)
        {
            string url = _configuration["DogInfo:BaseAdress"];
            Uri uri = new Uri($"{url}/breeds?limit={limit}&page={page}");
            try
            {
                return await RequestAPI(uri);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
         public async Task<IEnumerable<Breed>> GetByName(string Breed)
        {
            string url = _configuration["DogInfo:BaseAdress"];
            Uri uri = new Uri($"{url}/breeds/search?q={Breed}");

            try
            {
                return await RequestAPI(uri);
            }
            catch(Exception)
            {
                throw new Exception("Raça não encontrada, ou nao existe no nosso banco de dados.");
            }
        }

        public async Task<IEnumerable<Breed>> GetAll()
        {
            string url = _configuration["DogInfo:BaseAdress"];
            Uri uri = new Uri($"{url}/breeds?attach_breed=0");
            try
            {
                return await RequestAPI(uri);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<IEnumerable<Breed>> RequestAPI(Uri uri)
        {
                using (client = new HttpClient())
                {
                    var response = await client.GetAsync(uri);
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Breed[]>(content);

                    return result;
                }
        }
    }
}