namespace ReliefModeling.Model.Recognize
{
    public class PieaceOfRecognizeMap
    {
        public int Id { get; set; }
        public bool Discover { get; set; }

        public PieaceOfRecognizeMap()
        {
            Id = 0;
            Discover = false;
        }
    }
}