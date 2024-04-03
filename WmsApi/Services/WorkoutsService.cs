using WmsApi.Database;
using WmsApi.Database.Models;
using WmsApi.Database.Queries;
using WmsApi.Exceptions;

namespace WmsApi.Services;

public class WorkoutsService
{
    private readonly WmsDatabase _wmsDatabase;

    public WorkoutsService(WmsDatabase wmsDatabase)
    {
        _wmsDatabase = wmsDatabase;
    }

    public async Task<List<Workout>> Get(WorkoutsQuery query)
    {
        return await _wmsDatabase.WorkoutsRepository.GetWithExercises(query);
    }

    public async Task<Workout> Get(Guid workoutId)
    {
        var exercise =  await _wmsDatabase.WorkoutsRepository.GetWithExercises(workoutId);

        if (exercise is null)
        {
            throw new NotFoundException("Workout not found");
        }

        return exercise;
    }

    public async Task<Guid> Create(Workout workout)
    {
        var id = await _wmsDatabase.Create(workout);
        await _wmsDatabase.Save();

        return id;
    }

    public async Task AssignDate(Guid workoutId, DateTime date)
    {
        var workout = await _wmsDatabase.GetById<Workout>(workoutId);

        if (workout is null)
        {
            throw new NotFoundException("Workout not found");
        }

        workout.WorkoutDate = date;
        
        _wmsDatabase.Update(workout);
        await _wmsDatabase.Save();
    }

    public async Task Delete(Guid WorkoutId)
    {
        var workout = await _wmsDatabase.GetById<Workout>(WorkoutId);

        if (workout is null)
        {
            return;
        }
        
        _wmsDatabase.Delete(workout);
        await _wmsDatabase.Save();
    }
}