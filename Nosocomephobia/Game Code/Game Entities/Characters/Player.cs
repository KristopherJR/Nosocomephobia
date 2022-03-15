using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nosocomephobia.Engine_Code.Components;
using Nosocomephobia.Engine_Code.Entities;
using Nosocomephobia.Engine_Code.Interfaces;
using Nosocomephobia.Engine_Code.UserEventArgs;
using Nosocomephobia.Game_Code.GameLogic;
using System;
using System.Diagnostics;

/// <summary>
/// Author: Kristopher J Randle
/// Version: 1.8, 15-03-2022
/// </summary>
namespace Nosocomephobia.Game_Code.Game_Entities.Characters
{
    public class Player : AnimatedEntity, ICollidable, ICollisionResponder, IInputListener
    {
        #region FIELDS
        // DECLARE an EventHandler for UpdateEvents:
        private EventHandler<OnUpdateEventArgs> _updateBehaviourHandler;
        // DECLARE an EventHandler for CollisionEvents:
        private EventHandler<OnCollisionEventArgs> _collisionBehaviourHandler;
        // DECLARE a reference to IBehaviour, call it playerBehaviour:
        private IBehaviour playerBehaviour;
        // DECLARE a float, call it 'moveSpeed':
        private float moveSpeed;
        // DECLARE a float, call it '_sprintModifier':
        private float _sprintModifier;
        // DECLARE an array of Keys[] called keysOfInterest. This will contain only the keys that we need to know about being pressed:
        private Keys[] keysOfInterest = { Keys.W, Keys.A, Keys.S, Keys.D, Keys.F, Keys.LeftShift };
        // DECLARE a bool, call it 'isSprinting'. Used to flag when the user lets go off sprint:
        private bool isSprinting;
        // DECLARE a reference to a Flashlight, call it flashlight:
        private Flashlight flashlight;
        // DECLARE a reference to an IInventory, call it inventory:
        private IInventory inventory;
        // DECLARE a float, call it sprintTimer. Used to check how long the player has been sprinting:
        private float sprintTimer;
        // DECLARE a float, call it sprintDuration. Sets how long the player can sprint for before resting:
        private float sprintDuration;
        // DECLARE a Vector2, call it walkDirection:
        private Vector2 walkDirection;
        private bool isDestroyed;
        #endregion

        #region PROPERTIES
        // DECLARE a get-set property for the Players Flashlight:
        public Flashlight Flashlight
        {
            get { return flashlight; }
            set { flashlight = value; }
        }

        // DECLARE a get property for the Players Inventory:
        public IInventory Inventory
        {
            get { return inventory; }
        }
        // DECLARE a get property for isSprinting:
        public bool IsSprinting
        {
            get { return isSprinting; }
            set { isSprinting = value; }
        }
        // DECLARE a get-set property for sprintTimer:
        public float SprintTimer
        {
            get { return sprintTimer; }
            set { sprintTimer = value; }
        }
        // DECLARE a get-set property for sprintDuration:
        public float SprintDuration
        {
            get { return sprintDuration; }
            set { sprintDuration = value; }
        }
        // DECLARE a get property for walkDirection:
        public Vector2 WalkDirection
        {
            get { return walkDirection; }
        }
        // DECLARE a get property for isDestroyed:
        public bool IsDestroyed
        {
            get { return isDestroyed; }
        }
        #endregion PROPERTIES

        /// <summary>
        /// Constructor for objects of class Player.
        /// </summary>
        public Player() : base(GameContent.GetAnimation(AnimationGroup.PlayerIdleDown))
        {
            // INITALISE playerBehaviour:
            playerBehaviour = new PlayerBehaviour();
            // SET the Entity in PlayerBehaviour to point to this Player object:
            playerBehaviour.MyEntity = this;
            // SUBSCRIBE the PlayerBehaviour to listen for update events published by Player:
            _updateBehaviourHandler += (playerBehaviour as IUpdateEventListener).OnUpdate;
            // SUBSCRIBE the PlayerBehaviour to listen for collision events published by Player:
            _collisionBehaviourHandler += (playerBehaviour as ICollisionEventListener).OnCollision;

            // SET PLAYER location in the world:
            this.EntityLocn = new Vector2(3000, 5500);
            // INITIALIZE moveSpeed to '1.5f':
            this.moveSpeed = 1.5f;
            // SET _sprintModifier to 50% (1.5f):
            this._sprintModifier = 1.5f;
            // SET isSprinting to false as default:
            this.isSprinting = false;
            // SET isCharacter to true:
            this.isCharacter = true;
            // INITALISE _inventory:
            this.inventory = new Inventory();
            this.UName = "Player1";
            // INITIALISE timers:
            this.sprintTimer = 0.0f;
            // SET sprintDuration to 5 seconds:
            this.sprintDuration = 5.0f;
            // SET walkDirection to (0,1) by default (meaning the Player is walking down):
            this.walkDirection = new Vector2(0, 1);
            this.isDestroyed = false;

        }

        /// <summary>
        /// Update loop for Player, overrides the parent Update() method. Delegates Update functionality to PlayerBehaviour OnUpdate() Method.
        /// </summary>
        /// <param name="gameTime">A snapshot of the GameTime.</param>
        public override void Update(GameTime gameTime)
        {
            // UPDATE the parent class:
            base.Update(gameTime);
            // INVOKE the Update Behaviour Handler to enact player update behaviour, pass in GameTime to the EventArgs:
            _updateBehaviourHandler.Invoke(this, new OnUpdateEventArgs(gameTime));

            
        }

        /// <summary>
        /// Kills the Player.
        /// </summary>
        public void Kill()
        {
            // SCHEDULE the Terminate Command for the Player Flashlight:
            flashlight.ScheduleCommand(flashlight.TerminateMe);
            // REMOVE the _flashlight from the Penumbra Engine:
            Kernel.PENUMBRA.Lights.Remove(flashlight.Light);
            // FIRE the RemoveMe Command to remove the Entity from the SceneGraph:
            this.ScheduleCommand(RemoveMe);
            // FIRE the TerminateMe Command to remove the Entity from the EntityPool:
            this.ScheduleCommand(TerminateMe);

            this.isDestroyed = true;
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
                // INVOKE the PlayerBehaviour Collision Handler if the collision occured:
                _collisionBehaviourHandler.Invoke(this, new OnCollisionEventArgs(collidee));
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
                    if(isSprinting)
                    {
                        // MOVE player UP by movespeed:
                        this.EntityVelocity = new Vector2(0, -moveSpeed * _sprintModifier);
                        // SET player animation to sprint UP:
                        this.entityAnimation = GameContent.GetAnimation(AnimationGroup.PlayerSprintUp);
                        // SET the walkDirection to upwards:
                        this.walkDirection = Kernel.UP;

                    }
                    else
                    {
                        // MOVE player UP by movespeed:
                        this.EntityVelocity = new Vector2(0, -moveSpeed);
                        // SET player entityAnimation to walking UP:
                        this.entityAnimation = GameContent.GetAnimation(AnimationGroup.PlayerWalkUp);
                        // SET the walkDirection to upwards:
                        this.walkDirection = Kernel.UP;
                    }
                    break;
                case Keys.A:
                    if (isSprinting)
                    {
                        // MOVE player LEFT by movespeed:
                        this.EntityVelocity = new Vector2(-moveSpeed * _sprintModifier, 0);
                        // SET player animation to sprint LEFT:
                        this.entityAnimation = GameContent.GetAnimation(AnimationGroup.PlayerSprintLeft);
                        // SET the walkDirection to left:
                        this.walkDirection = Kernel.LEFT;

                    }
                    else
                    {
                        // MOVE player LEFT by movespeed:
                        this.EntityVelocity = new Vector2(-moveSpeed, 0);
                        // SET Sams entityAnimation to walking LEFT:
                        this.entityAnimation = GameContent.GetAnimation(AnimationGroup.PlayerWalkLeft);
                        // SET the walkDirection to left:
                        this.walkDirection = Kernel.LEFT;
                    }
                    break;
                case Keys.S:
                    if (isSprinting)
                    {
                        // MOVE player DOWN by movespeed:
                        this.EntityVelocity = new Vector2(0, moveSpeed * _sprintModifier);
                        // SET player animation to sprint DOWN:
                        this.entityAnimation = GameContent.GetAnimation(AnimationGroup.PlayerSprintDown);
                        // SET the walkDirection to downwards:
                        this.walkDirection = Kernel.DOWN;
                    }
                    else
                    {
                        // MOVE player DOWN by movespeed:
                        this.EntityVelocity = new Vector2(0, moveSpeed);
                        // SET player entityAnimation to walking DOWN:
                        this.entityAnimation = GameContent.GetAnimation(AnimationGroup.PlayerWalkDown);
                        // SET the walkDirection to downwards:
                        this.walkDirection = Kernel.DOWN;
                    }
                    break;
                case Keys.D:
                    if (isSprinting)
                    {
                        // MOVE player RIGHT by movespeed:
                        this.EntityVelocity = new Vector2(moveSpeed * _sprintModifier, 0);
                        // SET Sams animation to sprint RIGHT:
                        this.entityAnimation = GameContent.GetAnimation(AnimationGroup.PlayerSprintRight);
                        // SET the walkDirection to right:
                        this.walkDirection = Kernel.RIGHT;
                    }
                    else
                    {
                        // MOVE player RIGHT by movespeed:
                        this.EntityVelocity = new Vector2(moveSpeed, 0);
                        // SET player entityAnimation to walking RIGHT:
                        this.entityAnimation = GameContent.GetAnimation(AnimationGroup.PlayerWalkRight);
                        // SET the walkDirection to right:
                        this.walkDirection = Kernel.RIGHT;
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
                case Keys.F:
                    

                    // KILL the Player:
                    this.Kill();
                    
                    break;
                case Keys.LeftShift:
                    // FLAG the player has released sprint key:
                    this.isSprinting = !isSprinting;

                    break;

                case Keys.W:
                    // STOP the players movement:
                    this.EntityVelocity = new Vector2(0, 0);
                    this.entityAnimation = GameContent.GetAnimation(AnimationGroup.PlayerIdleUp);
                    break;
                case Keys.A:
                    // STOP the players movement:
                    this.EntityVelocity = new Vector2(0, 0);
                    this.entityAnimation = GameContent.GetAnimation(AnimationGroup.PlayerIdleLeft);
                    break;
                case Keys.S:
                    // STOP the players movement:
                    this.EntityVelocity = new Vector2(0, 0);
                    this.entityAnimation = GameContent.GetAnimation(AnimationGroup.PlayerIdleDown);
                    break;
                case Keys.D:
                    // STOP the players movement:
                    this.EntityVelocity = new Vector2(0, 0);
                    this.entityAnimation = GameContent.GetAnimation(AnimationGroup.PlayerIdleRight);
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
