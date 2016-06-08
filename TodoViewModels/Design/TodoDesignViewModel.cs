using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TodoModels;


namespace TodoViewModels.Design
{
    public class TodoDesignViewModel: TodoViewModel
    {
        #region  Properties & Indexers
        public override ObservableCollection<Todo> Todos { get; protected set; } =
            new ObservableCollection<Todo>(new List<Todo>
            {
                new Todo
                {
                    Content = "Do job 1",
                    Deadline = DateTime.Now,
                    IsDone = false
                },
                new Todo
                {
                    Content = "Do job 2",
                    Deadline = DateTime.Now.AddHours(1.5),
                    IsDone = true
                },
                new Todo
                {
                    Content = "Do job 3",
                    Deadline = DateTime.Now.AddHours(3),
                    IsDone = false
                }
                ,
                new Todo
                {
                    Content = "Do job 4",
                    Deadline = DateTime.Now.AddDays(1.5),
                    IsDone = false
                },
                new Todo
                {
                    Content = "Do job 5",
                    Deadline = DateTime.Now.AddDays(3),
                    IsDone = true
                },
                new Todo
                {
                    Content = "Do job 6",
                    Deadline = DateTime.Now.AddMonths(1),
                    IsDone = false
                }
            });
        #endregion
    }
}