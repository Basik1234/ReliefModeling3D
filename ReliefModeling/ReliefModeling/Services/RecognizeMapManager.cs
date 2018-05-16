using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ReliefModeling.Model.Recognize;

namespace ReliefModeling.Services
{
    public static class RecognizeMapManager
    {
        public static IEnumerable<PieaceOfRecognizeMap> GetAroundPixels(this RecognizeMap recognizeMap, int x, int y)
        {
            var idPixels = new HashSet<PieaceOfRecognizeMap>();
            for (var i = 0; i < 3; i++)
            {
                if (recognizeMap.IsInside(new Point(x - 1 + i, y - 1))) idPixels.Add(recognizeMap[x - 1 + i, y - 1]);
                if (recognizeMap.IsInside(new Point(x + 1, y - 1 + i))) idPixels.Add(recognizeMap[x + 1, y - 1 + i]);
                if (recognizeMap.IsInside(new Point(x + i, y + 1))) idPixels.Add(recognizeMap[x + i, y + 1]);
                if (recognizeMap.IsInside(new Point(x - 1, y + i))) idPixels.Add(recognizeMap[x - 1, y + i]);
            }

            return idPixels;
        }
        
        public static PieaceOfRecognizeMap GetPixelSafe(this RecognizeMap recognizeMap, int x, int y)
        {
            return GetPixelSafe(recognizeMap, new Point(x, y));
        }
        
        public static PieaceOfRecognizeMap GetPixelSafe(this RecognizeMap recognizeMap, Point point)
        {
            return recognizeMap.IsInside(point)
                ? recognizeMap[point.X, point.Y]
                : new PieaceOfRecognizeMap {Id = Const.ID_OUTSIDE_MAP};
        }

        public static void CheckOnValid(this RecognizeMap recognizeMap, MapLegend mapLegend)
        {
            var collectionId = new HashSet<PieaceOfRecognizeMap>();

            for (var y = 0; y < recognizeMap.Height; y++)
            {
                for (var x = 0; x < recognizeMap.Width; x++)
                {
                    collectionId.Add(recognizeMap[x, y]);
                }
            }
            
            if(collectionId.Count == 0) throw new Exception("Не удалось создать RecognizeMap");
            if(collectionId.ToList().Find(element => element.Id > Const.RESERVED_ID) == null) throw new Exception("Не удалось распознать уровни по легенде");
            
        }
        
        private static bool IsInside(this RecognizeMap recognizeMap, Point point)
        {
            return point.X >= 0 && point.X < recognizeMap.Width && point.Y >= 0 && point.Y < recognizeMap.Height;
        }
    }
}