using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Alexa.School.Data.Menus.Food.Provider.Nutrislice
{
    /// <summary>
    ///     Food provider using NutriSlice for data.
    /// </summary>
    internal class FoodProvider : IFoodProvider
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="FoodProvider" />.
        /// </summary>
        /// <param name="baseUri">The base URI to the school's Nutrislice website.</param>
        /// <param name="schoolSlug">The unique slug of the school.</param>
        public FoodProvider([NotNull] Uri baseUri, [NotNull] string schoolSlug)
        {
            this.BaseUri = baseUri;
            this.SchoolSlug = schoolSlug;
        }

        #endregion

        #region Properties, Indexers

        /// <summary>
        ///     Gets the base <see cref="Uri" /> to Nutrislice.
        /// </summary>
        /// <remarks>
        ///     Example: <see cref="http://henrico.nutrislice.com" /> in
        ///     <see cref="http://henrico.nutrislice.com/menu/pemberton" />.
        /// </remarks>
        [NotNull]
        public Uri BaseUri { get; set; }

        /// <summary>
        ///     Gets the name of the school slug.
        /// </summary>
        /// <remarks>
        ///     Example: "pemberton" in <see cref="http://henrico.nutrislice.com/menu/pemberton" />.
        /// </remarks>
        [NotNull]
        public string SchoolSlug { get; set; }

        #endregion

        #region IFoodProvider Members

        /// <inheritdoc />
        public Task<Menu> GetMenuAsync(MenuType type, DateTime date)
        {
            return Parser.GetMenuAsync(baseUri: this.BaseUri, schoolSlug: this.SchoolSlug, type: type, date: date);
        }

        #endregion
    }
}