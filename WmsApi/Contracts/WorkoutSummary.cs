namespace WmsApi.Contracts;

public class WorkoutSummary
{
    public int TotalSets { get; set; }
    public int TotalReps { get; set; }
    public TimeSpan TotalDuration { get; set; }
}