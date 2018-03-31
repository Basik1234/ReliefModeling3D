namespace ReliefModeling.Model.Recognize
{
    public class RecognizeMap
    {
        private int[,] _map;

        public int Width => _map.GetLength(0);
        public int Height => _map.GetLength(1);

        public int this[int x, int y]
        {
            get => _map[x, y];
            set => _map[x, y] = value;
        }

        public RecognizeMap(int[,] map)
        {
            _map = map;
        }
    }
}