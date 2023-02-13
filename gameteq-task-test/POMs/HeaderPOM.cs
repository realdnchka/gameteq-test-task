using gameteq_task_test.Elements;
using OpenQA.Selenium;

namespace gameteq_task_test.POMs;

public class HeaderPOM: CorePOM
{
    private bool _menuSelected;
    public LeftMenuPOM leftMenuPom;
    
    public HeaderPOM(IWebDriver driver) : base(driver)
    {
        _menuSelected = false;
        leftMenuPom = new(driver);
    }

    #region Selectors

    private By menuSwitch = By.XPath("//div[@class='mat-slide-toggle-bar']");

    #endregion

    #region Methods

    public void MenuSwitchClick()
    {
        Click(menuSwitch);
    }

    public bool MenuSwitchSelected()
    {
        NewSwitch sw = new NewSwitch(Driver.FindElement(menuSwitch));
        return sw.Selected;
    }

    public void ToggleMenu()
    {
        if (!MenuSwitchSelected())
        {
            MenuSwitchClick();
        }
    }



    #endregion

}