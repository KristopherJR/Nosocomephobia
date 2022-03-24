using Nosocomephobia.Game_Code.Game_Entities;
using System.Collections.Generic;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.1, 20-03-22
/// </summary>
namespace Nosocomephobia.Engine_Code.Interfaces
{
    public interface IObjectPlacementManager
    {
        #region PROPERTIES
        // DECLARE a get-set property for the List<Artefact> Artefacts:
        List<Artefact> Artefacts { get; set; }
        #endregion

        #region METHODS
        void RandomiseArtefactPlacements();
        #endregion
    }
}
