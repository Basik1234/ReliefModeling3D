using System;
using System.Collections.Generic;
using System.Drawing;
using ReliefModeling.Services;

namespace ReliefModeling.Model.Recognize
{
    public class FinderFullDots : IFinderIsolines
    {
        private readonly Bitmap _bitmap;
        private int _recursionDepth;
        
        public FinderFullDots(Bitmap bitmap)
        {
            _bitmap = bitmap;
        }
        
        public List<Isoline> Find()
        {
            var isolines = new List<Isoline>();
            for (var x = 0; x < _bitmap.Width; x++)
            {
                for (var y = 0; y < _bitmap.Height; y++)
                {
                    if (!_bitmap.GetPixel(x, y).ToArgb().Equals(Const.COLOR_ISOLINE.ToArgb())) continue;
                    var isoline = new Isoline{Dots = new List<Point>{new Point(x,y)}};
                    isolines.Add(isoline);
                    FindIsolineNodes(isoline.Dots);
                }
            }

            return isolines;
        }
        
        private void FindIsolineNodes(IList<Point> dots)
        {
            _recursionDepth++;
            
            if(_recursionDepth >= Const.MAX_RECURSIVE_CALLS)throw new Exception("Слишком глубокая рекурсия");
            
            var x = dots[dots.Count - 1].X;
            var y = dots[dots.Count - 1].Y;
            
            _bitmap.SetPixel(x, y, Color.White);

            if (_bitmap.GetPixelSafe(x - 1, y - 1).ToArgb().Equals(Const.COLOR_ISOLINE.ToArgb()))
            {
                dots.Add(new Point(x - 1, y - 1));
                FindIsolineNodes(dots);
            }

            if (_bitmap.GetPixelSafe(x, y - 1).ToArgb().Equals(Const.COLOR_ISOLINE.ToArgb()))
            {
                dots.Add(new Point(x, y - 1));
                FindIsolineNodes(dots);
            }

            if (_bitmap.GetPixelSafe(x + 1, y - 1).ToArgb().Equals(Const.COLOR_ISOLINE.ToArgb()))
            {
                dots.Add(new Point(x + 1, y - 1));
                FindIsolineNodes(dots);
            }

            if (_bitmap.GetPixelSafe(x + 1, y).ToArgb().Equals(Const.COLOR_ISOLINE.ToArgb()))
            {
                dots.Add(new Point(x + 1, y));
                FindIsolineNodes(dots);
            }

            if (_bitmap.GetPixelSafe(x + 1, y + 1).ToArgb().Equals(Const.COLOR_ISOLINE.ToArgb()))
            {
                dots.Add(new Point(x + 1, y + 1));
                FindIsolineNodes(dots);
            }

            if (_bitmap.GetPixelSafe(x, y + 1).ToArgb().Equals(Const.COLOR_ISOLINE.ToArgb()))
            {
                dots.Add(new Point(x, y + 1));
                FindIsolineNodes(dots);
            }

            if (_bitmap.GetPixelSafe(x - 1, y + 1).ToArgb().Equals(Const.COLOR_ISOLINE.ToArgb()))
            {
                dots.Add(new Point(x - 1, y + 1));
                FindIsolineNodes(dots);
            }

            if (_bitmap.GetPixelSafe(x - 1, y).ToArgb().Equals(Const.COLOR_ISOLINE.ToArgb()))
            {
                dots.Add(new Point(x - 1, y));
                FindIsolineNodes(dots);
            }
        }
    }
}