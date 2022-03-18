using Nosocomephobia.Engine_Code.Interfaces;
using Nosocomephobia.Game_Code.Game_Entities;
using System.Collections.Generic;
using System.Diagnostics;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.2, 18-03-2022
namespace Nosocomephobia.Engine_Code.Components
{
    /// <summary>
    /// Class Inventory
    /// </summary>
    public class Inventory : IInventory
    {
        #region FIELDS
        // DECLARE a Dictionary<string, Artefact>, call it _storage:
        private Dictionary<string, Artefact> _storage;
        #endregion FIELDS

        /// <summary>
        /// Constructor for class Inventory
        /// </summary>
        public Inventory()
        {
            // INITIALISE _storage:
            _storage = new Dictionary<string, Artefact>();
        }

        /// <summary>
        /// Adds an Artefact to the Inventory.
        /// </summary>
        /// <param name="pName">The name to reference the Artefact by in the inventory.</param>
        /// <param name="pArtefact">The artefact to store in the inventory.</param>
        public void Add(string pName, Artefact pArtefact)
        {
            // ADD the Artefact to the storage:
            _storage.Add(pName, pArtefact);
            Debug.WriteLine("ADDED " + pName + "TO INVENTORY");
        }

        /// <summary>
        /// Removes an Artefact to the Inventory.
        /// </summary>
        /// <param name="pName">The name to reference the Artefact by in the inventory.</param>
        public void Remove(string pName)
        {
            // REMOVE the Artefact from the storage:
            _storage.Remove(pName);
        }
        /// <summary>
        /// Returns the number of items in the inventory.
        /// </summary>
        /// <returns>Returns the number of items in the inventory.</returns>
        public int GetCount()
        {
            return _storage.Count;
        }
    }
}
