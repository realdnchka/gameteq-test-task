namespace gameteq_task_test.DTOs;

public class Segment
{
    public SegmentStatement Statement { get; set; }
    public List<string> Name { get; set; } = new();
    public List<Segment> Segments { get; set; } = new();

    public override bool Equals(object? obj)
    {
        if (obj == null || !(obj is Segment))
        {
            return false;
        }

        Segment other = (Segment) obj;

        return this.Statement == other.Statement
               && this.Name.SequenceEqual(other.Name)
               && this.Segments.SequenceEqual(other.Segments);
    }
}

public enum SegmentStatement
{
    Or,
    And,
}