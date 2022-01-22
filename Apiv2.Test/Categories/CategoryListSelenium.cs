using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Test.Framework;
using Api.Test.Framework.PageObjectModels;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Api.Test.Categories
{
  [Trait("CategoryList", "General")]
  public class CategoryListSelenium: IClassFixture<ChromeDriverWithLoginFixture>
  {
    private readonly ChromeDriverWithLoginFixture ChromeDriverFixture;
    public CategoryListSelenium(ChromeDriverWithLoginFixture chromeDriverFixture)
    {
      ChromeDriverFixture = chromeDriverFixture;
    }

    [Fact]
    public void LoadCategoriesPage()
    {
      var categoryListPage = new CategoryListPage(ChromeDriverFixture.Driver);
      categoryListPage.NavigateTo();
      categoryListPage.EnsurePageLoaded();

    }
    [Fact]
    public void InitialBreadCrumbsText()
    {
      var categoryListPage = new CategoryListPage(ChromeDriverFixture.Driver);
      categoryListPage.NavigateTo();
      var breadCrumbText = categoryListPage.BreadCrumbsText();

      Assert.Equal("...", breadCrumbText);
    }
    [Fact]
    public void BreadCrumbsTextUpdated_AfterCardSelected()
    {
      var categoryListPage = new CategoryListPage(ChromeDriverFixture.Driver);
      categoryListPage.NavigateTo();
      var randomCard = categoryListPage.GetRandomCard();
      var randomCardTitle = categoryListPage.GetRandomCardTitle(randomCard);

      categoryListPage.NavigateTo(randomCard);
      var breadCrumbText = categoryListPage.BreadCrumbsText();

      Assert.Equal(randomCardTitle + " >", breadCrumbText);
    }
  }
}
