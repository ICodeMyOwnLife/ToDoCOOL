using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using Todos;


namespace MainForm
{
    public class WarningLevelToBrushConverter: IValueConverter
    {
        #region Fields
        private const double ANGLE = 90.002;
        private static readonly Color _startColor = Colors.White;
        #endregion


        #region  Properties & Indexers
        public Brush ApproachingBrush { get; set; } = new LinearGradientBrush(_startColor, Color.FromRgb(255, 242, 148),
            ANGLE);

        public Brush InProgressBrush { get; set; } = new LinearGradientBrush(_startColor, Color.FromRgb(204, 148, 255),
            ANGLE);

        public Brush NoneBrush { get; set; } = new LinearGradientBrush(_startColor, Color.FromRgb(184, 184, 184), ANGLE);

        public Brush NormalBrush { get; set; } = new LinearGradientBrush(_startColor, Color.FromRgb(149, 255, 160),
            ANGLE);

        public Brush OverdueBrush { get; set; } = new LinearGradientBrush(_startColor, Color.FromRgb(255, 147, 147),
            ANGLE);
        #endregion


        #region Methods
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is WarningLevel)) return DependencyProperty.UnsetValue;

            var warningLevel = (WarningLevel)value;
            switch (warningLevel)
            {
                case WarningLevel.Overdue:
                    return OverdueBrush;
                case WarningLevel.InProgress:
                    return InProgressBrush;
                case WarningLevel.Approaching:
                    return ApproachingBrush;
                case WarningLevel.Normal:
                    return NormalBrush;
                case WarningLevel.None:
                    return NoneBrush;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
        #endregion
    }
}