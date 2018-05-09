using System.Drawing;
using ReliefModeling.Model.Recognize;

namespace ReliefModelingTests.AlgorithmsFinderIsolines
{
    public class FinderEdgeDotsTests : FinderTests<FinderEdgeDots>
    {
        protected override FinderEdgeDots GetInstance(Bitmap bitmap)
        {
            //TODO 
            return new FinderEdgeDots(bitmap, bitmap);
        }
    }
}