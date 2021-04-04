#nullable enable
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using ThemeCommons.Extension;
using ColorConverter = ThemeCommons.Extension.ColorConverter;
using MediaColor = System.Windows.Media.Color;

namespace ThemeCommons.Primitives
{
    public class Color : INotifyPropertyChanged
    {
        private HSVColor _hsvColor = ColorConverter.HSVColorFromARGB(0xFF, 0xFF, 0x00, 0x00);
        public HSVColor HSVColor
        {
            get => _hsvColor;
            set
            {
                _hsvColor = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Brush));
                OnPropertyChanged(nameof(A));
                OnPropertyChanged(nameof(HueBrush));
                OnPropertyChanged(nameof(SolidColor));
                OnPropertyChanged(nameof(TransparentColor));
                OnPropertyChanged(nameof(HexString));
            }
        }

        /// <summary>
        /// Colors Alpha Channel
        /// </summary>
        public double A
        {
            get => HSVColor.A;
            set
            {
                HSVColor = new HSVColor(value, HSVColor.H, HSVColor.S, HSVColor.V);
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Hue Value
        /// </summary>
        public double H
        {
            get => HSVColor.H;
            set
            {
                HSVColor = new HSVColor(HSVColor.A, value, HSVColor.S, HSVColor.V);
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Saturation Value
        /// </summary>
        public double S
        {
            get => HSVColor.S;
            set
            {
                HSVColor = new HSVColor(HSVColor.A, HSVColor.H, value, HSVColor.V);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Value Value
        /// </summary>
        public double V
        {
            get => HSVColor.V;
            set
            {
                HSVColor = new HSVColor(HSVColor.A, HSVColor.H, HSVColor.S, value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// HexString Representation
        /// </summary>
        public string HexString => HSVColor.ToHexString();

        private readonly SolidColorBrush _hueBrush = new SolidColorBrush();
        public SolidColorBrush HueBrush
        {
            get
            {
                _hueBrush.Color = ColorConverter.ColorFromAHSV(1, H, 1, 1);
                return _hueBrush;
            }
        }

        private readonly SolidColorBrush _brush = new SolidColorBrush();
        public SolidColorBrush Brush
        {
            get
            {
                _brush.Color = HSVColor.ToMediaColor();
                return _brush;
            }
        }

        public MediaColor SolidColor => ColorConverter.ColorFromAHSV(1, H, S, V);

        public MediaColor TransparentColor => ColorConverter.ColorFromAHSV(0, H, S, V);

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null!) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
#nullable restore
