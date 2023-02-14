using gameteq_task_test.DTOs;
using gameteq_task_test.POMs;
using NUnit.Framework;

namespace gameteq_task_test.Tests;

[Parallelizable(ParallelScope.Fixtures)]
[TestFixture]
public class OffersAddTest: CoreTest
{
    [Test]
    public void OfferAddNewWithoutSegments()
    {
        OffersPOM offersPom = new(driver);
        OfferAddPOM offerAddPom = new(driver);
        HeaderPOM headerPom = new(driver);
        RandomOffer randomOffer = new();
        
        headerPom.ToggleMenu();
        headerPom.leftMenuPom.OffersButtonClick();
        offersPom.AddButtonClick();
        offerAddPom.FillWithOfferDto(randomOffer);
        offerAddPom.SaveButtonClick();
        offersPom.EditButtonByNameClick(randomOffer.Name);

        Offer actualOffer = offerAddPom.GetOfferFromPage();
        
        Assert.IsTrue(randomOffer.Equals(actualOffer));
    }

    [Test]
    [Ignore("Bugged segments segment")]
    public void OfferAddNewWithSegments()
    {
        OffersPOM offersPom = new(driver);
        OfferAddPOM offerAddPom = new(driver);
        HeaderPOM headerPom = new(driver);
        RandomOffer randomOffer = new();
        RandomSegment randomSegment = new();
        
        headerPom.ToggleMenu();
        headerPom.leftMenuPom.OffersButtonClick();
        offersPom.AddButtonClick();
        offerAddPom.FillWithOfferDto(randomOffer);
        offerAddPom.FillWithSegmentDto(randomSegment, offerAddPom.segmentOfferPom);
        offerAddPom.SaveButtonClick();
        offersPom.EditButtonByNameClick(randomOffer.Name);
        
        Offer actualOffer = offerAddPom.GetOfferFromPage();
        Segment actualSegment = offerAddPom.GetSegmentFromPage();
        
        Assert.IsTrue(randomOffer.Equals(actualOffer));
        Assert.IsTrue(randomSegment.Equals(actualSegment));
    }
}