using System;
using System.Globalization;
using TodoModels;


namespace ToDoMainWindow.Helpers
{
    public class TodoToContentConverter: TodoValueConverterBase
    {
        #region Override
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => GetContent(value as Todo);
        #endregion
    }
}