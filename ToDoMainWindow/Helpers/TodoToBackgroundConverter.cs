using System;
using System.Configuration;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using CB.Model.Serialization;
using TodoModels;


namespace ToDoMainWindow.Helpers
{
    public abstract class TodoConverter: IValueConverter
    {
        #region Fields
        protected static readonly WarningLevelConfig _warningLevelConfig =
            SerializationHelpers.Load<WarningLevelConfig>(GetConfigFilePath());
        #endregion


        #region Abstract
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);
        #endregion


        #region Methods
        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => DependencyProperty.UnsetValue;
        #endregion


        #region Implementation
        protected object GetBackground(Todo todo)
            => todo == null
                   ? DependencyProperty.UnsetValue
                   : _warningLevelConfig.GetWarningLevelBrush(todo)?.Background ?? DependencyProperty.UnsetValue;

        private static string GetConfigFilePath()
            => ConfigurationManager.AppSettings["warningLevelConfig"];

        protected object GetContent(Todo todo)
            => todo == null
                   ? DependencyProperty.UnsetValue
                   : _warningLevelConfig.GetWarningLevelBrush(todo)?.Content ?? DependencyProperty.UnsetValue;

        protected object GetForeground(Todo todo)
            => todo == null
                   ? DependencyProperty.UnsetValue
                   : _warningLevelConfig.GetWarningLevelBrush(todo)?.Foreground ?? DependencyProperty.UnsetValue;
        #endregion
    }

    public class TodoToBackgroundConverter: TodoConverter
    {
        #region Override
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => GetBackground(value as Todo);
        #endregion
    }

    public class TodoToContentConverter: TodoConverter
    {
        #region Override
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => GetContent(value as Todo);
        #endregion
    }
}