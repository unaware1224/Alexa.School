using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Alexa.School.Data.Providers.Nutrislice;
using Alexa.School.Data.Providers.Nutrislice.Cleanup;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Alexa.School.Data.Menus.Food.Provider.Nutrislice
{
    public static class Parser
    {
        #region Public Methods

        /// <summary>
        ///     Gets the menu for the given data.
        /// </summary>
        /// <param name="baseUri">The base URI to the data.</param>
        /// <param name="type">The type of menu to retrieve.</param>
        /// <param name="date">The date to get the menu of.</param>
        /// <returns></returns>
        [NotNull, ItemNotNull, Pure]
        public static async Task<Menu> GetMenuAsync(
            [NotNull] Uri baseUri,
            MenuType type,
            DateTime date)
        {
            // download html
            Stream contentStream = await GetHtmlAsync(baseUri, type, date);

            // find the html element we want to read the menu from.
            List<string> menuItems = GetMenuData(contentStream, date);

            // return menu information
            return new Menu(type, date, menuItems);
        }

        #endregion

        #region Methods

        [NotNull, ItemNotNull, Pure]
        internal static List<string> GetMenuData([NotNull] Stream htmlStream, DateTime day)
        {
            // need to figure out how to read the json out of this variable on the page:
            // bootstrapData['menuMonthWeeks']
            using (htmlStream)
            using (var sr = new StreamReader(htmlStream))
            {
                string line;
                do
                {
                    line = sr.ReadLine();
                    if (line.Contains("bootstrapData['menuMonthWeeks']"))
                    {
                        // extract the json
                        string json = line.Substring(line.IndexOf("=") + 1)
                                          .Trim();

                        // remove trailing semicolon
                        json = json.Substring(0, json.Length - 1);

                        var data = JsonConvert.DeserializeObject<List<MenuMonthJson>>(json);

                        string dayString = day.ToString("yyyy-MM-dd"); // the key for the day we want to find in their JSON

                        return data.SelectMany(
                                               dt => dt.Days.Where(d => d.Date == dayString) // find the day we care about
                                                       .SelectMany(d => d.MenuItems)) // select the menu records
                                   .Where(MenuItemFilter.Predicate) // filter the records we do not care about
                                   .Select(mi => mi?.Food?.Name.CleanText()) // adjust the names of the food so that they are Alexa friendly
                                   .Where(n => !string.IsNullOrEmpty(n)) // remove any blank/useless records
                                   .ToList();
                    }
                } while (!sr.EndOfStream);
            }

            return new List<string>(0);
        }

        [NotNull, Pure]
        private static async Task<Stream> GetHtmlAsync(
            [NotNull] Uri baseUrl,
            MenuType type,
            DateTime date)
        {
            var client = new HttpClient();
            return await client.GetStreamAsync(GetUrl(baseUrl, type, date));
        }

        /// <summary>
        ///     Gets the URL to download the date from.
        /// </summary>
        /// <param name="school"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [NotNull, Pure]
        private static Uri GetUrl([NotNull] Uri baseUrl, MenuType type, DateTime date)
        {
            var uri = new Uri(
                baseUrl,
                string.Format(
                              "{0}/{1}/{2}",
                              WebUtility.HtmlEncode(type.ToString()
                                  .ToLowerInvariant()),
                              CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(date.Month)
                                         .ToLowerInvariant(),
                              date.Year));
            
            return uri;
        }

        #endregion
    }
}
