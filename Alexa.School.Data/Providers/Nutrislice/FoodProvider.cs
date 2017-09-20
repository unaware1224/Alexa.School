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
        public FoodProvider([NotNull] Uri baseUri)
        {
            this.BaseUri = baseUri;
        }

        #endregion

        #region Properties, Indexers

        /// <summary>
        ///     Gets the <see cref="Uri" /> to Nutrislice.
        /// </summary>
        [NotNull]
        public Uri BaseUri { get; set; }

        #endregion

        #region IFoodProvider Members

        /// <inheritdoc />
        public Task<Menu> GetMenuAsync(MenuType type, DateTime date)
        {
            return Parser.GetMenuAsync(baseUri: this.BaseUri, type: type, date: date);
        }

        #endregion
    }
}