using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Alexa.School.Data.Menus.Food;
using Alexa.School.Data.Providers.Nutrislice;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alexa.School.Data.Tests.Providers.Nutrislice
{
    [TestClass]
    public class NutrisliceMenuTests
    {
        #region Static Fields and Constants

        /// <summary>
        ///     Our test provider to work with.
        /// </summary>
        [NotNull] private static readonly NutrisliceFoodMenuProvider Provider = new NutrisliceFoodMenuProvider(
                                                                                                               baseUri: new Uri(
                                                                                                                                uriString:
                                                                                                                                "http://henrico.nutrislice.com/menu/pemberton/",
                                                                                                                                uriKind: UriKind.Absolute));

        #endregion

        #region Public Methods

        /// <summary>
        ///     Test we can parse data correctly.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TestParsingAsync()
        {
            List<string> result = await Provider.GetMenuAsync(type: MenuType.Lunch, date: GetNextSchoolDay());

            Assert.IsTrue(condition: result != null, message: "Found menu.");
            Assert.IsTrue(condition: result.Count > 0, message: "Found at least one menu item.");
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Crude way of finding a valid school day for running the test.
        /// </summary>
        /// <returns>The next available school day.</returns>
        private static DateTime GetNextSchoolDay()
        {
            DateTime date = DateTime.Today;
            while (true)
            {
                // exclude weekends
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    // exclude holidays-ish
                    if (!IsHoliday(date: date))
                    {
                        return date;
                    }
                }

                date = date.AddDays(value: 1);
            }
        }

        /// <summary>
        ///     Crude check to see if a date is a holiday.
        /// </summary>
        /// <param name="date">The date to check.</param>
        /// <returns></returns>
        private static bool IsHoliday(DateTime date)
        {
            // New Years
            if (date.Month == 1 && date.Day == 1)
            {
                return true;
            }

            // summer break -ish
            if (date.Month >= 6 && date.Month <= 9)
            {
                return true;
            }

            // Christmas
            if (date.Month == 12 && date.Day == 25)
            {
                return true;
            }

            return false;
        }

        #endregion
    }
}