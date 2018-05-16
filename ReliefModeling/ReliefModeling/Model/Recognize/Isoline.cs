using System.Collections.Generic;
using System.Drawing;

namespace ReliefModeling.Model.Recognize
{
    public struct Level
    {
        private int _min;
        private int _max;

        public int Min
        {
            get => _min;
            set => _min = (value-Const.RESERVED_ID) * Const.DISTANCE_BETWEEN_LAYER;
        }
        public int Max
        {
            get => _max;
            set => _max = (value-Const.RESERVED_ID) * Const.DISTANCE_BETWEEN_LAYER;
        }
    }

    public class Isoline
    {
        public List<Point> Dots { get; set; } = new List<Point>();
        public int Level { get; set; }
        //public Level Level;
    }
}