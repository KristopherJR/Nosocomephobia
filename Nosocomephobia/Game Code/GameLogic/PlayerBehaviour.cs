using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nosocomephobia.Engine_Code.Entities;
using Nosocomephobia.Engine_Code.Interfaces;
using Nosocomephobia.Engine_Code.UserEventArgs;
using Nosocomephobia.Game_Code.Game_Entities;
using Nosocomephobia.Game_Code.Game_Entities.Characters;
using Nosocomephobia.Game_Code.World;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.5, 16-03-2022
/// </summary>
namespace Nosocomephobia.Game_Code.GameLogic
{
    public class PlayerBehaviour : NosocomephobiaBehaviour, ICollisionEventListener
    {
        #region FIELDS
        // DECLARE a bool to check if a footstep sound is playing:
        private bool isFootstepSFXPlaying;
        // DECLARE a float, call it waitTimer:
        private float waitTimer;

        #endregion
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
            // IF the Player is sprinting:
            if((MyEntity as Player).IsSprinting)
            {
                // INCREMENT the sprintTimer by GameTime seconds:
                (MyEntity as Player).SprintTimer += (float)args.GameTime.ElapsedGameTime.TotalSeconds;
                // IF sprintTimer >= sprintDuration, disable sprinting:
                if((MyEntity as Player).SprintTimer >= (MyEntity as Player).SprintDuration)
                {
                    (MyEntity as Player).IsSprinting = false;
                }
            }
            else
            {
                // IF the player is resting, decrement their sprint timer:
                if((MyEntity as Player).SprintTimer > 0.0f)
                {
                    // DECREMENT the sprintTimer by GameTime seconds:
                    (MyEntity as Player).SprintTimer -= (float)args.GameTime.ElapsedGameTime.TotalSeconds;
                }
                // IF the sprintTimer drops below 0 seconds:
                if((MyEntity as Player).SprintTimer < 0.0f)
                {
                    // RESET it to 0 seconds:
                    (MyEntity as Player).SprintTimer = 0.0f;
                }
            }
            // IF the player is moving:
            if((MyEntity as Player).IsMoving)
            {
                // IF a footstep sound is playing:
                if(isFootstepSFXPlaying)
                {
                    // INCREMENT waitTimer until it reaches footstepInterval:
                    waitTimer += (float)args.GameTime.ElapsedGameTime.TotalSeconds;
                    if(waitTimer >= (MyEntity as Player).FootstepInterval)
                    {
                        // RESET isFootstepSFXPlaying to false:
                        isFootstepSFXPlaying = false;
                        // RESET the waitTimer:
                        waitTimer = 0.0f;
                    }
                }
                else
                {
                    // PLAY the footstep SFX:
                    GameContent.Footstep.Play(0.5f,0.0f,0.0f);
                    // FLAG that it is playing:
                    isFootstepSFXPlaying = true;
                }
            }

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
                if(args.CollidedObject.IsCollidable)
                {
                    // RESET MyEntities location to its last position:
                    (MyEntity as GameEntity).EntityLocn = (MyEntity as GameEntity).LastPosition;
                }    
                                                                 
            }
            if(args.CollidedObject is Artefact)
            {
                // COLLIDED with an Artefact:

                // ADD the artefact to the player inventory

                (MyEntity as Player).Inventory.Add((args.CollidedObject as IEntity).UName, (args.CollidedObject as Artefact));

                // PLAY the Artefacts Pickup SFX:
                (args.CollidedObject as Artefact).PickupSFX.Play(0.15f,0.0f,0.0f);

                // FLAG the artefact for removal from the scene by setting it as collected:
                (args.CollidedObject as GameEntity).ScheduleCommand((args.CollidedObject as GameEntity).RemoveMe);

            }
            if (args.CollidedObject is Door)
            {
                // CHECK the player has collected all Artefacts:
                if ((MyEntity as Player).Inventory.GetCount() == 4)
                {
                    // CHECK the player has pressed Enter to unlock the door:
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        // IF the door is locked:
                        if ((args.CollidedObject as Door).IsLocked)
                        {
                            // UNLOCK the door:
                            (args.CollidedObject as Door).Unlock();
                        }
                    }
                }

            }
        }
    }
}
