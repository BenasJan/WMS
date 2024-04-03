namespace WmsApi.Database.Models;

public class Exercise : BaseModel
{
    public string Name { get; set; }
    public int Sets { get; set; }
    public int Reps { get; set; }
    public TimeSpan Duration { get; set; }

    public Guid WorkoutId { get; set; }
    public Workout Workout { get; set; }
}