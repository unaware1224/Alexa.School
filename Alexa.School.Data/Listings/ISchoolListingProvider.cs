using System.Collections.Generic;
using JetBrains.Annotations;

namespace Alexa.School.Data.Listings
{
    /// <summary>
    ///     The data needed for each school.
    /// </summary>
    public interface ISchoolListingProvider
    {
        #region Properties, Indexers

        /// <summary>
        ///     Gets the name of the collection of schools.
        /// </summary>
        [NotNull]
        string Name { get; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Gets all of the available schools.
        /// </summary>
        /// <returns>All of the schools</returns>
        [NotNull]
        [ItemNotNull]
        [Pure]
        List<Alexa.School.Data.School> GetSchools();

        #endregion
    }
}