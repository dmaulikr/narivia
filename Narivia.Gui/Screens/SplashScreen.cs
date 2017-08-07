﻿using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Narivia.Graphics;
using Narivia.Input;
using Narivia.Input.Events;

namespace Narivia.Gui.Screens
{
    /// <summary>
    /// Splash screen.
    /// </summary>
    public class SplashScreen : Screen
    {
        /// <summary>
        /// Gets or sets the delay.
        /// </summary>
        /// <value>The delay.</value>
        public float Delay { get; set; }

        /// <summary>
        /// Gets or sets the background.
        /// </summary>
        /// <value>The background.</value>
        public Sprite BackgroundImage { get; set; }

        /// <summary>
        /// Gets or sets the overlay.
        /// </summary>
        /// <value>The overlay.</value>
        public Sprite OverlayImage { get; set; }

        /// <summary>
        /// Gets or sets the logo.
        /// </summary>
        /// <value>The logo.</value>
        public Sprite LogoImage { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SplashScreen"/> class.
        /// </summary>
        public SplashScreen()
        {
            BackgroundColour = Color.DodgerBlue;
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();

            BackgroundImage.LoadContent();
            OverlayImage.LoadContent();
            LogoImage.LoadContent();

            AlignItems();
            BackgroundImage.ActivateEffect("RotationEffect");
            BackgroundImage.ActivateEffect("ZoomEffect");

            InputManager.Instance.KeyboardKeyPressed += InputManager_OnKeyboardKeyPressed;
            InputManager.Instance.MouseButtonPressed += InputManager_OnMouseButtonPressed;
        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
        public override void UnloadContent()
        {
            base.UnloadContent();
            BackgroundImage.UnloadContent();
            OverlayImage.UnloadContent();
            LogoImage.UnloadContent();

            InputManager.Instance.KeyboardKeyPressed -= InputManager_OnKeyboardKeyPressed;
            InputManager.Instance.MouseButtonPressed -= InputManager_OnMouseButtonPressed;
        }

        /// <summary>
        /// Updates the content.
        /// </summary>
        /// <param name="gameTime">Game time.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            BackgroundImage.Update(gameTime);
            OverlayImage.Update(gameTime);
            LogoImage.Update(gameTime);

            Delay -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            AlignItems();
        }

        /// <summary>
        /// Draws the content on the specified spriteBatch.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            BackgroundImage.Draw(spriteBatch);
            OverlayImage.Draw(spriteBatch);
            LogoImage.Draw(spriteBatch);
        }

        void AlignItems()
        {
            BackgroundImage.Scale = Vector2.One * Math.Max(ScreenManager.Instance.Size.X, ScreenManager.Instance.Size.Y) /
                                                  Math.Max(BackgroundImage.SpriteSize.X, BackgroundImage.SpriteSize.Y);
            OverlayImage.Scale = new Vector2(ScreenManager.Instance.Size.X / OverlayImage.SpriteSize.X,
                                               ScreenManager.Instance.Size.Y / OverlayImage.SpriteSize.Y);

            BackgroundImage.Position = new Point((ScreenManager.Instance.Size.X - BackgroundImage.ScreenArea.Width) / 2,
                                                 (ScreenManager.Instance.Size.Y - BackgroundImage.ScreenArea.Height) / 2);
            LogoImage.Position = new Point((ScreenManager.Instance.Size.X - LogoImage.SpriteSize.X) / 2,
                                           (ScreenManager.Instance.Size.Y - LogoImage.SpriteSize.Y) / 2);
        }

        void InputManager_OnMouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            ScreenManager.Instance.ChangeScreens("TitleScreen");
        }

        void InputManager_OnKeyboardKeyPressed(object sender, KeyboardKeyEventArgs e)
        {
            ScreenManager.Instance.ChangeScreens("TitleScreen");
        }
    }
}