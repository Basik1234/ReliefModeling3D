using System;
using System.Windows.Forms;
using System.Windows.Threading;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace ReliefModeling.Model.Controls
{
    public class View3D : GLControl
    {
        #region PrivateField

        private const float PerspectiveViewNearZ = 1.0f;         //расстояние до ближней грани фрустума камеры
        private const float PerspectiveViewFarZ = 64.0f;         //расстояние до дальней грани фрустума камеры
        
        private VertexBufferObject VertexBufferObject { get; set; }
        private Camera Camera { get; set; }

        #endregion
        
        public Shape Shape { private get; set; }
        
        public View3D()
        {
            VertexBufferObject = new VertexBufferObject();
            Camera = new Camera();
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

            ActionOnCamera();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(0.1f, 0.1f, 0.5f, 0.0f);
            GL.Enable(EnableCap.DepthTest);
            
            GL.GenBuffers(1, out VertexBufferObject.VertexBufferId);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject.VertexBufferId);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Shape.Vertices.Length * Vector3.SizeInBytes), Shape.Vertices, BufferUsageHint.DynamicDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            GL.GenBuffers(1, out VertexBufferObject.ElementBufferId);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, VertexBufferObject.ElementBufferId);
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
            
            ActionOnCamera();
            
            GL.PushClientAttrib(ClientAttribMask.ClientVertexArrayBit);
            
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject.VertexBufferId);
            GL.VertexPointer(3, VertexPointerType.Float, Vector3.SizeInBytes, IntPtr.Zero);
            GL.EnableClientState(EnableCap.VertexArray);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, VertexBufferObject.ElementBufferId);
            GL.DrawElements(BeginMode.Lines, Shape.Indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);

            GL.PopClientAttrib();
            
            SwapBuffers();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            Camera.Transform = e.Delta > 0
                ? new Vector3(0, 0, Camera.Transform.Z + 1)
                : new Vector3(0, 0, Camera.Transform.Z - 1);
        }

        private void TimerOnTick(object sender, EventArgs e)
        { 
            Invalidate();
        }

        private void ActionOnCamera()
        {
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            var modelview = Matrix4.LookAt(Camera.Transform, Camera.Directiron, Camera.Up);
            GL.LoadMatrix(ref modelview);
        }
        
    }
}