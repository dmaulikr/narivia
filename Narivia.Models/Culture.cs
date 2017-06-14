﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Narivia.Models
{
    /// <summary>
    /// Culture domain model.
    /// </summary>
    public class Culture
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [Key]
        [StringLength(40, ErrorMessage = "The {0} must be between {1} and {2} characters long", MinimumLength = 3)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [StringLength(20, ErrorMessage = "The {0} must be between {1} and {2} characters long", MinimumLength = 3)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [StringLength(300, ErrorMessage = "The {0} must be between {1} and {2} characters long", MinimumLength = 3)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the texture set.
        /// </summary>
        /// <value>The texture set.</value>
        [StringLength(20, ErrorMessage = "The {0} must be between {1} and {2} characters long", MinimumLength = 3)]
        public string TextureSet { get; set; }

        /// <summary>
        /// Gets or sets the sample place names.
        /// </summary>
        /// <value>The sample place names.</value>
        public List<string> SamplePlaceNames { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Culture"/> class.
        /// </summary>
        public Culture()
        {
            SamplePlaceNames = new List<string>();
        }
    }
}
