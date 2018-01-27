using System;
using System.Windows.Forms;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace ReliefModeling.Model.Controls
{
    public class View3D : GLControl
    {
        private Shape _shape;

        public Shape Shape
        {
            get => _shape;
            set => _shape = value;
        }

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

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            GL.ClearColor(Color.Blue);
            
            GL.Clear(
                ClearBufferMask.ColorBufferBit |
                ClearBufferMask.DepthBufferBit |
                ClearBufferMask.StencilBufferBit);
            
            SwapBuffers();
        }
    }
}