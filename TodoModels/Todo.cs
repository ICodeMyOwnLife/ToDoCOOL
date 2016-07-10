using System;
using CB.Model.Common;


namespace TodoModels
{
    public class Todo: BindableObject
    {
        #region Fields
        private string _content;
        private DateTime? _createdOn = DateTime.Now;
        private DateTime _deadline = DateTime.Now;
        private int? _id;
        private bool _isDone;
        private DateTime? _updatedOn = DateTime.Now;
        #endregion


        #region  Properties & Indexers
        public string Content
        {
            get { return _content; }
            set { if (SetProperty(ref _content, value)) Update(); }
        }

        public DateTime? CreatedOn
        {
            get { return _createdOn; }
            set { SetProperty(ref _createdOn, value); }
        }

        public DateTime Deadline
        {
            get { return _deadline; }
            set { if (SetProperty(ref _deadline, value)) Update(); }
        }

        public int? Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        public bool IsDone
        {
            get { return _isDone; }
            set { if (SetProperty(ref _isDone, value)) Update(); }
        }

        public DateTime? UpdatedOn
        {
            get { return _updatedOn; }
            set { SetProperty(ref _updatedOn, value); }
        }
        #endregion


        #region Implementation
        private void Update()
            => UpdatedOn = DateTime.Now;
        #endregion
    }
}