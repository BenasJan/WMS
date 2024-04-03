using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WmsApi.Contracts;
using WmsApi.Services;
using WorkoutsQuery = WmsApi.Database.Queries.WorkoutsQuery;

namespace WmsApi.Controllers;

public static class WorkoutsController
{
    public static WebApplication MapWorkouts(this WebApplication app)
    {
        var group = app.MapGroup("api/workouts");

        group.MapGet("",
            async ([FromQuery] DateTime date, [FromServices] IMapper mapper,
                [FromServices] WorkoutsService workoutsService) =>
            {
                // var workoutQuery = mapper.Map<Database.Queries.WorkoutsQuery>(query);
                var workouts = await workoutsService.Get(new WorkoutsQuery{ Date = date });

                var calendarWorkouts = new CalendarWorkoutBulk
                {
                    Date = date,
                    Workouts = mapper.Map<List<CalendarWorkout>>(workouts)
                };

                return Results.Ok(calendarWorkouts);
            });

        group.MapGet("{id:guid}",
            async ([FromRoute] Guid id, [FromServices] IMapper mapper,
                [FromServices] WorkoutsService workoutsService) =>
            {
                var workout = await workoutsService.Get(id);
                var summary = mapper.Map<WorkoutSummary>(workout);

                return Results.Ok(summary);
            });

        group.MapPost("",
            async ([FromBody] Workout workout, [FromServices] IMapper mapper,
                [FromServices] WorkoutsService workoutsService) =>
            {
                var workoutModel = mapper.Map<Database.Models.Workout>(workout);

                var workoutId = await workoutsService.Create(workoutModel);

                return Results.Created($"{workoutId}", new { id = workoutId });
            });

        group.MapPatch("{workoutId:guid}",
            async ([FromRoute] Guid workoutId, [FromBody] WorkoutDate workoutDate,
                [FromServices] WorkoutsService workoutsService) =>
            {
                await workoutsService.AssignDate(workoutId, workoutDate.Date);

                return Results.Ok();
            });

        group.MapDelete("{workoutId:guid}",
            async ([FromRoute] Guid workoutId, [FromServices] WorkoutsService workoutsService) =>
            {
                await workoutsService.Delete(workoutId);

                return Results.NoContent();
            });

        group.WithOpenApi();

        return app;
    }
}