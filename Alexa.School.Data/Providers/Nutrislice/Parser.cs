using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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
        /// <param name="schoolSlug">The school unique uri slug.</param>
        /// <param name="type">The type of menu to retrieve.</param>
        /// <param name="date">The date to get the menu of.</param>
        /// <returns></returns>
        [NotNull, ItemNotNull, Pure]
        public static async Task<Menu> GetMenuAsync(
            [NotNull] Uri baseUri,
            [NotNull] string schoolSlug,
            MenuType type,
            DateTime date)
        {
            // download html
            Stream contentStream = await GetHtmlAsync(baseUri, schoolSlug, type, date);

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

                        string dayString = day.ToString("yyyy-MM-dd");

                        var menuItems = data.SelectMany(
                                                                  dt => dt.days.Where(d => d.date == dayString)
                                                                          .SelectMany(d => d.menu_items)).ToList();
                        var specificMenuItems = menuItems
                                .Where(mi => mi.food != null && mi.food.name.IndexOf("milk", StringComparison.OrdinalIgnoreCase) < 0).ToList();

                        List<string> fixedMenuItems = specificMenuItems
                                .Select(
                                        mi => MenuItemCleanup.Cleanup(mi.food.name))
                                .Where(n => !string.IsNullOrEmpty(n))
                                .ToList();

                        return fixedMenuItems;
                    }
                } while (!sr.EndOfStream);
            }

            return new List<string>(0);
        }

        [NotNull, Pure]
        private static async Task<Stream> GetHtmlAsync(
            [NotNull] Uri baseUrl,
            [NotNull] string schoolSlug,
            MenuType type,
            DateTime date)
        {
            var client = new WebClient();
            byte[] bytes = await client.DownloadDataTaskAsync(GetUrl(baseUrl, schoolSlug, type, date));

            return new MemoryStream(bytes);
        }

        /// <summary>
        ///     Gets the URL to download the date from.
        /// </summary>
        /// <param name="school"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [NotNull, Pure]
        private static Uri GetUrl([NotNull] Uri baseUrl, [NotNull] string schoolSlug, MenuType type, DateTime date)
        {
            var uri = new Uri(
                baseUrl,
                string.Format(
                              "/menu/{0}/{1}/{2}/{3}",
                              HttpUtility.HtmlEncode(schoolSlug),
                              HttpUtility.HtmlEncode(type.ToString()
                                  .ToLowerInvariant()),
                              CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(date.Month)
                                         .ToLowerInvariant(),
                              date.Year));

            Trace.Write($"Using {uri}");
            return uri;
        }

        #endregion
    }
}
