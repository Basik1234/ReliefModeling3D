using System.Collections.Generic;
using System.Drawing;

namespace ReliefModeling.Model.Recognize
{
    public class Isoline
    {
        public List<Point> Dots { get; set; } = new List<Point>();
        public int LevelBottom { get; set; }
        public int LevelTop { get; set; }
        

    }
}