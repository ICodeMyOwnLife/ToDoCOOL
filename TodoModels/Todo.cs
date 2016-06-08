using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using CB.Model.Common;


namespace TodoModels
{
    public class Todo: BindableObject
    {
        private string _content;

        public string Content
        {
            get { return _content; }
            set { SetProperty(ref _content, value); }
        }

        private DateTime _deadline;

        public DateTime Deadline
        {
            get { return _deadline; }
            set { SetProperty(ref _deadline, value); }
        }

        private bool _isDone;

        public bool IsDone
        {
            get { return _isDone; }
            set { SetProperty(ref _isDone, value); }
        }

        private WarningLevel _warningLevel;

        public WarningLevel WarningLevel
        {
            get { return _warningLevel; }
            set { SetProperty(ref _warningLevel, value); }
        }

        
    }
    /*public class Todo: BindableObject
    {
        #region Fields
        private const int IN_PROGRESS_HOURS = 2,
                          APPROACHING_HOURS = 24;

        [XmlArray]
        private List<AlertStrategy> _alertStrategies = new List<AlertStrategy>();

        private string _content = "Task 1";
        private DateTime _deadline = DateTime.Now;
        private bool _isDone;

        [NonSerialized]
        private WarningLevel _warningLevel;
        #endregion


        #region  Constructors & Destructor
        public Todo()
        {
            RepeatSchedule = new RepeatSchedule(this);
        }
        #endregion


        #region  Properties & Indexers
        public List<AlertStrategy> AlertStrategies
        {
            get { return _alertStrategies; }
            set { SetProperty(ref _alertStrategies, value); }
        }

        public string Content
        {
            get { return _content; }
            set { SetProperty(ref _content, value); }
        }

        public DateTime Deadline
        {
            get { return _deadline; }
            set
            {
                SetProperty(ref _deadline, value);
                SetWarningLevel();
            }
        }

        public bool IsDone
        {
            get { return _isDone; }
            set
            {
                SetProperty(ref _isDone, value);
                SetWarningLevel();
            }
        }

        public int Priority { get; set; } = 1;

        public RepeatSchedule RepeatSchedule { get; set; }

        public WarningLevel WarningLevel => _warningLevel;
        #endregion


        #region Events
        public event EventHandler<EventArgs<string>> Alert;
        public event EventHandler<EventArgs<Todo>> Repeated;
        #endregion


        #region Methods
        public void AddAlertStrategy(TimeSpan timeLeft, string message)
        {
            /*var stategyList = new List<AlertStrategy>(AlertStrategies)
            {
                new AlertStrategy(this)
                {
                    Message = message,
                    TimeLeft = timeLeft
                }
            };
            AlertStrategies = stategyList.ToArray();#1#
            AlertStrategies.Add(new AlertStrategy(this) { Message = message, TimeLeft = timeLeft });
        }

        public Todo CreateNextTodo()
        {
            var nextTodo = new Todo
            {
                Priority = Priority,
                AlertStrategies = AlertStrategies,
                Content = Content,
                RepeatSchedule = RepeatSchedule,
                Deadline = RepeatSchedule.GetNextDeadline(),
                IsDone = false
            };
            nextTodo.RepeatSchedule = new RepeatSchedule(nextTodo);
            return nextTodo;
        }
        #endregion


        #region Implementation
        protected internal virtual void OnAlert(string message)
        {
            Alert?.Invoke(this, new EventArgs<string>(message));
        }

        protected internal virtual void OnRepeated(Todo nextTodo)
        {
            Repeated?.Invoke(this, new EventArgs<Todo>(nextTodo));
        }

        private void SetWarningLevel()
        {
            var hoursToDeadline = (Deadline - DateTime.Now).TotalHours;
            var warningLevel = IsDone
                                   ? WarningLevel.None
                                   : hoursToDeadline < 0
                                         ? WarningLevel.Overdue
                                         : hoursToDeadline < IN_PROGRESS_HOURS
                                               ? WarningLevel.InProgress
                                               : hoursToDeadline < APPROACHING_HOURS
                                                     ? WarningLevel.Approaching : WarningLevel.Normal;

            SetField(ref _warningLevel, warningLevel, nameof(WarningLevel));
        }
        #endregion
    }*/
}