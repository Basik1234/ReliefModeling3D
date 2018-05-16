namespace ReliefModeling.Model.Recognize
{
    public class RecognizeMap
    {
        private PieaceOfRecognizeMap[,] _map;

        public int Width => _map.GetLength(0);
        public int Height => _map.GetLength(1);

        public PieaceOfRecognizeMap this[int x, int y]
        {
            get => _map[x, y];
            set => _map[x, y] = value;
        }

        public RecognizeMap(PieaceOfRecognizeMap[,] map)
        {
            _map = map;
        }
    }
}