using System;
using System.Collections.Generic;
using System.Windows.Media;


namespace ToDoMainWindow.Helpers
{
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
}