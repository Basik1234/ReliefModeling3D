using System.Drawing;
using ReliefModeling.Model.Recognize;

namespace ReliefModelingTests.AlgorithmsFinderIsolines
{
    public class FinderFullDotsTests : FinderTests<FinderFullDots>
    {
        protected override FinderFullDots GetInstance(Bitmap bitmap)
        {
            return new FinderFullDots(bitmap);
        }
    }
}