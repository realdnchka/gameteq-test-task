using aqaframework.DataObjects;

namespace gameteq_task_test.DTOs;

public class Offer
{
    public bool ForTest { get; set; } = true;
    public string Name { get; set; }
    public string Key { get; set; }
    public string Category { get; set; }
    public List<string> Networks { get; set; }
    public string Group { get; set; }
    public List<Segment> SegmentList { get; set; } = new();

    public override bool Equals(object obj)
    {
        if (obj == null || !(obj is Offer))
        {
            return false;
        }

        Offer other = (Offer) obj;

        var result = this.ForTest == other.ForTest
               && this.Name == other.Name
               && this.Key == other.Key
               && this.Category == other.Category
               && this.Networks.SequenceEqual(other.Networks)
               && this.Group == other.Group
               && this.SegmentList.SequenceEqual(other.SegmentList);
        return result;
    }
}