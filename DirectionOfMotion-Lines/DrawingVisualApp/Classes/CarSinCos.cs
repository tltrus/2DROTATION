using System;
using System.Windows.Media;


namespace DrawingVisualApp
{
    internal class CarSinCos : CarBase
    {
        public CarSinCos(Brush brush) : base(brush)
        {
        }

        public void Update(Vector2D mouse)
        {
            Vector2D dir = Vector2D.Sub(p0, mouse);

            // Останавливаем перемещение, если достигли мышки
            if (dir.Mag() <= length)
            {
                acceleration.Mult(0);
                velocity.Mult(0);
                return;
            }

            angle = Math.Atan2(dir.Y, dir.X);

            p1.X = p0.X - Math.Cos(angle) * length;
            p1.Y = p0.Y - Math.Sin(angle) * length;

            dir.Normalize();
            dir.Mult(k);
            acceleration = dir;

            velocity.Add(acceleration);
            velocity.Limit(topspeed);
            p0.Add(velocity);
            p1.Add(velocity);
        }
    }
}
