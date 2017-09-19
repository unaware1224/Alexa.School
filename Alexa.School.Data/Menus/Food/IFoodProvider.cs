﻿using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Alexa.School.Data.Menus.Food
{
    /// <summary>
    ///     The interface for retrieving food menu items for a school.
    /// </summary>
    public interface IFoodProvider
    {
        #region Public Methods

        /// <summary>
        ///     Gets the menu for the given data.
        /// </summary>
        /// <param name="type">The type of menu to retrieve.</param>
        /// <param name="date">The date to get the menu of.</param>
        /// <returns>An instance of <see cref="Menu"/>.</returns>
        [NotNull]
        [ItemNotNull]
        [Pure]
        Task<Menu> GetMenuAsync(
            MenuType type,
            DateTime date);

        #endregion
    }
}