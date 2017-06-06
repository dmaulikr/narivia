﻿using System;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Narivia.Settings;

namespace Narivia.Interface.Widgets
{
    public class MenuToggle : MenuItem
    {
        /// <summary>
        /// Gets or sets the property.
        /// </summary>
        /// <value>The type of the property.</value>
        public string Property { get; set; }

        /// <summary>
        /// Gets or sets the toggle state.
        /// </summary>
        /// <value>The type of the toggle state.</value>
        [XmlIgnore]
        public bool ToggleState { get; set; }
        /// <summary>
        /// Loads the content.
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();
        }

        /// <summary>
        /// Unloads the content.
        /// </summary>
        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        /// <summary>
        /// Updates the content.
        /// </summary>
        /// <param name="gameTime">Game time.</param>
        public override void Update(GameTime gameTime)
        {
            if (!Enabled)
            {
                return;
            }

            // TODO: Update the text in the image
            string displayText = Text + ": " + ToggleState;

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the content on the specified spriteBatch.
        /// </summary>
        /// <returns>The draw.</returns>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        protected override void OnActivated(object sender, EventArgs e)
        {
            base.OnActivated(sender, e);

            ToggleState = !ToggleState;
        }
    }
}
