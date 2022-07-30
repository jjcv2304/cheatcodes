using System.Collections;
using System.Net.Http.Json;
using System.Text.Json;
using Api.Utils;
using CheatCodesUI2022.Shared;
using Microsoft.AspNetCore.Components;

namespace CheatCodesUI2022.Client.Services
{
    public interface ICategoriesDataService
    {
        Task<IEnumerable<CategoryVM>?> GetAllParents();
    }

    public class CategoriesDataService : ICategoriesDataService
    {
        private readonly HttpClient _httpClient;

        public CategoriesDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CategoryVM>?> GetAllParents()
        {
            try
            {
                var envelope = await JsonSerializer.DeserializeAsync<Envelope<List<CategoryVM>>>
                    (await _httpClient.GetStreamAsync($"api/Categories"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                if (!string.IsNullOrEmpty(envelope?.ErrorMessage))
                {
                    throw new ApplicationException("There was an error on the envelope");
                }

                return envelope?.Result;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
