using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Alexa.School.Data.Menus.Food
{
    /// <summary>
    ///     Represents the menu for a particular day.
    /// </summary>
    public class Menu
    {
        #region Constructors

        public Menu(MenuType type, DateTime date, [NotNull] List<string> items)
        {
            this.Type = type;
            this.Date = date;
            this.Items = items;
        }

        #endregion

        #region Properties, Indexers

        public MenuType Type { get; }

        public DateTime Date { get; }

        [NotNull]
        [ItemNotNull]
        public ICollection<string> Items { get; }

        #endregion
    }
}