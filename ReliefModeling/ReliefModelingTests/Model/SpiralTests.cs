using System.Collections.Generic;
using System.Drawing;
using NUnit.Framework;
using ReliefModeling.Model.Carriages;

namespace ReliefModelingTests
{
    [TestFixture]
    public class SpiralTests
    {
        [Test]
        public void Move_Move16Iteration_PointsReturn()
        {
            var expected = new List<Point>
            {
                new Point(3,1), new Point(3,2),
                new Point(2,2), new Point(1,2), new Point(1,1),
                new Point(1,0), new Point(2,0), new Point(3,0),
                new Point(4,0), new Point(4,1), new Point(4,2),
                new Point(4,3), new Point(3,3), new Point(2,3),
                new Point(1,3), new Point(0,3)
            };

            var spiral = new Spiral(new Point(2, 1));
            var result = new List<Point>();
            for (var i = 0; i < 16; i++)
            {
                result.Add(spiral.Next());
            }
            
            CollectionAssert.AreEquivalent(expected,result);
        }
    }
}