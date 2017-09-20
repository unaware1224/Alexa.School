using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Alexa.School.Data.Menus.Food;
using JetBrains.Annotations;

namespace Alexa.School.Data.Providers.Nutrislice
{
    /// <inheritdoc />
    /// <summary>
    ///     Food provider using NutriSlice for data.
    /// </summary>
    internal class NutrisliceFoodMenuProvider : IFoodProvider
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="NutrisliceFoodMenuProvider" />.
        /// </summary>
        /// <param name="baseUri">The base URI to the school's Nutrislice website.</param>
        public NutrisliceFoodMenuProvider([NotNull] Uri baseUri)
        {
            this.BaseUri = baseUri;
        }

        #endregion

        #region Properties, Indexers

        /// <summary>
        ///     Gets the <see cref="Uri" /> to Nutrislice.
        /// </summary>
        [NotNull]
        private Uri BaseUri { get; }

        #endregion

        #region IFoodProvider Members

        /// <inheritdoc />
        public async Task<List<string>> GetMenuAsync(MenuType type, DateTime date)
        {
            Stream contentStream = await GetHtmlAsync(baseUrl: this.BaseUri, type: type, date: date);
            return Parser.Parse(contentStream: contentStream, date: date);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Gets a stream of the response from Nutrislice.
        /// </summary>
        /// <param name="baseUrl">The base Uri to the Nutrislice web page.</param>
        /// <param name="type">The type of menu to retrieve.</param>
        /// <param name="date">The date to retrieve the menu for (really only year+month matter).</param>
        /// <returns>A stream of the web page.</returns>
        [NotNull]
        [Pure]
        private static async Task<Stream> GetHtmlAsync(
            [NotNull] Uri baseUrl,
            MenuType type,
            DateTime date)
        {
            HttpClient client = new HttpClient();
            return await client.GetStreamAsync(requestUri: GetUrl(baseUrl: baseUrl, type: type, date: date));
        }

        /// <summary>
        ///     Gets the URL to download the date from.
        /// </summary>
        /// <param name="baseUrl">The base Uri to the Nutrislice web page.</param>
        /// <param name="type">The type of menu to retrieve.</param>
        /// <param name="date">The date to retrieve the menu for (really only year+month matter).</param>
        /// <returns>An instance of <see cref="Uri" />.</returns>
        [NotNull]
        [Pure]
        private static Uri GetUrl([NotNull] Uri baseUrl, MenuType type, DateTime date)
        {
            return new Uri(
                           baseUri: baseUrl,
                           relativeUri:
                           $"{baseUrl.LocalPath}{WebUtility.HtmlEncode(value: type.ToString() .ToLowerInvariant())}/{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month: date.Month) .ToLowerInvariant()}/{date.Year}");
        }

        #endregion
    }
}