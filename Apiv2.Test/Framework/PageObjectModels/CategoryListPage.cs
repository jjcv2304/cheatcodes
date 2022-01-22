using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Api.Test.Framework.PageObjectModels
{
  class CategoryListPage : Page
  {
    public CategoryListPage(IWebDriver driver)
    {
      Driver = driver;
    }
    protected override string PageUrl => SeleniumConfig.CardsUrl;
    protected override string PageTitle => "CheatCodes.UI";


    public string BreadCrumbsText() => Driver.FindElement(By.CssSelector("app-categories-bread-crumbs > div > p")).Text;

    public IWebElement GetRandomCard()
    {
      var allCards = Driver.FindElements(By.TagName("app-category-card"));

      var index = GetRandom.Int32(0, allCards.Count - 1);

      return allCards[index];
    }
    public void NavigateTo(IWebElement randomCard)
    {
      var randomCardTitle = GetRandomCardTitle(randomCard);
      randomCard.FindElement(By.ClassName("fa-angle-double-down")).Click();
      WebDriverWait wait = new WebDriverWait(Driver, timeout: TimeSpan.FromSeconds(10));
      //wait.Until(ExpectedConditions.ElementIsVisible(By.TagName("app-categories-list")));
      wait.Until(ExpectedConditions.TextToBePresentInElement(Driver.FindElement(By.CssSelector("app-categories-bread-crumbs > div > p")), randomCardTitle));
    }

    internal string GetRandomCardTitle(IWebElement randomCard) => randomCard.FindElement(By.ClassName("card__header")).GetAttribute("value");
  }

}
