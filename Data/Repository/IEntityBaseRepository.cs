using System;
using System.Threading.Tasks;

namespace TontonatorDesktopApp.Data.Repository
{
    public interface IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        T Add(T entity);
        void Delete(string id);
        T Update(T entity);
        T Read(string field, string query);
    }
}