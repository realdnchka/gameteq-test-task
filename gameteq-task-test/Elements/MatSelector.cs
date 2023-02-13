using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace gameteq_task_test.Elements;

public class MatSelect : IWrapsElement
{
    private readonly IWebElement element;
    private IJavaScriptExecutor executor;
    private IWebElement overlay;
    public MatSelect(IWebElement element, IJavaScriptExecutor executor)
    {
        string str = element != null ? element.TagName : throw new ArgumentNullException(nameof (element), "element cannot be null");
        if (string.IsNullOrEmpty(str) || string.Compare(str, "mat-select", StringComparison.OrdinalIgnoreCase) != 0)
            throw new UnexpectedTagNameException("mat-select", str);
        this.element = element;
        string attribute = element.GetAttribute("aria-multiselectable");
        IsMultiple = attribute != null && attribute.ToLowerInvariant() != "false";
        this.executor = executor;
    }
    
    public bool IsMultiple { get; private set; }

    public void SelectByText(List<string> textList)
    {
        if (textList == null)
            throw new ArgumentNullException(nameof (textList), "text must not be null");
        element.Click();
        
        foreach (var text in textList)
        {
            bool flag = false;
            IList<IWebElement> webElementList =
                element.FindElements(By.XPath($"//mat-option/span[text()='{text}']"));

            foreach (IWebElement option in (IEnumerable<IWebElement>)webElementList)
            {
                SetSelected(option, true);
                flag = true;
            }
            executor.ExecuteScript("document.elementFromPoint(0,0).click()");
            if (!flag)
            {
                throw new NoSuchElementException("Cannot locate element with text: " + text);
            }
        }
    }

    public void SelectByText(string text)
    {
        if (text == null)
            throw new ArgumentNullException(nameof(text), "text must not be null");
        element.Click();
        bool flag = false;
        IList<IWebElement> webElementList =
            element.FindElements(By.XPath($"//mat-option/span[contains(text(), '{text}')]"));
        foreach (IWebElement option in (IEnumerable<IWebElement>)webElementList)
        {
            SetSelected(option, true);
            flag = true;
        }
        executor.ExecuteScript("document.elementFromPoint(0,0).click()");
        if (!flag)
        {
            throw new NoSuchElementException("Cannot locate element with text: " + text);
        }
    }

    private void SetSelected(IWebElement option, bool select)
    {
        if (select && !option.Enabled)
            throw new InvalidOperationException("You may not select a disabled option");
        bool selected = option.Selected;
        if (!(!selected & select) && (!selected || select))
            return;

        executor.ExecuteScript("arguments[0].scrollIntoView(true);", option);
        option.Click();
    }

    public IWebElement WrappedElement => element;
}