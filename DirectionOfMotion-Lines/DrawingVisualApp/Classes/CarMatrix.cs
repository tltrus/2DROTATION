using System;
using System.Windows.Media;

namespace DrawingVisualApp
{
    class CarMatrix : CarBase
    {
        public CarMatrix(Brush brush) : base(brush)
        {
        }

        public void Update(Vector2D mouse)
        {
            // Поворот за мышкой
            Vector2D dir = Vector2D.Sub(p0, mouse);
            Vector2D dir2 = Vector2D.Sub(p0, p1);

            // Останавливаем перемещение, если достигли мышки
            if (dir.Mag() <= length)
            {
                acceleration.Mult(0);
                velocity.Mult(0);
                return;
            }

            angle = dir.angleBetween(dir2);

            // Направление вращения
            direction = Math.Sign(dir2.Cross(dir));


            angle = angle * 180 / Math.PI;

            // Костыль
            if (direction > 0)
                to = angle * 0.4;
            else
                to = angle * -0.4;


            #region Матрицы трансформации

            double[,] before_points = new double[2, 3] { { p0.X, p0.Y, 1 }, { p1.X, p1.Y, 1 } };

            Matrix2D mTranslNeg = new Matrix2D();
            mTranslNeg.Translate(-p0.X, -p0.Y);

            Matrix2D mRot = new Matrix2D();
            mRot.Rotate(to);

            Matrix2D mTransl = new Matrix2D();
            mTransl.Translate(p0.X, p0.Y);

            Matrix2D mRes = mTranslNeg * mRot * mTransl; // Перемножение матриц
            var matrixTrans = mRes.ToArray();

            var after_points = Matrix2D.Mult(before_points, matrixTrans);

            p0.X = after_points[0, 0];
            p0.Y = after_points[0, 1];
            p1.X = after_points[1, 0];
            p1.Y = after_points[1, 1];
            #endregion


            // Движение вперед

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
