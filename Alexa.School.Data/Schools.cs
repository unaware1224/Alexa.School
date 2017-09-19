using JetBrains.Annotations;

namespace Alexa.School.Data
{
    public static class Schools
    {
        #region Public Methods

        /// <summary>
        ///     Finds the school from the given query in the given data store of school data.
        /// </summary>
        /// <param name="query"></param>
        /// <param name="schoolData"></param>
        /// <returns></returns>
        public static School Find(string query, ISchoolData schoolData)
        {
            if (string.IsNullOrEmpty(value: query))
            {
                return null;
            }

            string[] allQueryTokens = query.ToLowerInvariant()
                                           .Split(' ');
            SchoolType? schoolType = GetSchoolType(query: query);

            int bestMatchScore = 0;
            School bestMatch = null;

            foreach (School school in schoolData.GetSchools())
            {
                int currentMatchScore = 0;
                if (school.IsMatch(type: schoolType, queryTokens: allQueryTokens, matchCount: out currentMatchScore) && currentMatchScore > bestMatchScore)
                {
                    bestMatch = school;
                }
            }

            return bestMatch;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Gets the school type from the query.
        /// </summary>
        /// <param name="query">The search query to check.</param>
        /// <returns>The matching school type, if any.</returns>
        internal static SchoolType? GetSchoolType([NotNull] string query)
        {
            query = query.ToLowerInvariant();

            foreach (SchoolType type in SchoolTypes.All)
            {
                if (query.Contains(
                                   value: type.ToString()
                                              .ToLowerInvariant()))
                {
                    return type;
                }
            }

            return null;
        }

        #endregion
    }
}