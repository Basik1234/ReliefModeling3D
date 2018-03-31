using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NUnit.Framework;
using ReliefModeling.Services;

namespace ReliefModelingTests.Services
{
    [TestFixture]
    public class ImageManagerTests
    {
        [Test]
        public void GetAroundPixels_ColorAroundDotX1Y1EqualListColors_YesReturn()
        {
            var point = new Point(1, 1);
            var bitmap = new Bitmap("C:\\Users\\Basik\\Desktop\\Univer\\Git\\3D Relief Modeling\\ReliefModeling\\ReliefModelingTests\\ResourceTests\\2.bmp");
            var listColor = new List<Color>
            {
                Color.FromArgb(34,177,76),
                Color.FromArgb(255,127,39), 
                Color.FromArgb(163,73,164),
                Color.FromArgb(0,0,0),
                Color.FromArgb(237,28,36),
                Color.FromArgb(0,162,232),
                Color.FromArgb(185,122,87),
                Color.FromArgb(136,0,21)
            };

            var result = bitmap.GetAroundPixels(point.X, point.Y);

            CollectionAssert.AreEqual(listColor,result);
        }
        
        [Test]
        public void GetAroundPixels_ColorAroundDotX1Y1ContainsColors_YesReturn()
        {
            var point = new Point(1, 1);
            var bitmap = new Bitmap("C:\\Users\\Basik\\Desktop\\Univer\\Git\\3D Relief Modeling\\ReliefModeling\\ReliefModelingTests\\ResourceTests\\2.bmp");
            var listColor = new List<Color>
            {
                Color.FromArgb(34,177,76),
                Color.FromArgb(255,127,39), 
                Color.FromArgb(163,73,164),
                Color.FromArgb(0,0,0),
                Color.FromArgb(237,28,36),
                Color.FromArgb(0,162,232),
                Color.FromArgb(185,122,87),
                Color.FromArgb(136,0,21)
            };

            var result = bitmap.GetAroundPixels(point.X, point.Y);

            foreach (var color in listColor)
            {
                Assert.Contains(color,result.ToList());
            }
        }
    }
}