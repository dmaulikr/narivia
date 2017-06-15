﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Narivia.Graphics;
using Narivia.Graphics.ImageEffects;
using Narivia.Input;
using Narivia.Input.Enumerations;

namespace Narivia.Interface
{
    public class Cursor
    {
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>The position.</value>
        public Vector2 Position { get; private set; }

        Image image;

        /// <summary>
        /// Loads the content.
        /// </summary>
        public void LoadContent()
        {
            image = new Image
            {
                Effects = "AnimationEffect",
                Active = true
            };

            MouseButtonState leftButtonState = InputManager.Instance.GetMouseButtonState(MouseButton.LeftButton);

            switch (leftButtonState)
            {
                case MouseButtonState.Pressed:
                    image.ImagePath = "Cursors/click";
                    break;

                default:
                    image.ImagePath = "Cursors/idle";
                    break;
            }

            image.AnimationEffect = new AnimationEffect
            {
                FrameAmount = new Vector2(8, 1),
                SwitchFrame = 150
            };

            image.LoadContent();
        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
        public void UnloadContent()
        {
            image.UnloadContent();
        }

        /// <summary>
        /// Updates the content.
        /// </summary>
        /// <param name="gameTime">Game time.</param>
        public void Update(GameTime gameTime)
        {
            // Reload the content when the left mouse button state changes
            if (InputManager.Instance.IsMouseButtonPressed(MouseButton.LeftButton) ||
                InputManager.Instance.IsMouseButtonReleased(MouseButton.LeftButton))
            {
                LoadContent();
            }

            image.Position = InputManager.Instance.MousePosition;
            image.Update(gameTime);
        }

        /// <summary>
        /// Draws the content on the specified spriteBatch.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            image.Draw(spriteBatch);
        }
    }
}
