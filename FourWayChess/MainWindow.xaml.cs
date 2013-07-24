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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FourWayChess
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        Rectangle[,] backgroundBoard = new Rectangle[14,14];
        private double unitSize;

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //There are 14 squares on the widest side so this is the size of the smallest.
            unitSize = root.ActualWidth/14.0;

            for (var x = 0; x < 14; x++)
            {
                for (var y = 0; y < 14; y++)
                {
                    if (!(x < 3 && y < 3) && !(x > 10 && y <3) && !(x < 3&& y > 10) && !(x >10 && y > 10))
                    {
                        Color col;
                        if (y%2 == 0)
                        {
                            if (x % 2 == 0)
                                col = Colors.SandyBrown;
                            else
                                col = Colors.SaddleBrown;
                        }
                        else
                        {
                            if (x % 2 != 0)
                                col = Colors.SandyBrown;
                            else
                                col = Colors.SaddleBrown;

                        }


                        var rect = new Rectangle
                        {
                            Fill = new SolidColorBrush(col),
                            Width=unitSize,Height=unitSize,
                            Stroke = new SolidColorBrush(Colors.Black),
                            StrokeThickness = 0.5
                        };

                        backgroundBoard[x, y] = rect;

                        rect.MouseEnter += (a, b) => ((a as Rectangle).Fill as SolidColorBrush).Opacity = 0.4;
                        rect.MouseLeave += (a, b) => ((a as Rectangle).Fill as SolidColorBrush).Opacity = 1;

                        root.Children.Add(rect);
                        Canvas.SetLeft(rect , x* unitSize);
                        Canvas.SetTop(rect, y * unitSize);
                    }
                }
            }
            DrawGame();
          
        }

        public PieceControl Selected;
        public void DrawGame()
        {
            gameBoard.Children.Clear();
            for (int x = 0; x < 14; x++)
            {
                for (int y = 0; y< 14; y++)
                {
                    if (GameDispatcher.GameBoard[x, y] != null)
                    {
                        if (x > 10) (GameDispatcher.GameBoard[x, y] as Piece).ForwardDirection.X = -1;
                        if (x < 3) (GameDispatcher.GameBoard[x, y] as Piece).ForwardDirection.X = 1;
                        if (y > 10) (GameDispatcher.GameBoard[x, y] as Piece).ForwardDirection.X = -1;
                        if (y < 3) (GameDispatcher.GameBoard[x, y] as Piece).ForwardDirection.X = 1;
                        //(GameDispatcher.GameBoard[x,y] as Piece).Position = new Point(x,y);
                        var cont = new PieceControl((GameDispatcher.GameBoard[x,y] as Piece).Type , x , y){Height=unitSize , Width = unitSize};
                        cont.x = x;
                        cont.y = y;
                        gameBoard.Children.Add(cont);
                        Canvas.SetLeft(cont , x* unitSize);
                        Canvas.SetTop(cont, y * unitSize);

                        cont.MouseLeftButtonDown += (a, b) =>
                        {
                            Selected = a as PieceControl;
                            root.RenderTransform = new ScaleTransform();
                            AnimationHelper.WPF.AnimationHelper.ScaleTo(target: Selected, duration: 0.2, toX: 1.4,
                                toY: 1.4);
                            ShowAvailableMovesFor(Selected.x,Selected.y);
                        };

                        cont.MouseLeftButtonUp += (a, b) =>
                        {
                            AnimationHelper.WPF.AnimationHelper.ScaleTo(target: Selected, duration: 0.2, toX: 1,
                                toY: 1);
                            Selected = null;
                        };



                    }
                }
            }
        }

        public void ShowAvailableMovesFor(int x , int y)
        {
            if ((GameDispatcher.GameBoard[x, y] as Piece).Type == PieceType.Knight)
            {
                try { if (!GameUtils.ContainsPiece(x+1, y+2))  backgroundBoard[x + 1, y + 2].Fill = new SolidColorBrush(Colors.DodgerBlue); }
                catch { }
                try{if (!GameUtils.ContainsPiece(x-1, y+2)) backgroundBoard[x - 1, y + 2].Fill = new SolidColorBrush(Colors.DodgerBlue);}catch{}
                try{if (!GameUtils.ContainsPiece(x+1, y-2))backgroundBoard[x + 1, y - 2].Fill = new SolidColorBrush(Colors.DodgerBlue);}catch{}
                try{if (!GameUtils.ContainsPiece(x-1, y-2))backgroundBoard[x - 1, y - 2].Fill = new SolidColorBrush(Colors.DodgerBlue);}catch{}

                try {if (!GameUtils.ContainsPiece(x+2, y+1)) backgroundBoard[x + 2, y + 1].Fill = new SolidColorBrush(Colors.DodgerBlue); }
                catch { }
                try {if (!GameUtils.ContainsPiece(x-2, y+1)) backgroundBoard[x - 2, y + 1].Fill = new SolidColorBrush(Colors.DodgerBlue); }
                catch { }
                try {if (!GameUtils.ContainsPiece(x+2, y-1)) backgroundBoard[x + 2, y - 1].Fill = new SolidColorBrush(Colors.DodgerBlue); }
                catch { }
                try
                {
                    if (!GameUtils.ContainsPiece(x - 2, y - 1))
                        backgroundBoard[x - 2, y - 1].Fill = new SolidColorBrush(Colors.DodgerBlue);
                }
                catch
                {
                }
            }
        }


    }
}
