using ParkingApplication.DAL.Entities;

namespace ParkingApplication.DAL.Repositories.Interfaces;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> AddAsync(TEntity entity);
    Task DeleteAsync(int id);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<TEntity> GetByIdAsync(int id);
    IQueryable<TEntity> GetAll();
}