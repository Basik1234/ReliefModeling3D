using System.Collections.Generic;
using System.Drawing;
using ReliefModeling.Services;

namespace ReliefModeling.Model.Recognize
{
    public class MapLegend
    {
        private readonly Bitmap _legend;
        private List<Color> _heights;

        public List<Color> Heights
        {
            get
            {
                if (_heights == null)
                {
                    Heights = DetermineHeightByColor();
                }
                return _heights;
            }
            private set => _heights = value;
        }

        public MapLegend(Bitmap legend)
        {
            _legend = legend;
        }

        private List<Color> DetermineHeightByColor()
        {
            var heights = new List<Color>();
            var x = 7;
            //TODO хардкод
            for (var y = 26; y < _legend.Height; y++)
            {
                if (_legend.GetPixelSafe(x, y).Equals(Color.White) ||
                    _legend.GetPixelSafe(x, y).Equals(Color.Black) ||
                    heights.Contains(_legend.GetPixel(x,y))) continue;
                
                heights.Add(_legend.GetPixel(x,y));
            }

            return heights;
        }
    }
}