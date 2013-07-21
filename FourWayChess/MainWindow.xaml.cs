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
                                col = Colors.LightGray;
                            else
                                col = Colors.Red;
                        }
                        else
                        {
                            if (x % 2 != 0)
                                col = Colors.LightGray;
                            else
                                col = Colors.Red;

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
                        (GameDispatcher.GameBoard[x,y] as Piece).Position = new Point(x,y);
                        var cont = new PieceControl((GameDispatcher.GameBoard[x,y] as Piece).Type){Height=unitSize , Width = unitSize};
                        gameBoard.Children.Add(cont);
                        Canvas.SetLeft(cont , x* unitSize);
                        Canvas.SetTop(cont, y * unitSize);

                        cont.MouseLeftButtonDown += (a, b) =>
                        {
                            Selected = a as PieceControl;
                            root.RenderTransform = new ScaleTransform();
                            AnimationHelper.WPF.AnimationHelper.ScaleTo(target:root, duration:1 , toX:1.4, toY:1.4).Completed+=(ax,bx)=> MessageBox.Show("");
                        };

                        cont.MouseLeftButtonUp += (a, b) =>
                        {
                            //(Selected.RenderTransform as ScaleTransform).ScaleX = 1;
                            //(Selected.RenderTransform as ScaleTransform).ScaleY = 1;
                            Selected = null;
                        };

                    }
                }
            }
        }

        public void ShowAvailableMovesFor()
        {
            
        }
    }
}
