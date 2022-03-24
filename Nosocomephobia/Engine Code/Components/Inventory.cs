using Nosocomephobia.Engine_Code.Interfaces;
using Nosocomephobia.Game_Code.Game_Entities;
using System.Collections.Generic;

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
        // DECLARE a List<Artefact>, call it _storage:
        private List<Artefact> _storage;
        #endregion FIELDS

        #region PROPERTIES
        // Get property for Storage:
        public List<Artefact> Storage
        {
            get { return _storage; }
        }
        #endregion

        /// <summary>
        /// Constructor for class Inventory
        /// </summary>
        public Inventory()
        {
            // INITIALISE _storage:
            _storage = new List<Artefact>();
        }

        /// <summary>
        /// Adds an Artefact to the Inventory.
        /// </summary>
        /// <param name="pName">The name to reference the Artefact by in the inventory.</param>
        /// <param name="pArtefact">The artefact to store in the inventory.</param>
        public void Add(Artefact pArtefact)
        {
            // ADD the Artefact to the storage:
            _storage.Add(pArtefact);
        }

        /// <summary>
        /// Removes an Artefact from the Inventory.
        /// </summary>
        /// <param name="pArtefact">The Artefact to remove.</param>
        public void Remove(Artefact pArtefact)
        {
            // REMOVE the Artefact from the storage:
            _storage.Remove(pArtefact);
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
