using System;
using System.Drawing;
using ReliefModeling.Model.Recognize;

namespace ReliefModeling.Model.Carriages
{
    public class Carriage
    {
        private Point _upLeftPoint;
        public Point[,] Points => new [,]
        {
            {_upLeftPoint, new Point(_upLeftPoint.X + 1, _upLeftPoint.Y)},
            {new Point(_upLeftPoint.X, _upLeftPoint.Y + 1), new Point(_upLeftPoint.X + 1, _upLeftPoint.Y + 1)}
        };

        public Carriage(Point upLeft)
        {
            _upLeftPoint = upLeft;
        }

        public void Move(Direction direction)
        {
            MovePoint(direction,ref _upLeftPoint);
        }

        public static void MovePoint(Direction direction,ref Point point)
        {
            switch (direction)
            {
                case Direction.Up:
                    point.Y -= 1;
                    break;
                case Direction.Down:
                    point.Y += 1;
                    break;
                case Direction.Left:
                    point.X -= 1;
                    break;
                case Direction.Right:
                    point.X += 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        } 
    }
}