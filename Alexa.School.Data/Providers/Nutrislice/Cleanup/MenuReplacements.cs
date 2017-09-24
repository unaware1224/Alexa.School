using System.Collections.Generic;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("Alexa.School.Data.Tests")]

namespace Alexa.School.Data.Providers.Nutrislice.Cleanup
{
    /// <summary>
    ///     For cleaning up the text we get back from Nutrislice that Alexa isn't a fan of.
    ///     Facilitates working with <see cref="MenuReplacement" />.
    /// </summary>
    internal static class MenuReplacements
    {
        #region Static Fields and Constants

        /// <summary>
        ///     Collection of the replacements to perform on the text.
        /// </summary>
        [NotNull] [ItemNotNull] private static readonly List<MenuReplacement> Replacements = new List<MenuReplacement>
                                                                                             {
                                                                                                 // make WITH speakable by Alexa
                                                                                                 new MenuReplacement(
                                                                                                                     match: new Regex(
                                                                                                                                      pattern: "w/",
                                                                                                                                      options: RegexOptions
                                                                                                                                              .Compiled),
                                                                                                                     replacement: " with "),

                                                                                                 // replace trademark
                                                                                                 new MenuReplacement(
                                                                                                                     match: new Regex(pattern: "®"),
                                                                                                                     replacement: string.Empty),

                                                                                                 // replace ampersand
                                                                                                 new MenuReplacement(
                                                                                                                     match: new Regex(
                                                                                                                                      pattern: "&",
                                                                                                                                      options: RegexOptions
                                                                                                                                              .Compiled),
                                                                                                                     replacement: " and "),

                                                                                                 // remove parenthesis
                                                                                                 new MenuReplacement(
                                                                                                                     match: new Regex(
                                                                                                                                      pattern: "\\(.*\\)",
                                                                                                                                      options: RegexOptions
                                                                                                                                              .Compiled),
                                                                                                                     replacement: " "),

                                                                                                 // remove extra whitespace
                                                                                                 new MenuReplacement(
                                                                                                                     match: new Regex(
                                                                                                                                      pattern: "\\s+",
                                                                                                                                      options: RegexOptions
                                                                                                                                              .Compiled),
                                                                                                                     replacement: " ")
                                                                                             };

        #endregion

        #region Public Methods

        /// <summary>
        ///     Cleans up text.
        /// </summary>
        /// <param name="menuText">The menu text to clean.</param>
        /// <returns>The cleaned text.</returns>
        [CanBeNull]
        [Pure]
        public static string CleanText(this string menuText)
        {
            if (string.IsNullOrEmpty(value: menuText))
            {
                return menuText;
            }

            foreach (MenuReplacement replacement in Replacements)
            {
                menuText = replacement.Match.Replace(input: menuText, replacement: replacement.Replacement);
            }

            return menuText?.Trim();
        }

        #endregion
    }
}