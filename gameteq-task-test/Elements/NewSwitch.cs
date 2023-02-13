using OpenQA.Selenium;

namespace gameteq_task_test.Elements;

public class NewSwitch: IWrapsElement
{
    private IWebElement element;
    private IWebElement hidenCheckbox;
    
    public NewSwitch(IWebElement element)
    {
        this.element = element;
        hidenCheckbox = element.FindElement(By.XPath("//input"));
    }

    public bool Selected => hidenCheckbox.Selected; 
    
    public IWebElement WrappedElement => element;
    
}