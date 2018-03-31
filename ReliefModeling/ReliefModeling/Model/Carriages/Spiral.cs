using System;
using System.Drawing;
using ReliefModeling.Model.Recognize;

namespace ReliefModeling.Model.Carriages
{
    public class Spiral
    {
        private Point _point;
        private Direction _direction;
        private int _iterator;
        private int _limitPositionInIteraor;
        private int _positionInIterator;

        public Spiral(Point startPoint)
        {
            _point = startPoint;
            _direction = Direction.Right;
            _limitPositionInIteraor = 1;
        }

        public Point Next()
        {
            Carriage.MovePoint(_direction, ref _point);
            _positionInIterator--;

            if (_positionInIterator > 0) return _point;
            SwitchDirection();
            UpdateIterator();

            return _point;
        }

        private void SwitchDirection()
        {
            switch (_direction)
            {
                case Direction.Up:
                    _direction = Direction.Right;
                    break;
                case Direction.Down:
                    _direction = Direction.Left;
                    break;
                case Direction.Left:
                    _direction = Direction.Up;
                    break;
                case Direction.Right:
                    _direction = Direction.Down;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void UpdateIterator()
        {
            _iterator++;
            if (_iterator % 2 == 0) _limitPositionInIteraor++;
            _positionInIterator = _limitPositionInIteraor;
        }
    }
}