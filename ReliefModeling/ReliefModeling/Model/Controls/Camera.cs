using System;
using OpenTK;
using ReliefModeling.Services;

namespace ReliefModeling.Model.Controls
{
    public class Camera
    {
        private float _polarDistance;
        private float _radius;
        
        public Vector3 Up { get; private set; }
        public Vector3 Directiron { get;}
        public Vector3 Transform => new Vector3(
            (float) (Radius * Math.Round(Math.Sin(Longitude*Math.PI/180.0) * Math.Sin(PolarDistance*Math.PI/180.0), 14)),
            (float) (Radius * Math.Round(Math.Cos(PolarDistance*Math.PI/180.0), 14)),
            (float) (Radius * Math.Round(Math.Cos(Longitude*Math.PI/180.0) * Math.Sin(PolarDistance*Math.PI/180.0), 14)));
        
        //TODO при перевороте камеры не меняется set у longitude
        public float Longitude { get; set; }     // долгота в градусах
        public float Radius                      // расстояние между Direction и Transform;
        {
            get => _radius;
            set => _radius = _radius + value < 0 ? 0 : value;
        }        
        public float PolarDistance               // широта в градусах
        {
            get => _polarDistance;
            set
            {
                var limitedValue = value.Range(360);
                if (CoverPolarPoint(_polarDistance, limitedValue)) TurnOver();
                _polarDistance = limitedValue;
            }
        }

        public Camera()
            : this(7, 90, 0){}

        public Camera(int radius, int polarDistance, int longitude)
        {
            Directiron = Vector3.Zero;
            Up = Vector3.UnitY;

            Radius = radius;
            PolarDistance = polarDistance;
            Longitude = longitude;
        }

        private void TurnOver()
        {
            Up = new Vector3(Up.X,Up.Y.Invert(),Up.Z);
        }
        private static bool CoverPolarPoint(float before, float after)
        {
            return !((int) (before / 180)).Equals((int) (after / 180));
        }
    }
}