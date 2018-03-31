namespace ReliefModeling.Services
{
    public static class MathHelper
    {
        public static float Invert(this float count)
        {
            return count * -1;
        }

        public static float Range(this float count, int range)
        {
            return (range + count) % range;
        }
    }
}