using OpenTK;

namespace ReliefModeling.Model.Controls
{
    public class Camera
    {
        public Vector3 Directiron { get; set; }
        public Vector3 Transform { get; set; }
        public Vector3 Up { get; set; }

        public Camera()
        {
            Directiron = new Vector3(0,0,0);
            Transform = new Vector3(0,0,7);
            Up = new Vector3(0,1,0);
        }
    }
}