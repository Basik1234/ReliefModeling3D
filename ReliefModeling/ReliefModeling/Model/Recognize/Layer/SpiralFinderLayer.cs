using System;
using System.Drawing;
using ReliefModeling.Model.Carriages;

namespace ReliefModeling.Model.Recognize.Layer
{
    public class SpiralFinderLayer : IFinderLayer
    {
        private RecognizeMap _recognizeMap;
        
        public SpiralFinderLayer(RecognizeMap recognizeMap)
        {
            _recognizeMap = recognizeMap;
        }

        public void FindLevelLayer(Isoline isoline)
        {
            if (isoline.Dots.Count == 0) throw new Exception("Изолиния не содержит точек");

            var spiral = new Spiral(isoline.Dots[0]);
            var point = spiral.Next();

        }
    }
}