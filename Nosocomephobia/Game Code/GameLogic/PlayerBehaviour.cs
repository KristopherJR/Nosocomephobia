using Microsoft.Xna.Framework.Input;
using Nosocomephobia.Engine_Code.Entities;
using Nosocomephobia.Engine_Code.Interfaces;
using Nosocomephobia.Engine_Code.UserEventArgs;
using Nosocomephobia.Game_Code.Game_Entities;
using Nosocomephobia.Game_Code.Game_Entities.Characters;
using Nosocomephobia.Game_Code.World;
using System;
using System.Diagnostics;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 0.6, 19-03-2022
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
        /// Kills the Player.
        /// </summary>
        /// <param name="source">The Player.</param>
        /// <param name="args">Death event information.</param>
        public void OnDeath(object source, EventArgs args)
        {
            // PLAY the death Sound Effects:
            GameContent.DeathBone.Play(0.3f, 0.0f, 0.0f);
            GameContent.DeathGore.Play(0.3f, 0.0f, 0.0f);
            GameContent.DeathScream.Play(0.3f, 0.0f, 0.0f);

            // SCHEDULE the Terminate Command for the Player Flashlight:
            (MyEntity as Player).Flashlight.ScheduleCommand((MyEntity as Player).Flashlight.TerminateMe);
            // REMOVE the _flashlight from the Penumbra Engine:
            Kernel.PENUMBRA.Lights.Remove((MyEntity as Player).Flashlight.Light);
            // FIRE the RemoveMe Command to remove the Entity from the SceneGraph:
            (MyEntity as Player).ScheduleCommand((MyEntity as Player).RemoveMe);
            // FIRE the TerminateMe Command to remove the Entity from the EntityPool:
            (MyEntity as Player).ScheduleCommand((MyEntity as Player).TerminateMe);
            // FLAG that the player has been destroyed:
            (MyEntity as Player).IsDestroyed = true;
            Kernel.STATE = State.GameOver;
        }


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

                Debug.WriteLine((MyEntity as GameEntity).EntityLocn);
            }
            // UPDATE the Player's Flashlight:
            (MyEntity as Player).Flashlight.Update(args.GameTime);
            // IF the Player is sprinting:
            if ((MyEntity as Player).IsSprinting)
            {
                // INCREMENT the sprintTimer by GameTime seconds:
                (MyEntity as Player).SprintTimer += (float)args.GameTime.ElapsedGameTime.TotalSeconds;
                // IF sprintTimer >= sprintDuration, disable sprinting:
                if ((MyEntity as Player).SprintTimer >= (MyEntity as Player).SprintDuration)
                {
                    (MyEntity as Player).IsSprinting = false;
                }
            }
            else
            {
                // IF the player is resting, decrement their sprint timer:
                if ((MyEntity as Player).SprintTimer > 0.0f)
                {
                    // DECREMENT the sprintTimer by GameTime seconds:
                    (MyEntity as Player).SprintTimer -= (float)args.GameTime.ElapsedGameTime.TotalSeconds;
                }
                // IF the sprintTimer drops below 0 seconds:
                if ((MyEntity as Player).SprintTimer < 0.0f)
                {
                    // RESET it to 0 seconds:
                    (MyEntity as Player).SprintTimer = 0.0f;
                }
            }
            // IF the player is moving:
            if ((MyEntity as Player).IsMoving)
            {
                // IF a footstep sound is playing:
                if (isFootstepSFXPlaying)
                {
                    // INCREMENT waitTimer until it reaches footstepInterval:
                    waitTimer += (float)args.GameTime.ElapsedGameTime.TotalSeconds;
                    if (waitTimer >= (MyEntity as Player).FootstepInterval)
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
                    GameContent.Footstep.Play(0.5f, 0.0f, 0.0f);
                    // FLAG that it is playing:
                    isFootstepSFXPlaying = true;
                }
            }
            if((MyEntity as Player).EntityLocn.Y < 630)
            {
                // SCHEDULE the Terminate Command for the Player Flashlight:
                (MyEntity as Player).Flashlight.ScheduleCommand((MyEntity as Player).Flashlight.TerminateMe);
                // REMOVE the _flashlight from the Penumbra Engine:
                Kernel.PENUMBRA.Lights.Remove((MyEntity as Player).Flashlight.Light);
                // FIRE the RemoveMe Command to remove the Entity from the SceneGraph:
                (MyEntity as Player).ScheduleCommand((MyEntity as Player).RemoveMe);
                // FIRE the TerminateMe Command to remove the Entity from the EntityPool:
                (MyEntity as Player).ScheduleCommand((MyEntity as Player).TerminateMe);
                // FLAG that the player has been destroyed:
                (MyEntity as Player).IsDestroyed = true;
                Kernel.STATE = State.Victory;
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
            if (MyEntity is GameEntity)
            {
                if (args.CollidedObject.IsCollidable)
                {
                    // RESET MyEntities location to its last position:
                    (MyEntity as GameEntity).EntityLocn = (MyEntity as GameEntity).LastPosition;
                }

            }
            if (args.CollidedObject is Artefact)
            {
                // COLLIDED with an Artefact:

                // ADD the artefact to the player inventory

                (MyEntity as Player).Inventory.Add((args.CollidedObject as IEntity).UName, (args.CollidedObject as Artefact));

                // PLAY the Artefacts Pickup SFX:
                (args.CollidedObject as Artefact).PickupSFX.Play(0.15f, 0.0f, 0.0f);

                // FLAG the artefact for removal from the scene by setting it as collected:
                (args.CollidedObject as GameEntity).ScheduleCommand((args.CollidedObject as GameEntity).RemoveMe);

            }
            if (args.CollidedObject is Door)
            {
                // CHECK the player has collected all Artefacts:
                if ((MyEntity as Player).Inventory.GetCount() == 0)
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
