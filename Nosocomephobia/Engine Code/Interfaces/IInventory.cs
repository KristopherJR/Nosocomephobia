using Nosocomephobia.Game_Code.Game_Entities;
using System.Collections.Generic;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.3, 19-03-2022
namespace Nosocomephobia.Engine_Code.Interfaces
{
    /// <summary>
    /// Interface IInventory
    /// </summary>
    public interface IInventory
    {
        // Get property for Storage:
        List<Artefact> Storage { get; }
        /// <summary>
        /// Adds an Artefact to the Inventory.
        /// </summary>
        /// <param name="pArtefact">The artefact to store in the inventory.</param>
        void Add(Artefact pArtefact);

        /// <summary>
        /// Removes an Artefact from the Inventory.
        /// </summary>
        /// <param name="pArtefact">The Artefact to remove.</param>
        void Remove(Artefact pArtefact);
        /// <summary>
        /// Returns the total number of items in the inventory.
        /// </summary>
        /// <returns>Returns the total number of items in the inventory.</returns>
        int GetCount();
    }
}
