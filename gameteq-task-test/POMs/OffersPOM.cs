using OpenQA.Selenium;

namespace gameteq_task_test.POMs;

public class OffersPOM: CorePOM
{
    public OffersPOM(IWebDriver driver) : base(driver) { }

    #region Selectors
    
    private By addButton = By.XPath("//button[@routerlink='../add']");
    
    private By editButtonById(int id) =>
        By.XPath($"//tbody/tr/td[1][text()='{id}']/parent::tr//span[contains(text(), 'Edit')]");
    
    private By deleteButtonById(int id) => 
        By.XPath($"//tbody/tr/td[1][text()='{id}']/parent::tr//span[contains(text(), 'Delete')]");
    
    private By editButtonByName(string name) => 
        By.XPath($"//tbody/tr/td[2][text()='{name}']/parent::tr//span[contains(text(), 'Edit')]");

    private By deleteButtonByName(string name) =>
        By.XPath($"//tbody/tr/td[2][text()='{name}']/parent::tr//span[contains(text(), 'Delete')]");

    #endregion

    #region Methods

    public void AddButtonClick()
    {
        Click(addButton);
        WaitForLoader();
    }

    public void EditButtonByNameClick(string name)
    {
        Click(editButtonByName(name));
        WaitForLoader();
    }

    #endregion
}