using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Api.Test.Framework
{
  public sealed class ChromeDriverWithLoginFixture : IDisposable
  {
    public IWebDriver Driver { get; private set; }

    public ChromeDriverWithLoginFixture()
    {
      Driver = new ChromeDriver();
      Login();
    }

    private void Login()
    {

      Driver.Navigate().GoToUrl(SeleniumConfig.LoginUrl);
      Driver.FindElement(By.Id("Username")).SendKeys("h@h.com");
      Driver.FindElement(By.Id("Password")).SendKeys("Test123$");
      Driver.FindElement(By.ClassName("btn-primary")).Click();
      Driver.Navigate().GoToUrl(SeleniumConfig.CardsUrl);//to force the first redirect
      WebDriverWait wait = new WebDriverWait(Driver, timeout: TimeSpan.FromSeconds(11));
       wait.Until(ExpectedConditions.ElementIsVisible(By.TagName("app-categories-list")));
    }

    public void Dispose()
    {
      Driver.Dispose();
    }
  }

}
