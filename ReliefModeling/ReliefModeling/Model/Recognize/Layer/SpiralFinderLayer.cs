using System;
using ReliefModeling.Model.Carriages;

namespace ReliefModeling.Model.Recognize.Layer
{
    public class SpiralFinderLayer : IFinderLayer
    {
        private readonly RecognizeMap _recognizeMap;
        
        public SpiralFinderLayer(RecognizeMap recognizeMap)
        {
            _recognizeMap = recognizeMap;
        }

        public void FindLevelLayer(Isoline isoline)
        {
            if (isoline.Dots.Count == 0) throw new Exception("Изолиния не содержит точек");

            var spiral = new Spiral(isoline.Dots[0]);
            var point = spiral.Next();

            /*while (true)
            {
                while (isoline.Dots.Contains(point)) point = spiral.Next();

                if (isoline.Level.Min == 0)
                {
                    isoline.Level.Min = _recognizeMap[point.X, point.Y];
                }
                if (isoline.Level.Min != _recognizeMap[point.X, point.Y])
                {
                    if (isoline.Level.Min > _recognizeMap[point.X, point.Y])
                    {
                        isoline.Level.Max = isoline.Level.Min;
                        isoline.Level.Min = _recognizeMap[point.X, point.Y];
                    }
                    else
                    {
                        isoline.Level.Max = _recognizeMap[point.X, point.Y];
                    }
                    return;
                }
            
                while (!isoline.Dots.Contains(point)) point = spiral.Next();   
            }*/
        }
    }
}