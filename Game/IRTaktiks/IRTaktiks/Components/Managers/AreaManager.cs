using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using IRTaktiks.Components.Scenario;

namespace IRTaktiks.Components.Managers
{
    /// <summary>
    /// Manager of areas.
    /// </summary>
    public class AreaManager
    {
        #region Properties

        /// <summary>
        /// The graphics device of the game.
        /// </summary>
        private GraphicsDevice GraphicsDeviceField;

        /// <summary>
        /// The graphics device of the game.
        /// </summary>
        public GraphicsDevice GraphicsDevice
        {
            get { return GraphicsDeviceField; }
        }

        /// <summary>
        /// The camera of the game.
        /// </summary>
        private Camera CameraField;

        /// <summary>
        /// The camera of the game.
        /// </summary>
        public Camera Camera
        {
            get { return CameraField; }
        }
                        
        /// <summary>
        /// The antialiasing effect applied over the area.
        /// </summary>
        private Effect EffectField;

        /// <summary>
        /// The antialiasing effect applied over the area.
        /// </summary>
        public Effect Effect
        {
            get { return EffectField; }
        }

        /// <summary>
        /// The vertex buffer.
        /// </summary>
        private VertexBuffer VertexBufferField;

        /// <summary>
        /// The vertex buffer.
        /// </summary>
        public VertexBuffer VertexBuffer
        {
            get { return VertexBufferField; }
        }

        /// <summary>
        /// The index buffer.
        /// </summary>
        private IndexBuffer IndexBufferField;

        /// <summary>
        /// The index buffer.
        /// </summary>
        public IndexBuffer IndexBuffer
        {
            get { return IndexBufferField; }
        }

        /// <summary>
        /// The vertex declaration.
        /// </summary>
        private VertexDeclaration VertexDeclarationField;

        /// <summary>
        /// The vertex declaration.
        /// </summary>
        public VertexDeclaration VertexDeclaration
        {
            get { return VertexDeclarationField; }
        }

        /// <summary>
        /// The number of vertices.
        /// </summary>
        private int VerticesNumberField;

        /// <summary>
        /// The number of vertices.
        /// </summary>
        public int VerticesNumber
        {
            get { return VerticesNumberField; }
        }

        /// <summary>
        /// The number of indices.
        /// </summary>
        private int IndicesNumberField;

        /// <summary>
        /// The number of indices.
        /// </summary>
        public int IndicesNumber
        {
            get { return IndicesNumberField; }
        }

        /// <summary>
        /// The number of primitives.
        /// </summary>
        private int PrimitivesNumberField;

        /// <summary>
        /// The number of primitives.
        /// </summary>
        public int PrimitivesNumber
        {
            get { return PrimitivesNumberField; }
        }

        /// <summary>
        /// The number of bytes per vertices.
        /// </summary>
        private int BytesPerVerticesField;

        /// <summary>
        /// The number of bytes per vertices.
        /// </summary>
        public int BytesPerVertices
        {
            get { return BytesPerVerticesField; }
        }

        /// <summary>
        /// The list of areas to be drawn.
        /// </summary>
        private List<Area> AreasField;

        /// <summary>
        /// The list of areas to be drawn.
        /// </summary>
        public List<Area> Areas
        {
            get { return AreasField; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of the class.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device of the game.</param>
        /// <param name="camera">The camera of the game.</param>
        public AreaManager(GraphicsDevice graphicsDevice, Camera camera)
        {
            this.AreasField = new List<Area>(10);

            this.GraphicsDeviceField = graphicsDevice;
            this.CameraField = camera;

            this.EffectField = EffectManager.Instance.AliasingEffect;
            
            this.CreateAreaMesh();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Create the mesh of the area.
        /// </summary>
        private void CreateAreaMesh()
        {
            int wedgesNumber = 72;
            this.VerticesNumberField = wedgesNumber + 1;
            this.BytesPerVerticesField = VertexPositionTexture.SizeInBytes;
            
            VertexPositionTexture[] polygon = new VertexPositionTexture[this.VerticesNumber];

            polygon[0] = new VertexPositionTexture(new Vector3(0, 0, 0), new Vector2(0, 0));

            for (int index = 1; index < this.VerticesNumber; index++)
            {
                float theta = (float)(index - 1) / wedgesNumber * 2 * (float)Math.PI;
                float radius = 1.0f;
                float x = (float)Math.Cos(theta);
                float y = (float)Math.Sin(theta);

                polygon[index] = new VertexPositionTexture(new Vector3(x, y, 0), new Vector2(theta, radius));
            }

            this.VertexBufferField = new VertexBuffer(this.GraphicsDevice, this.VerticesNumber * this.BytesPerVertices, BufferUsage.None);
            this.VertexBuffer.SetData<VertexPositionTexture>(polygon);

            this.VertexDeclarationField = new VertexDeclaration(this.GraphicsDevice, VertexPositionTexture.VertexElements);

            this.PrimitivesNumberField = wedgesNumber;
            this.IndicesNumberField = 3 * this.PrimitivesNumber;

            this.IndexBufferField = new IndexBuffer(this.GraphicsDevice, this.IndicesNumber * 2, BufferUsage.None, IndexElementSize.SixteenBits);

            short[] indices = new short[this.IndicesNumber];

            for (int pIndex = 0, iIndex = 0; pIndex < this.PrimitivesNumber; pIndex++)
            {
                indices[iIndex++] = 0;
                indices[iIndex++] = (short)(pIndex + 1);
                
                if (pIndex == this.PrimitivesNumber - 1)
                {
                    indices[iIndex++] = 1;
                }
                else
                {
                    indices[iIndex++] = (short)(pIndex + 2);
                }
            }

            this.IndexBuffer.SetData<short>(indices);
        }

        /// <summary>
        /// Queue the area to be drawn at the flush.
        /// </summary>
        /// <param name="area">The area to be drawed.</param>
        public void Draw(Area area)
        {
            this.Areas.Add(area);
        }

        /// <summary>
        /// Flushes all queued areas to be drawn.
        /// </summary>
        public void Flush()
        {
            if (this.Areas.Count > 0)
            {
                Matrix world;
                Matrix worldViewProjection;

                this.Effect.CurrentTechnique = this.Effect.Techniques[0];
                this.Effect.Begin();

                EffectPass pass = this.Effect.CurrentTechnique.Passes[0];

                this.GraphicsDevice.VertexDeclaration = this.VertexDeclaration;
                this.GraphicsDevice.Vertices[0].SetSource(this.VertexBuffer, 0, this.BytesPerVertices);
                this.GraphicsDevice.Indices = this.IndexBuffer;

                pass.Begin();

                for (int index = 0; index < this.Areas.Count; index++)
                {
                    world = this.Areas[index].World;
                    worldViewProjection = world * this.Camera.AreaView * this.Camera.AreaProjection;

                    this.Effect.Parameters["worldViewProj"].SetValue(worldViewProjection);
                    this.Effect.Parameters["color"].SetValue(this.Areas[index].Color.ToVector4());
                    this.Effect.Parameters["blurThreshold"].SetValue(0.95f);

                    this.Effect.CommitChanges();

                    this.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, this.VerticesNumber, 0, this.PrimitivesNumber);
                }

                pass.End();

                this.Effect.End();

                this.Areas.Clear();
            }
        }

        #endregion
    }
}
