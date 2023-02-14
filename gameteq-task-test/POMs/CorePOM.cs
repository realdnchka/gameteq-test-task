using gameteq_task_test.Elements;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace gameteq_task_test.POMs
{
    public abstract class CorePOM
    {
        protected readonly IWebDriver Driver;
        protected readonly WebDriverWait Wait;
        protected readonly IJavaScriptExecutor Executor;
        public CorePOM(IWebDriver driver)
        {
            Driver = driver;
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30.0));
            Executor = (IJavaScriptExecutor)driver;
        }
        
        private void WaitForLoadJs()
        {
            Wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
        }
        protected void WaitForLoader()
        {
            WaitForLoadJs();
            By loader = By.XPath("//mat-spinner");
            Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(loader));
        }
        
        protected void WaitForElementExist(By selector)
        {
            try
            {
                Wait.Until(ExpectedConditions.ElementIsVisible(selector));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        protected void Click(By selector)
        {
            WaitForLoader();
            WaitForElementExist(selector);
            FindElement(selector).Click();
        }

        protected string GetText(By selector)
        {
            WaitForElementExist(selector);
            return FindElement(selector).Text;
        }

        protected void SetText(By selector, string text)
        {
            WaitForElementExist(selector);
            FindElement(selector).SendKeys(text);
        }

        protected void SelectOptionByText(By selector, string options)
        {
            WaitForElementExist(selector);
            SelectElement el = new SelectElement(FindElement(selector));
            el.SelectByText(options);
        }

        protected void SelectOptionByText(MatSelect element, string text)
        {
            WaitForLoader();
            element.SelectByText(text);
        }
        
        protected void SelectOptionByText(MatSelect element, List<string> list)
        {
            WaitForLoader();
            element.SelectByText(list);
        }

        protected IWebElement FindElement(By selector)
        {
            WaitForLoader();
            return Driver.FindElement(selector);
        }

        protected string GetTextThroughValue(By selector)
        {
            var el = FindElement(selector);
            return (string)Executor.ExecuteScript("return arguments[0].value", el);
        }

        protected bool IsSelected(By selector)
        {
            return FindElement(selector).Selected;
        }
        
        protected IList<IWebElement> FindElements(By selector)
        {
            WaitForElementExist(selector);
            return Driver.FindElements(selector);
        }
    }
}