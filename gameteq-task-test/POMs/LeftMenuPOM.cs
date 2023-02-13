using OpenQA.Selenium;

namespace gameteq_task_test.POMs;

public class LeftMenuPOM: CorePOM
{
    public LeftMenuPOM(IWebDriver driver) : base(driver) { }

    #region Selectors

    private By offersButton = By.XPath("//button[@routerlink='/list']");

    #endregion

    #region Methods

    public void OffersButtonClick()
    {
        Click(offersButton);
    }

    #endregion
}