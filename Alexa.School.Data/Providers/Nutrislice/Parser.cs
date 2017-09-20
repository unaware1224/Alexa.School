using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Alexa.School.Data.Providers.Nutrislice.Cleanup;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Alexa.School.Data.Providers.Nutrislice
{
    /// <summary>
    ///     Parses the HTML from Nutrislice.
    /// </summary>
    /// <remarks>If only they had an API for this.</remarks>
    public static class Parser
    {
        #region Public Methods

        /// <summary>
        ///     Gets the menu for the given data.
        /// </summary>
        /// <param name="contentStream">The stream of content from the Nutrislice server.</param>
        /// <param name="date">The date to get the menu of.</param>
        /// <returns>The list of items on the menu.</returns>
        [NotNull]
        [ItemNotNull]
        [Pure]
        public static List<string> Parse(
            [NotNull] Stream contentStream,
            DateTime date)
        {
            // extract the JSON
            List<MenuMonthJson> data = ExtractJson(contentStream: contentStream);

            // select the menu records
            IEnumerable<MenuItem> availableMenuItems = GetAvailabileMenuItems(date: date, data: data);

            // perform any cleanup/filtering of records
            return availableMenuItems
                    .Where(predicate: MenuItemFilter.Predicate) // filter the records we do not care about
                    .Select(selector: mi => mi?.Food?.Name.CleanText()) // adjust the names of the food so that they are Alexa friendly
                    .Where(predicate: n => !string.IsNullOrEmpty(value: n)) // remove any blank/useless records
                    .ToList();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Extracts the JSON data from the Html stream.
        /// </summary>
        /// <param name="contentStream">The stream of the response from Nutrislice's HTML page.</param>
        /// <returns>A list of <see cref="MenuMonthJson" />.</returns>
        [NotNull]
        [ItemNotNull]
        [Pure]
        private static List<MenuMonthJson> ExtractJson([NotNull] Stream contentStream)
        {
            using (StreamReader sr = new StreamReader(stream: contentStream))
            {
                do
                {
                    string line = sr.ReadLine();

                    // keep checking for the line we are looking for that contains the menu as JSON
                    if (line != null && IsMenuLine(line: line))
                    {
                        // extract the JSON
                        return DeserializeMenu(line: line);
                    }
                } while (!sr.EndOfStream);
            }

            return new List<MenuMonthJson>(capacity: 0);
        }

        /// <summary>
        ///     Extracts the menu items from the JSON data in the HTML.
        /// </summary>
        /// <param name="date">The date to get the menu items for.</param>
        /// <param name="data">The JSON data from the HTML to read from.</param>
        /// <returns>A stream of <see cref="MenuItem" />.</returns>
        [Pure]
        [NotNull]
        private static IEnumerable<MenuItem> GetAvailabileMenuItems(DateTime date, [NotNull] List<MenuMonthJson> data)
        {
            // the key for the day we want to find in their JSON
            string dayString = date.ToString(format: "yyyy-MM-dd");

            // extract the menu items
            return data.Where(predicate: dt => dt?.Days != null)
                       .SelectMany(
                                   selector: dt => dt.Days.Where(predicate: d => d?.Date == dayString && d?.MenuItems != null) // find the day we care about
                                                     .SelectMany(selector: d => d.MenuItems));
        }

        /// <summary>
        ///     Gets the monthly menu from the HTML line.
        /// </summary>
        /// <param name="line">The line to read from.</param>
        /// <returns>A list of <see cref="MenuMonthJson" />.</returns>
        [Pure]
        [NotNull]
        [ItemNotNull]
        private static List<MenuMonthJson> DeserializeMenu([NotNull] string line)
        {
            // extract the json
            string json = line.Substring(startIndex: line.IndexOf(value: "=", comparisonType: StringComparison.Ordinal) + 1)
                              ?
                              .Trim();

            // make sure we have something to work with
            if (string.IsNullOrEmpty(value: json))
            {
                return new List<MenuMonthJson>(capacity: 0);
            }

            // remove trailing semicolon
            json = json.Substring(startIndex: 0, length: json.Length - 1);

            // deserialize JSON
            return JsonConvert.DeserializeObject<List<MenuMonthJson>>(value: json) ?? new List<MenuMonthJson>(capacity: 0);
        }

        /// <summary>
        ///     Checks whether the current line in the HTML is the one containing the JSON menu data.
        /// </summary>
        /// <param name="line">The line to check.</param>
        /// <returns>A value indicating whether or not this line containts the JSON menu data.</returns>
        [Pure]
        private static bool IsMenuLine([NotNull] string line)
        {
            return line.Contains(value: "bootstrapData['menuMonthWeeks']");
        }

        #endregion
    }
}