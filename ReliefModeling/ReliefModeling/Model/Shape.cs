using System;
using OpenTK;
 
 namespace ReliefModeling.Model
 {
     public class Shape
     {
         public Vector3[] Vertices { get; set; }
         public int[] Indices { get; set; }

         public Shape()
         {
             Vertices = new[]
             {
                 new Vector3(-1.0f, -1.0f,  1.0f),
                 new Vector3( 1.0f, -1.0f,  1.0f),
                 new Vector3( 1.0f,  1.0f,  1.0f),
                 new Vector3(-1.0f,  1.0f,  1.0f),
                 new Vector3(-1.0f, -1.0f, -1.0f),
                 new Vector3( 1.0f, -1.0f, -1.0f),
                 new Vector3( 1.0f,  1.0f, -1.0f),
                 new Vector3(-1.0f,  1.0f, -1.0f)
             };

             Indices = new[]
             {
                 ///////
                 0,1,
                 1,2,
                 2,3,
                 3,0,
                 ///////
                 7,6,
                 4,5,
                 4,7,
                 5,6,
                 //////
                 3,7,
                 0,4,
                 2,6,
                 1,5
             };
         }
     }
 }