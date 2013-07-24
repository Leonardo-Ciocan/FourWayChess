using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Linq;

namespace FourWayChess
{
	/// <summary>
	/// Interaction logic for PieceControl.xaml
	/// </summary>
	public partial class PieceControl : UserControl
	{
		public PieceControl(PieceType type , int x , int y)
		{
			this.InitializeComponent();
		    this.Loaded += (a, b) =>
		    {
                this.RenderTransform = new ScaleTransform{CenterX = ActualWidth/2 , CenterY = ActualHeight/2};
		        if (type == PieceType.Pawn) pawn.Visibility = Visibility.Visible;
                if (type == PieceType.Queen) queen.Visibility = Visibility.Visible;
                if (type == PieceType.King) king.Visibility = Visibility.Visible;
                if (type == PieceType.Rook) rook.Visibility = Visibility.Visible;
                if (type == PieceType.Knight) knight.Visibility = Visibility.Visible;
                if (type == PieceType.Bishop) bishop.Visibility = Visibility.Visible;
                SetPlayerColor(x,y);
		    };
		}


	    public void SetPlayerColor(int x , int y)
	    {
	        Path shape=new Path();
	        foreach (FrameworkElement p in LayoutRoot.Children)
	        {
	            if (p.GetType() == typeof (Path))
	            {
	                if (p.Visibility == Visibility.Visible) shape = p as Path;
	            }
	        }

	        if (x > 10)
	        {
	            (shape.Fill as LinearGradientBrush).GradientStops[0].Color = (Color)ColorConverter.ConvertFromString("#FF3EA7FF");
                (shape.Fill as LinearGradientBrush).GradientStops[1].Color = (Color)ColorConverter.ConvertFromString("#FF008BFF");
	        }
	        if (x < 3)
	        {
                (shape.Fill as LinearGradientBrush).GradientStops[0].Color = (Color)ColorConverter.ConvertFromString("#FF00A845");
                (shape.Fill as LinearGradientBrush).GradientStops[1].Color = (Color)ColorConverter.ConvertFromString("#FF007C1C");
	        }
	        if (y > 10)
	        {
                (shape.Fill as LinearGradientBrush).GradientStops[0].Color = (Color)ColorConverter.ConvertFromString("#FFFFA73E");
                (shape.Fill as LinearGradientBrush).GradientStops[1].Color = (Color)ColorConverter.ConvertFromString("#FFC96E00");
	        }
	        if (y < 3)
	        {
                (shape.Fill as LinearGradientBrush).GradientStops[0].Color = (Color)ColorConverter.ConvertFromString("#FFB93EFF");
                (shape.Fill as LinearGradientBrush).GradientStops[1].Color = (Color)ColorConverter.ConvertFromString("#FF5600AC");
	        }
	    }

	    public int x, y;
	}
}