using gameteq_task_test.Helpers;

namespace gameteq_task_test.DTOs;

public class RandomSegment : Segment
{
    private bool _last;
    public RandomSegment(bool last = false)
    {
        _last = last;
        RandomString rs = new RandomString(5);
        for (int i = 0; i < new Random().Next(1, 3); i++)
        {
            this.Name.Add($"At-{rs.Generate()}");
        }

        this.Statement = (SegmentStatement)new Random().Next(0, 2);

        if (!last)
        {
            this.Segments.Add(new RandomSegment(true));
            this.Segments.Add(new RandomSegment(true));
        }
        //infinity recursive function
        // for (int i = 0; i < new Random().Next(0, 3); i++)
        // {
        //     this.Segments.Add(new RandomSegment());
        // }
    }
}