using System.Threading.Tasks;
using TodoModels;


namespace TodoDataServices
{
    public interface ITodoDataService
    {
        #region Abstract
        Task<Todo[]> PullAsync();
        Task<Todo[]> PushAsync(Todo[] todos);
        #endregion
    }
}