using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;


namespace TodoModels
{
    public class WarningLevelConfig
    {
        #region  Properties & Indexers
        public WarningLevelBrushList BrushList { get; } = new WarningLevelBrushList();
        public WarningLevelBrush DefaultBrush { get; set; }
        #endregion


        #region Methods
        public WarningLevelBrush GetWarningLevelBrush(Todo todo)
        {
            var remainingHours = (DateTime.Now - todo.Deadline).TotalHours;
            return BrushList.FirstOrDefault(b => b.Hours >= remainingHours) ?? DefaultBrush;
        }
        #endregion
    }

    public class WarningLevelBrushList: SortedSet<WarningLevelBrush> { }

    public class WarningLevelBrush: IComparable, IComparable<WarningLevelBrush>, IComparer<WarningLevelBrush>
    {
        #region  Properties & Indexers
        public Brush Background { get; set; }
        public string Content { get; set; }
        public Brush Foreground { get; set; }
        public double Hours { get; set; }
        #endregion


        #region Methods
        public int Compare(WarningLevelBrush x, WarningLevelBrush y)
            => x == null && y == null ? 0 : x == null ? -1 : y == null ? 1 : x.Hours.CompareTo(y.Hours);

        public int CompareTo(object obj)
            => CompareTo(obj as WarningLevelBrush);

        public int CompareTo(WarningLevelBrush other)
            => Compare(this, other);
        #endregion
    }

    public class WarningLevel
    {
        #region Fields
        private readonly Func<TimeSpan, WarningLevel> _getWarningLevelFunc;
        #endregion


        #region  Constructors & Destructor
        public WarningLevel(WarningLevel[] all, Func<TimeSpan, WarningLevel> getWarningLevelFunc)
        {
            All = all;
            _getWarningLevelFunc = getWarningLevelFunc;
        }
        #endregion


        #region  Properties & Indexers
        public WarningLevel[] All { get; }

        public string Content { get; private set; }
        #endregion


        #region Methods
        public WarningLevel GetWarningLevel(Todo todo)
            => _getWarningLevelFunc(DateTime.Now - todo.Deadline);
        #endregion
    }
}