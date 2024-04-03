namespace WmsApi.Database.Models;

public class Workout : BaseModel
{
    public string Title { get; set; }
    public string? Description { get; set; }

    public DateTime? WorkoutDate { get; set; }

    public List<Exercise> Exercises { get; set; }
}