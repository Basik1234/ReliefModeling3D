using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Threading;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;

namespace ReliefModeling.Model.Controls
{
    public class View3D : GLControl
    {
        #region PrivateField

        private const float PerspectiveViewNearZ = 1.0f;         //расстояние до ближней грани фрустума камеры
        private const float PerspectiveViewFarZ = 500.0f;       //расстояние до дальней грани фрустума камеры

        private Point _coordMouse;
        private IShape _shape;
        private VertexBufferObject VertexBufferObject { get;}
        private Camera Camera { get;}

        #endregion
        
        public IShape Shape
        {
            private get => _shape;
            set
            {
                _shape = value;
                if(VertexBufferObject.VertexBufferId > 0) OnLoad(EventArgs.Empty);
            }
        }
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

            GL.ClearColor(Color.DimGray);
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
            GL.DrawElements(BeginMode.Points, Shape.Indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);

            GL.PopClientAttrib();
            
            SwapBuffers();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            Camera.Radius += e.Delta > 0 ? 1 : -1;
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (_coordMouse != Point.Empty)
            {
                if (Mouse.GetState()[MouseButton.Middle] || Keyboard.GetState()[Key.R])
                {
                    Camera.Longitude -= e.X - _coordMouse.X;
                    Camera.PolarDistance -= e.Y - _coordMouse.Y;
                }
            }
            _coordMouse = e.Location;
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