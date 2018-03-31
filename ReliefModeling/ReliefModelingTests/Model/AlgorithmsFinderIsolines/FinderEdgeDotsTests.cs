using System.Drawing;
using ReliefModeling.Model.Recognize;

namespace ReliefModelingTests.AlgorithmsFinderIsolines
{
    public class FinderEdgeDotsTests : FinderTests<FinderEdgeDots>
    {
        protected override FinderEdgeDots GetInstance(Bitmap bitmap)
        {
            return new FinderEdgeDots(bitmap);
        }
    }
}