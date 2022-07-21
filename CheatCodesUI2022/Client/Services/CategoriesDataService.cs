using System.Text.Json;
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
        [Inject]
        public HttpClient HttpClient { get; set; }

        public CategoriesDataService(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public async Task<IEnumerable<CategoryVM>?> GetAllParents()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<CategoryVM>>
                (await HttpClient.GetStreamAsync($"api/Categories"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
