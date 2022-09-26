namespace EmptyProject.Models.Repositories
{
    public interface IAnimleReposiroty<TEntity>
    {

        TEntity GetById(int id);
    }
}
