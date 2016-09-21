using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Media;

namespace GameOfLife
{
    /// <summary>
    /// Displays the game of life on a WPF canvas.
    /// </summary>
    public class Viewer
    {
        readonly Canvas viewport;
        readonly Life grid;
        /// <summary>
        /// Center of view in world coordinates.
        /// </summary>
        public Vector2 Center { get; set; } = new Vector2();
        /// <summary>
        /// Size of cells in canvas coordinates.
        /// </summary>
        public double Scale { get; set; } = 10;

        public Viewer(Canvas view, Life grid)
        {
            viewport = view;
            this.grid = grid;
        }

        /// <summary>
        /// Convert a world coordinate into a canvas coordinate.
        /// </summary>
        public Vector2 worldToCanvas(Vector2 worldCoord)
        {
            Vector2 canvasCoord = worldCoord;
            Vector2 viewSize = new Vector2(viewport.ActualWidth, viewport.ActualHeight);
            canvasCoord += Center;
            canvasCoord *= Scale;
            canvasCoord += viewSize / 2;
            return canvasCoord;
        }

        /// <summary>
        /// Remove all the contents of the canvas and add new rectangles for all live cells.
        /// </summary>
        public void Redraw()
        {
            viewport.Children.Clear();
            foreach (Point p in grid.GetAllCells())
            {
                Vector2 topLeft = worldToCanvas(new Vector2(p.X, p.Y));
                Vector2 bottomRight = worldToCanvas(new Vector2(p.X + 1, p.Y + 1));

                var rect = new System.Windows.Shapes.Rectangle();
                rect.Stroke = new SolidColorBrush(Colors.Black);
                rect.Fill = new SolidColorBrush(Colors.Black);
                rect.Width = bottomRight.X - topLeft.X;
                rect.Height = bottomRight.Y - topLeft.Y;
                rect.IsHitTestVisible = false;
                Canvas.SetLeft(rect, topLeft.X);
                Canvas.SetTop(rect, topLeft.Y);

                viewport.Children.Add(rect);
            }
        }
    }
}
