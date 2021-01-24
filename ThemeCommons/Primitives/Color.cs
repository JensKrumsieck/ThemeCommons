using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using ThemeCommons.Extension;
using MediaColor = System.Windows.Media.Color;

namespace ThemeCommons.Primitives
{
    public class Color : INotifyPropertyChanged
    {
        private HSVColor _hsvColor = ColorUtil.HSVColorFromARGB(0xFF, 0xFF, 0x00, 0x00);
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
            get => HSVColor.a;
            set
            {
                HSVColor = new HSVColor(value, HSVColor.h, HSVColor.s, HSVColor.v);
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Hue Value
        /// </summary>
        public double H
        {
            get => HSVColor.h;
            set
            {
                HSVColor = new HSVColor(HSVColor.a, value, HSVColor.s, HSVColor.v);
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// Saturation Value
        /// </summary>
        public double S
        {
            get => HSVColor.s;
            set
            {
                HSVColor = new HSVColor(HSVColor.a, HSVColor.h, value, HSVColor.v);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Value Value
        /// </summary>
        public double V
        {
            get => HSVColor.v;
            set
            {
                HSVColor = new HSVColor(HSVColor.a, HSVColor.h, HSVColor.s, value);
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
                _hueBrush.Color = ColorUtil.ColorFromAHSV(1, H, 1, 1);
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

        public MediaColor SolidColor => ColorUtil.ColorFromAHSV(1, H, S, V);

        public MediaColor TransparentColor => ColorUtil.ColorFromAHSV(0, H, S, V);

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
