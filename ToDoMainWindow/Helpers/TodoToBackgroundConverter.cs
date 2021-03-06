﻿using System;
using System.Globalization;
using TodoModels;


namespace ToDoMainWindow.Helpers
{
    public class TodoToBackgroundConverter: TodoValueConverterBase
    {
        #region Override
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => GetBackground(value as Todo);
        #endregion
    }
}