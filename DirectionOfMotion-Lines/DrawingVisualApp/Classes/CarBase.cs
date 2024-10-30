using System.Windows.Media;
using System.Windows;

namespace DrawingVisualApp
{
    internal class CarBase
    {
        public Vector2D velocity;
        public Vector2D acceleration;
        public Vector2D p0, p1;
        public Brush brush;
        public int topspeed = 4;
        public double angle;
        public int length = 20;
        public int direction;
        public double to;
        public double k = -0.5;

        public CarBase(Brush brush)
        {
            var x = MainWindow.rnd.Next((int)MainWindow.width);
            var y = MainWindow.rnd.Next((int)MainWindow.height);
            p0 = new Vector2D(x, y);
            p1 = new Vector2D(x - length, y);

            acceleration = new Vector2D();
            velocity = new Vector2D();

            this.brush = brush;
        }

        public void GoAway() => k *= -1;

        public void Draw(DrawingContext dc)
        {
            Point P0 = new Point();
            Point P1 = new Point();
            P0.X = p0.X;
            P0.Y = p0.Y;
            P1.X = p1.X;
            P1.Y = p1.Y;

            dc.DrawLine(new Pen(brush, 6), P0, P1);
        }
    }
}
