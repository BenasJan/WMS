using Microsoft.EntityFrameworkCore;
using WmsApi.Database.Models;
using WmsApi.Database.Queries;

namespace WmsApi.Database.Repositories;

public class WorkoutsRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public WorkoutsRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public Task<Workout?> GetWithExercises(Guid workoutId)
    {
        return _applicationDbContext
            .Set<Workout>()
            .Include(w => w.Exercises)
            .FirstOrDefaultAsync(w => w.Id.Equals(workoutId));
    }

    public Task<List<Workout>> GetWithExercises(WorkoutsQuery query)
    {
        var minimizedDate = query.Date
            .AddHours(-query.Date.Hour)
            .AddMinutes(-query.Date.Minute)
            .AddSeconds(-query.Date.Second)
            .AddMilliseconds(-query.Date.Millisecond);
        
        var maximizedDate = minimizedDate
            .AddHours(23)
            .AddMinutes(59)
            .AddSeconds(59)
            .AddMilliseconds(999);

        return _applicationDbContext.Set<Workout>()
            .Include(w => w.Exercises)
            .Where(w => w.WorkoutDate != null && minimizedDate < w.WorkoutDate && w.WorkoutDate < maximizedDate)
            .ToListAsync();
    }
}