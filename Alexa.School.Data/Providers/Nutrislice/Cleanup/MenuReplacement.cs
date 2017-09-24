using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace Alexa.School.Data.Providers.Nutrislice.Cleanup
{
    /// <summary>
    ///     Represents a regular expression to look for in the menu text, and its replacement.
    /// </summary>
    internal class MenuReplacement
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MenuReplacement" /> class.
        /// </summary>
        /// <param name="match">The regular expression to find text to modify.</param>
        /// <param name="replacement">What to replace the matched text with.</param>
        public MenuReplacement([NotNull] Regex match, [NotNull] string replacement)
        {
            this.Match = match;
            this.Replacement = replacement;
        }

        #endregion

        #region Properties, Indexers

        /// <summary>
        ///     The regular expression to find menu text with.
        /// </summary>
        [NotNull]
        public Regex Match { get; }

        /// <summary>
        ///     What to replace the found matches with.
        /// </summary>
        [NotNull]
        public string Replacement { get; }

        #endregion
    }
}