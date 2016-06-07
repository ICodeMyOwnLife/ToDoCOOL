using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Xml.Serialization;
using Todos;


namespace MainForm
{
    public partial class MainWindow
    {
        #region Fields
        private ICollection<Todo> _todoList;
        #endregion


        #region  Constructors & Destructor
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion


        #region Override
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            SerializeTodoList();
        }
        #endregion


        #region Event Handlers
        private void CmdSort_OnClick(object sender, RoutedEventArgs e)
        {
            SortTodoList();
        }

        private void DatMain_OnCommand(object sender, RoutedEventArgs e)
        {
            var cmd = e.OriginalSource as Hyperlink;
            if (cmd == null) return;

            datMain.CommitEdit(DataGridEditingUnit.Row, true);
            var tag = cmd.Tag as string;
            switch (tag)
            {
                case "Delete":
                    var todo = cmd.DataContext as Todo;
                    DeleteTodoItem(todo);
                    break;
                case "Setting":
                    MessageBox.Show("Setting :)");
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var todos = DeserializeTodoList();
            _todoList = new ObservableCollection<Todo>(todos);
            datMain.DataContext = _todoList;
            SortTodoList();
        }
        #endregion


        #region Implementation
        private static XmlSerializer CreateSerializer()
        {
            return new XmlSerializer(typeof(Todo[]));
        }

        private void DeleteTodoItem(Todo todo)
        {
            if (_todoList.Contains(todo) && IsSureToDeleteTodoItem())
            {
                _todoList.Remove(todo);
            }
        }

        private static IEnumerable<Todo> DeserializeTodoList()
        {
            var filePath = GetFilePath();
            if (!File.Exists(filePath)) return new Todo[0];

            using (var reader = new StreamReader(filePath))
            {
                var ser = CreateSerializer();
                return ser.Deserialize(reader) as Todo[];
            }
        }

        private static string GetFilePath()
        {
            return ConfigurationManager.AppSettings["file"];
        }

        private static bool IsSureToDeleteTodoItem()
        {
            return MessageBox.Show("Are you sure you want to delete this Todo item?", "Delete todo item",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes;
        }

        private void SerializeTodoList()
        {
            using (var writer = new StreamWriter(GetFilePath()))
            {
                var ser = CreateSerializer();
                ser.Serialize(writer, _todoList.ToArray());
            }
        }

        private void SortTodoList()
        {
            var itemsView = CollectionViewSource.GetDefaultView(_todoList) as ListCollectionView;
            if (itemsView != null)
            {
                itemsView.CustomSort = new TodoComparer();
            }
        }
        #endregion
    }
}


//TODO: Start with Windows
//TODO: Alert & Reroll Strategy: Implement
//TODO: Style Button, CheckBox
//TODO: Notification & NotifyIcon: Implement
//TODO: User Account
//TODO: WarningLevelToBrushConverter: Maintain
//TODO: DataGrid ContextMenu