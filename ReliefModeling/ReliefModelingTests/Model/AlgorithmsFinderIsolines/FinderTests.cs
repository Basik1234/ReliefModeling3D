using System.Collections.Generic;
using System.Drawing;
using NUnit.Framework;
using ReliefModeling;
using ReliefModeling.Model.Recognize;

namespace ReliefModelingTests.AlgorithmsFinderIsolines
{
    [TestFixture]
    public abstract class FinderTests<Finder> where Finder : IFinderIsolines
    {
        private const string Image = "C:\\Users\\Basik\\Desktop\\Univer\\Git\\3D Relief Modeling\\ReliefModeling\\ReliefModelingTests\\ResourceTests\\1.bmp";

        protected abstract Finder GetInstance(Bitmap bitmap);
        
        [Test]
        public void Find_FindIsolines_15IsolinesReturn()
        {
            var bitmap = new Bitmap(Image);
            var finder = GetInstance(bitmap);

            var isolines = finder.Find();
            
            Assert.AreEqual(15,isolines.Count);
        }

        [Test]
        public void Find_CheckAllPoints_IsolinesContainAllDots()
        {
            var bitmap = new Bitmap(Image);
            var finder = GetInstance(bitmap);

            var isolines = finder.Find();

            var allPoints = new List<Point>();
            for (var x = 0; x < bitmap.Width; x++)
            {
                for (var y = 0; y < bitmap.Height; y++)
                {
                    if (bitmap.GetPixel(x, y).ToArgb().Equals(Const.COLOR_ISOLINE.ToArgb()))
                        allPoints.Add(new Point(x, y));
                }
            }

            var result = 0;
            foreach (var point in allPoints)
            {
                foreach (var isoline in isolines)
                {
                    if (!isoline.Dots.Contains(point)) continue;
                    result++;
                    break;
                }
            }
            
            Assert.AreEqual(allPoints.Count,result);
        }
    }
}