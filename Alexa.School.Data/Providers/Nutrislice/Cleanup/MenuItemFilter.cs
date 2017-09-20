using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo(assemblyName: "Alexa.School.Data.Tests")]

namespace Alexa.School.Data.Providers.Nutrislice.Cleanup
{
    /// <summary>
    ///     Used to filter the results from Nutrislice.
    /// </summary>
    internal static class MenuItemFilter
    {
        #region Public Methods

        /// <summary>
        ///     The filter to remove menu items from Nutrislice that we do not care about.
        /// </summary>
        /// <param name="item">The menu item.</param>
        /// <returns>Whether or not to keep the menu item for Alexa to read.</returns>
        public static bool Predicate(MenuItem item)
        {
            string foodName = item?.Food?.Name;

            // no name, nothing for Alexa to say
            if (string.IsNullOrEmpty(value: foodName))
            {
                return false;
            }

            // remove any of the milk entries (they are the same every day)
            if (foodName.IndexOf(value: "milk", comparisonType: StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}