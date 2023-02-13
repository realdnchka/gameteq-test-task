namespace aqaframework.DataObjects;

public class Segment
{
    public SegmentStatement Statement { get; set; }
    public string Name { get; set; }
}

public enum SegmentStatement
{
    Or,
    And,
}