using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LanaBananaDivineQuestionModifierProject
{
    /// <summary>
    /// Interaction logic for LanaOverlay.xaml
    /// </summary>
    public partial class LanaOverlay : Window
    {
        public LanaOverlay()
        {
            InitializeComponent();

            Width = SystemParameters.VirtualScreenWidth;
            Height = SystemParameters.VirtualScreenHeight;
            Left = SystemParameters.VirtualScreenLeft;
            Top = SystemParameters.VirtualScreenTop;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //--------------------------------------------------------------------
            // 1. Spin each banana in place
            //--------------------------------------------------------------------
            var spin = new DoubleAnimation
            {
                From = 0,
                To = 360,
                Duration = TimeSpan.FromSeconds(30),
                RepeatBehavior = RepeatBehavior.Forever
            };
            BananaRotate.BeginAnimation(RotateTransform.AngleProperty, spin);
            var spin2 = spin.Clone();
            spin2.BeginTime = TimeSpan.FromSeconds(spin.Duration.TimeSpan.TotalSeconds / 2);
            Banana2Rotate.BeginAnimation(RotateTransform.AngleProperty, spin2);

            //--------------------------------------------------------------------
            // 2. Build a PATH centred at (0,0)   <-- key change
            //--------------------------------------------------------------------
            double halfBanana = Banana.Width / 2.0;   // 128 px
            double margin = 20;                   // keep fully visible
            double radiusX = ActualWidth / 2.0 - halfBanana - margin;
            double radiusY = ActualHeight / 2.0 - halfBanana - margin;



            radiusX = Math.Max(radiusX, 0);             // tiny-screen safeguard
            radiusY = Math.Max(radiusY, 0);             // tiny-screen safeguard

            // Path around origin (0,0)
            var circleOrigin = new EllipseGeometry(new Point(0, 0), radiusX, radiusY);
            var orbitRel = circleOrigin.GetOutlinedPathGeometry();

            //--------------------------------------------------------------------
            // 3. Helpers: make X/Y animations that output RELATIVE offsets
            //--------------------------------------------------------------------
            TimeSpan period = TimeSpan.FromSeconds(60);     // one full lap

            DoubleAnimationUsingPath MakeAnim(PathAnimationSource src,
                                              TimeSpan? begin = null)
            {
                return new DoubleAnimationUsingPath
                {
                    PathGeometry = orbitRel,
                    Source = src,
                    Duration = period,
                    BeginTime = begin ?? TimeSpan.Zero,
                    RepeatBehavior = RepeatBehavior.Forever
                };
            }

            //--------------------------------------------------------------------
            // 4. Apply to banana #1  (centre‑offset added implicitly by layout)
            //--------------------------------------------------------------------
            BananaTranslate.BeginAnimation(TranslateTransform.XProperty,
                                           MakeAnim(PathAnimationSource.X));
            BananaTranslate.BeginAnimation(TranslateTransform.YProperty,
                                           MakeAnim(PathAnimationSource.Y));

            //--------------------------------------------------------------------
            // 5. Banana #2 – same path, 180 ° phase‑shift
            //--------------------------------------------------------------------
            var halfLap = TimeSpan.FromTicks(period.Ticks / 2);

            Banana2Translate.BeginAnimation(TranslateTransform.XProperty,
                                            MakeAnim(PathAnimationSource.X, halfLap));
            Banana2Translate.BeginAnimation(TranslateTransform.YProperty,
                                            MakeAnim(PathAnimationSource.Y, halfLap));

            _ = Task.Delay(TimeSpan.FromSeconds(30))
        .ContinueWith(_ =>
            Dispatcher.Invoke(() =>
                Banana2.Visibility = Visibility.Visible));
        }
    }
}
