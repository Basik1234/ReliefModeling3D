﻿using System;
using System.Collections.Generic;
using System.Drawing;
using ReliefModeling.Model.Recognize;

namespace ReliefModeling.Services
{
    public static class ImageManager
    {
        public static IEnumerable<Isoline> GetIsolines(this Bitmap bitmap, AlgorithmsForSearchingIsolines algorithm)
        {
            IFinderIsolines finder;
            
            switch (algorithm)
            {
                case AlgorithmsForSearchingIsolines.FullDots:
                    finder = new FinderFullDots(bitmap);
                    break;
                case AlgorithmsForSearchingIsolines.EdgeDots:
                    finder = new FinderEdgeDots(bitmap);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(algorithm), algorithm, null);
            }

            var isolines = finder.Find();
            
            return isolines;
        }

        public static IEnumerable<Color> GetAroundPixels(this Bitmap bitmap, int x, int y)
        {
            var pixels = new HashSet<Color>();
            for (var i = 0; i < 3; i++)
            {
                if (bitmap.IsInside(new Point(x - 1 + i, y - 1))) pixels.Add(bitmap.GetPixel(x - 1 + i, y - 1));
                if (bitmap.IsInside(new Point(x + 1, y - 1 + i))) pixels.Add(bitmap.GetPixel(x + 1, y - 1 + i));
                if (bitmap.IsInside(new Point(x + i, y + 1))) pixels.Add(bitmap.GetPixel(x + i, y + 1));
                if (bitmap.IsInside(new Point(x - 1, y + i))) pixels.Add(bitmap.GetPixel(x - 1, y + i));
            }

            return pixels;
        }

        private static bool IsInside(this Image bitmap, Point point)
        {
            return point.X >= 0 && point.X < bitmap.Width && point.Y >= 0 && point.Y < bitmap.Height;
        }

        public static Color GetPixelSafe(this Bitmap bitmap, Point point)
        {
            return bitmap.IsInside(point) ? bitmap.GetPixel(point.X, point.Y) : Const.COLOR_OUTSIDE;
        }

        public static Color GetPixelSafe(this Bitmap bitmap, int x, int y)
        {
            return bitmap.GetPixelSafe(new Point(x, y));
        }

        public static RecognizeMap ConvertColorBitmapInRecognizeMap(this Bitmap bitmap)
        {
            var recognizeMap = new RecognizeMap(new int[bitmap.Width, bitmap.Height]);

            //TODO;
            return recognizeMap;
        }
    }
}