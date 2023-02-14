using gameteq_task_test.DTOs;
using gameteq_task_test.Elements;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace gameteq_task_test.POMs;

public class OfferAddPOM : CorePOM
{
    private AddEntityPOM addEntityPom;
    public SegmentOfferPOM segmentOfferPom;
    private List<SegmentOfferPOM> _segments = new();
    public OfferAddPOM(IWebDriver driver) : base(driver)
    {
        addEntityPom = new(driver);
        segmentOfferPom = new(driver, "//app-form-segments");
        _segments.Add(segmentOfferPom);
    }

    #region Selectors

    private By nameInput = By.XPath("//input[@name='name']");
    private By keyInput = By.XPath("//input[@name='key']");

    private By categorySelect = By.XPath("//select[@name='category']");
    private By categoryAddButton = By.XPath("//select[@name='category']/parent::div/following-sibling::div/button");
    private By networksSelect = By.XPath("//mat-select[@name='networks']");
    private By networkAddButton = By.XPath("//mat-select[@name='networks']/parent::div/following-sibling::div/button");
    private By groupSelect = By.XPath("//mat-select[@name='group']");
    private By groupAddButton = By.XPath("//mat-select[@name='group']/parent::div/following-sibling::div/button");

    private By saveButton = By.XPath("//button/span[contains(text(), 'Save')]");
    
    //Segments implementation
    private By addSegmentButton = By.XPath("//mat-card-title[contains(text(), 'Segments')]//span[contains(text(), 'Add')]");
    #endregion

    #region Methods

    public void NameInputSendKeys(string text)
    {
        SetText(nameInput, text);
    }

    public void KeyInputSendKeys(string text)
    {
        SetText(keyInput, text);
    }

    public void CategorySelectByText(string text)
    {
        new SelectElement(Driver.FindElement(categorySelect)).SelectByText(text);
    }

    public void NetworkSelectByText(List<string> list)
    {
        SelectOptionByText(new MatSelect(FindElement(networksSelect), Executor), list);
    }

    public void NetworkSelectByText(string text)
    {
        SelectOptionByText(new MatSelect(FindElement(networksSelect), Executor), text);
    }

    public void GroupSelectByText(string text)
    {
        SelectOptionByText(new MatSelect(FindElement(groupSelect), Executor), text);
    }

    public void CategoryAddButtonClick()
    {
        Click(categoryAddButton);
    }

    public void NetworkAddButtonClick()
    {
        Click(networkAddButton);
    }

    public void GroupAddButtonClick()
    {
        Click(groupAddButton);
    }

    public void SaveButtonClick()
    {
        Click(saveButton);
    }

    public void AddSegmentButtonClick()
    {
        Click(addSegmentButton);
    }

    public string NameInputGetText()
    {
        return GetTextThroughValue(nameInput);
    }

    public string KeyInputGetText()
    {
        return GetTextThroughValue(keyInput);
    }

    public string CategorySelectGetText()
    {
        var value = Driver.FindElement(categorySelect).GetAttribute("value");
        By valueXPath = By.XPath($"//select/option[@value={value}]");
        return GetText(valueXPath).Trim();
    }
    public string NetworkSelectGetText()
    {
        return GetText(networksSelect);
    }

    public string GroupSelectGetText()
    {
        return GetText(groupSelect);
    }

    public void AddSegmentGroup(SegmentOfferPOM segment)
    {
        _segments.Add(segment.AddGroupButtonClick());
    }

    public void FillWithSegmentDto(Segment segment, SegmentOfferPOM segmentPom)
    {
        int indexOfSegmentName = 0;
        switch (segment.Statement)
        {
            case SegmentStatement.Or: 
                segmentPom.OrButtonClick();
                break;
            default:
                segmentPom.AndButtonClick();
                break;
        }

        foreach (var name in segment.Name)
        {
            indexOfSegmentName++;
            segmentPom.AddSegmentButton();
            try
            {
                segmentPom.NameSegmentSelectByText(name, indexOfSegmentName);
            }
            catch (NoSuchElementException)
            {
                AddSegmentButtonClick();
                addEntityPom.FillForm(name);
                segmentPom.NameSegmentSelectByText(name, indexOfSegmentName);
            }
        }

        foreach (var seg in segment.Segments)
        {
            var newSegmentPom = segmentPom.AddGroupButtonClick();
            _segments.Add(newSegmentPom);
            FillWithSegmentDto(seg, newSegmentPom);
        }
    }

    public void FillWithOfferDto(Offer offer)
    {
        NameInputSendKeys(offer.Name);
        KeyInputSendKeys(offer.Key);
        try
        {
            CategorySelectByText(offer.Category);
        }
        catch (NoSuchElementException)
        {
            CategoryAddButtonClick();
            addEntityPom.FillForm(offer.Category);
            CategorySelectByText(offer.Category);
        }


        foreach (var net in offer.Networks)
        {
            try
            {
                NetworkSelectByText(net);
            }
            catch (NoSuchElementException)
            {
                NetworkAddButtonClick();
                addEntityPom.FillForm(net);
                NetworkSelectByText(net);
            }
        }
        
        try
        {
            GroupSelectByText(offer.Group);
        }
        catch (NoSuchElementException)
        {
            GroupAddButtonClick();
            addEntityPom.FillForm(offer.Group);
            GroupSelectByText(offer.Group);
        }
    }

    public Segment GetSegmentFromPage()
    {
        var segment = new Segment();
        for(int i = 0; i < _segments.Count; i++)
        {
            segment.Name = _segments[i].GetNameSegmentSelect();
            segment.Statement = _segments[i].GetStatement();
            
            if (_segments[i].NumberOfCreatedGroups == 0)
            {
                continue;
            }

            for (int j = 1; j <= _segments[i].NumberOfCreatedGroups; j++)
            {
                Segment newseg = new();
                newseg.Name = _segments[i + j].GetNameSegmentSelect();
                foreach (var name in newseg.Name)
                {
                    segment.Name.RemoveAt(segment.Name.Count - 1);
                }
                newseg.Statement = _segments[i + j].GetStatement();
                segment.Segments.Add(newseg);
            }
        }

        return segment;
    }
    
    public Offer GetOfferFromPage()
    {
        Offer offer = new();
        offer.Name = NameInputGetText();
        offer.Category = CategorySelectGetText();
        offer.Networks = NetworkSelectGetText().Split(", ").ToList();
        offer.Group = GroupSelectGetText();
        offer.Key = KeyInputGetText();
        return offer;
    }

    #endregion
}