namespace WmsApi.Contracts;

public class CalendarWorkoutBulk
{
    public DateTime Date { get; set; }
    public List<CalendarWorkout> Workouts { get; set; }
}