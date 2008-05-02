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

namespace IRTaktiks.Components.Scenario
{
    /// <summary>
    /// Component that represents a firework.
    /// </summary>
    public class Firework
    {
        #region Particle Struct

        /// <summary>
        /// Structure of one particle.
        /// </summary>
        public struct Particle
        {
            #region Properties

            /// <summary>
            /// The vertex elements of the particle.
            /// </summary>
            private static VertexElement[] VertexElementsField = new VertexElement[] {
                    new VertexElement(0, 0, VertexElementFormat.Vector3, VertexElementMethod.Default, VertexElementUsage.Position, 0),
                    new VertexElement(0, sizeof(float) * 3, VertexElementFormat.Color, VertexElementMethod.Default, VertexElementUsage.Color, 0),
                    new VertexElement(0, sizeof(float) * 7, VertexElementFormat.Vector4, VertexElementMethod.Default, VertexElementUsage.TextureCoordinate, 0),                
                };

            /// <summary>
            /// The vertex elements of the particle.
            /// </summary>
            public static VertexElement[] VertexElements
            {
                get { return VertexElementsField; }
            }
            
            /// <summary>
            /// The position of the particle.
            /// </summary>
            private Vector3 PositionField;
            
            /// <summary>
            /// The position of the particle.
            /// </summary>
            public Vector3 Position
            {
                get { return PositionField; }
                set { PositionField = value; }
            }

            /// <summary>
            /// The color of the particle.
            /// </summary>
            private Color ColorField;
            
            /// <summary>
            /// The color of the particle.
            /// </summary>
            public Color Color
            {
                get { return ColorField; }
                set { ColorField = value; }
            }

            /// <summary>
            /// The data of the particle.
            /// </summary>
            private Vector4 DataField;
            
            /// <summary>
            /// The data of the particle.
            /// </summary>
            public Vector4 Data
            {
                get { return DataField; }
                set { DataField = value; }
            }

            #endregion

            #region Constructor

            /// <summary>
            /// Constructor of the structure.
            /// </summary>
            /// <param name="position">The position of the particle.</param>
            /// <param name="color">The color of the particle.</param>
            public Particle(Vector3 position, Color color)
            {
                this.PositionField = position;
                this.ColorField = color;

                Random random = new Random();
                this.DataField = new Vector4(random.Next(0, 360), random.Next(0, 360), 0, 0);
            }

            #endregion
        }

        #endregion

        #region Properties

        /// <summary>
        /// The list of particles.
        /// </summary>
        private Particle[] ParticlesField;

        /// <summary>
        /// The list of particles.
        /// </summary>
        public Particle[] Particles
        {
            get { return ParticlesField; }
        }

        /// <summary>
        /// The color of the firework.
        /// </summary>
        private Color ColorField;

        /// <summary>
        /// The color of the firework.
        /// </summary>
        public Color Color
        {
            get { return ColorField; }
        }

        /// <summary>
        /// The life time of the firework, before its destruction
        /// </summary>
        private int LifeTimeField;

        /// <summary>
        /// The life time of the firework, before its destruction
        /// </summary>
        public int LifeTime
        {
            get { return LifeTimeField; }
        }
        
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of the class.
        /// </summary>
        /// <param name="particlesCount">The count of particles used to draw the firework.</param>
        /// <param name="lifeTime">The life time of the firework, before its destruction</param>
        /// <param name="color">The color of the firework.</param>
        public Firework(int particlesCount, int lifeTime, Color color)
        {
            this.LifeTimeField = lifeTime;
            this.ColorField = color;

            this.ParticlesField = new Particle[particlesCount];

            // Create the particles.
            for (int index = 0; index < particlesCount; index++)
            {
                this.Particles[index] = new Particle(new Vector3(0, 0, 0), color);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Updates the particles of the fireworks.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            for (int index = 0; index < this.Particles.Length; index++)
            {
                // Get the particle data.
                float angleX = this.Particles[index].Data.X;
                float angleY = this.Particles[index].Data.Y;
                float life = this.Particles[index].Data.Z;

                // Comment to disable rotation.
                angleX += 0.01f;
                angleY += 0.01f;

                // Mantain the angleX between 0 and 360.
                if (angleX > 360)
                {
                    angleX = 0;
                }

                // Mantain the angleY between 0 and 360.
                if (angleY > 360)
                {
                    angleY = 0;
                }

                // Increase the life time of the particle.
                life += 0.1f;

                // Calculate the new position of the particle.
                float cosX = life * (float)Math.Cos(angleX);
                float cosY = life * (float)Math.Cos(angleY);
                float sinX = life * (float)Math.Sin(angleX);
                float sinY = life * life * (float)Math.Sin(angleY);

                this.Particles[index].Position = new Vector3(cosX * cosY, sinX * sinY, sinY);

                // Calculate the new color of the particle.
                this.Particles[index].Color = new Color(new Vector4(this.Particles[index].Color.ToVector4(), 1.0f - (life / 3.0f)));

                // Kill the firework if the life time has trepassed the limit.
                if (life > this.LifeTime)
                {
                    life = -1;
                }

                // Store the particle data.
                this.Particles[index].Data = new Vector4(angleX, angleY, life, 0);
            }
        }

        #endregion

        /*
        VertexDeclaration m_vDec;

        Particle[] m_sprites;
        Texture2D myTexture;

        public Vector3 myPosition;
        public Vector3 myScale;
        public Quaternion myRotation;

        public Color particleColor;

        float myPointSize = 32f;

        Effect effect2;

        GraphicsDevice myDevice;

        public Firework(Game game)
            : base(game)
        {
            myPosition = Vector3.Zero;
            myScale = Vector3.One;
            myRotation = new Quaternion(0, 0, 0, 1);

            myDevice = game.GraphicsDevice;

            Rotate(Vector3.Left, MathHelper.PiOver2);
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            myTexture = this.Game.Content.Load<Texture2D>("Particle");

            m_vDec = new VertexDeclaration(myDevice, Particle.VertexElements);

            effect2 = this.Game.Content.Load<Effect>("ScaledParticle");

            effect2.Parameters["particleTexture"].SetValue(myTexture);

            Random m_rand = new Random();

            m_sprites = new Particle[300];

            for (int i = 0; i < m_sprites.Length; i++)
            {
                m_sprites[i] = new Particle();
                m_sprites[i].Position = new Vector3(0, 0, 0);
                m_sprites[i].Data = new Vector4(m_rand.Next(0, 360), m_rand.Next(0, 360), 0, 0);
                m_sprites[i].Color = new Color(new Vector4((float)m_rand.NextDouble(), (float)m_rand.NextDouble(), (float)m_rand.NextDouble(), 1f));
            }

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < m_sprites.Length; i++)
            {
                float angle = m_sprites[i].Data.X;
                float angle2 = m_sprites[i].Data.Y;
                float radius = m_sprites[i].Data.Z;

                angle += .01f;
                if (angle > 360)
                    angle = 0;
                angle2 += .01f;
                if (angle2 > 360)
                    angle2 = 0;

                radius += .1f;

                float cos = radius * (float)Math.Cos(angle);
                float sin = radius * (float)Math.Sin(angle);
                float cos2 = radius * (float)Math.Cos(angle2);
                float sin2 = (float)Math.Pow(radius, 2) * (float)Math.Sin(angle2);

                m_sprites[i].Position = new Vector3(cos * cos2, sin * cos2, sin2);
                m_sprites[i].Color = new Color(new Vector4(m_sprites[i].Color.ToVector3(), 1f - (radius / 3f)));

                if (radius > 15)
                    radius = 0;

                m_sprites[i].Data = new Vector4(angle, angle2, radius, 0);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            bool PointSpriteEnable = myDevice.RenderState.PointSpriteEnable;

            float PointSize = myDevice.RenderState.PointSize;

            bool AlphaBlendEnable = myDevice.RenderState.AlphaBlendEnable;
            BlendFunction AlphaBlendOperation = myDevice.RenderState.AlphaBlendOperation;
            Blend SourceBlend = myDevice.RenderState.SourceBlend;
            Blend DestinationBlend = myDevice.RenderState.DestinationBlend;
            bool SeparateAlphaBlendEnabled = myDevice.RenderState.SeparateAlphaBlendEnabled;
            bool AlphaTestEnable = myDevice.RenderState.AlphaTestEnable;
            CompareFunction AlphaFunction = myDevice.RenderState.AlphaFunction;
            int ReferenceAlpha = myDevice.RenderState.ReferenceAlpha;
            bool DepthBufferWriteEnable = myDevice.RenderState.DepthBufferWriteEnable;


            myDevice.RenderState.PointSpriteEnable = true;
            myDevice.RenderState.AlphaBlendEnable = true;
            myDevice.RenderState.DestinationBlend = Blend.One;
            myDevice.RenderState.DepthBufferWriteEnable = false;

            myDevice.VertexDeclaration = m_vDec;

            Matrix wvp = (Matrix.CreateScale(myScale) * Matrix.CreateFromQuaternion(myRotation) * Matrix.CreateTranslation(myPosition)) * Camera.myView * Camera.myProjection;

            Effect thisEffect = null;

            thisEffect = effect2;
            myPointSize = 0.5f;
            thisEffect.Parameters["Projection"].SetValue(Camera.myProjection);
            thisEffect.Parameters["ParticleSize"].SetValue(myPointSize);
            thisEffect.Parameters["ViewportHeight"].SetValue(Camera.myViewport.Height);

            thisEffect.Parameters["WorldViewProj"].SetValue(wvp);

            thisEffect.Begin();

            for (int ps = 0; ps < thisEffect.CurrentTechnique.Passes.Count; ps++)
            {
                thisEffect.CurrentTechnique.Passes[ps].Begin();
                myDevice.DrawUserPrimitives<Particle>(PrimitiveType.PointList, m_sprites, 0, m_sprites.Length);
                thisEffect.CurrentTechnique.Passes[ps].End();
            }

            thisEffect.End();

            myDevice.RenderState.PointSpriteEnable = PointSpriteEnable;
            myDevice.RenderState.AlphaBlendEnable = AlphaBlendEnable;
            myDevice.RenderState.DestinationBlend = DestinationBlend;
            myDevice.RenderState.DepthBufferWriteEnable = DepthBufferWriteEnable;

            base.Draw(gameTime);
        }

        public void Rotate(Vector3 axis, float angle)
        {
            axis = Vector3.Transform(axis, Matrix.CreateFromQuaternion(myRotation));
            myRotation = Quaternion.Normalize(Quaternion.CreateFromAxisAngle(axis, angle) * myRotation);
        }
        */
    }
}
