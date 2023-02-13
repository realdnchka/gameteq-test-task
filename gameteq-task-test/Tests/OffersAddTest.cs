using gameteq_task_test.DTOs;
using gameteq_task_test.POMs;
using NUnit.Framework;

namespace gameteq_task_test.Tests;

[TestFixture]
public class OffersAddTest: CoreTest
{
    [Test]
    public void OfferAddNew()
    {
        OffersPOM offersPom = new(driver);
        OfferAddPOM offerAddPom = new(driver);
        HeaderPOM headerPom = new(driver);
        RandomOffer randomOffer = new();
        
        headerPom.ToggleMenu();
        headerPom.leftMenuPom.OffersButtonClick();
        offersPom.AddButtonClick();
        offerAddPom.FillWithOfferDto(randomOffer);
        offersPom.EditButtonByNameClick(randomOffer.Name);

        Offer actualOffer = offerAddPom.GetOfferFromPage();
        Assert.IsTrue(randomOffer.Equals(actualOffer));
    }
}