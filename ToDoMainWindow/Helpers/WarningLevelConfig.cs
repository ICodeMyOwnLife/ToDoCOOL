using System;
using System.Linq;
using System.Windows.Markup;
using TodoModels;


namespace ToDoMainWindow.Helpers
{
    [ContentProperty(nameof(BrushList))]
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
            return BrushList?.FirstOrDefault(b => b.Hours <= remainingHours) ?? DefaultBrush;
        }
        #endregion
    }
}