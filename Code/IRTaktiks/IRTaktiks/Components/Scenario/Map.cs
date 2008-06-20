using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Content;
using IRTaktiks.Components.Manager;

namespace IRTaktiks.Components.Scenario
{
	/// <summary>
	/// Representation of the map of the game.
	/// </summary>
	public class Map : DrawableGameComponent
	{
		#region Properties

        /// <summary>
        /// Graphics device of the game.
        /// </summary>
        private GraphicsDevice DeviceField;

        /// <summary>
        /// Graphics device of the game.
        /// </summary>
        public GraphicsDevice Device
        {
            get { return DeviceField; }
        }

        /// <summary>
        /// The effect of the map
        /// </summary>
        private Effect EffectField;
        
        /// <summary>
        /// The effect of the map
        /// </summary>
        public Effect Effect
        {
            get { return EffectField; }
        }

        /// <summary>
        /// List of 3D vertices to be streamed to the graphics device.
        /// </summary>
        private VertexBuffer VertexBufferField;

        /// <summary>
        /// List of 3D vertices to be streamed to the graphics device.
        /// </summary>
        public VertexBuffer VertexBuffer
        {
            get { return VertexBufferField; }
        }

        /// <summary>
        /// Rendering order of the vertices.
        /// </summary>
        private IndexBuffer IndexBufferField;
        
        /// <summary>
        /// Rendering order of the vertices.
        /// </summary>
        public IndexBuffer IndexBuffer
        {
            get { return IndexBufferField; }
        }

        /// <summary>
        /// The vertices of the map.
        /// </summary>
        private VertexPositionNormalTexture[] VerticesField;
        
        /// <summary>
        /// The vertices of the map.
        /// </summary>
        public VertexPositionNormalTexture[] Vertices
        {
            get { return VerticesField; }
            set { VerticesField = value; }
        }

        /// <summary>
        /// Vertex declaration.
        /// </summary>
        private VertexDeclaration VertexDeclarationField;
        
        /// <summary>
        /// Vertex declaration.
        /// </summary>
        public VertexDeclaration VertexDeclaration
        {
            get { return VertexDeclarationField; }
        }

        /// <summary>
        /// The maximum height of the terrain.
        /// </summary>
        private int MaxHeightField;

        /// <summary>
        /// The maximum height of the terrain.
        /// </summary>
        public int MaxHeight
        {
            get { return MaxHeightField; }
        }

        /// <summary>
        /// Size of the cell of the terrain.
        /// </summary>
        private int CellSizeField;
        
        /// <summary>
        /// Size of the cell of the terrain.
        /// </summary>
        public int CellSize
        {
            get { return CellSizeField; }
        }

        /// <summary>
        /// Dimension of the terrain.
        /// </summary>
        private int DimensionField;
        
        /// <summary>
        /// Dimension of the terrain.
        /// </summary>
        public int Dimension
        {
            get { return DimensionField; }
        }

		#endregion

        #region Constructor

        /// <summary>
		/// Constructor of class.
		/// </summary>
		/// <param name="game">The instance of game that is running.</param>
		public Map(Game game)
			: base(game)
		{
            this.MaxHeightField = 2048;
            this.CellSizeField = 128;
            this.DimensionField = 48;

            this.EffectField = EffectManager.Instance.TerrainEffect;
		}

		#endregion

		#region Component Methods

		/// <summary>
		/// Allows the game component to perform any initialization it needs to before starting
		/// to run. This is where it can query for any required services and load content.
		/// </summary>
		public override void Initialize()
		{
            int[] indices = new int[this.Dimension * this.Dimension * 6];
            this.VerticesField = new VertexPositionNormalTexture[(this.Dimension + 1) * (this.Dimension + 1)];

            for (int i = 0; i < this.Dimension + 1; i++)
            {
                for (int j = 0; j < this.Dimension + 1; j++)
                {
                    VertexPositionNormalTexture vertice = new VertexPositionNormalTexture();
                    vertice.Position = new Vector3((i - this.Dimension / 2.0f) * this.CellSize, 0, (j - this.Dimension / 2.0f) * this.CellSize);
                    vertice.Normal = Vector3.Up;
                    vertice.TextureCoordinate = new Vector2((float)i / this.Dimension, (float)j / this.Dimension);
                    this.Vertices[i * (this.Dimension + 1) + j] = vertice;
                }
            }

            for (int i = 0; i < this.Dimension; i++)
            {
                for (int j = 0; j < this.Dimension; j++)
                {
                    indices[6 * (i * this.Dimension + j)] = (i * (this.Dimension + 1) + j);
                    indices[6 * (i * this.Dimension + j) + 1] = (i * (this.Dimension + 1) + j + 1);
                    indices[6 * (i * this.Dimension + j) + 2] = ((i + 1) * (this.Dimension + 1) + j + 1);

                    indices[6 * (i * this.Dimension + j) + 3] = (i * (this.Dimension + 1) + j);
                    indices[6 * (i * this.Dimension + j) + 4] = ((i + 1) * (this.Dimension + 1) + j + 1);
                    indices[6 * (i * this.Dimension + j) + 5] = ((i + 1) * (this.Dimension + 1) + j);
                }

            }

            IGraphicsDeviceService service = (IGraphicsDeviceService)this.Game.Services.GetService(typeof(IGraphicsDeviceService));
            this.DeviceField = service.GraphicsDevice;

            this.VertexBufferField = new VertexBuffer(this.Device, (this.Dimension + 1) * (this.Dimension + 1) * VertexPositionNormalTexture.SizeInBytes, BufferUsage.Points);
            this.IndexBufferField = new IndexBuffer(this.Device, 6 * this.Dimension * this.Dimension * sizeof(int), BufferUsage.Points, IndexElementSize.ThirtyTwoBits);
            
            this.VertexBuffer.SetData<VertexPositionNormalTexture>(this.Vertices);
            this.IndexBuffer.SetData<int>(indices);

            this.VertexDeclarationField = new VertexDeclaration(this.Device, VertexPositionNormalTexture.VertexElements);

            base.Initialize();
		}

		/// <summary>
		/// Allows the game component to update itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
		}

		/// <summary>
		/// Called when the DrawableGameComponent needs to be drawn. Override this method
		//  with component-specific drawing code.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public override void Draw(GameTime gameTime)
		{
            IGraphicsDeviceService service = (IGraphicsDeviceService)this.Game.Services.GetService(typeof(IGraphicsDeviceService));
            this.DeviceField = service.GraphicsDevice;

            this.Device.VertexDeclaration = this.VertexDeclaration;

            this.Device.Vertices[0].SetSource(this.VertexBuffer, 0, VertexPositionNormalTexture.SizeInBytes);
            this.Device.Indices = this.IndexBuffer;
            this.Device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, (this.Dimension + 1) * (this.Dimension + 1), 0, 2 * this.Dimension * this.Dimension);

            base.Draw(gameTime);
		}

		#endregion
    }
}