using System.Configuration;
using System.Threading.Tasks;
using CB.Web.WebServices;
using TodoModels;


namespace TodoDataServices
{
    public class TodoWebService: ITodoDataService
    {
        #region Fields
        private readonly string _urlSetting;
        #endregion


        #region  Constructors & Destructor
        public TodoWebService(string urlSetting)
        {
            _urlSetting = urlSetting;
        }

        public TodoWebService(): this("todosUrl") { }
        #endregion


        #region Methods
        public async Task<Todo[]> PullAsync()
            => GetTodos(await Http.GetAsync<Todo[]>(GetUrl()));

        public async Task<Todo[]> PushAsync(Todo[] todos)
            => GetTodos(await Http.PutAsync<Todo[], Todo[]>(GetUrl(), todos));
        #endregion


        #region Implementation
        private static Todo[] GetTodos(HttpResult<Todo[]> result)
            => !string.IsNullOrEmpty(result.Error) ? new Todo[0] : result.Value;

        private string GetUrl()
            => ConfigurationManager.AppSettings[_urlSetting];
        #endregion
    }
}