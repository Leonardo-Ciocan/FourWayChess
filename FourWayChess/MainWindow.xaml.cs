using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
using System.Linq;

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

                        rect.Tag = col;
                        backgroundBoard[x, y] = rect;

                        //rect.MouseEnter += (a, b) => ((a as Rectangle).Fill as SolidColorBrush).Opacity = 0.4;
                        //rect.MouseLeave += (a, b) => ((a as Rectangle).Fill as SolidColorBrush).Opacity = 1;
                        rect.MouseLeftButtonDown += (a, b) =>
                        {
                            if (Selected != null)
                            {
                                if ((a as Rectangle).Fill == Selected.shape.Fill)
                                {
                                    foreach (Rectangle r in backgroundBoard)
                                    {
                                        if (r != null) r.Fill = new SolidColorBrush((Color) r.Tag);
                                    }

                                    AnimationHelper.WPF.AnimationHelper.SmoothMove(target: Selected,
                                        toY: Canvas.GetTop((Rectangle) a), toX: Canvas.GetLeft((Rectangle) a),
                                        duration: 0.5);
                                    AnimationHelper.WPF.AnimationHelper.ScaleTo(target: Selected, duration: 0.35,
                                        toX: 1.7, toY: 1.7, autoReverse: true);
                                    int xcoord = (int)(Canvas.GetLeft((Rectangle) a)/unitSize);
                                    int ycoord = (int)(Canvas.GetTop((Rectangle)a) / unitSize);
                                    Piece temp = GameDispatcher.GameBoard[Selected.x, Selected.y] as Piece;
                                    GameDispatcher.GameBoard[Selected.x, Selected.y] = null;
                                    GameDispatcher.GameBoard[xcoord,ycoord] = temp;
                                    Selected.x = xcoord;
                                    Selected.y = ycoord;
                                }

                                if ((a as Rectangle).Fill == new SolidColorBrush(Colors.Red))
                                {
                                    
                                }
                            }
                        };
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
                        if (y > 10) (GameDispatcher.GameBoard[x, y] as Piece).ForwardDirection.Y = -1;
                        if (y < 3) (GameDispatcher.GameBoard[x, y] as Piece).ForwardDirection.Y = 1;
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
                            Canvas.SetZIndex(Selected , 1);
                            foreach(FrameworkElement fr in root.Children) Canvas.SetZIndex(fr,0);
                        };

                        cont.MouseLeftButtonUp += (a, b) =>
                        {
                            AnimationHelper.WPF.AnimationHelper.ScaleTo(target: Selected, duration: 0.2, toX: 1,
                                toY: 1);
                            //Selected = null;
                        };



                    }
                }
            }
        }

        public void ShowAvailableMovesFor(int x , int y)
        {
            foreach (Rectangle r in backgroundBoard)
            {
                if(r!=null)r.Fill = new SolidColorBrush( (Color)r.Tag);
            }
            Piece piece = (GameDispatcher.GameBoard[x, y] as Piece);
                        /*foreach(PieceControl cont in root.Children)
                            if ((GameDispatcher.GameBoard[cont.x, cont.y] as Piece).ForwardDirection !=
                                piece.ForwardDirection) cont.IsHitTestVisible = false;
                            else
                                cont.IsHitTestVisible = true;*/

            if (piece.Type == PieceType.Knight)
            {
                try {
                    if (!GameUtils.ContainsPiece(x + 1, y + 2))
                    {
                        backgroundBoard[x + 1, y + 2].Fill = Selected.shape.Fill;
                    }
                    else
                    {
                        if (piece.ForwardDirection != (GameDispatcher.GameBoard[x +1,y+2] as Piece).ForwardDirection) backgroundBoard[x + 1, y + 2].Fill = new SolidColorBrush(Colors.Red);
                    }}
                catch { }
                try { if (!GameUtils.ContainsPiece(x - 1, y + 2)){ backgroundBoard[x - 1, y + 2].Fill = Selected.shape.Fill;}
                    else
                    {
                        if (piece.ForwardDirection != (GameDispatcher.GameBoard[x - 1, y + 2] as Piece).ForwardDirection) backgroundBoard[x - 1, y + 2].Fill = new SolidColorBrush(Colors.Red);
                    } }
                catch { }
                try { if (!GameUtils.ContainsPiece(x + 1, y - 2)){backgroundBoard[x + 1, y - 2].Fill = Selected.shape.Fill;}
                    else
                    {
                        if (piece.ForwardDirection != (GameDispatcher.GameBoard[x + 1, y - 2] as Piece).ForwardDirection) backgroundBoard[x + 1, y - 2].Fill = new SolidColorBrush(Colors.Red);
                    } }
                catch { }
                try { if (!GameUtils.ContainsPiece(x - 1, y - 2)){backgroundBoard[x - 1, y - 2].Fill = Selected.shape.Fill;}
                    else
                    {
                        if (piece.ForwardDirection != (GameDispatcher.GameBoard[x - 1, y- 2] as Piece).ForwardDirection) backgroundBoard[x - 1, y - 2].Fill = new SolidColorBrush(Colors.Red);
                    } }
                catch { }

                try { if (!GameUtils.ContainsPiece(x + 2, y + 1)){ backgroundBoard[x + 2, y + 1].Fill = Selected.shape.Fill;}
                    else
                    {
                        if (piece.ForwardDirection != (GameDispatcher.GameBoard[x + 2, y + 1] as Piece).ForwardDirection) backgroundBoard[x + 2, y + 1].Fill = new SolidColorBrush(Colors.Red);
                    } }
                catch { }
                try { if (!GameUtils.ContainsPiece(x - 2, y + 1)){ backgroundBoard[x - 2, y + 1].Fill = Selected.shape.Fill;}
                    else
                    {
                        if (piece.ForwardDirection != (GameDispatcher.GameBoard[x -2, y + 1] as Piece).ForwardDirection) backgroundBoard[x - 2, y + 1].Fill = new SolidColorBrush(Colors.Red);
                    } }
                catch { }
                try
                {
                    if (!GameUtils.ContainsPiece(x + 2, y - 1))
                    {
                        backgroundBoard[x + 2, y - 1].Fill = Selected.shape.Fill;
                    }
                    else
                    {
                        if (piece.ForwardDirection != (GameDispatcher.GameBoard[x - 2, y + 1] as Piece).ForwardDirection) backgroundBoard[x + 1, y + 2].Fill = new SolidColorBrush(Colors.Red);
                    }
                }
                catch
                {
                }
                try
                {
                    if (!GameUtils.ContainsPiece(x - 2, y - 1))
                    {
                        backgroundBoard[x - 2, y - 1].Fill = Selected.shape.Fill;
                    }
                    else
                    {
                        if (piece.ForwardDirection != (GameDispatcher.GameBoard[x - 2, y - 1] as Piece).ForwardDirection)
                            ;

                    }
                }
                catch
                {
                }
            }

            if ((GameDispatcher.GameBoard[x, y] as Piece).Type == PieceType.Pawn)
            {
                try { if (!GameUtils.ContainsPiece(x + (int)(piece).ForwardDirection.X, y+  (int)(piece).ForwardDirection.Y)) 
                    backgroundBoard[x + (int)(piece).ForwardDirection.X, y +  (int)(piece).ForwardDirection.Y].Fill = Selected.shape.Fill; }
                catch { }
                
            }
        }
    }
}
