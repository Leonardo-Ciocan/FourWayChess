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

        Rectangle[,] backgroundBoard = new Rectangle[12,12];
        private double unitSize;

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //There are 12 squares on the widest side so this is the size of the smallest.
            unitSize = root.ActualWidth/12.0;

            for (var x = 0; x < 12; x++)
            {
                for (var y = 0; y < 12; y++)
                {
                    if (!(x < 2 && y < 2) && !(x > 9 && y < 2) && !(x < 2 && y > 9) && !(x > 9 && y > 9))
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

        public void DrawGame()
        {
            gameBoard.Children.Clear();
            for (int x = 0; x < 12; x++)
            {
                for (int y = 0; y< 12; y++)
                {
                    if (GameDispatcher.GameBoard[x, y] != null)
                    {
                        var cont = new PieceControl((GameDispatcher.GameBoard[x,y] as Piece).Type){Height=unitSize , Width = unitSize};
                        gameBoard.Children.Add(cont);
                        Canvas.SetLeft(cont , x* unitSize);
                        Canvas.SetTop(cont, y * unitSize);

                    }
                }
            }
        }

        public void ShowAvailableMovesFor()
        {
            
        }
    }
}
