using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;
using OpenTK;
using ReliefModeling.Model;
using ReliefModeling.Model.Recognize;

namespace ReliefModeling.Services
{
    public static class Convertor
    {
        public static Shape To3D(this Image image2D)
        {
            var isolines = new Bitmap(image2D).GetIsolines(AlgorithmsForSearchingIsolines.EdgeDots);

            var vertices = (from isoline in isolines 
                            from point in isoline.Dots 
                            select new Vector3(point.X, point.Y, isoline.LevelBottom)
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
    }
}