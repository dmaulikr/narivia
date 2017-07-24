﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Narivia.Graphics;
using Narivia.Graphics.ImageEffects;
using Narivia.Infrastructure.Helpers;

namespace Narivia.Gui.GuiElements
{
    /// <summary>
    /// Image GUI element.
    /// </summary>
    public class GuiImage : GuiElement
    {
        /// <summary>
        /// Gets or sets the content file.
        /// </summary>
        /// <value>The content file.</value>
        public string ContentFile { get; set; }

        /// <summary>
        /// Gets or sets the mask file.
        /// </summary>
        /// <value>The mask file.</value>
        public string MaskFile { get; set; }

        /// <summary>
        /// Gets or sets the tint colour.
        /// </summary>
        /// <value>The tint colour.</value>
        public Colour TintColour { get; set; }

        /// <summary>
        /// Gets or sets the source rectangle.
        /// </summary>
        /// <value>The source rectangle.</value>
        public Rectangle SourceRectangle { get; set; }

        /// <summary>
        /// Gets or sets the scale.
        /// </summary>
        /// <value>The scale.</value>
        public Vector2 Scale { get; set; }

        /// <summary>
        /// Gets or sets the texture fill mode.
        /// </summary>
        /// <value>The fill mode.</value>
        public TextureFillMode FillMode { get; set; }

        /// <summary>
        /// Gets or sets the fade effect.
        /// </summary>
        /// <value>The fade effect.</value>
        public FadeEffect FadeEffect { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the effects are active.
        /// </summary>
        /// <value><c>true</c> if the effects are active; otherwise, <c>false</c>.</value>
        public bool EffectsActive { get; set; }

        Image image;

        /// <summary>
        /// Initializes a new instance of the <see cref="GuiImage"/> class.
        /// </summary>
        public GuiImage()
        {
            TintColour = Colour.White;
            Scale = Vector2.One;
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        public override void LoadContent()
        {
            image = new Image
            {
                ImagePath = ContentFile,
                TransparencyMaskPath = MaskFile
            };

            SetChildrenProperties();

            image.LoadContent();

            base.LoadContent();
        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
        public override void UnloadContent()
        {
            image.UnloadContent();

            base.UnloadContent();
        }

        /// <summary>
        /// Updates the content.
        /// </summary>
        /// <param name="gameTime">Game time.</param>
        public override void Update(GameTime gameTime)
        {
            SetChildrenProperties();

            image.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the content on the specified spriteBatch.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            image.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }

        /// <summary>
        /// Activates the effect.
        /// </summary>
        /// <param name="effect">Effect.</param>
        public void ActivateEffect(string effect)
        => image.ActivateEffect(effect);

        void SetChildrenProperties()
        {
            image.SourceRectangle = SourceRectangle;
            image.Tint = TintColour;
            image.Position = Position;
            image.Scale = Scale;
            image.TextureFillMode = FillMode;
            image.FadeEffect = FadeEffect;
            image.Active = EffectsActive;

            Size = new Vector2(image.ScreenArea.Width, image.ScreenArea.Height);
        }
    }
}
