using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FourWayChess
{
	/// <summary>
	/// Interaction logic for PieceControl.xaml
	/// </summary>
	public partial class PieceControl : UserControl
	{
		public PieceControl(PieceType type)
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
		    };
		}
	}
}