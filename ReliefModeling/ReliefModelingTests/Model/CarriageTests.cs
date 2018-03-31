using System.Drawing;
using NUnit.Framework;
using ReliefModeling.Model.Carriages;
using ReliefModeling.Model.Recognize;

namespace ReliefModelingTests
{
    [TestFixture]
    public class CarriageTests
    {
        [Test]
        public void Point_InitPoint0_ArrayAroundPointReturn()
        {
            var carriage = new Carriage(new Point(0,0));
            var expected = new [,] {{new Point(0, 0), new Point(1, 0)}, {new Point(0, 1), new Point(1, 1)}};

            var result = carriage.Points;
            
            Assert.AreEqual(expected,result);
        }
        [Test]
        public void Move_Up_AllPointsUp()
        {
            var carriage = new Carriage(new Point(0,0));
            var expected = new [,] {{new Point(0, -1), new Point(1, -1)}, {new Point(0, 0), new Point(1, 0)}};
            
            carriage.Move(Direction.Up);
            
            Assert.AreEqual(expected, carriage.Points);
        }
        [Test]
        public void Move_Down_AllPointsDown()
        {
            var carriage = new Carriage(new Point(0,0));
            var expected = new [,] {{new Point(0, 1), new Point(1, 1)}, {new Point(0, 2), new Point(1, 2)}};
            
            carriage.Move(Direction.Down);
            
            Assert.AreEqual(expected, carriage.Points);
        }
        [Test]
        public void Move_Left_AllPointsLeft()
        {
            var carriage = new Carriage(new Point(0,0));
            var expected = new [,] {{new Point(-1, 0), new Point(0, 0)}, {new Point(-1, 1), new Point(0, 1)}};
            
            carriage.Move(Direction.Left);
            
            Assert.AreEqual(expected, carriage.Points);
        }
        [Test]
        public void Move_Right_AllPointsRight()
        {
            var carriage = new Carriage(new Point(0,0));
            var expected = new [,] {{new Point(1, 0), new Point(2, 0)}, {new Point(1, 1), new Point(2, 1)}};
            
            carriage.Move(Direction.Right);
            
            Assert.AreEqual(expected, carriage.Points);
        }
    }
}