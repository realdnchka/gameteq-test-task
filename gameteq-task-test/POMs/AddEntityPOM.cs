using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace gameteq_task_test.POMs;

public class AddEntityPOM: CorePOM
{
    private static string rootXPath = "//mat-dialog-container";
    
    public AddEntityPOM(IWebDriver driver) : base(driver) { }

    #region Selectors

    private By nameInput = By.XPath($"{rootXPath}//input"); 
    private By cancelButton = By.XPath($"{rootXPath}//span[text()='Cancel']");
    private By createButton = By.XPath($"{rootXPath}//span[text()=' Create ']");

    #endregion

    public void NameInputSendKeys(string text)
    {
        SetText(nameInput, text);
    }

    public void CancelButtonCick()
    {
        Click(cancelButton);
    }

    public void CreateButtonClick()
    {
        Click(createButton);
    }

    public void FillForm(string text)
    {
        NameInputSendKeys(text);
        CreateButtonClick();
        Wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath(rootXPath)));
    }
}