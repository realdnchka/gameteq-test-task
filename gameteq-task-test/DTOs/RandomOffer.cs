using gameteq_task_test.Helpers;

namespace gameteq_task_test.DTOs;

public class RandomOffer: Offer
{
    public RandomOffer()
    {
        RandomString rs = new RandomString(5);
        
        this.Name = $"AT-{DateTime.Now}-Name-{rs.Generate()}";
        this.Key = $"AT-{DateTime.Now}-Key-{rs.Generate()}";
        this.Category = $"AT-Category-{rs.Generate()}";
        this.Group = $"AT-Group-{rs.Generate()}";
        this.Networks = new List<string>();
        
        int numOfNet = new Random().Next(2, 4);
        for (int i = 0; i < numOfNet; i++)
        {
            Networks.Add($"At-network-{rs.Generate()}");
        }
    }
}