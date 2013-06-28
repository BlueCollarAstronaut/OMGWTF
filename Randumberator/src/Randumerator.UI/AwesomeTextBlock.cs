using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Randumerator.UI
{
    public class AwesomeTextBlock
        : FrameworkElement
    {
        #region Properties, etc.

        #region Fill

        public static readonly DependencyProperty FillProperty = DependencyProperty.Register(
            "Fill",
            typeof (Brush),
            typeof (AwesomeTextBlock),
            new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.AffectsRender));

        public Brush Fill
        {
            get { return (Brush) GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        #endregion
        
        #region Stroke

        public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register(
            "Stroke",
            typeof (Brush),
            typeof (AwesomeTextBlock),
            new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.AffectsRender));

        public Brush Stroke
        {
            get { return (Brush) GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }


        #endregion

        #region StrokeThickness

        public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register(
            "StrokeThickness",
            typeof (double),
            typeof (AwesomeTextBlock),
            new FrameworkPropertyMetadata(1d, FrameworkPropertyMetadataOptions.AffectsRender));

        public double StrokeThickness
        {
            get { return (double) GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        #endregion

        #region Text

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text",
            typeof (string),
            typeof (AwesomeTextBlock));


        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        #endregion

        #region FontFamily

        public static readonly DependencyProperty FontFamilyProperty = TextElement.FontFamilyProperty.AddOwner(
        typeof(AwesomeTextBlock));

        public FontFamily FontFamily
        {
        get { return (FontFamily)GetValue(FontFamilyProperty); }
        set { SetValue(FontFamilyProperty, value); }
        }

        #endregion

        #region FontSize

        public static readonly DependencyProperty FontSizeProperty = TextElement.FontSizeProperty.AddOwner(
        typeof(AwesomeTextBlock));

        [TypeConverter(typeof(FontSizeConverter))]
        public double FontSize
        {
        get { return (double)GetValue(FontSizeProperty); }
        set { SetValue(FontSizeProperty, value); }
        }
        #endregion

        #region FontStyle

        public static readonly DependencyProperty FontStyleProperty = TextElement.FontStyleProperty.AddOwner(
        typeof (AwesomeTextBlock));

        public FontStyle FontStyle
        {
        get { return (FontStyle)GetValue(FontStyleProperty); }
        set { SetValue(FontStyleProperty, value); }
        }
        #endregion

        #region FontWeight

        public static readonly DependencyProperty FontWeightProperty = TextElement.FontWeightProperty.AddOwner(
        typeof(AwesomeTextBlock));

        public FontWeight FontWeight
        {
            get { return (FontWeight)GetValue(FontWeightProperty); }
            set { SetValue(FontWeightProperty, value); }
        }
        #endregion

        #region FontStretch

        public static readonly DependencyProperty FontStretchProperty = TextElement.FontStretchProperty.AddOwner(
        typeof(AwesomeTextBlock));

        public FontStretch FontStretch
        {
            get { return (FontStretch)GetValue(FontStretchProperty); }
            set { SetValue(FontStretchProperty, value); }
        }
        #endregion

        #endregion

        private Geometry _textGeometry;

        private void BuildGeometries()
        {
            // Create Type face
            Typeface typeface = new Typeface(FontFamily, FontStyle, FontWeight, FontStretch);

            // Create text
            FormattedText formattedText = new FormattedText(
                Text,
                CultureInfo.GetCultureInfo("en-us"),
                FlowDirection.LeftToRight,
                typeface, 
                FontSize, 
                Brushes.Black
            );

            _textGeometry = formattedText.BuildGeometry(new Point(0, 0));
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            BuildGeometries();
            drawingContext.DrawGeometry(Fill, new Pen(Stroke, StrokeThickness), _textGeometry);
        }
    }
}
