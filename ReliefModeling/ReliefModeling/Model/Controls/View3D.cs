using System;
using System.Windows.Forms;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace ReliefModeling.Model.Controls
{
    public class View3D : GLControl
    {
        public Shape Shape { get; set; } = new Shape();
        private VertexBufferObject _vertexBufferObject;
        
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(0.1f, 0.1f, 0.5f, 0.0f);
            GL.Enable(EnableCap.DepthTest);
            
            GL.GenBuffers(1, out _vertexBufferObject.VertexBufferID);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject.VertexBufferID);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Shape.Vertices.Length * Vector3.SizeInBytes), Shape.Vertices, BufferUsageHint.DynamicDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            GL.GenBuffers(1, out _vertexBufferObject.ElementBufferID);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _vertexBufferObject.ElementBufferID);
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
            
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject.VertexBufferID);
            GL.VertexPointer(3, VertexPointerType.Float, Vector3.SizeInBytes, IntPtr.Zero);
            GL.EnableClientState(EnableCap.VertexArray);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _vertexBufferObject.ElementBufferID);
            GL.DrawElements(BeginMode.Lines, Shape.Indices.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);

            GL.PopClientAttrib();
            
            SwapBuffers();
        }
    }
}