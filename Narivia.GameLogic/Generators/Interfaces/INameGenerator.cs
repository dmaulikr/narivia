﻿using System.Collections.Generic;

namespace Narivia.GameLogic.Generators.Interfaces
{
    public interface INameGenerator
    {
        /// <summary>
        /// Gets or sets the minimum length of the name.
        /// </summary>
        /// <value>The minimum length of the name.</value>
        int MinNameLength { get; set; }

        /// <summary>
        /// Gets or sets the maximum length of the name.
        /// </summary>
        /// <value>The maximum length of the name.</value>
        int MaxNameLength { get; set; }

        /// <summary>
        /// Gets or sets the maximum processing time.
        /// </summary>
        /// <value>The maximum processing time in milliseconds.</value>
        int MaxProcessingTime { get; set; }

        /// <summary>
        /// Gets or sets the excluded strings.
        /// </summary>
        /// <value>The excluded strings.</value>
        List<string> ExcludedStrings { get; set; }

        /// <summary>
        /// Gets or sets the included strings.
        /// </summary>
        /// <value>The included strings.</value>
        List<string> IncludedStrings { get; set; }

        /// <summary>
        /// Gets or sets the string that all generated names must start with.
        /// </summary>
        /// <value>The string start filter.</value>
        string StartsWithFilter { get; set; }

        /// <summary>
        /// Gets or sets the string that all generated names must end with.
        /// </summary>
        /// <value>The string end filter.</value>
        string EndsWithFilter { get; set; }

        /// <summary>
        /// Gets the used words.
        /// </summary>
        /// <value>The used words.</value>
        List<string> UsedWords { get; }

        /// <summary>
        /// Generates a name.
        /// </summary>
        /// <returns>The name.</returns>
        string GenerateName();

        /// <summary>
        /// Generates names.
        /// </summary>
        /// <returns>The names.</returns>
        /// <param name="maximumCount">Maximum count.</param>
        List<string> GenerateNames(int maximumCount);

        /// <summary>
        /// Reset the list of used names.
        /// </summary>
        void Reset();
    }
}
