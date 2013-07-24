using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Xaml;

namespace AnimationHelper.WPF
{
    public class AnimationHelper
    {
        public enum SmoothMoveType
        {
            Left, Top, Both
        }

        #region Opacity Animations
        /// <summary>
        /// Fade out an element to 0 opacity.
        /// </summary>
        /// <param name="target">The element to fade</param>
        /// <param name="duration">The time it should take for the animation to complete</param>
        /// <param name="autoStart">Whether the animation should start imidiately after the call or will be handled using the return value.</param>
        /// <param name="autoReverse">Whether the animation should loop back to the original opacity.</param>
        /// <returns></returns>
        public static Storyboard FadeTo(FrameworkElement target, double targetValue, double duration, bool autoStart = true, bool autoReverse = false)
        {
            //create a storyboard to handle the animation
            var sb = new Storyboard();
            //a double animation to animate opacity (of type double)
            var dt = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(duration)),
                From = target.Opacity,
                To = targetValue
            };
            //set the property to Opacity and the target to target
            Storyboard.SetTargetProperty(dt, new PropertyPath("Opacity"));
            Storyboard.SetTarget(sb, target);
            //add the double animation to the storyboard
            sb.Children.Add(dt);
            sb.AutoReverse = autoReverse;
            if (autoStart) sb.Begin();
            return sb;//return the storyboard so additional operations can be chained (i.e complete events)
        }
        #endregion

        /// <summary>
        /// This will move an element to a specific location in a Canvas with a smooth animation
        /// </summary>
        /// <param name="target">The element to be moved</param>
        /// <param name="to">The Y coordonate of the wanted position</param>
        /// <param name="duration">Time for the movement to complete</param>
        /// <param name="autoStart">Whether the animation should immidiately start</param>
        /// <param name="autoReverse">Whether the animation should loop to its starting values</param>
        /// <returns></returns>
        public static Storyboard SmoothMove(
            FrameworkElement target,
            double to,
            double duration,
            bool autoStart = true,
            bool autoReverse = false)
        {
            Storyboard sb = new Storyboard();

            var dy = new DoubleAnimation
            {
                From = Canvas.GetTop(target),
                Duration = new Duration(TimeSpan.FromSeconds(duration))
            };
            dy.To = to;
            Storyboard.SetTargetProperty(dy, new PropertyPath("(Canvas.Top)"));
            sb.Children.Add(dy);

            Storyboard.SetTarget(sb, target);

            sb.AutoReverse = autoReverse;
            if (autoStart) sb.Begin();
            return sb;
        }

        public static Storyboard ScaleTo(
            FrameworkElement target,
            double           duration,
            double?          toX = null,
            double?          toY = null,
            Point?           CenterProportion = null,
            bool             autoStart = true,
            bool             autoReverse = false)
        {
            if (toX == null && toY == null) throw new Exception("Must specify either X or Y scaling");

            target.RenderTransform = new ScaleTransform();
            if(CenterProportion == null) CenterProportion = new Point(0.5,0.5);
            (target.RenderTransform as ScaleTransform).CenterX = CenterProportion.Value.X * target.ActualWidth;
            (target.RenderTransform as ScaleTransform).CenterY = CenterProportion.Value.Y * target.ActualHeight;

            var sb = new Storyboard();
            if (toX != null)
            {
                var dy = new DoubleAnimation
                {
                    Duration = new Duration(TimeSpan.FromSeconds(duration)),
                    From = (target.RenderTransform as ScaleTransform).ScaleX,
                    To = toX
                };
                Storyboard.SetTargetProperty(dy, new PropertyPath("RenderTransform.ScaleX"));
                sb.Children.Add(dy);
            }

            if (toX != null)
            {
                var dy = new DoubleAnimation
                {
                    Duration = new Duration(TimeSpan.FromSeconds(duration)),
                    From = (target.RenderTransform as ScaleTransform).ScaleY,
                    To = toY
                };
                Storyboard.SetTargetProperty(dy, new PropertyPath("RenderTransform.ScaleY"));
                sb.Children.Add(dy);
            }

            Storyboard.SetTarget(sb, target);

            sb.AutoReverse = autoReverse;
            if (autoStart) sb.Begin();
            return sb;
        }
    }
}
