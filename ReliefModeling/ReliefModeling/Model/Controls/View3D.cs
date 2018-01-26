using System;
using OpenTK;

namespace ReliefModeling.Model.Controls
{
    public class View3D : GLControl
    {
        private Shape _shape;

        public View3D(Shape shape)
        {
            _shape = shape;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
    }
}