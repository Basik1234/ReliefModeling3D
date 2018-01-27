using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Threading;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace ReliefModeling.Model.Controls
{
    public class View3D : GLControl
    {
        #region PrivateField

        private const float PerspectiveViewNearZ = 1.0f;
        private const float PerspectiveViewFarZ = 64.0f;
        
        private VertexBufferObject _vertexBufferObject;

        #endregion
        
        public Shape Shape { private get; set; }
        
        public View3D()
        {
            Shape = new Shape();
            
            var timer = new DispatcherTimer {Interval = TimeSpan.FromMilliseconds(1)};
            timer.Tick += TimerOnTick;
            timer.Start();
        }
        
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            
            GL.Viewport(0, 0, Width, Height);

            GL.MatrixMode(MatrixMode.Projection);
            var p = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, Width / (float)Height, PerspectiveViewNearZ, PerspectiveViewFarZ);
            GL.LoadMatrix(ref p);

            GL.MatrixMode(MatrixMode.Modelview);
            //TODO рефакторинг
            var mv = Matrix4.LookAt(new Vector3(0,0,7), Vector3.Zero, new Vector3(1,0,0));
            GL.LoadMatrix(ref mv);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(0.1f, 0.1f, 0.5f, 0.0f);
            GL.Enable(EnableCap.DepthTest);
            
            GL.GenBuffers(1, out _vertexBufferObject.VertexBufferId);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject.VertexBufferId);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Shape.Vertices.Length * Vector3.SizeInBytes), Shape.Vertices, BufferUsageHint.DynamicDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            GL.GenBuffers(1, out _vertexBufferObject.ElementBufferId);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _vertexBufferObject.ElementBufferId);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(Shape.Indices.Length * sizeof(int)), Shape.Indices, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);            
            GL.Clear(
                ClearBufferMask.ColorBufferBit |
                ClearBufferMask.DepthBufferBit |
                ClearBufferMask.StencilBufferBit);
            
            GL.PushClientAttrib(ClientAttribMask.ClientVertexArrayBit);
            
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject.VertexBufferId);
            GL.VertexPointer(3, VertexPointerType.Float, Vector3.SizeInBytes, IntPtr.Zero);
            GL.EnableClientState(EnableCap.VertexArray);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _vertexBufferObject.ElementBufferId);
            GL.DrawElements(BeginMode.Lines, Shape.Indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);

            GL.PopClientAttrib();
            
            SwapBuffers();
        }
        
        private void TimerOnTick(object sender, EventArgs e)
        { 
            Invalidate();
        }
        
    }
}