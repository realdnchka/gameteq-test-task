namespace gameteq_task_test.DTOs;

public class Offer
{
    public bool ForTest { get; set; } = true;
    public string Name { get; set; }
    public string Key { get; set; }
    public string Category { get; set; }
    public List<string> Networks { get; set; }
    public string Group { get; set; }
    public Segment Segment { get; set; } = new();

    public bool Equals(Offer other)
    {
        return ForTest == other.ForTest && 
               Name == other.Name && 
               Key == other.Key && 
               Category == other.Category && 
               Networks.SequenceEqual(other.Networks) && 
               Group == other.Group && 
               Segment.Equals(other.Segment);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Offer)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(ForTest, Name, Key, Category, Networks, Group, Segment);
    }
}