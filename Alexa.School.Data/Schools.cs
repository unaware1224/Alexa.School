using System;
using Alexa.School.Data.Menus.Food.Provider.Nutrislice;

namespace Alexa.School.Data
{
    /// <summary>
    /// Helps with working with <see cref="School"/> data.
    /// </summary>
    public static class Schools
    {
        #region Properties, Indexers

        /// <summary>
        /// Gets the school and its configuration for where to get data from.
        /// </summary>
        public static School Current { get; set; } = new School(
                                                               name: "Pemberton",
                                                               foodProvider: new FoodProvider(
                                                                                              baseUri: new Uri(
                                                                                                               uriString: "http://henrico.nutrislice.com/menu/pemberton",
                                                                                                               uriKind: UriKind.Absolute)));

        #endregion
    }
}