﻿using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Narivia.Helpers;
using Narivia.Settings;

namespace Narivia.Gui
{
    /// <summary>
    /// FPS Counter
    /// </summary>
    public class FpsCounter
    {
        GameTime gameTime;
        SpriteFont fpsFont;
        Vector2 fpsCounterSize;
        string fpsString;

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>The position.</value>
        public Vector2 Position { get; set; }

        public FpsCounter()
        {
            Position = Vector2.Zero;
        }
      
        /// <summary>
        /// Loads the content.
        /// </summary>
        public void LoadContent(ContentManager content)
        {
            fpsFont = content.Load<SpriteFont>("Fonts/FrameCounterFont");
        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
        public void UnloadContent()
        {

        }

        /// <summary>
        /// Updates the content.
        /// </summary>
        /// <param name="gameTime">Game time.</param>
        public void Update(GameTime gameTime)
        {
            this.gameTime = gameTime;

            fpsString = $"FPS: {Math.Round(FramerateCounter.Instance.AverageFramesPerSecond)}";
            fpsCounterSize = fpsFont.MeasureString(fpsString);
        }

        /// <summary>
        /// Draws the content on the specified spriteBatch.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            FramerateCounter.Instance.Update(deltaTime);

            if (SettingsManager.Instance.DebugMode)
            {
                spriteBatch.DrawString(fpsFont, fpsString, new Vector2(1, 1), Color.Lime);
            }
        }
    }
}
