﻿using System;
using System.ComponentModel.DataAnnotations;

using Narivia.Models.Enumerations;

namespace Narivia.Models
{
    /// <summary>
    /// Unit domain model.
    /// </summary>
    public class Unit : IEquatable<Unit>
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
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public UnitType Type { get; set; }

        /// <summary>
        /// Gets or sets the power.
        /// </summary>
        /// <value>The power.</value>
        [Range(0, int.MaxValue)]
        public int Power { get; set; }

        /// <summary>
        /// Gets or sets the health.
        /// </summary>
        /// <value>The health.</value>
        [Range(0, int.MaxValue)]
        public int Health { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>The price.</value>
        [Range(0, int.MaxValue)]
        public int Price { get; set; }

        /// <summary>
        /// Gets or sets the maintenance.
        /// </summary>
        /// <value>The maintenance.</value>
        [Range(0, int.MaxValue)]
        public int Maintenance { get; set; }

        /// <summary>
        /// Determines whether the specified <see cref="Unit"/> is equal to the current <see cref="Unit"/>.
        /// </summary>
        /// <param name="other">The <see cref="Unit"/> to compare with the current <see cref="Unit"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="Unit"/> is equal to the current
        /// <see cref="Unit"/>; otherwise, <c>false</c>.</returns>
        public bool Equals(Unit other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return string.Equals(Id, other.Id) &&
                   string.Equals(Name, other.Name) &&
                   string.Equals(Description, other.Description) &&
                   Equals(Type, other.Type) &&
                   Equals(Power, other.Power) &&
                   Equals(Health, other.Health) &&
                   Equals(Price, other.Price) &&
                   Equals(Maintenance, other.Maintenance);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="Unit"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="Unit"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
        /// <see cref="Unit"/>; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((Unit)obj);
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="Unit"/> object.
        /// </summary>
        /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a
        /// hash table.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return ((Id != null ? Id.GetHashCode() : 0) * 397) ^
                       (Name != null ? Name.GetHashCode() : 0) ^
                       (Description != null ? Description.GetHashCode() : 0) ^
                       Type.GetHashCode() ^
                       Power.GetHashCode() ^
                       Health.GetHashCode() ^
                       Price.GetHashCode() ^
                       Maintenance.GetHashCode();
            }
        }
    }
}
