using System.Collections;
using Todos;


namespace MainForm
{
    internal class TodoComparer: IComparer
    {
        #region Methods
        public int Compare(object x, object y)
        {
            Todo todo1 = x as Todo, todo2 = y as Todo;
            return todo1 == null || todo2 == null ? 0 : Compare(todo1, todo2);
        }

        public int Compare(Todo todo1, Todo todo2)
        {
            var isDoneComp = GetIsDoneComparíon(todo1) - GetIsDoneComparíon(todo2);
            var deadlineComp = (int)(todo1.Deadline - todo2.Deadline).TotalSeconds;
            var priorityComp = todo1.Priority - todo2.Priority;
            return isDoneComp != 0 ? isDoneComp
                       : deadlineComp != 0 ? deadlineComp
                             : priorityComp;
        }
        #endregion


        #region Implementation
        private static int GetIsDoneComparíon(Todo todo1)
        {
            return todo1.IsDone ? 1 : -1;
        }
        #endregion
    }
}