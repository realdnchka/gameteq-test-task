using gameteq_task_test.DTOs;
using gameteq_task_test.Elements;
using OpenQA.Selenium;

namespace gameteq_task_test.POMs;

public class SegmentOfferPOM: CorePOM
{
    private readonly string _rootXPath;
    public int NumberOfCreatedGroups;
    public SegmentOfferPOM(IWebDriver driver, string rootXPath) : base(driver)
    {
        _rootXPath = rootXPath;
    }

    #region Selectors
    
    //in future is possible to make MatRadioButton IWrapsElement
    private By orButton(string rootXPath, int index = 1) => By.XPath($"({rootXPath}//div[@class='mat-radio-label-content' and contains(text(), 'Or')])[{index}]");
    private By andButton(string rootXPath, int index = 1) => By.XPath($"({rootXPath}//div[@class='mat-radio-label-content' and contains(text(), 'And')])[{index}]");
    private By orButtonHiden(string rootXPath, int index = 1) => By.XPath($"({rootXPath}//div[contains(text(), 'Or')]/preceding-sibling::div//input)[{index}]");
    private By andButtonHiden(string rootXPath, int index = 1) => By.XPath($"({rootXPath}//div[contains(text(), 'And')]/preceding-sibling::div//input)[{index}]");
    
    private By addGroupButton(string rootXPath) => By.XPath($"{rootXPath}//button/span[contains(text(), 'Add group')]");
    private By addSegmentButton(string rootXPath) => By.XPath($"{rootXPath}//button/span[contains(text(), 'Add segment')]");

    private By nameSegmentSelect(string rootXPath, int index = 1) => By.XPath($"({rootXPath}//mat-select[@name='val'])[{index}]");
    private By deleteNameButton(string rootXPath) => By.XPath($"{rootXPath}//mat-select[@name='val']//ancestor::mat-form-field//following-sibling::button");
    private By allNameSegmentSelect(string rootXPath) => By.XPath($"{rootXPath}//mat-select[@name='val']");
    
    #endregion

    #region Methods

    public SegmentOfferPOM AddGroupButtonClick()
    {
        Click(addGroupButton(_rootXPath));
        NumberOfCreatedGroups++;
        var sop = new SegmentOfferPOM(Driver, $"{_rootXPath}//app-form-segments[{NumberOfCreatedGroups}]");
        return sop;
    }

    public void AddSegmentButton()
    {
        Click(addSegmentButton(_rootXPath));
    }

    public void OrButtonClick()
    {
        Click(orButton(_rootXPath));
    }

    public void AndButtonClick()
    {
        Click(andButton(_rootXPath));
    }
    
    public void NameSegmentSelectByText(string text, int index)
    {
        SelectOptionByText(new MatSelect(FindElement(nameSegmentSelect(_rootXPath, index)), Executor), text);
    }

    public List<string> GetNameSegmentSelect(int index = 1)
    {
        var list = new List<string>();
        var elements = FindElements(allNameSegmentSelect(_rootXPath));
        foreach (var el in elements)
        {
            list.Add(el.Text);
        }

        return list;
    }

    public SegmentStatement GetStatement(int index = 1)
    {
        return IsSelected(andButtonHiden(_rootXPath, index)) ? SegmentStatement.And : SegmentStatement.Or;
    }
    
    #endregion
}