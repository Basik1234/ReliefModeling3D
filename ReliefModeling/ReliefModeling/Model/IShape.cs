using OpenTK;

namespace ReliefModeling.Model
{
    public interface IShape
    {
        Vector3[] Vertices { get; set; }
        int[] Indices { get; set; }
    }
}