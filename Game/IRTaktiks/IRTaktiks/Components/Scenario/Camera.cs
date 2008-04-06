using System;
using System.Collections.Generic;
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
    /// Representation of the camera.
    /// </summary>
    public class Camera : GameComponent
    {
        #region Properties

        /// <summary>
        /// Camera arc.
        /// </summary>
        private float CameraArcField;
        
        /// <summary>
        /// Camera arc.
        /// </summary>
        public float CameraArc
        {
            get { return CameraArcField; }
        }

        /// <summary>
        /// Camera rotation.
        /// </summary>
        private float CameraRotationField;
        
        /// <summary>
        /// Camera rotation.
        /// </summary>
        public float CameraRotation
        {
            get { return CameraRotationField; }
        }

        /// <summary>
        /// Camera distance.
        /// </summary>
        private float CameraDistanceField;

        /// <summary>
        /// Camera distance.
        /// </summary>
        public float CameraDistance
        {
            get { return CameraDistanceField; }
        }

        /// <summary>
        /// Vision matrix of the camera.
        /// </summary>
        public Matrix View
        {
            get
            {
                return Matrix.CreateTranslation(0, -10, 0) * Matrix.CreateRotationY(MathHelper.ToRadians(this.CameraRotation)) * Matrix.CreateRotationX(MathHelper.ToRadians(this.CameraArc)) * Matrix.CreateLookAt(new Vector3(0, 0, -this.CameraDistance), new Vector3(0, 0, 0), Vector3.Up);
            }
        }

        /// <summary>
        /// Projection matrix of the camera.
        /// </summary>
        public Matrix Projection
        {
            get
            {
                return Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)(Game.Window.ClientBounds.Width / Game.Window.ClientBounds.Height), 1, 10000);
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor of class.
        /// </summary>
        /// <param name="game">The instance of game that is running.</param>
        public Camera(Game game)
            : base(game)
        {
            this.CameraArcField = -65;
            this.CameraRotationField = 0;
            this.CameraDistanceField = 7800;
        }

        #endregion

        #region Component Methods

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            /*
            KeyboardState currentKeyboardState = Keyboard.GetState();
            GamePadState currentGamePadState = GamePad.GetState(PlayerIndex.One);

            float time = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            // Check for input to rotate the camera up and down around the model.
            if (currentKeyboardState.IsKeyDown(Keys.Up) || currentKeyboardState.IsKeyDown(Keys.W))
            {
                this.CameraArcField += time * 0.1f;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Down) || currentKeyboardState.IsKeyDown(Keys.S))
            {
                this.CameraArcField -= time * 0.1f;
            }

            this.CameraArcField += currentGamePadState.ThumbSticks.Right.Y * time * 0.05f;

            // Limit the arc movement.
            if (this.CameraArcField > 90.0f)
            {
                this.CameraArcField = 90.0f;
            }
            else if (this.CameraArcField < -90.0f)
            {
                this.CameraArcField = -90.0f;
            }

            // Check for input to rotate the camera around the model.
            if (currentKeyboardState.IsKeyDown(Keys.Right) ||
                currentKeyboardState.IsKeyDown(Keys.D))
            {
                this.CameraRotationField += time * 0.1f;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Left) ||
                currentKeyboardState.IsKeyDown(Keys.A))
            {
                this.CameraRotationField -= time * 0.1f;
            }

            this.CameraRotationField += currentGamePadState.ThumbSticks.Right.X * time * 0.05f;

            // Check for input to zoom camera in and out.
            if (currentKeyboardState.IsKeyDown(Keys.Z))
            {
                this.CameraDistanceField += time * 0.25f;
            }

            if (currentKeyboardState.IsKeyDown(Keys.X))
            {
                this.CameraDistanceField -= time * 0.25f;
            }

            this.CameraDistanceField += currentGamePadState.Triggers.Left * time * 0.25f;
            this.CameraDistanceField -= currentGamePadState.Triggers.Right * time * 0.25f;

            // Limit the arc movement.
            if (this.CameraDistanceField > 10000.0f)
            {
                this.CameraDistanceField = 10000.0f;
            }
            else if (this.CameraDistanceField < 10.0f)
            {
                this.CameraDistanceField = 10.0f;
            }

            if (currentGamePadState.Buttons.RightStick == ButtonState.Pressed || currentKeyboardState.IsKeyDown(Keys.R))
            {
                this.CameraArcField = -30;
                this.CameraRotationField = 0;
                this.CameraDistanceField = 100;
            }

            this.Game.Window.Title = String.Format("ARC: {0} - DISTANCE: {1} - ROTATION: {2}", this.CameraArc, this.CameraDistance, this.CameraRotation);
            */
            base.Update(gameTime);
        }

        #endregion
    }
}


