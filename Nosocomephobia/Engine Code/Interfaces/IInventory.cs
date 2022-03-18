﻿using Nosocomephobia.Game_Code.Game_Entities;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.2, 18-03-2022
namespace Nosocomephobia.Engine_Code.Interfaces
{
    /// <summary>
    /// Interface IInventory
    /// </summary>
    public interface IInventory
    {
        /// <summary>
        /// Adds an Artefact to the Inventory.
        /// </summary>
        /// <param name="pName">The name to reference the Artefact by in the inventory.</param>
        /// <param name="pArtefact">The artefact to store in the inventory.</param>
        void Add(string pName, Artefact pArtefact);

        /// <summary>
        /// Removes an Artefact to the Inventory.
        /// </summary>
        /// <param name="pName">The name to reference the Artefact by in the inventory.</param>
        void Remove(string pName);
        /// <summary>
        /// Returns the total number of items in the inventory.
        /// </summary>
        /// <returns>Returns the total number of items in the inventory.</returns>
        int GetCount();
    }
}
