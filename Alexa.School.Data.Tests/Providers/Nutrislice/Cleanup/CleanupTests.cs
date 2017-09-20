using Alexa.School.Data.Providers.Nutrislice;
using Alexa.School.Data.Providers.Nutrislice.Cleanup;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Alexa.School.Data.Tests.Providers.Nutrislice.Cleanup
{
    /// <summary>
    ///     Test that the cleanup of Nutrislice data functions correctly.
    /// </summary>
    [TestClass]
    public class CleanupTests
    {
        #region Public Methods

        /// <summary>
        ///     Test that the different oddities in the menu text are translated to be Alexa friendly.
        /// </summary>
        [TestMethod]
        public void TestCleaning()
        {
            TestCleaning(input: null, expectedOuput: null, testName: "Null does not cause an error.");
            TestCleaning(input: string.Empty, expectedOuput: string.Empty, testName: "Empty string is not an error.");
            TestCleaning(input: "milk", expectedOuput: "milk", testName: "Plain text is not modified.");
            TestCleaning(input: "hamburger w/ cheese", expectedOuput: "hamburger with cheese", testName: "'w/' is translated to 'with'.");
            TestCleaning(input: "   hamburger     ", expectedOuput: "hamburger", testName: "Leading and trailing whitespace is removed.");
            TestCleaning(input: "hamburger    fries", expectedOuput: "hamburger fries", testName: "Extra whitespace is removed.");
            TestCleaning(input: "hamburger & fries", expectedOuput: "hamburger and fries", testName: "Ampersand is converted to AND.");
            TestCleaning(input: "(cheese) burger", expectedOuput: "burger", testName: "Parenthesis are removed.");
            TestCleaning(input: "Microsoft®", expectedOuput: "Microsoft", testName: "Trademark is removed.");
        }

        /// <summary>
        ///     Test that our record filtering removes unwanted menu items.
        /// </summary>
        [TestMethod]
        public void TestFiltering()
        {
            this.TestFiltering(menuItem: null, shouldBeIncluded: false, testName: "NULL record ignored");
            this.TestFiltering(menuItem: new MenuItem(), shouldBeIncluded: false, testName: "Empty record ignored");
            this.TestFiltering(
                               menuItem: new MenuItem
                                         {
                                             Food = new Food()
                                         },
                               shouldBeIncluded: false,
                               testName: "Empty food record ignored");
            this.TestFiltering(
                               menuItem: new MenuItem
                                         {
                                             Food = new Food
                                                    {
                                                        Name = "Hamburger"
                                                    }
                                         },
                               shouldBeIncluded: true,
                               testName: "Food with name included");
            this.TestFiltering(
                               menuItem: new MenuItem
                                         {
                                             Food = new Food
                                                    {
                                                        Name = "Strawberry Milk"
                                                    }
                                         },
                               shouldBeIncluded: false,
                               testName: "Food with excluded name filtered out ... who on earth actually wants strawberry milk?!?!");
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Runs actual cleaning tests.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="expectedOuput">The expected cleaned string.</param>
        /// <param name="testName">What we are testing.</param>
        private static void TestCleaning(string input, string expectedOuput, string testName)
        {
            string output = input.CleanText();
            Assert.AreEqual(expected: expectedOuput, actual: output, message: testName);
        }

        /// <summary>
        ///     Runs actual filtering tests.
        /// </summary>
        /// <param name="menuItem">The menu item to test.</param>
        /// <param name="shouldBeIncluded">Whether or not the menu item should be included.</param>
        /// <param name="testName">What we are testing.</param>
        private void TestFiltering(MenuItem menuItem, bool shouldBeIncluded, string testName)
        {
            bool include = MenuItemFilter.Predicate(item: menuItem);
            Assert.AreEqual(expected: shouldBeIncluded, actual: include, message: testName);
        }

        #endregion
    }
}