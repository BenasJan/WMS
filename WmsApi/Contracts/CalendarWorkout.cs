namespace WmsApi.Contracts;

public class CalendarWorkout
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public WorkoutSummary Summary { get; set; }
}