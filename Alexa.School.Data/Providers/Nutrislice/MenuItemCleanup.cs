using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace Alexa.School.Data.Providers.Nutrislice
{
    /// <summary>
    ///     Facilitates cleaning up text so that Alexa can read it.
    /// </summary>
    internal static class MenuItemCleanup
    {
        #region Static Fields and Constants

        private static readonly Listings<MenuReplacement>

        [NotNull] private static readonly Regex WithRegex = new Regex(pattern: "w/", options: RegexOptions.Compiled);

        [NotNull] private static readonly Regex TrademarkRegex = new Regex(pattern: "®");

        [NotNull] private static readonly Regex AmpersandRegex = new Regex(pattern: "&", options: RegexOptions.Compiled);

        [NotNull] private static readonly Regex ParenthesisRegex = new Regex(pattern: "\\(.*\\)", options: RegexOptions.Compiled);

        [NotNull] private static readonly Regex WhitespaceRegex = new Regex(pattern: "\\s+", options: RegexOptions.Compiled);

        #endregion

        #region Public Methods

        /// <summary>
        ///     Cleans up the text to ensure Alexa can read the data.
        /// </summary>
        /// <param name="menuItemText">The menu text to clean up.</param>
        /// <returns>The text cleaned up.</returns>
        [Pure]
        public static string Cleanup([CanBeNull] string menuItemText)
        {
            if (menuItemText == null)
            {
                return null;
            }

            return menuItemText.CleanWith()
                               .CleanAnd()
                               .CleanRegisterTrademark()
                               .CleanParenthesis()
                               .CleanWhitespace()
                               .Trim();
        }

        #endregion

        #region Methods

        private static string CleanWith([NotNull] this string input)
        {
            return WithRegex.Replace(input: input, replacement: " with ");
        }

        private static string CleanRegisterTrademark([NotNull] this string input)
        {
            return TrademarkRegex.Replace(input: input, replacement: string.Empty);
        }

        private static string CleanAnd([NotNull] this string input)
        {
            return AmpersandRegex.Replace(input: input, replacement: " and ");
        }

        private static string CleanParenthesis([NotNull] this string input)
        {
            return ParenthesisRegex.Replace(input: input, replacement: " ");
        }

        private static string CleanWhitespace([NotNull] this string input)
        {
            return WhitespaceRegex.Replace(input: input, replacement: " ");
        }

        #endregion

        #region Nested type: MenuReplacement

        private class MenuReplacement
        {
            #region Constructors

            public MenuReplacement([NotNull] Regex match, [NotNull] string replacement)
            {
                this.Match = match;
                this.Replacement = replacement;
            }

            #endregion

            #region Properties, Indexers

            [NotNull]
            public Regex Match { get; }

            [NotNull]
            public string Replacement { get; }

            #endregion
        }

        #endregion
    }
}