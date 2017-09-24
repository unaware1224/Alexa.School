using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Alexa.School.Data.Menus.Food
{
    /// <summary>
    /// Helper methods for working with <see cref="MenuType"/>.
    /// </summary>
    public static class MenuTypes
    {
        #region Static Fields and Constants

        /// <summary>
        ///     The dictionary mapping of string to <see cref="MenuType" />.
        /// </summary>
        [NotNull] private static readonly Dictionary<string, MenuType> Dictionary;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes static members of the <see cref="MenuTypes" /> class.
        /// </summary>
        static MenuTypes()
        {
            //List<MenuType> types = new List<MenuType>();
            Dictionary = new Dictionary<string, MenuType>(comparer: StringComparer.OrdinalIgnoreCase);

            foreach (MenuType type in Enum.GetValues(enumType: typeof(MenuType))
                                          .Cast<MenuType>())
            {
                //types.Add(item: type);
                Dictionary.Add(key: type.ToString(), value: type);
            }

            //All = types;
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Gets the menu type from the string representation.
        /// </summary>
        /// <param name="value">The string value from Alexa.</param>
        /// <returns>The menu type enumeration, or NULL if unable to parse.</returns>
        public static MenuType? GetMenuType(string value)
        {
            return Dictionary.TryGetValue(key: value, value: out MenuType type)
                       ? type
                       : (MenuType?) null;
        }

        #endregion

        ///// <summary>
        /////     Gets all of the available menu types.
        ///// </summary>
        //[NotNull]
        //public static ICollection<MenuType> All { get; }
    }
}