using CheatCodesUI2022.Client.Services;
using CheatCodesUI2022.Shared;
using Microsoft.AspNetCore.Components;

namespace CheatCodesUI2022.Client.Pages
{
    public class CategoriesBase: ComponentBase
    {
        //[Inject]
        //ICategoriesDataService CategoriesDataService { get; set; }

        public IEnumerable<CategoryVM>? Categories { get; set; }

        protected override async Task OnInitializedAsync()
        {
            //string parentId = "266";
            //categories = await Http.GetFromJsonAsync<CategoryVM[]>($"api/Categories/GetChildsOf/{parentId}");
            //Categories = await CategoriesDataService.GetAllParents();
            var cat = new CategoryVM() { Name="Test", Description="desc" };
            var cat2 = new CategoryVM() { Name="Test2", Description="desc2" };
            Categories = new List<CategoryVM> {cat, cat};
        }
    }
}
