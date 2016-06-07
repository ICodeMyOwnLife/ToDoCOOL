using System;
using System.ComponentModel;
using System.Diagnostics;
using CB.Data;


namespace Todos
{
    public class RepeatSchedule: ObservableObject
    {
        #region Fields
        private static readonly DateTime OUT_OF_SCHEDULE = DateTime.MaxValue;
        private TimeSpan? _afterDone;
        private TimeSpan? _each;
        private Todo _owner;
        private readonly TimeSchedule _timeSchedule;
        private RepeatScheduleUse _use = RepeatScheduleUse.None;
        #endregion


        #region  Constructors & Destructor
        internal RepeatSchedule()
        {
            
        }

        internal RepeatSchedule(Todo owner)
        {
            _owner = owner;
            _owner.PropertyChanged += Owner_PropertyChanged;
            _timeSchedule = new TimeSchedule
            {
                Callback = OnSchedule,
                Source = this,
                Time = OUT_OF_SCHEDULE
            };
            Clock.Schedule(_timeSchedule);
        }
        #endregion


        #region  Properties & Indexers
        public TimeSpan? AfterDone
        {
            get { return _afterDone; }
            set
            {
                if (!SetProperty(ref _afterDone, value)) return;

                if (value != null)
                {
                    SetAfterDoneSchedule();
                }
                else if (Each == null)
                {
                    SetNoneSchedule();
                }
                else
                {
                    SetEachSchedule();
                }
            }
        }

        public TimeSpan? Each
        {
            get { return _each; }
            set
            {
                if (!SetProperty(ref _each, value)) return;

                if (value != null)
                {
                    SetEachSchedule();
                }
                else if (AfterDone == null)
                {
                    SetNoneSchedule();
                }
                else
                {
                    SetAfterDoneSchedule();
                }
            }
        }
        #endregion


        #region Methods
        public DateTime GetNextDeadline()
        {
            switch (_use)
            {
                case RepeatScheduleUse.Each:
                    Debug.Assert(Each != null, "Each != null");
                    return _owner.Deadline.Add(Each.Value);
                case RepeatScheduleUse.AfterDone:
                    Debug.Assert(AfterDone != null, "AfterDone != null");
                    return DateTime.Now.Add(AfterDone.Value);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        #endregion


        #region Event Handlers
        private void Owner_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (_use == RepeatScheduleUse.AfterDone && e.PropertyName == nameof(Todo.IsDone))
            {
                _timeSchedule.Time = _owner.IsDone ? DateTime.Now : OUT_OF_SCHEDULE;
            }
        }
        #endregion


        #region Implementation
        private Todo CreateNextTodo()
        {
            var nextTodo = _owner.CreateNextTodo();
            _owner = nextTodo;
            return nextTodo;
        }

        private void OnSchedule(object o)
        {
            _owner.OnRepeated(CreateNextTodo());
        }

        private void SetAfterDoneSchedule()
        {
            _use = RepeatScheduleUse.AfterDone;
            _timeSchedule.Time = OUT_OF_SCHEDULE;
        }

        private void SetEachSchedule()
        {
            _use = RepeatScheduleUse.Each;
            _timeSchedule.Time = _owner.Deadline;
        }

        private void SetNoneSchedule()
        {
            _use = RepeatScheduleUse.None;
            _timeSchedule.Time = OUT_OF_SCHEDULE;
        }
        #endregion
    }
}