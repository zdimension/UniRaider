﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace UniRaider
{
    /// <summary>
    /// Description of a single vertex array attribute.
    /// </summary>
    public struct VertexArrayAttribute
    {
        /// <summary>
        /// The number of the attribute, used to match it to the shader.
        /// </summary>
        public int Index;

        /// <summary>
        /// The number of components of the attribute.
        /// </summary>
        public int Size;

        /// <summary>
        /// The type of the attribute.
        /// </summary>
        public VertexAttribPointerType Type;

        /// <summary>
        /// Only for integer types: Whether the attribute is normalized to [0; 1] range (unsigned) or [-1; 1] range (signed).
        /// </summary>
        public bool Normalized;

        /// <summary>
        /// The VBO in which the attribute is stored. It is not legal to delete this vbo while it is referenced here, and it will not be deleted when the VAO is.
        /// </summary>
        public uint VBO;

        /// <summary>
        /// The stride of the attribute in bytes. 0 is not allowed.
        /// </summary>
        public int Stride;

        /// <summary>
        /// The start offset of the attribute within its vbo in bytes.
        /// </summary>
        public int Offset;

        /// <summary>
        /// Initializes a new instance of the <see cref="VertexArrayAttribute"/> class.
        /// </summary>
        public VertexArrayAttribute(int index, int size, VertexAttribPointerType type, bool normalized, uint vbo, int stride, int offset)
        {
            Index = index;
            Size = size;
            Type = type;
            Normalized = normalized;
            VBO = vbo;
            Stride = stride;
            Offset = offset;
        }
    }

    public class VertexArray
    {
        private int vertexArrayObject = 0;

        public VertexArray(uint elementVBO, VertexArrayAttribute[] attributes)
        {
            vertexArrayObject = GL.GenVertexArray();

            // TODO: assert vertexArrayObject != 0 -> Incorrect OpenGL function setup
            GL.BindVertexArray(vertexArrayObject);

            GL.BindBuffer(BufferTarget.ArrayBuffer, elementVBO);

            foreach (var cur in attributes)
            {
                // TODO: assert cur.VBO != 0
                // TODO: assert cur.Stride != 0

                GL.BindBuffer(BufferTarget.ArrayBuffer, cur.VBO);
                GL.EnableVertexAttribArray(cur.Index);
                GL.VertexAttribPointer(cur.Index, cur.Size, cur.Type, cur.Normalized, cur.Stride, cur.Offset);
            }

            GL.BindVertexArray(0);
        }

        ~VertexArray()
        {
            GL.DeleteVertexArray(vertexArrayObject);
        }

        public void Bind()
        {
            GL.BindVertexArray(vertexArrayObject);
        }
    }
}