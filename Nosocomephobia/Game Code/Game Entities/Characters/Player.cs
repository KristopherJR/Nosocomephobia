using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nosocomephobia.Engine_Code.Entities;
using Nosocomephobia.Engine_Code.Interfaces;
using Nosocomephobia.Engine_Code.UserEventArgs;
using System;
using System.Diagnostics;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 1.3, 14-02-2022
/// </summary>
namespace Nosocomephobia.Game_Code.Game_Entities.Characters
{
    public class Player : AnimatedEntity, ICollidable, ICollisionResponder, IInputListener
    {
        #region FIELDS
        // DECLARE a float, call it 'moveSpeed':
        private float moveSpeed;
        // DECLARE a float, call it '_sprintModifier':
        private float _sprintModifier;
        // DECLARE an array of Keys[] called keysOfInterest. This will contain only the keys that we need to know about being pressed:
        private Keys[] keysOfInterest = { Keys.W, Keys.A, Keys.S, Keys.D, Keys.LeftShift };
        // DECLARE a Vector2, call it 'lastPosition'. Used to keep track of Sams position and reset him if he collides with something:
        private Vector2 lastPosition;
        // DECLARE a bool, call it 'isSprintReleased'. Used to flag when the user lets go off sprint:
        private bool isSprintReleased;
        #endregion

        #region PROPERTIES
        #endregion

        /// <summary>
        /// Constructor for objects of class Player.
        /// </summary>
        public Player() : base(GameContent.GetAnimation(AnimationGroup.PlayerWalkDown))
        {
            // SET PLAYER location in the world:
            this.EntityLocn = new Vector2(1534, 2894);
            // INITIALIZE moveSpeed to '1.5f':
            this.moveSpeed = 1.5f;
            // SET _sprintModifier to 50% (1.5f):
            this._sprintModifier = 1.5f;
            // SET isSprintReleased to false as default:
            this.isSprintReleased = false;
            // SET isCharacter to true:
            this.isCharacter = true;

        }

        /// <summary>
        /// Update loop for Player, overrides the parent Update() method. Stores his lastPosition before moving him on each update loop.
        /// </summary>
        /// <param name="gameTime">A snapshot of the GameTime.</param>
        public override void Update(GameTime gameTime)
        {
            // UPDATE the parent class:
            base.Update(gameTime);
            // STORE Sams last position as his current one before he moves:
            lastPosition = EntityLocn;
            // MOVE Player by his velocity:
            this.EntityLocn += entityVelocity;
            //Debug.WriteLine(entityLocn);
        }

        #region IMPLEMENTATION OF ICollisionResponder
        /// <summary>
        /// Called when a collision happens, tells the object how to react.
        /// </summary>
        /// <param name="collidee">The object that this object collided into.</param>
        public void CheckAndRespond(ICollidable collidee)
        {
            if (GameEntity.hasCollided(this, collidee))
            {
                entityLocn = lastPosition;
            }
        }
        #endregion
        #region IMPLEMENTATION OF IInputListener

        /// <summary>
        /// Event Handler for the event OnNewInput, fired from the InputManager. This will be triggered when a new input occurs. Method from Marc Price, Week 18 Input slides on BlackBoard.
        /// </summary>
        /// <param name="sender">The object sending the event.</param>
        /// <param name="eventInformation">Information about the input event.</param>
        public virtual void OnNewInput(object sender, OnInputEventArgs eventInformation)
        {

            // RESPOND to new input, checking which key was pressed by the user:
            switch (eventInformation.KeyInput)
            {
                case Keys.W:
                    if(isSprintReleased)
                    {
                        // MOVE player UP by movespeed:
                        this.EntityVelocity = new Vector2(0, -moveSpeed * _sprintModifier);
                        // SET Sams animation to sprint UP:
                        this.entityAnimation = GameContent.GetAnimation(AnimationGroup.PlayerSprintUp);

                    }
                    else
                    {
                        // MOVE player UP by movespeed:
                        this.EntityVelocity = new Vector2(0, -moveSpeed);
                        // SET Sams entityAnimation to walking UP:
                        this.entityAnimation = GameContent.GetAnimation(AnimationGroup.PlayerWalkUp);
                        
                    }
                    break;
                case Keys.A:
                    if (isSprintReleased)
                    {
                        // MOVE player LEFT by movespeed:
                        this.EntityVelocity = new Vector2(-moveSpeed * _sprintModifier, 0);
                        // SET Sams animation to sprint LEFT:
                        this.entityAnimation = GameContent.GetAnimation(AnimationGroup.PlayerSprintLeft);

                    }
                    else
                    {
                        // MOVE player LEFT by movespeed:
                        this.EntityVelocity = new Vector2(-moveSpeed, 0);
                        // SET Sams entityAnimation to walking LEFT:
                        this.entityAnimation = GameContent.GetAnimation(AnimationGroup.PlayerWalkLeft);

                    }
                    break;
                case Keys.S:
                    if (isSprintReleased)
                    {
                        // MOVE player DOWN by movespeed:
                        this.EntityVelocity = new Vector2(0, moveSpeed * _sprintModifier);
                        // SET Sams animation to sprint LEFT:
                        this.entityAnimation = GameContent.GetAnimation(AnimationGroup.PlayerSprintDown);

                    }
                    else
                    {
                        // MOVE player DOWN by movespeed:
                        this.EntityVelocity = new Vector2(0, moveSpeed);
                        // SET Sams entityAnimation to walking LEFT:
                        this.entityAnimation = GameContent.GetAnimation(AnimationGroup.PlayerWalkDown);
                    }
                    break;
                case Keys.D:
                    if (isSprintReleased)
                    {
                        // MOVE player RIGHT by movespeed:
                        this.EntityVelocity = new Vector2(moveSpeed * _sprintModifier, 0);
                        // SET Sams animation to sprint RIGHT:
                        this.entityAnimation = GameContent.GetAnimation(AnimationGroup.PlayerSprintRight);
                    }
                    else
                    {
                        // MOVE player RIGHT by movespeed:
                        this.EntityVelocity = new Vector2(moveSpeed, 0);
                        // SET Sams entityAnimation to walking RIGHT:
                        this.entityAnimation = GameContent.GetAnimation(AnimationGroup.PlayerWalkRight);
                    }
                    break;
            }
        }

        /// <summary>
        /// Event Handler for the event OnKeyReleased, fired from the InputManager. This will be triggered when a key is released.
        /// </summary>
        /// <param name="sender">The object sending the event.</param>
        /// <param name="eventInformation">Information about the input event.</param>
        public virtual void OnKeyReleased(object sender, OnKeyReleasedEventArgs eventInformation)
        {
            // RESPOND to new input, checking which key was released by the user:
            switch (eventInformation.KeyReleased)
            {
                case Keys.LeftShift:
                    // FLAG the player has released sprint key:
                    this.isSprintReleased = !isSprintReleased;
                    // FIRE the RemoveMe Command to remove the Entity from the SceneGraph:
                    this.ScheduleCommand(RemoveMe);
                    // FIRE the TerminateMe Command to remove the Entity from the EntityPool:
                    this.ScheduleCommand(TerminateMe);
                    break;
                case Keys.W:
                    // STOP the players movement:
                    this.EntityVelocity = new Vector2(0, 0);
                    break;
                case Keys.A:
                    // STOP the players movement:
                    this.EntityVelocity = new Vector2(0, 0);
                    break;
                case Keys.S:
                    // STOP the players movement:
                    this.EntityVelocity = new Vector2(0, 0);
                    break;
                case Keys.D:
                    // STOP the players movement:
                    this.EntityVelocity = new Vector2(0, 0);
                    break; 
            }
        }

        /// <summary>
        /// Event Handler for the even OnNewMouseInput, fired from the InputManager. This will be triggered when a new mouse input occurs.
        /// </summary>
        /// <param name="sender">The object sending the event.</param>
        /// <param name="eventInformation">Information about the input event.</param>
        public virtual void OnNewMouseInput(object sender, OnMouseInputEventArgs eventInformation)
        {
            //Respond to the new mouse input:
        }

        /// <summary>
        /// Used to return the KeysOfInterst contained in the Listener.
        /// </summary>
        /// <returns>The array of KeysOfInterest.</returns>
        public Keys[] getKOI()
        {
            // return keysOfInterest:
            return keysOfInterest;
        }
        #endregion

    }
}
