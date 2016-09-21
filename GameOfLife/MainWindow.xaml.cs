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
using System.Windows.Threading;

namespace GameOfLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Viewer viewer;
        Life life;
        Point mousePrevious;

        public MainWindow()
        {
            InitializeComponent();

            life = new Life();

            //Randomly turn on some of the cells.
            Random random = new Random();
            for (int i = -10; i < 10; i++)
            {
                for (int j = -10; j < 10; j++)
                {
                    life[i, j] = random.Next(3) > 1;
                }
            }

            viewer = new Viewer(canvas, life);
            viewer.Redraw();

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += Step;
            dispatcherTimer.Interval = new TimeSpan(TimeSpan.TicksPerSecond/4);
            dispatcherTimer.Start();
        }

        private void Step(object sender, EventArgs e)
        {
            life.Step();
            viewer.Redraw();
        }

        /// <summary>
        /// Check if the user is dragging the canvas in order to move the view.
        /// </summary>
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            bool mouseIsDown = 
                Mouse.LeftButton == MouseButtonState.Pressed || 
                Mouse.RightButton == MouseButtonState.Pressed;
            if (mouseIsDown)
            {
                var offset = Mouse.GetPosition(this) - mousePrevious;
                viewer.Center += new Vector2(offset.X, offset.Y) / viewer.Scale;
                viewer.Redraw();
            }
            mousePrevious = Mouse.GetPosition(this);
        }

        /// <summary>
        /// Handle zooming in or out on the canvas with the mouse wheel.
        /// </summary>
        private void canvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            double scaleNext = viewer.Scale * Math.Pow(2, e.Delta/100);
            //Clamp the scale value.
            viewer.Scale = Math.Max(Math.Min(scaleNext, 50), 1);
            viewer.Redraw();
        }

        /// <summary>
        /// Redraw the canvas if it's resized.
        /// </summary>
        private void canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            viewer.Redraw();
        }
    }
}
