namespace gameteq_task_test.DTOs;

public class Segment
{
    public SegmentStatement Statement { get; set; }
    public List<string> Name { get; set; } = new();
    public List<Segment> Segments { get; set; } = new();

    public bool Equals(Segment other)
    {
        return Statement == other.Statement && 
               Name.SequenceEqual(other.Name) && 
               Segments.SequenceEqual(other.Segments);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Segment)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine((int)Statement, Name, Segments);
    }
}

public enum SegmentStatement
{
    Or,
    And,
}