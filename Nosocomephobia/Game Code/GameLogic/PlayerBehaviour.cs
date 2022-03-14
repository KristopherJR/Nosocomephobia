using Microsoft.Xna.Framework;
using Nosocomephobia.Engine_Code.Entities;
using Nosocomephobia.Engine_Code.Interfaces;
using Nosocomephobia.Engine_Code.UserEventArgs;
using Nosocomephobia.Game_Code.Game_Entities;
using Nosocomephobia.Game_Code.Game_Entities.Characters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.2, 15-02-2022
/// </summary>
namespace Nosocomephobia.Game_Code.GameLogic
{
    public class PlayerBehaviour : NosocomephobiaBehaviour, ICollisionEventListener
    {
        /// <summary>
        /// Called on each update loop from Player. Enacts Update behaviour.
        /// </summary>
        /// <param name="source">The player.</param>
        /// <param name="args">UpdateEvent information.</param>
        public override void OnUpdate(object source, OnUpdateEventArgs args)
        {
            // VERIFY type safety - check the Entity is a GameEntity:
            if (MyEntity is GameEntity)
            {
                // STORE the players last position as the current one before they move:
                (MyEntity as GameEntity).LastPosition = (MyEntity as GameEntity).EntityLocn;
                // MOVE Player by velocity:
                (MyEntity as GameEntity).EntityLocn += (MyEntity as GameEntity).EntityVelocity;
            }
            // UPDATE the Player's Flashlight:
            (MyEntity as Player).Flashlight.Update(args.GameTime);
        }
        /// <summary>
        /// Called whenever a Collision Event involving the Entity occurs.
        /// </summary>
        /// <param name="source">The object calling the collision event.</param>
        /// <param name="args">event information including the object that was collided with (colidee).</param>
        public void OnCollision(object source, OnCollisionEventArgs args)
        {
            // VERIFY type safety - check the Entity is a GameEntity:
            if(MyEntity is GameEntity)
            {
                // RESET MyEntities location to its last position:
                (MyEntity as GameEntity).EntityLocn = (MyEntity as GameEntity).LastPosition;
                                                                 
            }
            if(args.CollidedObject is Artefact)
            {
                // COLLIDED with an Artefact:

                // ADD the artefact to the player inventory

                (MyEntity as Player).Inventory.Add((args.CollidedObject as IEntity).UName, (args.CollidedObject as Artefact));

                // FLAG the artefact for removal from the scene by setting it as collected:
                (args.CollidedObject as GameEntity).ScheduleCommand((args.CollidedObject as GameEntity).RemoveMe);

            }
        }
    }
}
