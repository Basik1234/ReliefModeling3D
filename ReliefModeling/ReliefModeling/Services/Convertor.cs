using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Media.Imaging;
using OpenTK;
using ReliefModeling.Model;
using ReliefModeling.Model.Recognize;

namespace ReliefModeling.Services
{
    public static class Convertor
    {
        public static Shape To3D(this Image image2D, Image imageLegend)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var isolines = new Bitmap(image2D).GetIsolines(new Bitmap(imageLegend), AlgorithmsForSearchingIsolines.EdgeDots);
            stopWatch.Stop();
            Logger.Instance.WriteLine($"Алгоритм затратил - {stopWatch.Elapsed}");

            var vertices = (from isoline in isolines 
                            from point in isoline.Dots 
                            select new Vector3(point.X, point.Y, isoline.Level.Max)
                            ).ToList();

            var indices = Enumerable.Range(0,vertices.Count);

            return new Shape {Vertices = vertices.ToArray(), Indices = indices.ToArray()};
        }
        
        public static BitmapImage ToBitmapImage(this Image img)
        {
            using (var memory = new MemoryStream())
            {
                img.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }
        
        public static RecognizeMap ConvertColorBitmapInRecognizeMap(this Bitmap bitmap, MapLegend mapLegend)
        {
            var recognizeMap = new RecognizeMap(new int[bitmap.Width, bitmap.Height]);
            var sw = new StreamWriter("C:\\Users\\Basik\\Desktop\\Univer\\Git\\3D Relief Modeling\\ReliefModeling\\ReliefModeling\\Resource\\debug.txt");
  
            for (var y = 0; y < bitmap.Height; y++)
            {
                for (var x = 0; x < bitmap.Width; x++)
                {
                    if (bitmap.GetPixel(x, y).Equals(Const.COLOR_ISOLINE)) recognizeMap[x, y] = Const.ID_ISOLINE_MAP;
                    if (bitmap.GetPixel(x, y).Equals(Const.COLOR_DISCOVER_ISOLINE)) recognizeMap[x, y] = Const.ID_DISCOVER_ISOLINE_MAP;
                    if (mapLegend.Heights.Contains(bitmap.GetPixel(x, y)))
                        recognizeMap[x, y] = Const.RESERVED_ID+mapLegend.Heights.FindIndex(color => color == bitmap.GetPixel(x, y));
                    
                    sw.Write(recognizeMap[x,y]);
                }
                sw.WriteLine();
            }
            sw.Close();
            recognizeMap.CheckOnValid(mapLegend);
            return recognizeMap;
        }
    }
}