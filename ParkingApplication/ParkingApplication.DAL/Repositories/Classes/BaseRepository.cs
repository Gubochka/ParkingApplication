using Microsoft.EntityFrameworkCore;
using ParkingApplication.DAL.Context;
using ParkingApplication.DAL.Entities;
using ParkingApplication.DAL.Repositories.Interfaces;

namespace ParkingApplication.DAL.Repositories.Classes;

public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly DataBaseContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public BaseRepository(DataBaseContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        var result = await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task DeleteAsync(int id)
    {
        var item = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        _context.Remove(item);
        await _context.SaveChangesAsync();
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        return result;
    }
         
    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var result = _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public IQueryable<TEntity> GetAll()
    {
        return _dbSet;
    }
}