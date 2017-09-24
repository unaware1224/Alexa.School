using Alexa.School.Data.Menus.Food;
using JetBrains.Annotations;

namespace Alexa.School.Data
{
    /// <summary>
    ///     Represents a school and where its data comes from.
    /// </summary>
    public class School
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="School" /> class.
        /// </summary>
        /// <param name="name">The name of the school.</param>
        /// <param name="foodProvider">The provider of food menu data.</param>
        public School([NotNull] string name, [NotNull] IFoodProvider foodProvider)
        {
            this.Name = name;
            this.FoodProvider = foodProvider;
        }

        #endregion

        #region Properties, Indexers

        /// <summary>
        ///     Gets the name of the school.
        /// </summary>
        [NotNull]
        public string Name { get; }


        /// <summary>
        ///     Gets the provider of the food menu data.
        /// </summary>
        [NotNull]
        public IFoodProvider FoodProvider { get; }

        #endregion
    }
}