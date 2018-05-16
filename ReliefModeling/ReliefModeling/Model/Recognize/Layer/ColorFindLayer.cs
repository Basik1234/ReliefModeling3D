using System;
using System.Linq;

namespace ReliefModeling.Model.Recognize.Layer
{
    public class ColorFindLayer : IFinderLayer
    {
        private readonly RecognizeMap _recognizeMap;
        
        public ColorFindLayer(RecognizeMap recognizeMap)
        {
            _recognizeMap = recognizeMap;
        }
        
        public void FindLevelLayer(Isoline isoline)
        {
            if (isoline.Dots.Count == 0) throw new Exception("Изолиния не содержит точек");

            isoline.Level = _recognizeMap[isoline.Dots.First().X, isoline.Dots.First().Y].Id;
        }
    }
}