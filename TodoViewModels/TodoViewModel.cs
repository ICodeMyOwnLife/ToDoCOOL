using System.Collections.ObjectModel;
using System.Windows.Data;
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
            protected set
            {
                if (SetProperty(ref _todos, value))
                {
                    TodosView = new ListCollectionView(value);
                }
            }
        }

        public ListCollectionView TodosView { get; protected set; }
        #endregion
    }
}