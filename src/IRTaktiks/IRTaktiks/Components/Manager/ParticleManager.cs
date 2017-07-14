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

namespace IRTaktiks.Components.Manager
{
    /// <summary>
    /// Manager of particle effects.
    /// </summary>
    public class ParticleManager
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
        /// The texture used to draw the particles.
        /// </summary>
        private Texture2D TextureField;

        /// <summary>
        /// The texture used to draw the particles.
        /// </summary>
        public Texture2D Texture
        {
            get { return TextureField; }
            set { TextureField = value; }
        }

        /// <summary>
        /// The effect applied over the particles.
        /// </summary>
        private Effect EffectField;

        /// <summary>
        /// The effect applied over the particles.
        /// </summary>
        public Effect Effect
        {
            get { return EffectField; }
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
        /// The position of the observer.
        /// </summary>
        private Vector3 ObserverField;

        /// <summary>
        /// The position of the observer.
        /// </summary>
        public Vector3 Observer
        {
            get { return ObserverField; }
        }

        /// <summary>
        /// The scale of the particles.
        /// </summary>
        private Vector3 ScaleField;

        /// <summary>
        /// The scale of the particles.
        /// </summary>
        public Vector3 Scale
        {
            get { return ScaleField; }
        }

        /// <summary>
        /// The rotation quaternion.
        /// </summary>
        private Quaternion RotationField;

        /// <summary>
        /// The rotation quaternion.
        /// </summary>
        public Quaternion Rotation
        {
            get { return RotationField; }
        }

        /// <summary>
        /// The list of particle effects to be drawn.
        /// </summary>
        private List<ParticleEffect> ParticleEffectsField;

        /// <summary>
        /// The list of particle effects to be drawn.
        /// </summary>
        public List<ParticleEffect> ParticleEffects
        {
            get { return ParticleEffectsField; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of the class.
        /// </summary>
        /// <param name="observer">The observer position.</param>
        /// <param name="graphicsDevice">The graphics device of the game.</param>
        /// <param name="camera">The camera of the game.</param>
        public ParticleManager(Vector3 observer, GraphicsDevice graphicsDevice, Camera camera)
        {
            this.ObserverField = observer;

            this.ScaleField = Vector3.One;
            this.RotationField = new Quaternion(0, 0, 0, 1);

            this.GraphicsDeviceField = graphicsDevice;
            this.CameraField = camera;

            this.TextureField = TextureManager.Instance.Textures.Particle;
            this.EffectField = EffectManager.Instance.ParticleEffect;

            this.VertexDeclarationField = new VertexDeclaration(this.GraphicsDevice, ParticleEffect.Particle.VertexElements);

            this.Effect.Parameters["particleTexture"].SetValue(this.Texture);

            this.ParticleEffectsField = new List<ParticleEffect>(20);

            Vector3 axis = Vector3.Transform(Vector3.Left, Matrix.CreateFromQuaternion(this.Rotation));
            this.RotationField = Quaternion.Normalize(Quaternion.CreateFromAxisAngle(Vector3.Left, MathHelper.PiOver2) * this.Rotation);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Queue the particle effect to be updated and drawn at the flush.
        /// </summary>
        /// <param name="particleEffect">The particle effect to be updated and drawed.</param>
        public void Queue(ParticleEffect particleEffect)
        {
            this.ParticleEffects.Add(particleEffect);
        }

        /// <summary>
        /// Update all the particles, removing the dead ones.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            for (int index = 0; index < this.ParticleEffects.Count; index++)
            {
                if (this.ParticleEffects[index] != null)
                {
                    if (this.ParticleEffects[index].Alive)
                    {
                        this.ParticleEffects[index].Update(gameTime);
                    }
                    else
                    {
                        this.ParticleEffects.RemoveAt(index);
                    }
                }
            }
        }

        /// <summary>
        /// Draw all particles.
        /// </summary>
        public void Draw()
        {
            if (this.ParticleEffects.Count > 0)
            {
                // Save the render state of the graphics device.                
                bool pointSpriteEnable = this.GraphicsDevice.RenderState.PointSpriteEnable;
                bool alphaBlendEnable = this.GraphicsDevice.RenderState.AlphaBlendEnable;
                Blend destinationBlend = this.GraphicsDevice.RenderState.DestinationBlend;
                bool depthBufferWriteEnable = this.GraphicsDevice.RenderState.DepthBufferWriteEnable;

                // Set new properties to the render state.
                this.GraphicsDevice.RenderState.PointSpriteEnable = true;
                this.GraphicsDevice.RenderState.AlphaBlendEnable = true;
                this.GraphicsDevice.RenderState.DestinationBlend = Blend.One;
                this.GraphicsDevice.RenderState.DepthBufferWriteEnable = false;

                this.GraphicsDevice.VertexDeclaration = this.VertexDeclaration;

                Matrix worldViewProjection = (Matrix.CreateScale(this.Scale) * Matrix.CreateFromQuaternion(this.Rotation) * Matrix.CreateTranslation(this.Observer)) * this.Camera.ParticleView * this.Camera.ParticleProjection;

                // Set the effect properties.
                this.Effect.Parameters["Projection"].SetValue(this.Camera.ParticleProjection);
                this.Effect.Parameters["ParticleSize"].SetValue(0.5f);
                this.Effect.Parameters["ViewportHeight"].SetValue(this.GraphicsDevice.Viewport.Height);
                this.Effect.Parameters["WorldViewProj"].SetValue(worldViewProjection);

                this.Effect.Begin();

                // For all passes in the effect.
                for (int pass = 0; pass < this.Effect.CurrentTechnique.Passes.Count; pass++)
                {
                    this.Effect.CurrentTechnique.Passes[pass].Begin();

                    // For all particles to draw.
                    for (int index = 0; index < this.ParticleEffects.Count; index++)
                    {
                        this.GraphicsDevice.DrawUserPrimitives<ParticleEffect.Particle>(PrimitiveType.PointList, this.ParticleEffects[index].Particles, 0, this.ParticleEffects[index].Particles.Length);
                    }
                    
                    this.Effect.CurrentTechnique.Passes[pass].End();
                }

                this.Effect.End();

                // Restore the render state.
                this.GraphicsDevice.RenderState.PointSpriteEnable = pointSpriteEnable;
                this.GraphicsDevice.RenderState.AlphaBlendEnable = alphaBlendEnable;
                this.GraphicsDevice.RenderState.DestinationBlend = destinationBlend;
                this.GraphicsDevice.RenderState.DepthBufferWriteEnable = depthBufferWriteEnable;
            }
        }

        #endregion
    }
}
