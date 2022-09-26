using System.Collections.Generic;

namespace EmptyProject.Models.Repositories
{
    public interface ICompanyRepository<TEntity>
    {
        TEntity get(int id);

        TEntity Add(TEntity entity);

        IEnumerable<TEntity> GetEntities();

        TEntity Update(TEntity entityChanged);

        TEntity Delete(int id);
    }
}
