﻿using System.Xml.Serialization;

using Microsoft.Xna.Framework;

namespace Narivia.Graphics.SpriteEffects
{
    /// <summary>
    /// Animation sprite effect.
    /// </summary>
    public class AnimationEffect : CustomSpriteEffect
    {
        /// <summary>
        /// Gets or sets the frame counter.
        /// </summary>
        /// <value>The frame counter.</value>
        public int FrameCounter { get; set; }

        /// <summary>
        /// Gets or sets the switch frame.
        /// </summary>
        /// <value>The switch frame.</value>
        public int SwitchFrame { get; set; }

        /// <summary>
        /// Gets or sets the current frame.
        /// </summary>
        /// <value>The current frame.</value>
        public Vector2 CurrentFrame { get; set; }

        /// <summary>
        /// Gets or sets the frame amount.
        /// </summary>
        /// <value>The frame amount.</value>
        public Vector2 FrameAmount { get; set; }

        /// <summary>
        /// Gets the width of the frame.
        /// </summary>
        /// <value>The width of the frame.</value>
        [XmlIgnore]
        public int FrameWidth => (int)(Sprite.TextureSize.X / FrameAmount.X);

        /// <summary>
        /// Gets the height of the frame.
        /// </summary>
        /// <value>The height of the frame.</value>
        [XmlIgnore]
        public int FrameHeight => (int)(Sprite.TextureSize.Y / FrameAmount.Y);

        /// <summary>
        /// Initializes a new instance of the <see cref="AnimationEffect"/> class.
        /// </summary>
        public AnimationEffect()
        {
            FrameAmount = Vector2.Zero;
            CurrentFrame = Vector2.Zero;
            SwitchFrame = 100;
            FrameCounter = 0;
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="sprite">Sprite.</param>
        public override void LoadContent(ref Sprite sprite)
        {
            base.LoadContent(ref sprite);
        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        /// <summary>
        /// Update the content.
        /// </summary>
        /// <param name="gameTime">Game time.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Vector2 newFrame = CurrentFrame;

            if (Sprite.Active)
            {
                FrameCounter += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (FrameCounter >= SwitchFrame)
                {
                    FrameCounter = 0;
                    newFrame.X += 1;

                    if (CurrentFrame.X >= Sprite.TextureSize.X / FrameWidth - 1)
                    {
                        newFrame.X = 0;
                    }
                }
            }
            else
            {
                newFrame.X = 1;
            }

            CurrentFrame = newFrame;

            Sprite.SourceRectangle = new Rectangle(
                (int)CurrentFrame.X * FrameWidth,
                (int)CurrentFrame.Y * FrameHeight,
                FrameWidth,
                FrameHeight);
        }
    }
}