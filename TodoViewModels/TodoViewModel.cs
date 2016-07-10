using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CB.Model.Common;
using CB.Model.Prism;
using Prism.Commands;
using TodoDataServices;
using TodoModels;


namespace TodoViewModels
{
    public class TodoViewModel: PrismViewModelBase, IInitialize, ITerminate
    {
        #region Fields
        private readonly DelegateCommand _pullAsynCommand;
        private readonly DelegateCommand _pushAsynCommand;
        private readonly ITodoDataService _service = new TodoWebService("todosUrl");
        private PrismModelCollectionBase<Todo, ObservableCollection<Todo>> _todosCollection;
        #endregion


        #region  Constructors & Destructor
        public TodoViewModel()
        {
            _pullAsynCommand = DelegateCommand.FromAsyncHandler(PullAsync);
            _pushAsynCommand = DelegateCommand.FromAsyncHandler(PushAsync);
        }
        #endregion


        #region  Properties & Indexers
        public ICommand PullAsynCommand => _pullAsynCommand;
        public ICommand PushAsynCommand => _pushAsynCommand;

        public PrismModelCollectionBase<Todo, ObservableCollection<Todo>> TodosCollection
        {
            get { return _todosCollection; }
            private set { SetProperty(ref _todosCollection, value); }
        }
        #endregion


        #region Methods
        public void Initialize()
            => PullAsync();

        public async Task InitializeAsync()
            => await PullAsync();

        public async Task PullAsync()
        {
            ActiveServiceCommands(false);
            InitializeCollection(await _service.PullAsync());
            ActiveServiceCommands(true);
        }

        private void ActiveServiceCommands(bool isActive)
            => _pullAsynCommand.IsActive = _pushAsynCommand.IsActive = isActive;

        public async Task PushAsync()
        {
            ActiveServiceCommands(false);
            if (TodosCollection != null)
            {
                InitializeCollection(await _service.PushAsync(TodosCollection.Collection.ToArray()));
            }
            ActiveServiceCommands(true);
        }

        public void Terminate()
            => PushAsync();

        public async Task TerminateAsync()
            => await PushAsync();
        #endregion


        #region Implementation
        protected void InitializeCollection(IEnumerable<Todo> todos)
            => TodosCollection =
               new PrismModelCollectionBase<Todo, ObservableCollection<Todo>>(
                   new ObservableCollection<Todo>(todos));
        #endregion
    }
}


// TODO: Edit View, Readonly DataGrid
// TODO: WarningLevel update after Deadline
// TODO: Warning foreground
// TODO: Use PHP Web Service