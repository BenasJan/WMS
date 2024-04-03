using Microsoft.EntityFrameworkCore;
using WmsApi.Database.Models;
using WmsApi.Database.Repositories;

namespace WmsApi.Database;

public class WmsDatabase
{
    private readonly ApplicationDbContext _dbContext;
    
    public WorkoutsRepository WorkoutsRepository { get; }

    public WmsDatabase(
        ApplicationDbContext dbContext,
        WorkoutsRepository workoutsRepository
    )
    {
        _dbContext = dbContext;
        WorkoutsRepository = workoutsRepository;
    }

    public async Task<TModel?> GetById<TModel>(Guid id) where TModel : BaseModel
    {
        return await _dbContext.Set<TModel>().FirstOrDefaultAsync(m => m.Id.Equals(id));
    } 

    public async Task<Guid> Create<TModel>(TModel model) where TModel : BaseModel
    {
        await _dbContext.AddAsync(model);

        return model.Id;
    }

    public void Update<TModel>(TModel model) where TModel : BaseModel
    {
        _dbContext.Update(model);
    }

    public void Delete<TModel>(TModel model) where TModel : BaseModel
    {
        _dbContext.Remove(model);
    }

    public async Task<int> Save()
    {
        var result = await _dbContext.SaveChangesAsync();
        
        return result;
    }
}