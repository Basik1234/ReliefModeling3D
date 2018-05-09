using System.Drawing;

namespace ReliefModeling
{
    public static class Const
    {
        public static Color COLOR_ISOLINE { get; } = Color.FromArgb(0,0,0);
        public static Color COLOR_DISCOVER_ISOLINE { get; } = Color.Aqua;
        public static Color COLOR_OUTSIDE { get; } = Color.FromArgb(255,255,254);
        public static int MAX_RECURSIVE_CALLS { get; } = 1000;

        public const int ID_ISOLINE_MAP = 1;
        public const int ID_DISCOVER_ISOLINE_MAP = 2;
        public const int ID_OUTSIDE_MAP = 3;
        public const int RESERVED_ID = 4;

        public const int DISTANCE_BETWEEN_LAYER = 5;
    }
}