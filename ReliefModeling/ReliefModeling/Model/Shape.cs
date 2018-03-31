using OpenTK;
 
 namespace ReliefModeling.Model
 {
     public class Shape
     {
         public Vector3[] Vertices { get; set; }
         public int[] Indices { get; set; }

         public Shape()
         {
             Vertices = new[] {new Vector3(0f, 0f, 0f)};
             Indices = new[] {0};
         }
     }
 }