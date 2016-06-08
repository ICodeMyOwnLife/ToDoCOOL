using System.Collections.ObjectModel;
using CB.Model.Prism;
using TodoModels;


namespace TodoViewModels
{
    public class TodoViewModel: PrismViewModelBase
    {
        #region Fields
        private ObservableCollection<Todo> _todos;
        #endregion


        #region  Properties & Indexers
        public virtual ObservableCollection<Todo> Todos
        {
            get { return _todos; }
            protected set { SetProperty(ref _todos, value); }
        }
        #endregion
    }
}