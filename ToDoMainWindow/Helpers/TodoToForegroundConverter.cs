using System;
using System.Globalization;
using TodoModels;


namespace ToDoMainWindow.Helpers
{
    public class TodoToForegroundConverter: TodoValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => GetForeground(value as Todo);
    }
}