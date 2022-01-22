using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Test.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace Api.Test.Categories
{
  [Trait("CategorySwagger", "Smoke")]
  public class CategoriesSwagerSelenium
  {
    
    [Fact]
    public void CategoriesSwaggerPage_Title()
    {
      using (IWebDriver driver = new ChromeDriver())
      {
        driver.Navigate().GoToUrl(SeleniumConfig.CardsSwaggerUrl);
        string pageTitle = driver.Title;

        Assert.Equal("Swagger UI", pageTitle);
      }
    }
    [Fact]
    public void CategoriesSwaggerPage_HasNoRedirects()
    {
      using (IWebDriver driver = new ChromeDriver())
      {
        driver.Navigate().GoToUrl(SeleniumConfig.CardsSwaggerUrl);

        Assert.Equal(SeleniumConfig.CardsSwaggerUrl, driver.Url);
      }
    }
    [Fact]
    
    public void CategoriesPage_TitleSame_AfterRefresh()
    {
      using (IWebDriver driver = new ChromeDriver())
      {
        driver.Navigate().GoToUrl(SeleniumConfig.CardsSwaggerUrl);
        string pageTitle = driver.Title;

        driver.Navigate().Refresh();
        
        Assert.Equal("Swagger UI", pageTitle);
      }
    }
  }
}
