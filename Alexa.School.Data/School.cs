using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Alexa.School.Data
{
    public class School
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="School" /> class.
        /// </summary>
        public School([NotNull] string name, SchoolType type, [NotNull] string routeName, [ItemNotNull] params string[] alternativeNames)
        {
            this.Name = name;
            this.Type = type;
            this.RouteName = routeName;

            List<string[]> matches = new List<string[]>();
            matches.Add(
                        item: this.Name.ToLowerInvariant()
                                  .Split(' '));
            foreach (string alternativeName in alternativeNames ?? new string[0])
            {
                matches.Add(
                            item: alternativeName.ToLowerInvariant()
                                                 .Split(' '));
            }

            this.SearchMatches = matches;
        }

        #endregion

        #region Properties, Indexers

        [NotNull]
        public string Name { get; }

        public SchoolType Type { get; }

        /// <summary>
        ///     Gets the name of the route in nutrislice.
        /// </summary>
        [NotNull]
        public string RouteName { get; }

        [NotNull]
        public string FullName => this.Name + " " + this.Type;

        /// <summary>
        ///     Gets the search matches to look for.
        /// </summary>
        [NotNull]
        [ItemNotNull]
        public ICollection<string[]> SearchMatches { get; }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Sees if the given
        /// </summary>
        /// <param name="type">The type of school the query is for.</param>
        /// <param name="queryTokens">The query tokens.</param>
        /// <param name="matchCount">The number of strings that matched.</param>
        /// <returns>Whether or not there was a match.</returns>
        public bool IsMatch(SchoolType? type, [NotNull] string[] queryTokens, out int matchCount)
        {
            if (type.HasValue && type != this.Type)
            {
                matchCount = 0;
                return false;
            }

            // go through each of our possible matches and see how well we match the search term.
            matchCount = 0;
            foreach (string[] matches in this.SearchMatches)
            {
                // find the number of words we matched
                int currentMatchCount = matches.Count(predicate: queryTokens.Contains);

                // if this is a better match that we have had previously, set this as our count
                if (currentMatchCount > matchCount)
                {
                    matchCount = currentMatchCount;
                }
            }

            // if we never found a match
            if (matchCount == 0)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}