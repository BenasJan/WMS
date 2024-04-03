namespace WmsApi.Contracts;

public class Workout
{
    public string Title { get; set; }
    public string? Description { get; set; }

    public List<Exercise> Exercises { get; set; }
}