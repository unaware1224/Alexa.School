using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Alexa.School.Data
{
    /// <summary>
    ///     Helpers for working with <see cref="SchoolType" />.
    /// </summary>
    public static class SchoolTypes
    {
        #region Constructors

        /// <summary>
        ///     Initializes static members of the <see cref="SchoolTypes" /> class.
        /// </summary>
        static SchoolTypes()
        {
            var types = new List<SchoolType>();

            // get all of the forecast types.
            foreach (SchoolType type in Enum.GetValues(typeof(SchoolType))
                                            .Cast<SchoolType>())
            {
                types.Add(type);
            }

            All = types;
        }

        #endregion

        #region Properties, Indexers

        /// <summary>
        ///     Gets all of the available school types.
        /// </summary>
        [NotNull]
        public static IReadOnlyCollection<SchoolType> All { get; }

        #endregion
    }
}
